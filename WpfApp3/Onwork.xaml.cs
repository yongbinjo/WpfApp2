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
using System.Data.SqlClient;
using System.Data;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using WpfApp3.Model;

namespace WpfApp3
{
    /// <summary>
    /// Onwork.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Onwork : Window
    {
        public Onwork()
        {
            InitializeComponent();
        }

        private void BtnOnwork_Click(object sender, RoutedEventArgs e)
        {
            Sql sql = new Sql();
            SqlDataReader sd = sql.WorkIdCheck(textId.Text, textPassword.Password);
            if (sd.HasRows)
            {
                sd.Close();
                sd = sql.Onwork(textId.Text);
                sd.Read();
                string msg = sd["msg"].ToString();
                MessageBox.Show(msg);
                sd.Close();
                this.Close();
            }
            else
            {
                sd.Close();
                MessageBox.Show("아이디나 패스워드를 확인해주세요.");
            }
            sql = null;
        }
    }
}
