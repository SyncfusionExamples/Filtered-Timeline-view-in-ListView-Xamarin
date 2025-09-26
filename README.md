# Filtered-Timeline-view-in-ListView-Xamarin

## Sample

```C#
Button filterButton = new Button { Text = "Filter", HeightRequest = 40 };
filterButton.Clicked += FilterButton_Clicked;
pickerStack.Children.Add(filterButton, 0, 3);
Grid.SetColumnSpan(filterButton, 2);

listview = new SfListView
{
    SelectionMode = SelectionMode.None,
    AutoFitMode = AutoFitMode.Height,
    BindingContext = listViewModel,
    ItemsSource = listViewModel.ContactsInfo,
    ItemTemplate = new DataTemplate(() =>
    {
        Label doctorName = new Label { FontSize = 14 };
        doctorName.SetBinding(Label.TextProperty, "DoctorName");

        Label dateLabel = new Label { FontSize = 12 };
        dateLabel.SetBinding(Label.TextProperty, "Date");

        Label patientDetail = new Label { FontSize = 12 };
        patientDetail.SetBinding(Label.TextProperty, "PatientDetail");

        return new StackLayout
        {
            Padding = new Thickness(10),
            Children = { doctorName, dateLabel, patientDetail }
        };
    })
};

listview.BindingContext = listViewModel;
listview.ItemsSource = listViewModel.ContactsInfo;

private void FilterButton_Clicked(object sender, EventArgs e)
{
    DateTime fromDate = fromDatePicker.Date;
    DateTime toDate = toDatePicker.Date;
    string selectedDoctor = comboBox.SelectedItem?.ToString();

    var filteredList = listViewModel.ContactsInfo.Where(contact =>
        contact.Date >= fromDate &&
        contact.Date <= toDate &&
        (string.IsNullOrEmpty(selectedDoctor) || contact.DoctorName == selectedDoctor)
    ).ToList();

    listview.ItemsSource = filteredList;
}
```