using Syncfusion.DataSource;
using Syncfusion.ListView.XForms;
using Syncfusion.ListView.XForms.Control.Helpers;
using Syncfusion.XForms.ComboBox;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SfListViewSample
{
   
    public partial class MainPage : ContentPage
    {
        ContactsViewModel listViewModel;
        public bool flag = false, isPresent ;
        public List<string> NameCollection;
        public List<string> dateCollection;
        DateTime ToDate=new DateTime();
        DateTime FromDate = new DateTime();
        SfListView listview;
        DatePicker fromDatePicker;
        DatePicker toDatePicker;
        int i = 0;

        #region Constructor
        public MainPage()
        {
            InitializeComponent();
            listViewModel = new ContactsViewModel();
            Grid mainGrid = new Grid() { };
            mainGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            mainGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });

            Grid pickerStack = new Grid() { };
            pickerStack.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            pickerStack.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            pickerStack.ColumnDefinitions.Add(new ColumnDefinition{ Width = new GridLength(1, GridUnitType.Star) });
            pickerStack.RowDefinitions.Add(new RowDefinition { Height = new GridLength(30) });
            pickerStack.RowDefinitions.Add(new RowDefinition { Height = new GridLength(40) });

            Label fromDateLabel = new Label() { Text = "From Date ", FontSize = 18 };
            fromDatePicker = new DatePicker() { };
            fromDatePicker.MinimumDate = new DateTime(2018, 1, 1);
            fromDatePicker.MaximumDate = new DateTime(2022, 12, 31);
            fromDatePicker.Date = new DateTime(2018, 1, 1);
            pickerStack.Children.Add(fromDateLabel, 0, 0);
            pickerStack.Children.Add(fromDatePicker, 0, 1);

            Label toDateLabel = new Label() { Text = "To Date ", FontSize = 18 };
            toDatePicker = new DatePicker() {};
            toDatePicker.MinimumDate = new DateTime(2018, 1, 1);
            toDatePicker.MaximumDate = new DateTime(2022, 12, 31);
            toDatePicker.Date = new DateTime(2018, 1,1);
            pickerStack.Children.Add(toDateLabel, 1, 0);
            pickerStack.Children.Add(toDatePicker, 1, 1);

            Label doctorNameLabel = new Label() { Text = "Select Name" ,FontSize=18 };
            SfComboBox comboBox = new SfComboBox() {BorderColor=Color.Transparent, HeightRequest = 50, DisplayMemberPath = "DoctorName" };
            comboBox.DataSource = listViewModel.NameCollection;
            
            pickerStack.Children.Add(doctorNameLabel, 2,0 );
            pickerStack.Children.Add(comboBox, 2, 1);

            listview = new SfListView() { SelectionMode = SelectionMode.None, AutoFitMode = AutoFitMode.Height };
            listview.BindingContext = listViewModel;
            listview.ItemsSource = listViewModel.ContactsInfo;
            listview.ItemTemplate = new DataTemplate(() =>
            {
                Grid mainGridList = new Grid() { };
                mainGridList.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength (80)});
                mainGridList.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(30) });
                mainGridList.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star)});

                Grid gridOne = new Grid();
                gridOne.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
                gridOne.Padding = new Thickness(0, 25, 0, 0);
                Label dateLabel = new Label()
                {
                    TextColor = Color.Teal,
                    FontSize = 12,
                };
                Binding binding = new Binding("Date");
                dateLabel.SetBinding(Label.TextProperty, binding);
                gridOne.Children.Add(dateLabel);

                Grid gridTwo = new Grid();
                gridTwo.VerticalOptions = LayoutOptions.StartAndExpand;
                BoxView box = new BoxView()
                {
                    VerticalOptions = LayoutOptions.CenterAndExpand,
                    HorizontalOptions = LayoutOptions.CenterAndExpand,
                    WidthRequest = 2,
                    BackgroundColor = Color.LightGray
                };
                if (Device.RuntimePlatform == Device.UWP)
                    box.HeightRequest = 150;
                else if (Device.RuntimePlatform == Device.Android || Device.RuntimePlatform == Device.iOS)
                    box.HeightRequest = 160;
                Grid childGrid = new Grid() { VerticalOptions = LayoutOptions.Start };
                Image image = new Image() { VerticalOptions = LayoutOptions.Start, HorizontalOptions = LayoutOptions.Center, WidthRequest = 95 };
                if (Device.RuntimePlatform == Device.UWP)
                    image.HeightRequest = 30;
                else if (Device.RuntimePlatform == Device.Android || Device.RuntimePlatform == Device.iOS)
                    image.HeightRequest = 35;
                Binding bind = new Binding("ContactImage");
                image.SetBinding(Image.SourceProperty, bind);
                childGrid.Children.Add(image);
                gridTwo.Children.Add(box);
                gridTwo.Children.Add(childGrid);

                Grid gridThree = new Grid() { Padding = new Thickness(5),ColumnSpacing=-20};
                gridOne.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                gridTwo.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });

                Grid contentGrid = new Grid() {Padding=5, RowSpacing = 0, VerticalOptions = LayoutOptions.FillAndExpand, HorizontalOptions = LayoutOptions.FillAndExpand };
                contentGrid.BackgroundColor = Color.FromRgb(192, 238, 252);
                contentGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto});
                StackLayout stackContent = new StackLayout() { Spacing = 5 };
                Label detailsLabel = new Label() { Text = "Details", FontSize = 15 };
                stackContent.Children.Add(detailsLabel);
                StackLayout stacktwo = new StackLayout() { Spacing = -2 };
                Label contentLabel = new Label() { Text = "Attended patient details of the ", FontSize = 12 };
                Label monthLabel = new Label() { FontSize = 12 };
                monthLabel.SetBinding(Label.TextProperty, new Binding("Months"));
                stacktwo.Children.Add(contentLabel);
                stacktwo.Children.Add(monthLabel);
                stackContent.Children.Add(stacktwo);
                Grid DoctorPatientGrid = new Grid() { HorizontalOptions = LayoutOptions.Start, VerticalOptions = LayoutOptions.Center };
                DoctorPatientGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
                DoctorPatientGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                DoctorPatientGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(20) });
                DoctorPatientGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
                Label doctor = new Label() { Text = "Doctor Name", FontSize = 12 };
                Label Patient = new Label() { Text = "Patient Details", FontSize = 12 };
                Label doctorName = new Label() { FontSize = 12 };
                doctorName.SetBinding(Label.TextProperty, new Binding("DoctorName"));
                Label PatienteName = new Label() { FontSize = 12 };
                PatienteName.SetBinding(Label.TextProperty, new Binding("PatientDetail"));
                DoctorPatientGrid.Children.Add(doctor, 0, 0);
                DoctorPatientGrid.Children.Add(Patient, 0, 1);
                DoctorPatientGrid.Children.Add(doctorName, 1, 0);
                DoctorPatientGrid.Children.Add(PatienteName, 1, 1);
                stackContent.Children.Add(DoctorPatientGrid);
                contentGrid.Children.Add(stackContent);
                gridThree.Children.Add(contentGrid, 0, 0);

                mainGridList.Children.Add(gridOne,0,0);
                mainGridList.Children.Add(gridTwo,1,0);
                mainGridList.Children.Add(gridThree,2,0);
                return mainGridList;

            });

            mainGrid.Children.Add(pickerStack, 0, 0);
            mainGrid.Children.Add(listview, 0, 1);
            this.Content = mainGrid;

            listview.Loaded += ListView_Loaded;
          
            fromDatePicker.DateSelected += FromPicker_DateSelected;
            toDatePicker.DateSelected += ToPicker_DateSelected;
            comboBox.FilterCollectionChanged += NameComboBox_FilterCollectionChanged;

            }

        private void NameComboBox_FilterCollectionChanged(object sender, Syncfusion.XForms.ComboBox.FilterCollectionChangedEventArgs e)
        {
            var send = sender as SfComboBox;
            isPresent = false;
            var name = send.Text.ToString();
            var itemsDate = new ObservableCollection<object>();

            if (name != null)
            {
                for (int i = 0; i <= listViewModel.ContactsInfo.Count(); i++)
                {
                    if (name == listViewModel.ContactsInfo[i].DoctorName && DateTime.Compare(FromDate, listViewModel.ContactsInfo[i].DateTime) < 0 && DateTime.Compare(ToDate, listViewModel.ContactsInfo[i].DateTime) > 0)
                    {
                        //Displays items only when item picked with selected date and name as in collection
                        itemsDate.Add(listViewModel.ContactsInfo[i]);
                        listview.ItemsSource = itemsDate;
                    }
                    else
                    {
                        if(name == listViewModel.ContactsInfo[i].DoctorName && !isPresent)
                        {
                            if (DateTime.Compare(FromDate, listViewModel.ContactsInfo[i].DateTime) < 0)
                            {
                                DisplayAlert("Messgae", "Doctor's name with selected date is not present in the collection", "ok");
                                isPresent = true;
                            }
                        }
                    }
                    if (name == "None")
                        listview.ItemsSource = listViewModel.ContactsInfo;
                }
            }
        }
        private void ToPicker_DateSelected(object sender, DateChangedEventArgs e)
        {
            if (flag == true)
            {
                ToDate = e.NewDate;
            }
        }

        private void FromPicker_DateSelected(object sender, DateChangedEventArgs e)
        {
            if (flag == true)
            {
                 FromDate = e.NewDate;
            }
        }

        private void ListView_Loaded(object sender, ListViewLoadedEventArgs e)
        {
            flag = true;
        }

        #endregion

        string[] Names = new string[] {
            "Kyle",
            "Gina",
            "Irene",
            "Katie",
            "Michael",
            "Oscar",
            "Ralph",
            "Torrey",
            "William",
            "None",
        };
    }
}
