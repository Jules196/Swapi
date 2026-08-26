using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using SwapiBackend;
using SwapiBackend.DTOs;
using SwapiFrontEndWPF.Utilities;

namespace SwapiFrontEndWPF.ViewModels;

public partial class MainViewModel : ObservableObject, IDisposable
{
    [ObservableProperty]
    private ISwapiPersons? swapiService;

    public MainViewModel()
    {
        SwapiService = LoadSwapiPersons(SourcePath);
    }

    public async Task<PersonDetailDTO?> GetSelectedDetailedPerson()
    {
        if (SelectedPerson is null) return null;
        else if (SwapiService is null) return null;
        return await SwapiService.GetPersonDetailAsync(SelectedPerson.Name);
    }

    public async Task<Result> UpdatePerson(PersonUpdateDTO person)
    {
        if (SwapiService is null) return new Result(false, "No Swapi Service");
        Result res = await SwapiService.UpdatePersonAsync(person);

        if (res.Success)
        {
            LoadData(SwapiService);
        }

        return res;
    }

    public void ReloadData()
    {
        SwapiService?.CancelPendingRequests();

        if (SwapiService is null)
        {
            SwapiService = LoadSwapiPersons(SourcePath);
        }
        else
        {
            SwapiService.InvalidData = true;
            LoadData(SwapiService);
        }
    }

    [ObservableProperty]
    private string? sourcePath = "";

    [ObservableProperty]
    private string? currentYear = "24ABY";

    [ObservableProperty]
    private string averageHeight = "---";

    [ObservableProperty]
    private string averageAge = "---";

    [ObservableProperty]
    private double maleRelation = 0;

    [ObservableProperty]
    private double femaleRelation = 0;

    [ObservableProperty]
    private ObservableCollection<PersonDTO> personDTOs = [];

    [ObservableProperty]
    private PersonDTO? selectedPerson = null;

    [ObservableProperty]
    private string? errorMessage = null;

    partial void OnSourcePathChanged(string? value)
    {
        SwapiService = LoadSwapiPersons(value);
    }

    partial void OnCurrentYearChanged(string? value)
    {
        if (SwapiService is null) return;

        _ = UpdateAge(SwapiService);
    }

    partial void OnSwapiServiceChanged(ISwapiPersons? value)
    {
        LoadData(value);
    }

    private async void LoadData(ISwapiPersons? swapi)
    {
        if (swapi is null)
        {
            FillEmptyAsEmptyDatabase();
            ErrorMessage = Resources.Resources.ErrorLoadingData;
            return;
        }
        ErrorMessage = null;

        FillLoadingData();
        Task<Result[]> fillingDataResults = Task.WhenAll(FillPersons(swapi), FillData(swapi));
        
        string? errorMessageMerge = null;
        foreach (Result result in await fillingDataResults)
        {
            if (!result.Success)
            {
                if (string.IsNullOrWhiteSpace(errorMessageMerge))
                {
                    errorMessageMerge = result.Message;
                }
                else
                {
                    errorMessageMerge += $"\n{result.Message}";
                }
            }
        }

        ErrorMessage = errorMessageMerge;
        //_ = FillPersons(swapi);
        //_ = FillData(swapi);
    }

    private void FillLoadingData()
    {
        PersonDTOs.Clear();
        PersonDTOs.Add(new PersonDTO(Resources.Resources.Loading));
        AverageAge = Resources.Resources.Calculating;
        AverageHeight = Resources.Resources.Calculating;
        MaleRelation = 0;
        FemaleRelation = 0;
    }

    private async Task<Result> FillPersons(ISwapiPersons value)
    {
        List<PersonDTO>? allPersons = await value.GetAllPersonNamesAsync();
        if (allPersons is null)
        {
            PersonDTOs.Clear();
            return new Result(false, Resources.Resources.ErrorLoadingData);
        }
        PersonDTOs = new ObservableCollection<PersonDTO>(allPersons);
        return new Result(true, "");
    }

    private async Task<Result> FillData(ISwapiPersons value)
    {
        List<PersonDetailDTO>? allPersonsDetailed = await value.GetAllPersonDetailsAsync();
        if (allPersonsDetailed is null)
        {
            FillEmptyAsEmptyDatabase();
            return new Result(false, Resources.Resources.ErrorCalculatingData);
        }
        AverageAge = allPersonsDetailed.CalculateAverageAge(CurrentYear).ToString("N2");
        AverageHeight = allPersonsDetailed.CalculateAverageHeight().ToString("N2");
        (MaleRelation, FemaleRelation) = allPersonsDetailed.CountMalesAndFemales();
        return new Result(true, "");
    }

    private void FillEmptyAsEmptyDatabase()
    {
        AverageAge = "---";
        AverageHeight = "---";
        MaleRelation = 0;
        FemaleRelation = 0;
        PersonDTOs.Clear();
    }

    private async Task UpdateAge(ISwapiPersons value)
    {
        AverageAge = Resources.Resources.Calculating;
        List<PersonDetailDTO>? allPersonsDetailed = await value.GetAllPersonDetailsAsync();
        if (allPersonsDetailed is null)
        {
            AverageAge = "---";
            return;
        }
        AverageAge = allPersonsDetailed.CalculateAverageAge(CurrentYear).ToString();
    }

    private static ISwapiPersons? LoadSwapiPersons(string? sourcePath)
    {
        try
        {
            return new StarWarsData(sourcePath);
        }
        catch // ToDo: Error view
        {
            return null;
        }
    }

    public void Dispose()
    {
        SwapiService?.CancelPendingRequests();
        SwapiService?.Dispose();
    }
}
