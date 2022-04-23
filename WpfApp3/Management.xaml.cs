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
    /// Management.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Management : Window
    {
        DataTable datatable;
        public Management()
        {
            InitializeComponent();
        }

        private void BtnSearch_Click(object sender, RoutedEventArgs e)
        {
           
            datatable = new DataTable();

            datatable.Columns.Add("아이디", typeof(string));
            datatable.Columns.Add("이름", typeof(string));
            datatable.Columns.Add("주소", typeof(string));
            datatable.Columns.Add("핸드폰", typeof(string));
            datatable.Columns.Add("등록일", typeof(string));
            datatable.Columns.Add("시급", typeof(string));
            TableRead();

        }
        
        private void Table_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(table.SelectedIndex != -1)
            {
                textName.Text = datatable.Rows[table.SelectedIndex]["이름"].ToString();
                textId.Text = datatable.Rows[table.SelectedIndex]["아이디"].ToString();
                textAddress.Text = datatable.Rows[table.SelectedIndex]["주소"].ToString();
                textPhone.Text = datatable.Rows[table.SelectedIndex]["핸드폰"].ToString();
                textDate.Text = datatable.Rows[table.SelectedIndex]["등록일"].ToString();
                textTimepay.Text = datatable.Rows[table.SelectedIndex]["시급"].ToString();
                btnModify.IsEnabled = true;
                btnDelete.IsEnabled = true;
            }
        }

        private void BtnModify_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult msg = MessageBox.Show("회원정보를 수정하시겠습니까?", "회원정보 수정", MessageBoxButton.YesNo);
            if (msg.ToString() == "Yes")
            {
                Sql sql = new Sql();
                sql.Sqlmembermodify(textId.Text, textName.Text, textAddress.Text, textPhone.Text, textTimepay.Text);
                datatable.Rows.Clear();
                TableRead();
                MessageBox.Show("회원수정 되었습니다.");
                btnModify.IsEnabled = false;
                btnDelete.IsEnabled = false;
            }

        }
        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult msg = MessageBox.Show("회원정보를 삭제하시겠습니까?", "회원정보 삭제", MessageBoxButton.YesNo);
            if (msg.ToString() == "Yes")
            {
                Sql sql = new Sql();
                SqlDataReader sd = sql.Sqlsalary(textId.Text, "1000-01-01", "9999-12-31");
                if (sd.HasRows)
                {
                    MessageBox.Show("지급되지 않은 급여가 있습니다.\n지급처리 후 다시 시도하십시오.","회원정보 삭제");
                    Window search = new Search();
                    search.Show();
                }
                else
                {
                    sd.Close();
                    sql.Sqlmemberdelete(textId.Text);
                    datatable.Clear();
                    TableRead();
                    textId.Text = "";
                    textName.Text = "";
                    textAddress.Text = "";
                    textPhone.Text = "";
                    textDate.Text = "";
                    textTimepay.Text = "";
                    MessageBox.Show("회원삭제 되었습니다.", "회원정보 삭제");
                    btnModify.IsEnabled = false;
                    btnDelete.IsEnabled = false;
                }
                
            }
        }
        private void TableRead()
        {
            string search = textSearch.Text.ToString();
            string id, name, address, phone, date, timepay;
            Sql sql = new Sql();
            SqlDataReader sd = sql.MemberShearch(search);
            while (sd.Read())
            {
                id = sd["tID"].ToString();
                name = sd["tNAME"].ToString();
                address = sd["tADDRESS"].ToString();
                phone = sd["tPHONE"].ToString();
                date = sd["tDATE"].ToString();
                timepay = sd["tTIMEPAY"].ToString();
                datatable.Rows.Add(id, name, address, phone, date, timepay);
            }
            table.ItemsSource = datatable.DefaultView;
        }
    }
}
