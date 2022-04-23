using System;
using System.Windows;
using System.Data.SqlClient;

namespace WpfApp3
{
    /// <summary>
    /// Search.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Search : Window
    {
        public Search()
        {
            InitializeComponent();
        }
        private void TextSearch_Click(object sender, RoutedEventArgs e)
        {
            Sql sql = new Sql();
            if(textId.Text.Length>0)
            { 
                SqlDataReader sd = sql.Sqlsalary(textId.Text, dateStart.Text, dateEnd.Text);
                if (sd.HasRows)
                {
                    sd.Read();
                    textWorktime.Text = (Convert.ToInt32(sd["ttime"].ToString())/60).ToString() + "시간" + (Convert.ToInt32(sd["ttime"].ToString()) % 60).ToString() + "분";
                    textSalary.Text = string.Format("{0:n0}",Convert.ToInt32(sd["tpay"].ToString())) + "원";
                    sd.Close();
                }
                else
                {
                    sd.Close();
                    textWorktime.Text = "0";
                    textSalary.Text = "0원";
                }
                sql = null;
            }
            else
            {
                MessageBox.Show("아이디를 입력해 주세요.", "검색");
            }
        }
        private void BtnInit_Click(object sender, RoutedEventArgs e)
        {
            Sql sql = new Sql();
            SqlDataReader sd=sql.CheckID(textId.Text, "search");
            if (sd.HasRows)
            {
                sd.Close();
                MessageBoxResult msg = MessageBox.Show("지급 처리 하시겠습니까?\n지급 처리시 다음 급여에 포함되지 않습니다.", "급여지급", MessageBoxButton.YesNo);
                if (msg.ToString() == "Yes")
                {
                    sd = sql.Sqlsalary(textId.Text, dateStart.Text, dateEnd.Text);
                    if(sd.HasRows)
                    {
                        sd.Close();
                        sql.Sqlworkrecorddelete(textId.Text, dateStart.Text, dateEnd.Text);
                        textSalary.Text = "";
                        textWorktime.Text = "";
                        MessageBox.Show("지급 처리 되었습니다.", "급여지급");
                    }
                    else
                    {
                        MessageBox.Show("지급할 급여가 없습니다.", "급여지급");
                    }
                }
            }
            else
                MessageBox.Show("지급 대상이 없습니다.", "급여지급");

            sql = null;
        }
    }
}
