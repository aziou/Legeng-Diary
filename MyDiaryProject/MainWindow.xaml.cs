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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using MyDiaryProject.Model;
using MyDiaryProject.ViewModel;
namespace MyDiaryProject
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Load();
            listB_itemList.DataContext = ViewModel.ViewModel.CreateInstance();
        }

        private void Load()
        {
            #region 测试
            MyDiaryProject.Model.DiaryModel currentDiary = new Model.DiaryModel();
            ObservableCollection<DiaryModel> DiaryCol = new ObservableCollection<DiaryModel>() {

                new DiaryModel() { itemTitle="Hello World",writeDate=Convert.ToDateTime("2018/1/25")},
                new DiaryModel() { itemTitle="船长的开发路",writeDate=Convert.ToDateTime("2018/1/24")}

            };

            ViewModel.ViewModel.CreateInstance().DiaryInfo= DiaryCol;
            #endregion
        }

        private void cmb_Item_Loaded(object sender, RoutedEventArgs e)
        {
            ComboBox cmb = (ComboBox)sender as ComboBox;

            #region 日记类型
            cmb_diaryType.Items.Add("个人日记");
            cmb_diaryType.Items.Add("出差日记");
            cmb_diaryType.SelectedIndex = 0;

            #endregion 
        }

        private void Functino_buttonClitk(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender as Button;

            switch (btn.Name)
            {
                case "txt_save":
                    break;
                default:
                    break;
            }
        }
    }
}
