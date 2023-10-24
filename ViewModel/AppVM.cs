using LibraryHome.Model;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Policy;
using System.Windows;

namespace LibraryHome.ViewModel
{
    public class AppVM : INotifyPropertyChanged
    {
        private People selectedPeople;
        private People selectedReader;
        private Books selectedReadersBook;
        private Books selectedBook;

        private string findingBookText;
        private string findingPeopleText;
        private string findingReaderText;

        public ObservableCollection<People> Peoples { get; set; }
        public ObservableCollection<Books> Books { get; set; }

        private RelayCommand passCommand;
        private RelayCommand getBackCommand;

        public People SelectedPeople
        {
            get { return selectedPeople; }
            set { selectedPeople = value; OnPropertyChanged("SelectedUser"); OnPropertyChanged(nameof(PeoplesBooks)); }
        }
        public People SelectedReader
        {
            get { return selectedReader; }
            set { selectedReader = value; OnPropertyChanged(nameof(SelectedReader)); OnPropertyChanged(nameof(ReadersBooks)); }
        } // из списка всех читателей при выдаче
        public Books SelectedReadersBook
        {
            get { return selectedReadersBook; }
            set { selectedReadersBook = value; OnPropertyChanged(nameof(SelectedReadersBook)); }
        } // из списка владельцев выбранной книги
        public Books SelectedBook
        {
            get { return selectedBook; }
            set { selectedBook = value; OnPropertyChanged("SelectedBook"); }
        }

        
        public string FindingPeopleText
        {
            get { return findingPeopleText; }
            set
            {
                findingPeopleText = value;
                OnPropertyChanged("FindingPeopleText");
                OnPropertyChanged("FindingPeople");
            }
        }        
        public string FindingBookText
        {
            get { return findingBookText; }
            set
            {
                findingBookText = value;
                OnPropertyChanged(nameof(FindingBookText));
                OnPropertyChanged(nameof(FindingBooks));
            }
        }
        public string FindingReaderText
        {
            get { return findingReaderText; }
            set
            {
                findingReaderText = value;
                OnPropertyChanged(nameof(FindingReaderText));
                OnPropertyChanged(nameof(FindingReaders));
            }
        }
        
        
        public ObservableCollection<People> FindingPeople
        {
            get
            {
                if (findingPeopleText != null)
                {
                    return new ObservableCollection<People>(Peoples.Where(x => Convert.ToString(x.Id).Contains(findingPeopleText, StringComparison.OrdinalIgnoreCase) ||
                    x.Name.Contains(findingPeopleText, StringComparison.OrdinalIgnoreCase) || x.Email.Contains(findingPeopleText)));
                }
                else
                {
                    return Peoples;
                }
            }
        } // поиск людей на 1 табе
        public ObservableCollection<People> FindingReaders
        {
            get
            {
                if (findingReaderText != null)
                {
                    return new ObservableCollection<People>(Peoples.Where(x => Convert.ToString(x.Id).Contains(findingReaderText, StringComparison.OrdinalIgnoreCase) ||
                    x.Name.Contains(findingReaderText, StringComparison.OrdinalIgnoreCase) || x.Email.Contains(findingReaderText)));
                }
                else
                {
                    return Peoples;
                }
            }
        } // поиск людей на 2 табе 
        public ObservableCollection<Books> FindingBooks
        {
            get
            {
                if (findingBookText != null)
                {
                    return new ObservableCollection<Books>(Books.Where(x => Convert.ToString(x.Article).Contains(findingBookText, StringComparison.OrdinalIgnoreCase) ||
                    x.BookName.Contains(findingBookText, StringComparison.OrdinalIgnoreCase) || Convert.ToString(x.Num).Contains(findingBookText, StringComparison.OrdinalIgnoreCase) ||
                    Convert.ToString(x.Year).Contains(findingBookText, StringComparison.OrdinalIgnoreCase)));
                }
                else
                {
                    return Books;
                }
            }
        } // поиск книги
        public ObservableCollection<Books> PeoplesBooks
        {
            get
            {
                if (selectedPeople != null)
                    return SelectedPeople.UserBooks;
                else return new ObservableCollection<Books>();
            }
        } // книги пользователя в Обзоре
        public ObservableCollection<Books> ReadersBooks 
        {
            get
            {
                if (selectedReader != null)
                    return SelectedReader.UserBooks;
                else return new ObservableCollection<Books>();
            }
        } // книги пользователя в Книгах

        public RelayCommand PassCommand
        {
            get
            {
                return passCommand ??
                    (passCommand = new RelayCommand(obj =>
                    {
                        if (selectedReader == null)
                            MessageBox.Show("Не выбран читатель для выдачи книги!", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Warning);
                        else if (selectedBook == null)
                            MessageBox.Show("Не выбрана книга для выдачи!", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Warning);
                        else
                        {
                            if (selectedBook.Num == 0)
                                MessageBox.Show("Этой книги больше нет!", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Warning);
                            else
                            {
                                selectedReader.UserBooks.Add(selectedBook);
                                selectedBook.Num--;
                            }
                        }
                    }));
            }
        }

