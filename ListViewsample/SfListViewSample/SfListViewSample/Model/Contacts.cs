using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SfListViewSample
{
    public class Contacts : INotifyPropertyChanged
    {
        private string contactName;
        private string contactNumber;
        private ImageSource image;
        private ImageSource indicator;
        private DateTime dateTime;
        private string months;
        private string detail;

        public Contacts(string name, string number)
        {
            contactName = name;
            contactNumber = number;
        }

        public Contacts()
        {

        }
        public string Date { get; set; }
        public DateTime DateTime
        {
            get { return dateTime; }
            set
            {
                dateTime = value;
                RaisedOnPropertyChanged("DateTime");
            }
        }
        public string DoctorName
        {
            get { return contactName; }
            set
            {
                if (contactName != value)
                {
                    contactName = value;
                    this.RaisedOnPropertyChanged("DoctorName");
                }
            }
        }
        public string Months
        {
            get { return months; }
            set
            {
                if (months != value)
                {
                    months = value;
                    this.RaisedOnPropertyChanged("Months");
                }
            }
        }
        public string PatientDetail
        {
            get { return detail; }
            set
            {
                if (detail != value)
                {
                    detail = value;
                    this.RaisedOnPropertyChanged("PatientDetail");
                }
            }
        }

        public ImageSource Indicator
        {
            get { return indicator; }
            set
            {
                if (indicator != value)
                {
                    indicator = value;
                    this.RaisedOnPropertyChanged("Indicator");
                }
            }
        }
        public string ContactNumber
        {
            get { return contactNumber; }
            set
            {
                if (contactNumber != value)
                {
                    contactNumber = value;
                    this.RaisedOnPropertyChanged("ContactNumber");
                }
            }
        }

        public ImageSource ContactImage
        {
            get { return this.image; }
            set
            {
                this.image = value;
                this.RaisedOnPropertyChanged("ContactImage");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void RaisedOnPropertyChanged(string _PropertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(_PropertyName));
            }
        }
    }
}
