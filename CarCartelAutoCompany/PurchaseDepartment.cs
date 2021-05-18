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
    public partial class PurchaseDepartment : DevExpress.XtraEditors.XtraForm
    {
        public PurchaseDepartment()
        {
            InitializeComponent();
        }
        private void tileBar_SelectedItemChanged(object sender, TileItemEventArgs e)
        {
            navigationFrame.SelectedPageIndex = tileBarGroupTables.Items.IndexOf(e.Item);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            labelTime.Text = DateTime.Now.ToLongTimeString();
            labelDate.Text = DateTime.Now.ToLongDateString();
        }

        SqlConnect sqlConnect = new SqlConnect();
        LoginScreen loginScreen = new LoginScreen();
        public string perID = "";
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
        void stockList()
        {
            SqlCommand command = new SqlCommand("select Tbl_CarList.ID,CarID,Model+' '+Brand AS 'Car',CarBodyStyle,Statu,Number,Price2 from Tbl_CarList inner join Tbl_Car on Tbl_CarList.CarID= Tbl_Car.ID", sqlConnect.sqlConnection());
            SqlDataAdapter da = new SqlDataAdapter(command);
            DataTable dt = new DataTable();
            da.Fill(dt);
            gridControlStock.DataSource = dt;
            sqlConnect.sqlConnection().Close();
        }
        void carList()
        {
            SqlCommand command = new SqlCommand("select ID,Brand,Model,Class,Year,Price,FuelType,Color,GearType,HorsePower,MotorVolume,NumberOfDoors,Capacity,KM,LicensePlate,VehicleStatus,Price2 from Tbl_Car", sqlConnect.sqlConnection());
            SqlDataAdapter da = new SqlDataAdapter(command);
            DataTable dt = new DataTable();
            da.Fill(dt);
            gridControlCarList.DataSource = dt;
            sqlConnect.sqlConnection().Close();
        }
        private void PurchaseDepartment_Load(object sender, EventArgs e)
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
            stockList();
            carList();
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

        private void gridView4_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dataRow = gridView4.GetDataRow(gridView4.FocusedRowHandle);
            comboBoxReceiverMEssage.Text = dataRow["Receiver"].ToString();
            txtSubjectMessage.Text = dataRow["MessageSubject"].ToString();
            richTextBoxMsgContent.Text = dataRow["MessageContent"].ToString();
        }

        private void gridView12_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dataRow = gridView12.GetDataRow(gridView12.FocusedRowHandle);
            comboBoxReceiverMEssage.Text = dataRow["Sender"].ToString();
            txtSubjectMessage.Text = dataRow["MessageSubject"].ToString();
            richTextBoxMsgContent.Text = dataRow["MessageContent"].ToString();
        }

        private void btnListCarOrder_Click(object sender, EventArgs e)
        {
            carList();
        }

        private void gridView5_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dataRow = gridView5.GetDataRow(gridView5.FocusedRowHandle);
            txtCarID.Text = dataRow["ID"].ToString();
            txtBrand.Text = dataRow["Brand"].ToString();
            txtCarModel.Text = dataRow["Model"].ToString();
            txtClass.Text = dataRow["Class"].ToString();
            txtCarYear.Text = dataRow["Year"].ToString();
            txtCarPrice.Text = dataRow["Price"].ToString();
            txtCarPrice2.Text = dataRow["Price2"].ToString();
            cmbFuelType.Text = dataRow["FuelType"].ToString();
            cbmGearType.Text = dataRow["GearType"].ToString();
            txtCarHorsePower.Text = dataRow["HorsePower"].ToString();
            MotorVolume.Text = dataRow["MotorVolume"].ToString();
            txtCarDoors.Text = dataRow["NumberofDoors"].ToString();
            txtCarCapacity.Text = dataRow["Capacity"].ToString();
            txtCarKM.Text = dataRow["KM"].ToString();
            txtCarLicensePlate.Text = dataRow["LicensePlate"].ToString();
            cmbCarStatus.Text = dataRow["VehicleStatus"].ToString();


        }

        private void btnCarOrderClear_Click(object sender, EventArgs e)
        {
            txtCarID.Text = "";
            txtBrand.Text = "";
            txtCarModel.Text = "";
            txtClass.Text = "";
            txtCarYear.Text = "";
            txtCarPrice.Text = "";
            txtCarPrice2.Text = "";
            cmbFuelType.Text = "";
            cbmGearType.Text = "";
            txtCarHorsePower.Text = "";
            MotorVolume.Text = "";
            txtCarDoors.Text = "";
            txtCarCapacity.Text = "";
            txtCarKM.Text = "";
            txtCarLicensePlate.Text = "";
            cmbCarStatus.Text = "";
        }

        private void btnCarSave_Click(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand("insert into Tbl_Car (Brand,Model,Class,Year,Price,Price2,FuelType,GearType,HorsePower,MotorVolume,NumberofDoors,Capacity,KM,LicensePlate,VehicleStatus) values (@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10,@p11,@p12,@p13,@p14,@p15)", sqlConnect.sqlConnection());
            command.Parameters.AddWithValue("@p1", txtBrand.Text);
            command.Parameters.AddWithValue("@p2", txtCarModel.Text);
            command.Parameters.AddWithValue("@p3", txtClass.Text);
            command.Parameters.AddWithValue("@p4", txtCarYear.Text);
            command.Parameters.AddWithValue("@p5", Convert.ToDecimal(txtCarPrice.Text));
            command.Parameters.AddWithValue("@p6", Convert.ToDecimal(txtCarPrice2.Text));
            command.Parameters.AddWithValue("@p7", cmbFuelType.Text);
            command.Parameters.AddWithValue("@p8", cbmGearType.Text);
            command.Parameters.AddWithValue("@p9", int.Parse(txtCarHorsePower.Text));
            command.Parameters.AddWithValue("@p10", int.Parse(MotorVolume.Text));
            command.Parameters.AddWithValue("@p11", int.Parse(txtCarDoors.Text));
            command.Parameters.AddWithValue("@p12", int.Parse(txtCarCapacity.Text));
            command.Parameters.AddWithValue("@p13", int.Parse(txtCarKM.Text));
            command.Parameters.AddWithValue("@p14", txtCarLicensePlate.Text);
            command.Parameters.AddWithValue("@p15", cmbCarStatus.Text);
            command.ExecuteNonQuery();
            sqlConnect.sqlConnection().Close();
            MessageBox.Show("New Car information has been added!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

            int carID = 0;
            string carStatu = null;
            SqlCommand command1 = new SqlCommand("SELECT top 1 ID,VehicleStatus from Tbl_Car order by ID desc", sqlConnect.sqlConnection());
            SqlDataReader dr = command1.ExecuteReader();
            while (dr.Read())
            {
                carID = int.Parse(dr[0].ToString());
                carStatu = dr[1].ToString();
               

            }
            SqlCommand command2 = new SqlCommand("insert into Tbl_CarList (CarID,Statu) values (@p1,@p2) ", sqlConnect.sqlConnection());
            command2.Parameters.AddWithValue("@p1", carID);
            command2.Parameters.AddWithValue("@p2", carStatu);
            command2.ExecuteNonQuery();

            sqlConnect.sqlConnection().Close();

        }

        private void btnUpdateCar_Click(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand("update Tbl_Car set Brand=@p1,Model=@p2,Class=@p3,Year=@p4,Price=@p5,Price2=@p6,FuelType=@p7,GearType=@p8,HorsePower=@p9,MotorVolume=@p10,NumberofDoors=@p11,Capacity=@p12,KM=@p13,LicensePlate=@p14,VehicleStatus=@p15 where ID=@p16", sqlConnect.sqlConnection());
            command.Parameters.AddWithValue("@p1", txtBrand.Text);
            command.Parameters.AddWithValue("@p2", txtCarModel.Text);
            command.Parameters.AddWithValue("@p3", txtClass.Text);
            command.Parameters.AddWithValue("@p4", txtCarYear.Text);
            command.Parameters.AddWithValue("@p5", Convert.ToDecimal(txtCarPrice.Text));
            command.Parameters.AddWithValue("@p6", Convert.ToDecimal(txtCarPrice.Text));
            command.Parameters.AddWithValue("@p7", cmbFuelType.Text);
            command.Parameters.AddWithValue("@p8", cbmGearType.Text);
            command.Parameters.AddWithValue("@p9", int.Parse(txtCarHorsePower.Text));
            command.Parameters.AddWithValue("@p10", int.Parse(MotorVolume.Text));
            command.Parameters.AddWithValue("@p11", int.Parse(txtCarDoors.Text));
            command.Parameters.AddWithValue("@p12", int.Parse(txtCarCapacity.Text));
            command.Parameters.AddWithValue("@p13", int.Parse(txtCarKM.Text));
            command.Parameters.AddWithValue("@p14", txtCarLicensePlate.Text);
            command.Parameters.AddWithValue("@p15", cmbCarStatus.Text);
            command.Parameters.AddWithValue("@p16", txtCarID.Text);
            command.ExecuteNonQuery();
            sqlConnect.sqlConnection().Close();
            MessageBox.Show("Car information has been updated!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

          

        }

        private void tileBarItem2_ItemClick(object sender, TileItemEventArgs e)
        {
            stockList();
        }  

        private void gridView3_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dataRow = gridView3.GetDataRow(gridView3.FocusedRowHandle);
            txtCarListID.Text = dataRow["ID"].ToString();
            txtCarListModel.Text = dataRow["Car"].ToString();
            txtBodyStyle.Text = dataRow["CarBodyStyle"].ToString();
            txtCarStatu.Text = dataRow["Statu"].ToString();
            textEdit3.Text = dataRow["Number"].ToString();
            lblCarPrice.Text = dataRow["Price2"].ToString();
            lblCarId.Text= dataRow["CarID"].ToString();
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            stockList();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            txtCarListID.Text = "";
            txtCarListModel.Text = "";
            txtBodyStyle.Text = "";
            txtCarStatu.Text = "";
            textEdit3.Text = "";
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            int carCount = 0;
            SqlCommand command1 = new SqlCommand("select ISNULL(Number,0) from Tbl_CarList where ID=@p1", sqlConnect.sqlConnection());
            command1.Parameters.AddWithValue("@p1", txtCarListID.Text);
            SqlDataReader dr = command1.ExecuteReader();
            while (dr.Read())
            {
                carCount=int.Parse(dr[0].ToString());
            }
            sqlConnect.sqlConnection().Close();
            SqlCommand command = new SqlCommand("update Tbl_CarList set CarBodyStyle=@p1,Number=@p2 where ID=@p3", sqlConnect.sqlConnection());
            command.Parameters.AddWithValue("@p1", txtBodyStyle.Text);
            command.Parameters.AddWithValue("@p2", int.Parse(textEdit3.Text)+carCount);
            command.Parameters.AddWithValue("@p3", txtCarListID.Text);
            command.ExecuteNonQuery();
            sqlConnect.sqlConnection().Close();
            MessageBox.Show("Car stock information has been updated!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            
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

            SqlCommand command2 = new SqlCommand("insert into Tbl_Purchase (Date,CarID,CarPrice,PersonnelID,ExpenseID) values (@p1,@p2,@p3,@p4,@p5)", sqlConnect.sqlConnection());
            command2.Parameters.AddWithValue("@p1", Convert.ToDateTime(labelDate.Text));
            command2.Parameters.AddWithValue("@p2", lblCarId.Text);
            command2.Parameters.AddWithValue("@p3", decimal.Parse(lblCarPrice.Text)*decimal.Parse(textEdit3.Text));
            command2.Parameters.AddWithValue("@p4", textEditIDPersonnel.Text);
            command2.Parameters.AddWithValue("@p5", ExpenseID);
            command2.ExecuteNonQuery();
            sqlConnect.sqlConnection().Close();

            SqlCommand command4 = new SqlCommand("update Tbl_Expense set TotalPurchase=TotalPurchase+@p1 where ID=@p2", sqlConnect.sqlConnection());
            command4.Parameters.AddWithValue("@p1", decimal.Parse(lblCarPrice.Text) * decimal.Parse(textEdit3.Text));
            command4.Parameters.AddWithValue("@p2", ExpenseID);
            command4.ExecuteNonQuery();
            sqlConnect.sqlConnection().Close();

            SqlCommand command5 = new SqlCommand("update Tbl_Expense set Total=Total+@p1 where ID=@p2", sqlConnect.sqlConnection());
            command5.Parameters.AddWithValue("@p1", decimal.Parse(lblCarPrice.Text) * decimal.Parse(textEdit3.Text));
            command5.Parameters.AddWithValue("@p2", ExpenseID);
            command5.ExecuteNonQuery();
            sqlConnect.sqlConnection().Close();
        }
    }
}