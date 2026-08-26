using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using SwapiBackend.DTOs;
using SwapiFrontEndWPF.Dialogs;

namespace SwapiFrontEndWPF.Views;

/// <summary>
/// Interaction logic for MainView.xaml
/// </summary>
public partial class MainView : UserControl
{
    public MainView()
    {
        InitializeComponent();
        // Autoload of the ViewModel
        this.DataContext = new ViewModels.MainViewModel();
    }

    /// <summary>
    /// Registrate a selection changed event to open a dialog to edit the selected person.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private async void ListBox_SelectionChanged(object sender, EventArgs e)
    {
        if (PersonListBox.SelectedItem is PersonDTO selectedPerson
            && this.DataContext is ViewModels.MainViewModel mainVM)
        {
            PersonDetailDTO? personDetail = await mainVM.GetSelectedDetailedPerson();
            if (personDetail is null) return;

            var dialog = new PersonEditingDialog(personDetail);
            if (dialog.ShowDialog() == true && dialog.ResultPersonUpdate is not null)
            {
                _ = await mainVM.UpdatePerson(dialog.ResultPersonUpdate);
            }
        }
    }

    /// <summary>
    /// Catch the preview event to set the selected item again.
    /// This is a small trick to fires the <see cref="ListBox_SelectionChanged"/> event again."/>
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void ListBox_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        // Small trick to open again the dialog
        var item = ItemsControl.ContainerFromElement((ItemsControl)sender, (DependencyObject)e.OriginalSource) as ListBoxItem;
        if (item != null && item.IsSelected && item.DataContext == PersonListBox.SelectedItem)
        {
            PersonListBox.SelectedItem = null;
            PersonListBox.SelectedItem = item.DataContext;
            e.Handled = true;
        }
    }

    /// <summary>
    /// Reload the data from the API.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void LoadDataButton_Click(object sender, RoutedEventArgs e)
    {
        if (this.DataContext is ViewModels.MainViewModel mainVM)
        {
            mainVM.ReloadData();
        }
    }
}
