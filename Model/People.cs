using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace LibraryHome.Model
{
    public class People
    {
        private int id;
        private string name;
        private string email;
        ObservableCollection<Books> userBooks;

        public int Id
        {
            get { return id; }
            set { id = value; OnPropertyChanged("Id"); }
        }

        public string Name
        {
            get { return name; }
            set { name = value; OnPropertyChanged("Name"); }
        }

        public string Email
        {
            get { return email; }
            set { email = value; OnPropertyChanged("Email"); }
        }

        public ObservableCollection<Books> UserBooks
        { 
            get { if (userBooks == null)
                {
                    userBooks = new ObservableCollection<Books>();
                }
                return userBooks;
            }
            set
            {
                userBooks = value;
                OnPropertyChanged("UserBooks");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
