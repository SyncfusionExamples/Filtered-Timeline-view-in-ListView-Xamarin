
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SfListViewSample
{
    public class ContactsViewModel : INotifyPropertyChanged
    {
        #region Properties

        public ObservableCollection<Contacts> contactsinfo { get; set; }
        public ObservableCollection<Contacts> nameCollection { get; set; }
        public ObservableCollection<Contacts> collection { get; set; }

        #endregion

        #region Constructor

        public ContactsViewModel()
        {
            contactsinfo = new ObservableCollection<Contacts>();
            nameCollection = new ObservableCollection<Contacts>();
            Random r = new Random();
            for (int i=0;i< DoctorNames.Count(); i++)
            {
                var contact = new Contacts(DoctorNames[i], r.Next(720, 799).ToString() + " - " + r.Next(3010, 3999).ToString());
                contact.ContactColor = Color.FromRgb(r.Next(40, 255), r.Next(40, 255), r.Next(40, 255));
                contact.Date = string.Format("{0:d MMM yyyy}", System.DateTime.Now.AddMonths(-i));
                contact.DateTime = System.DateTime.Now.AddMonths(-i);
                contact.Indicator = ImageSource.FromResource("SfListViewSample.Images.ImageIndicator.png");
                contact.PatientDetail = PatientDetails[r.Next(1,12)]; 
                contact.Months= string.Format("{0: MMMM yyyy}", System.DateTime.Now.AddMonths(-i-1));
                contact.ContactImage = ImageSource.FromResource("SfListViewSample.Images.CalenderIcon.png");
                contactsinfo.Add(contact);
            }

            //To remove duplicate entries
            for (int i=0;i< DoctorNames.Count();i++)
            {
                for(int j = i + 1; j < DoctorNames.Count();j++)
                {
                    if (DoctorNames[i] == DoctorNames[j])
                    {
                        DoctorNames[j] =null;
                    }
                }
            }
            // Avoids repeated names and added in to combobox collection
            for(int k=0;k< DoctorNames.Count();k++)
            {
                if (DoctorNames[k] != null)
                {
                    var contact = new Contacts();
                    contact.DoctorName = DoctorNames[k];
                    nameCollection.Add(contact);
                }
            }
        }

        #endregion

        #region Fields
        string[] PatientDetails = new string[]
        {
            "R. Radha \n Assosiate Technical Engineer \n India",
            "A. James \n Assisant Professsor \n Australia",
            "W. Williams \n Network Engineer \n Turkey",
            "L. John \n Techincal Member Staff \n Italy",
            "K. Jabes \n Junior Assistant \n France",
            "J. Krithik \n Lab Technician \n Germany",
            "B. Larry \n Junior Assistant \n London",
            "H. Reddy \n Lab Technician \n Ukraine",
            "G. Pinar \n Techincal Member Staff\n UAE",
            "T. Barry \n Junior Assistant \n Japan",
            "W. Mohan \n Lab Technician \n Chinna",
            "S. kareena \n Techincal Member Staff\n Africa",
        };

       public string[] DoctorNames = new string[] {
           "None",
            "Kyle",
            "Gina",
            "Gina",
            "Gina",
            "Irene",
            "Katie",
            "Michael",
            "Michael",
            "Michael",
            "Michael",
            "Oscar",
            "Ralph",
            "Torrey",
            "William",
            "William",
        };

        #endregion

        #region Interface Member

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string name)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(name));
        }

        #endregion
    }
}
