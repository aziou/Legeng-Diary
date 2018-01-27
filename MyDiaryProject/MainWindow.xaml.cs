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

        public string localParh = @"Data Source=" + System.Environment.CurrentDirectory + "\\database\\MyFirstDataBase.db" + ";Pooling=true;FailIfMissing=false";
        private void Load()
        {
            #region 测试
            Picker_Time.Text = DateTime.Now.ToShortDateString();
            RefreshLisbox();
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
                case "btn_save":
                    DiaryModel.DiaryModel tempDiaryModel = new DiaryModel.DiaryModel();
                    tempDiaryModel.UserName = "lee";
                    tempDiaryModel.diaryType = cmb_diaryType.SelectedIndex;
                    tempDiaryModel.itemTitle = txt_Title.Text.ToString();
                    tempDiaryModel.diaryContent = Editor.ContentHtml;
                    tempDiaryModel.writeDate =Convert.ToDateTime(Picker_Time.Text);

                  Dictionary<string,string> DataList = new Dictionary<string, string>();
                    List<string> SqlList = new List<string>();
                    SoftHelper.SqlLiteHelper helper = new SoftHelper.SqlLiteHelper(localParh)
;                    DataList = helper.GetDateAndIDDataList("select * from baseinfo order by WriteDownDate desc");
                 
                    string sql = "";
                    if (DataList.ContainsKey(Convert.ToDateTime(Picker_Time.Text).ToString("yyyy-MM-dd")))
                    {
                        sql = string.Format("update  baseinfo set UserName='{0}',DiaryType={1},DiaryTitle='{2}',DiaryContext='{3}' where UserId='{4}'"
, tempDiaryModel.UserName
, tempDiaryModel.diaryType
, tempDiaryModel.itemTitle
, tempDiaryModel.diaryContent
, DataList[Convert.ToDateTime( Picker_Time.Text).ToString("yyyy-MM-dd")]);
                    }
                    else
                    {
                         sql = string.Format("insert into baseinfo (UserName,DiaryType,DiaryTitle,WriteDownDate,DiaryContext,UserId) values('{0}',{1},'{2}','{3}','{4}','{5}')"
                        , tempDiaryModel.UserName
                        , tempDiaryModel.diaryType
                        , tempDiaryModel.itemTitle
                        , tempDiaryModel.writeDate.ToString("s")
                        , tempDiaryModel.diaryContent.Replace("'","\"")
                        ,Convert.ToUInt64(Convert.ToDateTime(Picker_Time.Text).ToString("yyyyMMdd")+DateTime.Now.ToString("HHmmss")).ToString());
                    }
                    SqlList.Add(sql);
                    bool result= helper.InsertData(SqlList);

                    if (result)
                    {
                        MessageBox.Show("日记保存 成功！", "Tip");
                    }
                    else
                    {

                        MessageBox.Show("日记保存 失败！", "Tip");
                    }
                    RefreshLisbox();
                    break;
                default:
                    break;
            }
        }

        private void RefreshLisbox()
        {
            try
            {
                ObservableCollection<DiaryModel.DiaryModel> DiaryCol = new ObservableCollection<DiaryModel.DiaryModel>()
                {

               

                };
                SoftHelper.SqlLiteHelper helper = new SoftHelper.SqlLiteHelper(localParh)
;
                DiaryCol = helper.GetDiaryItem();



                ViewModel.ViewModel.CreateInstance().DiaryInfo = DiaryCol;
            }
            catch
            {

            }
        }

        private void listB_itemList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            DiaryModel.DiaryModel curItem =(DiaryModel.DiaryModel) listB_itemList.SelectedItem;

            Picker_Time.Text = curItem.writeDate.ToString("yyyy-MM-dd");

            cmb_diaryType.SelectedIndex = curItem.diaryType;

            txt_Title.Text = curItem.itemTitle;

            Editor.ContentHtml = curItem.diaryContent;
        }
    }
}
