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
using System.IO;

namespace CarCartelAutoCompany
{
    public partial class ManagerDepartment : DevExpress.XtraEditors.XtraForm
    {
        public ManagerDepartment()
        {
            InitializeComponent();
        }
        private void tileBar_SelectedItemChanged(object sender, TileItemEventArgs e)
        {
            navigationFrame.SelectedPageIndex = tileBarGroupTables.Items.IndexOf(e.Item);
        }
             

        private void timer1_Tick(object sender, EventArgs e)
        {
            labelDate.Text = DateTime.Now.ToLongDateString();
            labelTime.Text = DateTime.Now.ToLongTimeString();
        }

     
        void toDoList()
        {
            SqlCommand command = new SqlCommand("Select ID,Subject,Contents from Tbl_ToDoList where PersonnelID=@P1", sqlConnect.sqlConnection());
            command.Parameters.AddWithValue("@P1", textEditIDPersonnel.Text);
            SqlDataAdapter da = new SqlDataAdapter(command);
            DataTable dt = new DataTable();
            da.Fill(dt);
            gridControlToDoList.DataSource = dt;
            sqlConnect.sqlConnection().Close();
        }
        void campaignList()
        {
            SqlCommand command = new SqlCommand("Select Campaign from Tbl_Campaigns", sqlConnect.sqlConnection());
            SqlDataAdapter da = new SqlDataAdapter(command);
            DataTable dt = new DataTable();
            da.Fill(dt);
            gridControlCampaigns.DataSource = dt;
            sqlConnect.sqlConnection().Close();
        }
        void receiverList()
        {
            comboBoxReceiverMEssage.Items.Clear();
            SqlCommand command = new SqlCommand("select ID,Name+' '+Surname as 'Personnel' from Tbl_Personnel", sqlConnect.sqlConnection());
            SqlDataAdapter da = new SqlDataAdapter(command);
            DataTable dt = new DataTable();
            da.Fill(dt);
            comboBoxReceiverMEssage.ValueMember = "ID";
            comboBoxReceiverMEssage.DisplayMember = "Personnel";
            comboBoxReceiverMEssage.DataSource = dt;
            sqlConnect.sqlConnection().Close();
        }
        void incomesList()
        {
            SqlCommand command = new SqlCommand("select Tbl_Customer.Name+' '+ Tbl_Customer.Surname AS 'Customer',Brand+' '+Model as 'Car',Tbl_Personnel.Name+' '+ Tbl_Personnel.Surname AS 'Personnel',SalePrice as 'Income',Tbl_Income.Month+' '+Tbl_Income.Year as 'Income Date','Sale' as 'Income Type' from Tbl_Sale inner join Tbl_Customer on Tbl_Sale.CustomerID = Tbl_Customer.ID inner join  Tbl_Car on Tbl_Sale.CarID = Tbl_Car.ID inner join  Tbl_Personnel on Tbl_Sale.PersonnelID = Tbl_Personnel.ID inner join  Tbl_Income on Tbl_Sale.IncomeID = Tbl_Income.ID union select Tbl_Customer.Name +' '+ Tbl_Customer.Surname AS 'Customer',Brand+' '+Model as 'Car',Tbl_Personnel.Name+' '+ Tbl_Personnel.Surname AS 'Personnel',RentPrice as 'Income',Tbl_Income.Month+' '+Tbl_Income.Year as 'Income Date','Rental' as 'Income Type' from Tbl_Rent inner join Tbl_Customer on Tbl_Rent.CustomerID = Tbl_Customer.ID inner join  Tbl_Car on Tbl_Rent.CarID = Tbl_Car.ID inner join  Tbl_Personnel on Tbl_Rent.PersonnelID = Tbl_Personnel.ID inner join  Tbl_Income on Tbl_Rent.IncomeID = Tbl_Income.ID", sqlConnect.sqlConnection());
            SqlDataAdapter da = new SqlDataAdapter(command);
            DataTable dt = new DataTable();
            da.Fill(dt);
            gridControlIncomes.DataSource = dt;
            sqlConnect.sqlConnection().Close();
        }
        void expenseList()
        {
            SqlCommand command = new SqlCommand("select 'Salary' as 'Expense Type',Tbl_Salary.Month+'-'+Tbl_Salary.Year AS 'Date',Salary as 'Price' from Tbl_Salary union select  Tbl_Bill.BillName as 'Expense Type',Tbl_Bill.Month+'-'+Tbl_Bill.Year AS 'Date',BillPrice as 'Price' from Tbl_Bill union select 'Purchase Car' as 'Expense Type', CONVERT(varchar,Tbl_Purchase.Date,1) as 'Date',Tbl_Purchase.CarPrice as 'Price'  from Tbl_Purchase", sqlConnect.sqlConnection());
            SqlDataAdapter da = new SqlDataAdapter(command);
            DataTable dt = new DataTable();
            da.Fill(dt);
            gridControlExpenses.DataSource = dt;
            sqlConnect.sqlConnection().Close();
        }
        void bankAccounts()
        {
            SqlCommand command = new SqlCommand("select ID,BankName,AccountType,AccountID,Wallet from Tbl_BankAccount", sqlConnect.sqlConnection());
            SqlDataAdapter da = new SqlDataAdapter(command);
            DataTable dt = new DataTable();
            da.Fill(dt);
            gridControlBankAccounts.DataSource = dt;
            sqlConnect.sqlConnection().Close();
        }
        void rentedCars()
        {
            SqlCommand command = new SqlCommand("select Tbl_Rent.ID,StartDate,FinishDate,Tbl_Personnel.Name+' '+Tbl_Personnel.Surname as 'Personnel',Tbl_Customer.Name+' '+Tbl_Customer.Surname as 'Customer',Brand+' '+Model as 'Car',RentPrice from Tbl_Rent inner join Tbl_Car on Tbl_Rent.CarID = Tbl_Car.ID inner join Tbl_Customer on Tbl_Rent.CustomerID=Tbl_Customer.ID inner join Tbl_Personnel on Tbl_Rent.PersonnelID = Tbl_Personnel.ID", sqlConnect.sqlConnection());
            SqlDataAdapter da = new SqlDataAdapter(command);
            DataTable dt = new DataTable();
            da.Fill(dt);
            gridControlRentedCars.DataSource = dt;
            sqlConnect.sqlConnection().Close();
        }
        void saleHistory()
        {
            SqlCommand command = new SqlCommand("select Tbl_Sale.ID,Date,Tbl_Personnel.Name+' '+Tbl_Personnel.Surname as 'Personnel',Tbl_Customer.Name+' '+Tbl_Customer.Surname as 'Customer',Tbl_Car.Brand+' '+Tbl_Car.Model as 'Car',Price from Tbl_Sale inner join Tbl_Customer on Tbl_Sale.CustomerID=Tbl_Customer.ID inner join Tbl_Car on Tbl_Sale.CarID=Tbl_Car.ID inner join Tbl_Personnel on Tbl_Sale.PersonnelID=Tbl_Personnel.ID ", sqlConnect.sqlConnection());
            SqlDataAdapter da = new SqlDataAdapter(command);
            DataTable dt = new DataTable();
            da.Fill(dt);
            gridControlSales.DataSource = dt;
            sqlConnect.sqlConnection().Close();
        }
        void stockList()
        {
            SqlCommand command = new SqlCommand("Select Tbl_CarList.ID,Brand+' '+Model as 'Car',CarBodyStyle,Number,Statu from Tbl_CarList inner join Tbl_Car on Tbl_CarList.CarID = Tbl_Car.ID", sqlConnect.sqlConnection());
            SqlDataAdapter da = new SqlDataAdapter(command);
            DataTable dt = new DataTable();
            da.Fill(dt);
            gridControlStocks.DataSource = dt;
            sqlConnect.sqlConnection().Close();
        }
        void purchaseList()
        {

            SqlCommand command = new SqlCommand("select Tbl_Purchase.ID,Tbl_Purchase.Date,Brand+' '+Model as 'Car',CarPrice,Tbl_Personnel.Name+' '+Tbl_Personnel.Surname as 'Personnel' from Tbl_Purchase inner join Tbl_Car on Tbl_Purchase.CarID=Tbl_Car.ID inner join Tbl_Personnel on Tbl_Purchase.PersonnelID = Tbl_Personnel.ID", sqlConnect.sqlConnection());
            SqlDataAdapter da = new SqlDataAdapter(command);
            DataTable dt = new DataTable();
            da.Fill(dt);
            gridControlPurchases.DataSource = dt;
            sqlConnect.sqlConnection().Close();
        }
        void campaignListCreation()
        {
            SqlCommand command = new SqlCommand("Select * from Tbl_Campaigns", sqlConnect.sqlConnection());
            SqlDataAdapter da = new SqlDataAdapter(command);
            DataTable dt = new DataTable();
            da.Fill(dt);
            gridControlCamp.DataSource = dt;
            sqlConnect.sqlConnection().Close();
        }
        void personnelList()
        {
            SqlCommand command = new SqlCommand("Select ID,Name,Surname,PhoneNumber,SocialSecurityID,Email,HireDate,CurrentSalary,DepartmentID,Username,Password,isActive from Tbl_Personnel", sqlConnect.sqlConnection());
            SqlDataAdapter da = new SqlDataAdapter(command);
            DataTable dt = new DataTable();
            da.Fill(dt);
            gridControlPersonnels.DataSource = dt;
            sqlConnect.sqlConnection().Close();
        }
        void departmentList()
        {
            comboBoxDepartment.Items.Clear();
            SqlCommand command = new SqlCommand("select ID,Name from Tbl_Department", sqlConnect.sqlConnection());
            SqlDataAdapter da = new SqlDataAdapter(command);
            DataTable dt = new DataTable();
            da.Fill(dt);
            comboBoxDepartment.ValueMember = "ID";
            comboBoxDepartment.DisplayMember = "Name";
            comboBoxDepartment.DataSource = dt;
            sqlConnect.sqlConnection().Close();
        }
        void PersonnelListComboBox()
        {
            comboBoxPersonnel.Items.Clear();
            SqlCommand command = new SqlCommand("select ID,Name+' '+Surname as 'Personnel' from Tbl_Personnel where DepartmentID=1", sqlConnect.sqlConnection());
            SqlDataAdapter da = new SqlDataAdapter(command);
            DataTable dt = new DataTable();
            da.Fill(dt);
            comboBoxPersonnel.ValueMember = "ID";
            comboBoxPersonnel.DisplayMember = "Personnel";
            comboBoxPersonnel.DataSource = dt;
            sqlConnect.sqlConnection().Close();
        }
        void updateCashRegister()
        {
            SqlCommand command2 = new SqlCommand("select SUM(Total)-(select SUM(Total) from Tbl_Expense)-(select SUM(Wallet) from Tbl_BankAccount) from Tbl_Income", sqlConnect.sqlConnection());
            SqlDataReader dr2 = command2.ExecuteReader();
            while (dr2.Read())
            {
                lblMoney.Text = dr2[0].ToString();
            }


        }
        SqlConnect sqlConnect = new SqlConnect();
        LoginScreen loginScreen = new LoginScreen();
        public string perID = "";
        private void ManagerDepartment_Load(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand("Select * from Tbl_Personnel where ID=@P1", sqlConnect.sqlConnection());
            command.Parameters.AddWithValue("@P1", int.Parse(perID));
            SqlDataReader dataReader = command.ExecuteReader();
            while (dataReader.Read())
            {
                textEditIDPersonnel.Text = dataReader[0].ToString();
                textEditName.Text = dataReader[1].ToString();
                textEditSurname.Text = dataReader[2].ToString();
                maskedTextBoxTC.Text = dataReader[4].ToString();
                maskedTextBoxPhone.Text = dataReader[3].ToString();
                textEditEmail.Text = dataReader[5].ToString();
                textEditHireDate.Text = dataReader[6].ToString();
                if (dataReader[11] != null)
                {
                    Byte[] data = new Byte[0];
                    data = (Byte[])(dataReader[11]);
                    MemoryStream mem = new MemoryStream(data);
                    pictureBoxPersonnel.Image = Image.FromStream(mem);
                }

            }
            toDoList();
            campaignList();
            receiverList();
            incomesList();
            expenseList();
            bankAccounts();
            rentedCars();
            saleHistory();
            stockList();
            purchaseList();
            campaignListCreation();
            personnelList();
            departmentList();
            PersonnelListComboBox();
            updateCashRegister();
        }
     

