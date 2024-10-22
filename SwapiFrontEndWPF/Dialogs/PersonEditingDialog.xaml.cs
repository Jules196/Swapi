using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using SwapiBackend.DTOs;

namespace SwapiFrontEndWPF.Dialogs;

/// <summary>
/// Interaction logic for PersonEditingDialog.xaml
/// </summary>
public partial class PersonEditingDialog : Window
{
    public PersonUpdateDTO? ResultPersonUpdate { get; private set; } = null;

    public PersonEditingDialog(PersonDetailDTO personDetails)
    {
        InitializeComponent();
        DataContext = personDetails; // Datatemplate
    }

    private void OkButton_Click(object sender, RoutedEventArgs e)
    {
        if (DataContext is PersonDetailDTO personDetail)
        {
            ResultPersonUpdate = new PersonUpdateDTO
            (
                Name: personDetail.Name,
                Height: personDetail.Height,
                BirthYear: personDetail.BirthYear,
                Gender: personDetail.Gender
            );

            DialogResult = true;
            Close();
        }
    }
}
