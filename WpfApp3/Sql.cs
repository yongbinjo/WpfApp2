using System;
using System.Text;
using System.Data.SqlClient;
using ybcDatabaseClass;

namespace WpfApp3
{
    public class Sql
    {
        DatabaseClass dbc;
        public Sql()
        {
            string strsql = "Data Source = localhost; Database=db_workmanagement;Integrated Security = SSPI";
            dbc = new DatabaseClass(strsql);
        }
        public void NewMember(string id, string name, string password, string phone, string address, string timepay)
        {
            string str = "INSERT INTO dbo.tb_member(tID,tNAME,tPASSWORD,tPHONE,tADDRESS,tTIMEPAY) VALUES('"+id+"','"+name + "','" + password + "','" + phone + "','" + address + "','" + timepay + "')";
            dbc.CallInsert(str);
        }
        public SqlDataReader MemberShearch(string text)
        {
            StringBuilder strb = new StringBuilder();
            strb.Append("select * from dbo.tb_member where tuse=0 and (tID LIKE '%" + text + "%'" +
                " or tNAME LIKE '%" + text + "%'" +
                " or tADDRESS LIKE '%" + text + "%' " +
                " or tPHONE LIKE '%" + text + "%' " +
                " or tDATE LIKE '%" + text + "%'" +
                " or tTIMEPAY LIKE '%" + text + "%') ");

            SqlDataReader sd = dbc.CallSelect(strb.ToString());
            strb = null;
            return sd;
        }
        public SqlDataReader CheckID(string id, string state)
        {
            StringBuilder strb = new StringBuilder();
            if(state =="join")
                strb.Append("select * from dbo.tb_member where tID = '" + id + "'");
            if(state =="search")
                strb.Append("select * from dbo.tb_member where tID = '" + id + "' and tuse=0");
            SqlDataReader sd = dbc.CallSelect(strb.ToString());
            return sd;
        }

        public SqlDataReader WorkIdCheck(string id, string password)
        {
            StringBuilder strb = new StringBuilder();
            strb.Append("select * from dbo.tb_member where tID = '" + id + "' and tPASSWORD = '" + password + "' and tuse=0 ");
            SqlDataReader sd = dbc.CallSelect(strb.ToString());
            return sd;
        }
        public SqlDataReader SqlOnworkselect()
        {
            StringBuilder strb = new StringBuilder();
            strb.Append("select tID,tONWORK from dbo.tb_workrecord where tOFFWORK = '' ");
            SqlDataReader sd = dbc.CallSelect(strb.ToString());
            return sd;
        }
        public void Sqlmembermodify(string id, string name ,string address, string phone,  string timepay)
        {
            StringBuilder strb = new StringBuilder();
            strb.Append("UPDATE dbo.tb_member SET tNAME = ('"+ name + "'), tADDRESS = ('"+ address + "'), tPHONE = ('"+ phone + "'), tTIMEPAY = ('"+ timepay + "') WHERE tID = '" + id + "' ");
            dbc.CallInsert(strb.ToString());
        }
        public SqlDataReader Onwork(string id)
        {
            StringBuilder strb = new StringBuilder();
            strb.Append("declare @tid varchar(20), @msg varchar(30)" +
                " select @tid = tid  from dbo.tb_workrecord where tID = '"+ id + "' and tOFFWORK = ''" +
                " if (@tid is not null)" +
                    " begin" +
                        " set @msg = '이미 출근처리 되었습니다.'" +
                        " select @msg as msg" +
                    " end" +
                " else" +
                    " begin" +
                        " INSERT INTO dbo.tb_workrecord(tID, tONWORK) VALUES('" + id + "', Convert(varchar(30), Getdate(), 120))" +
                        " set @msg = '출근 등록되었습니다.'" +
                        " select @msg as msg" +
                    " end");
            SqlDataReader sd = dbc.CallSelect(strb.ToString());
            return sd;
        }
        public SqlDataReader OffWork(string id, string password)
        {
            StringBuilder strb = new StringBuilder();
            strb.Append("declare @tid varchar(20), @time varchar(20),@diff int, @msg varchar(30)" +
                " select @time = tonwork from dbo.tb_workrecord where tid = '" + id + "' and toffwork = ''" +
                " if (@time is null or @time = '')" +
                    " begin" +
                        " set @msg = '출근기록이 없습니다.'" +
                        " select @msg as msg" +
                    " end" +
                " else" +
                    " begin" +
                        " select @diff = DATEDIFF(MINUTE, (select tONWORK from dbo.tb_workrecord where tid = '" + id + "' and toffwork = ''), GETDATE())" +
                        " UPDATE dbo.tb_workrecord SET toffwork = Convert(varchar(30), Getdate(), 120), tworktime = @diff where tid = '" + id + "' and toffwork = ''" +
                        " set @msg = '퇴근 등록되었습니다.'" +
                        " select @msg as msg " +
                    " end");

            SqlDataReader sd = dbc.CallSelect(strb.ToString());
            return sd;
        }
        public SqlDataReader Daterecord(string date)
        {
            StringBuilder strb = new StringBuilder();
            strb.Append("select * from dbo.tb_workrecord where tONWORK LIKE '" + date + "%'");
            SqlDataReader sd = dbc.CallSelect(strb.ToString());
            return sd;
        }
        public SqlDataReader Sqlsalary(string id, string start, string end)
        {
            StringBuilder strb = new StringBuilder();
            strb.Append("select a.tid, sum(a.tworktime) as ttime, sum(((a.tworktime / 60) * b.ttimepay) + (a.tworktime % 60) * (b.ttimepay / 60)) as tpay from dbo.tb_Workrecord a " +
                " inner join dbo.tb_Member b on a.tid = b.tid where a.tid = '"+ id + "' and a.tuse=0 and" +
                " (a.tonwork LIKE '"+ start + "%' or a.toffwork BETWEEN '"+ start + "%' and '"+ end + "%' or a.toffwork LIKE '"+ end + "%') group by a.tid ");
            SqlDataReader sd = dbc.CallSelect(strb.ToString());
            return sd;
        }
        public void Sqlworkrecorddelete(string id, string start, string end)
        {
            StringBuilder strb = new StringBuilder();
            strb.Append("update dbo.tb_workrecord set tuse=1 where tID = '" + id + "' and (tonwork LIKE '" + start + "%' or toffwork BETWEEN '" + start + "%' and '" + end + "%' or toffwork LIKE '" + end + "%') ");
            dbc.CallInsert(strb.ToString());
        }
        public void Sqlmemberdelete(string id)
        {
            StringBuilder strb = new StringBuilder();
            strb.Append("update dbo.tb_member set tuse=1 where tID = '" + id + "' " +
                "update dbo.tb_workrecord set tuse=1 where tID = '" + id + "' ");
            SqlDataReader sd = dbc.CallSelect(strb.ToString());
            sd.Close();
        }
    }
}