        private void BtnListToDo_Click(object sender, EventArgs e)
        {
            toDoList();
        }

        private void BtnAddToDo_Click(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand("Insert into Tbl_ToDoList (Subject,Contents,PersonnelID) values (@p1,@p2,@p3)", sqlConnect.sqlConnection());
            command.Parameters.AddWithValue("@p1", textEditSubject.Text);
            command.Parameters.AddWithValue("@p2", richTextBoxContent.Text);
            command.Parameters.AddWithValue("@p3", textEditIDPersonnel.Text);
            command.ExecuteNonQuery();
            sqlConnect.sqlConnection().Close();
            MessageBox.Show("The new subject added...", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BtnUpdateToDo_Click(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand("Update Tbl_ToDoList set Subject=@p1,Contents=@p2 where ID=@p3", sqlConnect.sqlConnection());
            command.Parameters.AddWithValue("@p1", textEditSubject.Text);
            command.Parameters.AddWithValue("@p2", richTextBoxContent.Text);
            command.Parameters.AddWithValue("@p3", lblToDoListID.Text);
            command.ExecuteNonQuery();
            sqlConnect.sqlConnection().Close();
            MessageBox.Show("The subject updated...", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BtnDeleteToDo_Click(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand("Delete from Tbl_ToDoList where ID=@p1", sqlConnect.sqlConnection());
            command.Parameters.AddWithValue("@p1", lblToDoListID.Text);
            command.ExecuteNonQuery();
            sqlConnect.sqlConnection().Close();
            MessageBox.Show("The subject deleted...", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dataRow = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            textEditSubject.Text = dataRow["Subject"].ToString();
            richTextBoxContent.Text = dataRow["Contents"].ToString();
            lblToDoListID.Text = dataRow["ID"].ToString();
        }

        private void btnSendMessage_Click(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand("insert into Tbl_Message (MessageSubject,MessageContent,Receiver,Sender) values (@p1,@p2,@p3,@p4)", sqlConnect.sqlConnection());
            command.Parameters.AddWithValue("@p1", txtSubjectMessage.Text);
            command.Parameters.AddWithValue("@p2", richTextBoxMsgContent.Text);
            command.Parameters.AddWithValue("@p3", comboBoxReceiverMEssage.SelectedValue);
            command.Parameters.AddWithValue("@p4", textEditIDPersonnel.Text);
            command.ExecuteNonQuery();
            sqlConnect.sqlConnection().Close();
            MessageBox.Show("Your message has been sent!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnClearMessage_Click(object sender, EventArgs e)
        {
            comboBoxReceiverMEssage.Text = "";
            txtSubjectMessage.Text = "";
            richTextBoxMsgContent.Text = "";
        }

        private void btnRecevingMessages_Click(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand("select Tbl_Message.ID,MessageSubject,MessageContent,Name+' '+Surname as 'Sender' from Tbl_Message inner join Tbl_Personnel on Tbl_Message.Sender = Tbl_Personnel.ID where Receiver=@p1", sqlConnect.sqlConnection());
            command.Parameters.AddWithValue("@p1", textEditIDPersonnel.Text);
            SqlDataAdapter da = new SqlDataAdapter(command);
            DataTable dt = new DataTable();
            da.Fill(dt);
            gridControlMessageBox.DataSource = dt;
            sqlConnect.sqlConnection().Close();
        }

        private void btnSubmittedMEssages_Click(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand("select Tbl_Message.ID,MessageSubject,MessageContent,Tbl_Personnel.Name+' '+Tbl_Personnel.Surname as 'Receiver' from Tbl_Message inner join Tbl_Personnel on Tbl_Message.Receiver = Tbl_Personnel.ID  where Sender=@p1", sqlConnect.sqlConnection());
            command.Parameters.AddWithValue("@p1", textEditIDPersonnel.Text);
            SqlDataAdapter da = new SqlDataAdapter(command);
            DataTable dt = new DataTable();
            da.Fill(dt);
            gridControlSubmiitedMessage.DataSource = dt;
            sqlConnect.sqlConnection().Close();
        }

        private void gridView13_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dataRow = gridView13.GetDataRow(gridView13.FocusedRowHandle);
            comboBoxReceiverMEssage.Text = dataRow["Receiver"].ToString();
            txtSubjectMessage.Text = dataRow["MessageSubject"].ToString();
            richTextBoxMsgContent.Text = dataRow["MessageContent"].ToString();
        }

        private void gridView14_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dataRow = gridView14.GetDataRow(gridView14.FocusedRowHandle);
            comboBoxReceiverMEssage.Text = dataRow["Sender"].ToString();
            txtSubjectMessage.Text = dataRow["MessageSubject"].ToString();
            richTextBoxMsgContent.Text = dataRow["MessageContent"].ToString();
        }

        private void gridView15_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dataRow = gridView15.GetDataRow(gridView15.FocusedRowHandle);
            txtCampID.Text = dataRow["ID"].ToString();
            richTextBoxCamp.Text = dataRow["Campaign"].ToString();
         
        }

        private void simpleButton7_Click(object sender, EventArgs e)
        {
            campaignListCreation();
            campaignList();
        }

        private void simpleButton8_Click(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand("insert into Tbl_Campaigns (Campaign) values (@p1)",sqlConnect.sqlConnection());
            command.Parameters.AddWithValue("@p1",richTextBoxCamp.Text);
            command.ExecuteNonQuery();
            sqlConnect.sqlConnection().Close();
            MessageBox.Show("Your campaign has been added!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            campaignList();
        }

        private void simpleButton9_Click(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand("update Tbl_Campaigns set Campaign=@p1 where ID=@p2", sqlConnect.sqlConnection());
            command.Parameters.AddWithValue("@p1", richTextBoxCamp.Text);
            command.Parameters.AddWithValue("@p2", txtCampID.Text);
            command.ExecuteNonQuery();
            sqlConnect.sqlConnection().Close();
            MessageBox.Show("Your campaign has been updated!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            campaignList();
        }

        private void simpleButton10_Click(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand("Delete from Tbl_Campaigns where ID=@p1", sqlConnect.sqlConnection());
            command.Parameters.AddWithValue("@p1", txtCampID.Text);
            command.ExecuteNonQuery();
            sqlConnect.sqlConnection().Close();
            MessageBox.Show("Your campaign has been deleted!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            campaignList();
        }

        private void gridView11_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dataRow = gridView11.GetDataRow(gridView11.FocusedRowHandle);
            txtPersID.Text = dataRow["ID"].ToString();
            txtPersName.Text = dataRow["Name"].ToString();
            txtPersSurname.Text = dataRow["Surname"].ToString();
            mskPerPhone.Text = dataRow["PhoneNumber"].ToString();
            mskPerTC.Text = dataRow["SocialSecurityID"].ToString();
            txtPersEmail.Text = dataRow["Email"].ToString();
            dateTimePickerHireDate.Text = dataRow["HireDate"].ToString();
            txtPersUsername.Text = dataRow["Username"].ToString();
            txtPersPassword.Text = dataRow["Password"].ToString();
            textEditSalary.Text = dataRow["CurrentSalary"].ToString();
            if (dataRow["DepartmentID"].ToString()=="1")
            {
                comboBoxDepartment.Text = "Marketing";
            }
            if (dataRow["DepartmentID"].ToString() == "3")
            {
                comboBoxDepartment.Text = "Purchase";
            }
            if (dataRow["DepartmentID"].ToString() == "4")
            {
                comboBoxDepartment.Text = "Accounting ";
            }
            if (dataRow["DepartmentID"].ToString() == "5")
            {
                comboBoxDepartment.Text = "Management";
            }
            if (dataRow["isActive"].ToString() == "True")
            {
                textEditisActive.Text = "True";
            }
            if (dataRow["isActive"].ToString() == "False")
            {
                textEditisActive.Text = "False";
            }


        }

        private void btnPersList_Click(object sender, EventArgs e)
        {
            personnelList();
        }

        private void btnAddPers_Click(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand("insert into Tbl_Personnel (Name,Surname,PhoneNumber,SocialSecurityID,Email,HireDate,Username,Password,DepartmentID,isActive,CurrentSalary) values(@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10,@p11,@p12)", sqlConnect.sqlConnection());
            command.Parameters.AddWithValue("@p1",txtPersName.Text);
            command.Parameters.AddWithValue("@p2",txtPersSurname.Text);
            command.Parameters.AddWithValue("@p3",mskPerPhone.Text);
            command.Parameters.AddWithValue("@p4",mskPerTC.Text);
            command.Parameters.AddWithValue("@p5",txtPersEmail.Text);
            command.Parameters.AddWithValue("@p7",Convert.ToDateTime(dateTimePickerHireDate.Text));
            command.Parameters.AddWithValue("@p8",txtPersUsername.Text);
            command.Parameters.AddWithValue("@p9",txtPersPassword.Text);
            command.Parameters.AddWithValue("@p10",comboBoxDepartment.SelectedValue);
            command.Parameters.AddWithValue("@p11",textEditisActive.Text);
            command.Parameters.AddWithValue("@p12", Convert.ToDecimal(textEditSalary.Text));
            command.ExecuteNonQuery();
            sqlConnect.sqlConnection().Close();
            MessageBox.Show("Personnel has been added!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnPersUpdate_Click(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand("update Tbl_Personnel set CurrentSalary=@p9,PhoneNumber=@p1,Email=@p2,Username=@p4,Password=@p5,DepartmentID=@p6,isActive=@p7 where ID=@p8", sqlConnect.sqlConnection());
            command.Parameters.AddWithValue("@p1", mskPerPhone.Text);
            command.Parameters.AddWithValue("@p2", txtPersEmail.Text);
            command.Parameters.AddWithValue("@p4", txtPersUsername.Text);
            command.Parameters.AddWithValue("@p5", txtPersPassword.Text);
            command.Parameters.AddWithValue("@p6", comboBoxDepartment.SelectedValue);
            command.Parameters.AddWithValue("@p7", textEditisActive.Text);
            command.Parameters.AddWithValue("@p8", txtPersID.Text);
            command.Parameters.AddWithValue("@p9", Convert.ToDecimal(textEditSalary.Text));
            command.ExecuteNonQuery();
            sqlConnect.sqlConnection().Close();
            MessageBox.Show("Personnel has been updated!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void simpleButton3_Click(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand("update Tbl_Personnel set isActive=@p1 where ID=@p2", sqlConnect.sqlConnection());
            command.Parameters.AddWithValue("@p1", "False");
            command.Parameters.AddWithValue("@p2", txtPersID.Text);
            command.ExecuteNonQuery();
            sqlConnect.sqlConnection().Close();
            MessageBox.Show("Personnel has been fired!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void simpleButtonSlarySta_Click(object sender, EventArgs e)
        {
            chartControl1.Series["Months"].Points.Clear();
            SqlCommand command = new SqlCommand("select TotalSalary,Month,ID from Tbl_Expense where Year=@p1 order by ID", sqlConnect.sqlConnection());
            command.Parameters.AddWithValue("@p1", comboBoxYears.Text);
            SqlDataReader dr = command.ExecuteReader();
            while (dr.Read())
            {
                chartControl1.Series["Months"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr[1], dr[0]));
            }
            sqlConnect.sqlConnection().Close();
        }

        private void simpleButtonPurchaseSta_Click(object sender, EventArgs e)
        {
            chartControl1.Series["Months"].Points.Clear();
            SqlCommand command = new SqlCommand("select TotalPurchase,Month,ID from Tbl_Expense where Year=@p1 order by ID", sqlConnect.sqlConnection());
            command.Parameters.AddWithValue("@p1", comboBoxYears.Text);
            SqlDataReader dr = command.ExecuteReader();
            while (dr.Read())
            {
                chartControl1.Series["Months"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr[1], dr[0]));
            }
            sqlConnect.sqlConnection().Close();
        }

        private void simpleButtonbillPriceSta_Click(object sender, EventArgs e)
        {
            chartControl1.Series["Months"].Points.Clear();
            SqlCommand command = new SqlCommand("select TotalBillPrice,Month,ID from Tbl_Expense where Year=@p1 order by ID", sqlConnect.sqlConnection());
            command.Parameters.AddWithValue("@p1", comboBoxYears.Text);
            SqlDataReader dr = command.ExecuteReader();
            while (dr.Read())
            {
                chartControl1.Series["Months"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr[1], dr[0]));
            }
            sqlConnect.sqlConnection().Close();
        }

        private void simpleButton15_Click(object sender, EventArgs e)
        {
            chartControl1.Series["Months"].Points.Clear();
            SqlCommand command = new SqlCommand("select Total,Month,ID from Tbl_Expense where Year=@p1 order by ID", sqlConnect.sqlConnection());
            command.Parameters.AddWithValue("@p1", comboBoxYears.Text);
            SqlDataReader dr = command.ExecuteReader();
            while (dr.Read())
            {
                chartControl1.Series["Months"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr[1], dr[0]));
            }
            sqlConnect.sqlConnection().Close();
        }

        private void simpleButtonSaleIncomes_Click(object sender, EventArgs e)
        {
            chartControl1.Series["Months"].Points.Clear();
            SqlCommand command = new SqlCommand("select sum(SalePrice) as 'Total Car Sale',Tbl_Income.Month,IncomeID from Tbl_Sale inner join Tbl_Income on Tbl_Sale.IncomeID = Tbl_Income.ID where Year = @p1 group by Tbl_Income.Month, IncomeID order by IncomeID", sqlConnect.sqlConnection());
            command.Parameters.AddWithValue("@p1", comboBoxYears.Text);
            SqlDataReader dr = command.ExecuteReader();
            while (dr.Read())
            {
                chartControl1.Series["Months"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr[1], dr[0]));
            }
            sqlConnect.sqlConnection().Close();
        }

        private void simpleButtonRentIncomes_Click(object sender, EventArgs e)
        {
            chartControl1.Series["Months"].Points.Clear();
            SqlCommand command1 = new SqlCommand("select sum(RentPrice) as 'Total Car Sale',Tbl_Income.Month,IncomeID from Tbl_Rent inner join Tbl_Income on Tbl_Rent.IncomeID = Tbl_Income.ID where Year = @p1 group by Tbl_Income.Month, IncomeID order by IncomeID", sqlConnect.sqlConnection());
            command1.Parameters.AddWithValue("@p1", comboBoxYears.Text);
            SqlDataReader dr1 = command1.ExecuteReader();
            while (dr1.Read())
            {
                chartControl1.Series["Months"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr1[1], dr1[0]));
            }
            sqlConnect.sqlConnection().Close();
        }

        private void simpleButtonTotalIncomes_Click(object sender, EventArgs e)
        {
            chartControl1.Series["Months"].Points.Clear();
            SqlCommand command1 = new SqlCommand("select Total,Month from Tbl_Income where Year = @p1 order by ID", sqlConnect.sqlConnection());
            command1.Parameters.AddWithValue("@p1", comboBoxYears.Text);
            SqlDataReader dr1 = command1.ExecuteReader();
            while (dr1.Read())
            {
                chartControl1.Series["Months"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr1[1], dr1[0]));
            }
            sqlConnect.sqlConnection().Close();
        }
               
        private void simpleButtonSaleSta_Click(object sender, EventArgs e)
        {
            chartControl1.Series["Months"].Points.Clear();
            SqlCommand command = new SqlCommand("select COUNT(CarID) as 'Total Car Sale',Tbl_Income.Month,IncomeID from Tbl_Sale inner join Tbl_Income on Tbl_Sale.IncomeID = Tbl_Income.ID where PersonnelID = @p1 and Year = @p2 group by Tbl_Income.Month, IncomeID order by IncomeID", sqlConnect.sqlConnection());
            command.Parameters.AddWithValue("@p1", comboBoxPersonnel.SelectedValue);
            command.Parameters.AddWithValue("@p2", comboBoxYears.Text);
            SqlDataReader dr = command.ExecuteReader();
            while (dr.Read())
            {
                chartControl1.Series["Months"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr[1], dr[0]));
            }
            sqlConnect.sqlConnection().Close();
        }

        private void simpleButtonRentalSta_Click(object sender, EventArgs e)
        {
            chartControl1.Series["Months"].Points.Clear();
            SqlCommand command1 = new SqlCommand("select COUNT(CarID) as 'Total Car Sale',Tbl_Income.Month,IncomeID from Tbl_Rent inner join Tbl_Income on Tbl_Rent.IncomeID = Tbl_Income.ID where PersonnelID = @p1 and Year = @p2 group by Tbl_Income.Month, IncomeID order by IncomeID", sqlConnect.sqlConnection());
            command1.Parameters.AddWithValue("@p1", comboBoxPersonnel.SelectedValue);
            command1.Parameters.AddWithValue("@p2", comboBoxYears.Text);
            SqlDataReader dr1 = command1.ExecuteReader();
            while (dr1.Read())
            {
                chartControl1.Series["Months"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr1[1], dr1[0]));
            }
            sqlConnect.sqlConnection().Close();
        }

        private void simpleButton17_Click(object sender, EventArgs e)
        {
            chartControl1.Series["Months"].Points.Clear();
            SqlCommand command1 = new SqlCommand("select Sum(SalePrice) as 'Total Car Sale Price',Tbl_Income.Month,IncomeID from Tbl_Sale inner join Tbl_Income on Tbl_Sale.IncomeID = Tbl_Income.ID where PersonnelID = @p1 and Year=@p2 group by Tbl_Income.Month, IncomeID order by IncomeID", sqlConnect.sqlConnection());
            command1.Parameters.AddWithValue("@p1", comboBoxPersonnel.SelectedValue);
            command1.Parameters.AddWithValue("@p2", comboBoxYears.Text);
            SqlDataReader dr1 = command1.ExecuteReader();
            while (dr1.Read())
            {
                chartControl1.Series["Months"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr1[1], dr1[0]));
            }
            sqlConnect.sqlConnection().Close();
        }

