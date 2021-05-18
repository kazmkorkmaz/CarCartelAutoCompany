using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace CarCartelAutoCompany
{
    public partial class LoginScreen : DevExpress.XtraEditors.XtraForm
    {
        public LoginScreen()
        {
            InitializeComponent();
        }
        SqlConnect sqlConnect = new SqlConnect();
        
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand("Select * from Tbl_Personnel where Username=@p1 and Password=@p2",sqlConnect.sqlConnection());
            command.Parameters.AddWithValue("@p1",txtUsername.Text);
            command.Parameters.AddWithValue("@p2",txtpassword.Text);
            SqlDataReader dataReader = command.ExecuteReader();
            if (dataReader.Read())
            {
                if (int.Parse(dataReader[7].ToString()) == 1)
                {
                    MarketingForm marketingForm = new MarketingForm();
                    marketingForm.perID = dataReader[0].ToString();
                    marketingForm.Show();
                    
                }
                else if (int.Parse(dataReader[7].ToString()) == 3)
                {
                    PurchaseDepartment purchaseDepartment = new PurchaseDepartment();
                    purchaseDepartment.perID = dataReader[0].ToString();
                    purchaseDepartment.Show();
                   
                }
                else if (int.Parse(dataReader[7].ToString()) == 4)
                {
                    AccountingDepartment accountingDepartment = new AccountingDepartment();
                    accountingDepartment.perID = dataReader[0].ToString();
                    accountingDepartment.Show();
                 
                }
                else if (int.Parse(dataReader[7].ToString()) == 5)
                {
                    ManagerDepartment managerDepartment = new ManagerDepartment();
                    managerDepartment.perID = dataReader[0].ToString();
                    managerDepartment.Show();
                      
                }
            }
            else 
            {
                MessageBox.Show("Username or Password is wrong! Please try again or contact with manager..","Warning",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }

        }

        private void LoginScreen_Load(object sender, EventArgs e)
        {

        }
    }
}