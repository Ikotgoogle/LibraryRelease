using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace LibraryHome.Model
{
    public class Books : INotifyPropertyChanged
    {
        private int article;
        private string bookName;
        private int num; // количество
        private int year;

        public int Article { 
            get { return article; } 
            set { article = value; OnPropertyChanged("Article"); } 
        }

        public string BookName
        {
            get { return bookName; }
            set { bookName = value; OnPropertyChanged("BookName"); }
        }

        public int Num
        {
            get { return num; }
            set { num = value; OnPropertyChanged("Num"); }
        }

        public int Year
        {
            get { return year; }
            set { year = value; OnPropertyChanged("Year"); }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