        private void simpleButton16_Click(object sender, EventArgs e)
        {
            chartControl1.Series["Months"].Points.Clear();
            SqlCommand command1 = new SqlCommand("select Sum(RentPrice) as 'Total Car Rent Price',Tbl_Income.Month,IncomeID from Tbl_Rent inner join Tbl_Income on Tbl_Rent.IncomeID = Tbl_Income.ID  where PersonnelID = @p1 and Year=@p2 group by Tbl_Income.Month, IncomeID order by IncomeID", sqlConnect.sqlConnection());
            command1.Parameters.AddWithValue("@p1", comboBoxPersonnel.SelectedValue);
            command1.Parameters.AddWithValue("@p2", comboBoxYears.Text);
            SqlDataReader dr1 = command1.ExecuteReader();
            while (dr1.Read())
            {
                chartControl1.Series["Months"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr1[1], dr1[0]));
            }
            sqlConnect.sqlConnection().Close();
        }

        private void gridView10_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dataRow = gridView10.GetDataRow(gridView10.FocusedRowHandle);
            txtBankName.Text = dataRow["BankName"].ToString();
            txtBankAccountType.Text = dataRow["AccountType"].ToString();
            txtBankAccountID.Text = dataRow["AccountID"].ToString();
            txtBankWalletValue.Text = dataRow["Wallet"].ToString();
            txtBankID.Text = dataRow["ID"].ToString();
            textEditAccountIDCashBox.Text = dataRow["AccountID"].ToString();
            textEditbanknameCashBox.Text = dataRow["BankName"].ToString();
            textEditaccountypeCashBox.Text = dataRow["AccountType"].ToString();
        }

        private void btnBankList_Click(object sender, EventArgs e)
        {
            bankAccounts();            
        }

        private void btnBankAdd_Click(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand("insert into Tbl_BankAccount (BankName,AccountType,AccountID,Wallet) values (@p1,@p2,@p3,@p4)", sqlConnect.sqlConnection());
            command.Parameters.AddWithValue("@p1", txtBankName.Text);
            command.Parameters.AddWithValue("@p2", txtBankAccountType.Text);
            command.Parameters.AddWithValue("@p3", txtBankAccountID.Text);
            command.Parameters.AddWithValue("@p4", Convert.ToDecimal(txtBankWalletValue.Text));
            command.ExecuteNonQuery();
            sqlConnect.sqlConnection().Close();
            MessageBox.Show("The bank account has been added.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnBankUpdate_Click(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand("update Tbl_BankAccount set BankName=@p1,AccountType=@p2,AccountID=@p3,Wallet=@p4 where ID=@p5", sqlConnect.sqlConnection());
            command.Parameters.AddWithValue("@p1", txtBankName.Text);
            command.Parameters.AddWithValue("@p2", txtBankAccountType.Text);
            command.Parameters.AddWithValue("@p3", txtBankAccountID.Text);
            command.Parameters.AddWithValue("@p4", Convert.ToDecimal(txtBankWalletValue.Text));
            command.Parameters.AddWithValue("@p5", txtBankID.Text);
            command.ExecuteNonQuery();
            sqlConnect.sqlConnection().Close();
            MessageBox.Show("The bank account has been updated.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnBankDelete_Click(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand("Delete from Tbl_BankAccount where ID=@p1", sqlConnect.sqlConnection());
            command.Parameters.AddWithValue("@p1", txtBankID.Text);
            command.ExecuteNonQuery();
            sqlConnect.sqlConnection().Close();
            MessageBox.Show("The bank account has been deleted.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void simpleButton19_Click(object sender, EventArgs e)
        {
            if (Convert.ToDecimal(lblMoney.Text) < Convert.ToDecimal(textEditValueCashBox.Text))
            {
                MessageBox.Show("Money can not transfer to Bank", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            else
            {
                SqlCommand command = new SqlCommand("update Tbl_BankAccount set Wallet=@p1+(select Wallet from Tbl_BankAccount  where ID=@p2) where ID=@p3", sqlConnect.sqlConnection());
                command.Parameters.AddWithValue("@p3", txtBankID.Text);
                command.Parameters.AddWithValue("@p1", Convert.ToDecimal(textEditValueCashBox.Text));
                command.Parameters.AddWithValue("@p2", txtBankID.Text);
                command.ExecuteNonQuery();
                sqlConnect.sqlConnection().Close();
                updateCashRegister();
                MessageBox.Show("Money transfered to Bank", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void simpleButton18_Click(object sender, EventArgs e)
        {
            decimal walletValue = 0;
            SqlCommand command1 = new SqlCommand("select Wallet from Tbl_BankAccount where ID=@p1", sqlConnect.sqlConnection());
            command1.Parameters.AddWithValue("@p1", txtBankID.Text);
            SqlDataReader dr = command1.ExecuteReader();
            while (dr.Read())
            {
                walletValue = decimal.Parse(dr[0].ToString());
            }
            sqlConnect.sqlConnection().Close();


            if (Convert.ToDecimal(textEditValueCashBox.Text) > walletValue)
            {
                MessageBox.Show("Money can not transfer to Cash Box", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                SqlCommand command = new SqlCommand("update Tbl_BankAccount set Wallet=(select Wallet from Tbl_BankAccount  where ID=@p2)-@p1 where ID=@p3", sqlConnect.sqlConnection());
                command.Parameters.AddWithValue("@p3", txtBankID.Text);
                command.Parameters.AddWithValue("@p1", Convert.ToDecimal(textEditValueCashBox.Text));
                command.Parameters.AddWithValue("@p2", txtBankID.Text);
                command.ExecuteNonQuery();
                sqlConnect.sqlConnection().Close();
                updateCashRegister();
                MessageBox.Show("Money transfered to Cash Box", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void butonPaySalary_Click(object sender, EventArgs e)
        {
            if (decimal.Parse(lblMoney.Text) <= 0)
            {
                MessageBox.Show("No momey","Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else 
            {

                int ExpenseID = 0;
                DateTime dateTime = DateTime.Now;
                int Month = dateTime.Month;
                int Year = dateTime.Year;
                SqlCommand command3 = new SqlCommand("select  Tbl_Expense.ID from Tbl_Expense where Month+''+Year =@p1", sqlConnect.sqlConnection());
                command3.Parameters.AddWithValue("@p1", Month + "" + Year);
                SqlDataReader dr3 = command3.ExecuteReader();
                while (dr3.Read())
                {
                    ExpenseID = int.Parse(dr3[0].ToString());
                }

                SqlCommand command4 = new SqlCommand("update Tbl_Expense set TotalSalary=TotalSalary+@p1 where ID=@p2", sqlConnect.sqlConnection());
                command4.Parameters.AddWithValue("@p1", Convert.ToDecimal(textEditSalary.Text));
                command4.Parameters.AddWithValue("@p2", ExpenseID);
                command4.ExecuteNonQuery();
                sqlConnect.sqlConnection().Close();

                SqlCommand command5 = new SqlCommand("update Tbl_Expense set Total=Total+@p1 where ID=@p2", sqlConnect.sqlConnection());
                command5.Parameters.AddWithValue("@p1", Convert.ToDecimal(textEditSalary.Text));
                command5.Parameters.AddWithValue("@p2", ExpenseID);
                command5.ExecuteNonQuery();
                sqlConnect.sqlConnection().Close();

                SqlCommand command6 = new SqlCommand("insert into Tbl_Salary (Month,Year,Salary,PersonnelID,ExpenseID) values (@p1,@p2,@p3,@p4,@p5)",sqlConnect.sqlConnection());
                command6.Parameters.AddWithValue("@p1", Month);
                command6.Parameters.AddWithValue("@p2", Year);
                command6.Parameters.AddWithValue("@p3", Convert.ToDecimal(textEditSalary.Text));
                command6.Parameters.AddWithValue("@p4", textEditIDPersonnel.Text);
                command6.Parameters.AddWithValue("@p5", ExpenseID);
                command6.ExecuteNonQuery();
                sqlConnect.sqlConnection().Close();

                MessageBox.Show("Salary has been pay..", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}