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
using System.Data.SqlClient;
using System.Data;

namespace WpfApp3
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        
        public MainWindow()
        {
            InitializeComponent();
            double screenWidth = SystemParameters.PrimaryScreenWidth;
            double screenHeight = SystemParameters.PrimaryScreenHeight;
            double windowWidth = this.Width;
            double windowHeight = this.Height;
            this.Left = (screenWidth / 2) - (windowWidth / 2);
            this.Top = (screenHeight / 2) - (windowHeight / 2);
            gridcontrol();
        }

        private void BtnNewmember_Click(object sender, RoutedEventArgs e)
        {
            Window newmember = new Newmember();
            newmember.Owner = Application.Current.MainWindow;
            newmember.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            newmember.Show();
            
        }

        private void btnManagement_Click(object sender, RoutedEventArgs e)
        {
            Window management = new Management();
            management.Owner = Application.Current.MainWindow;
            management.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            management.Closed += SubWindow_Closed;
            management.Show();
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            Window search = new Search();
            search.Owner = Application.Current.MainWindow;
            search.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            search.Show();
        }

        private void BtnOnwork_Click(object sender, RoutedEventArgs e)
        {
            Window onwork = new Onwork();
            onwork.Owner = Application.Current.MainWindow;
            onwork.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            onwork.Closed += SubWindow_Closed;
            onwork.ShowDialog();
            
        }

        private void BtnOffwork_Click(object sender, RoutedEventArgs e)
        {
            var offwork = new Offwork();
            offwork.Owner = Application.Current.MainWindow;
            offwork.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            offwork.Closed += SubWindow_Closed;
            offwork.ShowDialog();
        }

        private void BtnRecord_Click(object sender, RoutedEventArgs e)
        {
            Window record = new Record();
            record.Owner = Application.Current.MainWindow;
            record.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            record.Show();
        }

        private void SubWindow_Closed(object sender, EventArgs e)
        {
            gridcontrol();
        }
        private void gridcontrol()
        { //main datagrid에 data 업데이트
            Sql sql = new Sql();
            SqlDataReader sd = sql.SqlOnworkselect();
            DataTable datatable = new DataTable();

            datatable.Columns.Add("아이디", typeof(string));
            datatable.Columns.Add("출근시간", typeof(string));
            while (sd.Read())
            {
                string id = sd["tID"].ToString();
                string workday = sd["tONWORK"].ToString();
                datatable.Rows.Add(id, workday);
            }
            
            datagrid.ItemsSource = datatable.DefaultView;
        }

      
    }
}
