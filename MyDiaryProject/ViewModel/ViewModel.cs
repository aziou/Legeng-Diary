using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.ComponentModel;
using MyDiaryProject.Model;
namespace MyDiaryProject.ViewModel
{
    public  class ViewModel:INotifyPropertyChanged
    {
        private volatile static ViewModel _instance = null;
        private static readonly object lockHelper = new object();

      

        public static ViewModel CreateInstance()
        {
            if (_instance == null)
            {
                lock (lockHelper)
                {
                    if (_instance == null)
                        _instance = new ViewModel();
                }
            }
            return _instance;
        }

        private ObservableCollection<DiaryModel> diaryInfo;
        public ObservableCollection<DiaryModel> DiaryInfo
        {
            get
            {
                return diaryInfo;
            }
            set
            {
                diaryInfo = value;
                OnPropertyChanged("DiaryInfo");
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
