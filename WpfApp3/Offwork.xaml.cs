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

namespace WpfApp3
{
    /// <summary>
    /// Offwork.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Offwork : Window
    {
        public Offwork()
        {
            InitializeComponent();
        }

        private void BtnOffwork_Click(object sender, RoutedEventArgs e)
        {
            Sql sql = new Sql();
            SqlDataReader sd = sql.WorkIdCheck(textId.Text, textPassword.Password);
            if (sd.HasRows)
            {
                sd.Close();
                sd = sql.OffWork(textId.Text, textPassword.Password);
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
        }
    }
}
