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
using System.Windows.Shapes;
using System.Data;
using System.Data.SqlClient;

namespace WpfApp3
{
    /// <summary>
    /// Record.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Record : Window
    {
        public Record()
        {
            InitializeComponent();
        }

        private void Brnsearch_Click(object sender, RoutedEventArgs e)
        {
            string date = datepicker.Text;

            Sql sql = new Sql();
            SqlDataReader sd = sql.Daterecord(date);
            DataTable datatable = new DataTable();
            datatable.Columns.Add("아이디", typeof(string));
            datatable.Columns.Add("출근시간", typeof(string));
            datatable.Columns.Add("퇴근시간", typeof(string));
            datatable.Columns.Add("근무시간", typeof(string));
            while (sd.Read())
            {
                string id = sd["tID"].ToString();
                string onwork = sd["tONWORK"].ToString();
                string offwork = sd["tOFFWORK"].ToString();
                string time = sd["tWORKTIME"].ToString();
                datatable.Rows.Add(id,onwork,offwork,time);
            }
            datagrid.ItemsSource = datatable.DefaultView;

            sql = null;
        }
    }
}