        public RelayCommand GetBackCommand
        {
            get
            {
                return getBackCommand ??
                    (getBackCommand = new RelayCommand(obj =>
                    {
                        if (selectedReader == null) 
                            MessageBox.Show("Не выбран читатель для возврата книги!", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Warning);
                        else if (selectedReadersBook == null)
                            MessageBox.Show("Не выбрана книга для возврата!", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Warning);
                        else
                        {
                            selectedReadersBook.Num++;
                            selectedReader.UserBooks.Remove(selectedReadersBook);
                        }
                    }));
            }
        }

        public AppVM()
        {
            Books = new ObservableCollection<Books>
            {
                new Books{ Article = 1, BookName = "Кодекс тайн", Num = 5, Year = 2011 },
                new Books{ Article = 2, BookName = "Легенды прошлого", Num = 13, Year = 1976 },
                new Books{ Article = 3, BookName = "Потерянные артефакты", Num = 1, Year = 2001 },
                new Books{ Article = 4, BookName = "Тайны и загадки 18 века", Num = 2, Year = 1754 },
                new Books{ Article = 5, BookName = "Приключения Тома Сойера", Num = 12, Year = 2003 },
                new Books{ Article = 6, BookName = "Забытые истории", Num = 7, Year = 2020 },
                new Books{ Article = 7, BookName = "Загадки времени", Num = 9, Year = 1985 },
                new Books{ Article = 8, BookName = "Война и мир", Num = 13, Year = 1869 },
                new Books{ Article = 9, BookName = "Али-Баба и сорок разбойников", Num = 11, Year = 1704 },
                new Books{ Article = 10, BookName = "1984", Num = 18, Year = 1949 },
                new Books{ Article = 11, BookName = "Анна Каренина", Num = 3, Year = 1878 },
                new Books{ Article = 12, BookName = "Улисс", Num = 4, Year = 1920 },
                new Books{ Article = 13, BookName = "Бойцовский клуб", Num = 1, Year = 1996 },
                new Books{ Article = 14, BookName = "Дубровский", Num = 2, Year = 1841 },
                new Books{ Article = 15, BookName = "Собачье сердце", Num = 3, Year = 1925 },
                new Books{ Article = 16, BookName = "Зеленая миля", Num = 1, Year = 1996 },
                new Books{ Article = 17, BookName = "Портрет Дориана Грея", Num = 7, Year = 1890 },
                new Books{ Article = 18, BookName = "Преступление и наказание", Num = 8, Year = 1866 },
            };

            Peoples = new ObservableCollection<People>
            {
                new People{Id = 0, Name = "Василий Иванович", Email = "vasiliy.ivanovitch@outlook.com", UserBooks = {Books[1], Books[2], Books[3], Books[2] } },
                new People{Id = 1, Name = "Мария Соколова", Email = "maria.sokolova_87@yahoo.com", UserBooks ={} },
                new People{Id = 2, Name = "Елена Соловьева ", Email = "elena_solovyeva@live.com", UserBooks ={} },
                new People{Id = 3, Name = "Алексей Егоров", Email = "lex.egorov@gmail.com", UserBooks ={} },
                new People{Id = 4, Name = "Дмитрий Васильев", Email = "dmitry.vasilev@yandex.com", UserBooks ={} },
                new People{Id = 5, Name = "Михаил Козлов", Email = "m.kozlov@protonmail.com", UserBooks ={} },
                new People{Id = 6, Name = "Максим Кузнецов", Email = "kuznetsov.maxim@gmail.com", UserBooks ={} },
                new People{Id = 7, Name = "Дмитрий Петров", Email = "petrov.dmitry@yandex.com", UserBooks ={} },
                new People{Id = 8, Name = "Иван Лебедев", Email = "ivan_lebedev@icloud.com", UserBooks ={} },
                new People{Id = 9, Name = "Ольга Петрова", Email = "o.petrova@hotmail.com", UserBooks ={} },
                new People{Id = 10, Name = "Алексей Петров", Email = "alexeypetrov@hotmail.com", UserBooks ={} },
                new People{Id = 11, Name = " Анастасия Николаева", Email = "anasikol@hotmail.com", UserBooks ={} },
                new People{Id = 12, Name = "Павел Михайлов", Email = "pavelmikhailov@gmail.com", UserBooks ={} },
                new People{Id = 13, Name = "Ирина Федорова", Email = "irinafedorova@gmail.com", UserBooks ={} },
                new People{Id = 14, Name = "Сергей Иванов", Email = "sergeiivanov@mail.ru", UserBooks ={} },
                new People{Id = 15, Name = "Александр Белов", Email = "alexanderbelov@mail.ru", UserBooks ={} },
            };
        }


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}

