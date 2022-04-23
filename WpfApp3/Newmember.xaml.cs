using System;
using System.Windows;
using System.Data.SqlClient;

namespace WpfApp3
{
    /// <summary>
    /// Newmember.xaml에 대한 상호 작용 논리
    /// </summary>

    public partial class Newmember : Window
    {
        public Newmember()
        {
            InitializeComponent();
           
        }

        public void BtnSignUp_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult msg = MessageBox.Show("직원등록을 하시겠습니까?", "직원등록", MessageBoxButton.YesNo);
            if (msg.ToString() == "Yes")
            {
                Sql sql = new Sql();

                SqlDataReader sd = sql.CheckID(textId.Text, "join");
                if (!sd.HasRows)
                {
                    sd.Close();
                    sql.NewMember(textId.Text, textName.Text, textPassword.Password, textPhone.Text, textAddress.Text, textTimepay.Text);
                    MessageBox.Show("등록되었습니다!");
                    Window.GetWindow(this).Close();
                }
                else
                {
                    MessageBox.Show("이미 등록된 아이디가 있습니다.");
                    textId.Focus();
                    sd.Close();
                }
                sql = null;
            }
        }
    }
}
