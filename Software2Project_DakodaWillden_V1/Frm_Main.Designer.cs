namespace Software2Project_DakodaWillden_V1
{
    partial class Frm_Main
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.Lbl_Username = new System.Windows.Forms.Label();
            this.Txt_Username = new System.Windows.Forms.TextBox();
            this.Txt_Password = new System.Windows.Forms.TextBox();
            this.Lbl_Password = new System.Windows.Forms.Label();
            this.Btn_Login = new System.Windows.Forms.Button();
            this.Btn_LogOut = new System.Windows.Forms.Button();
            this.dataGridViewCustomers = new System.Windows.Forms.DataGridView();
            this.Btn_AddCustomer = new System.Windows.Forms.Button();
            this.Btn_UpdateCustInfo = new System.Windows.Forms.Button();
            this.Btn_DeleteCustomer = new System.Windows.Forms.Button();
            this.Btn_Exit = new System.Windows.Forms.Button();
            this.dataGridView_UserAppointments = new System.Windows.Forms.DataGridView();
            this.Btn_DeleteAppointment = new System.Windows.Forms.Button();
            this.Btn_UpdateAppointment = new System.Windows.Forms.Button();
            this.Btn_AddAppointment = new System.Windows.Forms.Button();
            this.Btn_NewUser = new System.Windows.Forms.Button();
            this.Lbl_Clock = new System.Windows.Forms.Label();
            this.Btn_Reports = new System.Windows.Forms.Button();
            this.Lbl_Loading = new System.Windows.Forms.Label();
            this.monthCalendar_Main = new System.Windows.Forms.MonthCalendar();
            this.groupBox_AppointmentView = new System.Windows.Forms.GroupBox();
            this.RBtn_WeekView = new System.Windows.Forms.RadioButton();
            this.RBtn_CalendarView = new System.Windows.Forms.RadioButton();
            this.groupBox_Customers = new System.Windows.Forms.GroupBox();
            this.progressBar_Main = new System.Windows.Forms.ProgressBar();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewCustomers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_UserAppointments)).BeginInit();
            this.groupBox_AppointmentView.SuspendLayout();
            this.groupBox_Customers.SuspendLayout();
            this.SuspendLayout();
            // 
            // Lbl_Username
            // 
            this.Lbl_Username.AutoSize = true;
            this.Lbl_Username.Location = new System.Drawing.Point(28, 73);
            this.Lbl_Username.Name = "Lbl_Username";
            this.Lbl_Username.Size = new System.Drawing.Size(110, 25);
            this.Lbl_Username.TabIndex = 0;
            this.Lbl_Username.Text = "Username";
            // 
            // Txt_Username
            // 
            this.Txt_Username.Location = new System.Drawing.Point(159, 70);
            this.Txt_Username.Name = "Txt_Username";
            this.Txt_Username.Size = new System.Drawing.Size(180, 31);
            this.Txt_Username.TabIndex = 1;
            // 
            // Txt_Password
            // 
            this.Txt_Password.Location = new System.Drawing.Point(159, 111);
            this.Txt_Password.Name = "Txt_Password";
            this.Txt_Password.PasswordChar = '*';
            this.Txt_Password.Size = new System.Drawing.Size(180, 31);
            this.Txt_Password.TabIndex = 2;
            this.Txt_Password.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Txt_Password_KeyDown);
            // 
            // Lbl_Password
            // 
            this.Lbl_Password.AutoSize = true;
            this.Lbl_Password.Location = new System.Drawing.Point(28, 114);
            this.Lbl_Password.Name = "Lbl_Password";
            this.Lbl_Password.Size = new System.Drawing.Size(106, 25);
            this.Lbl_Password.TabIndex = 2;
            this.Lbl_Password.Text = "Password";
            // 
            // Btn_Login
            // 
            this.Btn_Login.Location = new System.Drawing.Point(36, 144);
            this.Btn_Login.Name = "Btn_Login";
            this.Btn_Login.Size = new System.Drawing.Size(102, 41);
            this.Btn_Login.TabIndex = 4;
            this.Btn_Login.Text = "Log In";
            this.Btn_Login.UseVisualStyleBackColor = true;
            this.Btn_Login.Click += new System.EventHandler(this.Btn_Login_Click);
            // 
            // Btn_LogOut
            // 
            this.Btn_LogOut.Location = new System.Drawing.Point(29, 144);
            this.Btn_LogOut.Name = "Btn_LogOut";
            this.Btn_LogOut.Size = new System.Drawing.Size(116, 41);
            this.Btn_LogOut.TabIndex = 5;
            this.Btn_LogOut.Text = "Log Out";
            this.Btn_LogOut.UseVisualStyleBackColor = true;
            this.Btn_LogOut.Visible = false;
            this.Btn_LogOut.Click += new System.EventHandler(this.Btn_LogOut_Click);
            // 
            // dataGridViewCustomers
            // 
            this.dataGridViewCustomers.AllowUserToAddRows = false;
            this.dataGridViewCustomers.AllowUserToDeleteRows = false;
            this.dataGridViewCustomers.AllowUserToResizeColumns = false;
            this.dataGridViewCustomers.AllowUserToResizeRows = false;
            this.dataGridViewCustomers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewCustomers.Location = new System.Drawing.Point(98, 37);
            this.dataGridViewCustomers.MultiSelect = false;
            this.dataGridViewCustomers.Name = "dataGridViewCustomers";
            this.dataGridViewCustomers.RowTemplate.Height = 33;
            this.dataGridViewCustomers.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dataGridViewCustomers.Size = new System.Drawing.Size(615, 353);
            this.dataGridViewCustomers.TabIndex = 6;
            this.dataGridViewCustomers.Visible = false;
            // 
            // Btn_AddCustomer
            // 
            this.Btn_AddCustomer.Location = new System.Drawing.Point(722, 37);
            this.Btn_AddCustomer.Name = "Btn_AddCustomer";
            this.Btn_AddCustomer.Size = new System.Drawing.Size(112, 45);
            this.Btn_AddCustomer.TabIndex = 7;
            this.Btn_AddCustomer.Text = "Add";
            this.Btn_AddCustomer.UseVisualStyleBackColor = true;
            this.Btn_AddCustomer.Visible = false;
            this.Btn_AddCustomer.Click += new System.EventHandler(this.Btn_AddCustomer_Click);
            // 
            // Btn_UpdateCustInfo
            // 
            this.Btn_UpdateCustInfo.Location = new System.Drawing.Point(722, 88);
            this.Btn_UpdateCustInfo.Name = "Btn_UpdateCustInfo";
            this.Btn_UpdateCustInfo.Size = new System.Drawing.Size(112, 45);
            this.Btn_UpdateCustInfo.TabIndex = 8;
            this.Btn_UpdateCustInfo.Text = "Update";
            this.Btn_UpdateCustInfo.UseVisualStyleBackColor = true;
            this.Btn_UpdateCustInfo.Visible = false;
            this.Btn_UpdateCustInfo.Click += new System.EventHandler(this.Btn_UpdateCustInfo_Click);
            // 
            // Btn_DeleteCustomer
            // 
            this.Btn_DeleteCustomer.Location = new System.Drawing.Point(722, 139);
            this.Btn_DeleteCustomer.Name = "Btn_DeleteCustomer";
            this.Btn_DeleteCustomer.Size = new System.Drawing.Size(112, 45);
            this.Btn_DeleteCustomer.TabIndex = 9;
            this.Btn_DeleteCustomer.Text = "Delete";
            this.Btn_DeleteCustomer.UseVisualStyleBackColor = true;
            this.Btn_DeleteCustomer.Visible = false;
            this.Btn_DeleteCustomer.Click += new System.EventHandler(this.Btn_DeleteCustomer_Click);
            // 
            // Btn_Exit
            // 
            this.Btn_Exit.Location = new System.Drawing.Point(1496, 804);
            this.Btn_Exit.Name = "Btn_Exit";
            this.Btn_Exit.Size = new System.Drawing.Size(101, 39);
            this.Btn_Exit.TabIndex = 10;
            this.Btn_Exit.Text = "Exit";
            this.Btn_Exit.UseVisualStyleBackColor = true;
            this.Btn_Exit.Click += new System.EventHandler(this.Btn_Exit_Click);
            // 
            // dataGridView_UserAppointments
            // 
            this.dataGridView_UserAppointments.AllowUserToAddRows = false;
            this.dataGridView_UserAppointments.AllowUserToDeleteRows = false;
            this.dataGridView_UserAppointments.AllowUserToResizeColumns = false;
            this.dataGridView_UserAppointments.AllowUserToResizeRows = false;
            this.dataGridView_UserAppointments.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_UserAppointments.Location = new System.Drawing.Point(508, 84);
            this.dataGridView_UserAppointments.MultiSelect = false;
            this.dataGridView_UserAppointments.Name = "dataGridView_UserAppointments";
            this.dataGridView_UserAppointments.RowTemplate.Height = 33;
            this.dataGridView_UserAppointments.Size = new System.Drawing.Size(797, 306);
            this.dataGridView_UserAppointments.TabIndex = 12;
            this.dataGridView_UserAppointments.Visible = false;
            // 
            // Btn_DeleteAppointment
            // 
            this.Btn_DeleteAppointment.Location = new System.Drawing.Point(1311, 186);
            this.Btn_DeleteAppointment.Name = "Btn_DeleteAppointment";
            this.Btn_DeleteAppointment.Size = new System.Drawing.Size(112, 45);
            this.Btn_DeleteAppointment.TabIndex = 16;
            this.Btn_DeleteAppointment.Text = "Delete";
            this.Btn_DeleteAppointment.UseVisualStyleBackColor = true;
            this.Btn_DeleteAppointment.Visible = false;
            this.Btn_DeleteAppointment.Click += new System.EventHandler(this.Btn_DeleteAppointment_Click);
            // 
            // Btn_UpdateAppointment
            // 
            this.Btn_UpdateAppointment.Location = new System.Drawing.Point(1311, 135);
            this.Btn_UpdateAppointment.Name = "Btn_UpdateAppointment";
            this.Btn_UpdateAppointment.Size = new System.Drawing.Size(112, 45);
            this.Btn_UpdateAppointment.TabIndex = 15;
            this.Btn_UpdateAppointment.Text = "Update";
            this.Btn_UpdateAppointment.UseVisualStyleBackColor = true;
            this.Btn_UpdateAppointment.Visible = false;
            this.Btn_UpdateAppointment.Click += new System.EventHandler(this.Btn_UpdateAppointment_Click);
            // 
            // Btn_AddAppointment
            // 
            this.Btn_AddAppointment.Location = new System.Drawing.Point(1311, 84);
            this.Btn_AddAppointment.Name = "Btn_AddAppointment";
            this.Btn_AddAppointment.Size = new System.Drawing.Size(112, 45);
            this.Btn_AddAppointment.TabIndex = 14;
            this.Btn_AddAppointment.Text = "Add";
            this.Btn_AddAppointment.UseVisualStyleBackColor = true;
            this.Btn_AddAppointment.Visible = false;
            this.Btn_AddAppointment.Click += new System.EventHandler(this.Btn_AddAppointment_Click);
            // 
            // Btn_NewUser
            // 
            this.Btn_NewUser.Location = new System.Drawing.Point(159, 144);
            this.Btn_NewUser.Name = "Btn_NewUser";
            this.Btn_NewUser.Size = new System.Drawing.Size(125, 41);
            this.Btn_NewUser.TabIndex = 5;
            this.Btn_NewUser.Text = "New User";
            this.Btn_NewUser.UseVisualStyleBackColor = true;
            this.Btn_NewUser.Visible = false;
            this.Btn_NewUser.Click += new System.EventHandler(this.Btn_NewUser_Click);
            // 
            // Lbl_Clock
            // 
            this.Lbl_Clock.AutoSize = true;
            this.Lbl_Clock.Location = new System.Drawing.Point(1319, 9);
            this.Lbl_Clock.Name = "Lbl_Clock";
            this.Lbl_Clock.Size = new System.Drawing.Size(70, 25);
            this.Lbl_Clock.TabIndex = 17;
            this.Lbl_Clock.Text = "label1";
            // 
            // Btn_Reports
            // 
            this.Btn_Reports.Location = new System.Drawing.Point(392, 144);
            this.Btn_Reports.Name = "Btn_Reports";
            this.Btn_Reports.Size = new System.Drawing.Size(123, 41);
            this.Btn_Reports.TabIndex = 18;
            this.Btn_Reports.Text = "Reports";
            this.Btn_Reports.UseVisualStyleBackColor = true;
            this.Btn_Reports.Visible = false;
            this.Btn_Reports.Click += new System.EventHandler(this.Btn_Reports_Click);
            // 
            // Lbl_Loading
            // 
            this.Lbl_Loading.AutoSize = true;
            this.Lbl_Loading.Location = new System.Drawing.Point(577, 159);
            this.Lbl_Loading.Name = "Lbl_Loading";
            this.Lbl_Loading.Size = new System.Drawing.Size(107, 25);
            this.Lbl_Loading.TabIndex = 19;
            this.Lbl_Loading.Text = "Loading...";
            this.Lbl_Loading.Visible = false;
            // 
            // monthCalendar_Main
            // 
            this.monthCalendar_Main.Location = new System.Drawing.Point(24, 84);
            this.monthCalendar_Main.Name = "monthCalendar_Main";
            this.monthCalendar_Main.TabIndex = 20;
            this.monthCalendar_Main.Visible = false;
            this.monthCalendar_Main.DateChanged += new System.Windows.Forms.DateRangeEventHandler(this.monthCalendar_Main_DateChanged);
            // 
            // groupBox_AppointmentView
            // 
            this.groupBox_AppointmentView.Controls.Add(this.RBtn_WeekView);
            this.groupBox_AppointmentView.Controls.Add(this.monthCalendar_Main);
            this.groupBox_AppointmentView.Controls.Add(this.RBtn_CalendarView);
            this.groupBox_AppointmentView.Controls.Add(this.dataGridView_UserAppointments);
            this.groupBox_AppointmentView.Controls.Add(this.Btn_AddAppointment);
            this.groupBox_AppointmentView.Controls.Add(this.Btn_UpdateAppointment);
            this.groupBox_AppointmentView.Controls.Add(this.Btn_DeleteAppointment);
            this.groupBox_AppointmentView.Location = new System.Drawing.Point(33, 433);
            this.groupBox_AppointmentView.Name = "groupBox_AppointmentView";
            this.groupBox_AppointmentView.Size = new System.Drawing.Size(1434, 410);
            this.groupBox_AppointmentView.TabIndex = 21;
            this.groupBox_AppointmentView.TabStop = false;
            this.groupBox_AppointmentView.Text = "Appointments";
            this.groupBox_AppointmentView.Visible = false;
            // 
            // RBtn_WeekView
            // 
            this.RBtn_WeekView.AutoSize = true;
            this.RBtn_WeekView.Location = new System.Drawing.Point(264, 34);
            this.RBtn_WeekView.Name = "RBtn_WeekView";
            this.RBtn_WeekView.Size = new System.Drawing.Size(150, 29);
            this.RBtn_WeekView.TabIndex = 1;
            this.RBtn_WeekView.TabStop = true;
            this.RBtn_WeekView.Text = "Week View";
            this.RBtn_WeekView.UseVisualStyleBackColor = true;
            this.RBtn_WeekView.CheckedChanged += new System.EventHandler(this.RBtn_WeekView_CheckedChanged);
            // 
            // RBtn_CalendarView
            // 
            this.RBtn_CalendarView.AutoSize = true;
            this.RBtn_CalendarView.Location = new System.Drawing.Point(50, 32);
            this.RBtn_CalendarView.Name = "RBtn_CalendarView";
            this.RBtn_CalendarView.Size = new System.Drawing.Size(182, 29);
            this.RBtn_CalendarView.TabIndex = 0;
            this.RBtn_CalendarView.TabStop = true;
            this.RBtn_CalendarView.Text = "Calendar View";
            this.RBtn_CalendarView.UseVisualStyleBackColor = true;
            this.RBtn_CalendarView.CheckedChanged += new System.EventHandler(this.RBtn_CalendarView_CheckedChanged);
            // 
            // groupBox_Customers
            // 
            this.groupBox_Customers.Controls.Add(this.dataGridViewCustomers);
            this.groupBox_Customers.Controls.Add(this.Btn_AddCustomer);
            this.groupBox_Customers.Controls.Add(this.Btn_UpdateCustInfo);
            this.groupBox_Customers.Controls.Add(this.Btn_DeleteCustomer);
            this.groupBox_Customers.Location = new System.Drawing.Point(625, 37);
            this.groupBox_Customers.Name = "groupBox_Customers";
            this.groupBox_Customers.Size = new System.Drawing.Size(842, 428);
            this.groupBox_Customers.TabIndex = 22;
            this.groupBox_Customers.TabStop = false;
            this.groupBox_Customers.Text = "Customers";
            this.groupBox_Customers.Visible = false;
            // 
            // progressBar_Main
            // 
            this.progressBar_Main.Location = new System.Drawing.Point(12, 12);
            this.progressBar_Main.Name = "progressBar_Main";
            this.progressBar_Main.Size = new System.Drawing.Size(100, 23);
            this.progressBar_Main.TabIndex = 23;
            // 
            // Frm_Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1609, 855);
            this.Controls.Add(this.progressBar_Main);
            this.Controls.Add(this.groupBox_Customers);
            this.Controls.Add(this.groupBox_AppointmentView);
            this.Controls.Add(this.Lbl_Loading);
            this.Controls.Add(this.Btn_Reports);
            this.Controls.Add(this.Lbl_Clock);
            this.Controls.Add(this.Btn_NewUser);
            this.Controls.Add(this.Btn_Exit);
            this.Controls.Add(this.Btn_LogOut);
            this.Controls.Add(this.Btn_Login);
            this.Controls.Add(this.Txt_Password);
            this.Controls.Add(this.Lbl_Password);
            this.Controls.Add(this.Txt_Username);
            this.Controls.Add(this.Lbl_Username);
            this.Name = "Frm_Main";
            this.Text = "International Consulting Firm Scheduler";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewCustomers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_UserAppointments)).EndInit();
            this.groupBox_AppointmentView.ResumeLayout(false);
            this.groupBox_AppointmentView.PerformLayout();
            this.groupBox_Customers.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label Lbl_Username;
        private System.Windows.Forms.TextBox Txt_Username;
        private System.Windows.Forms.TextBox Txt_Password;
        private System.Windows.Forms.Label Lbl_Password;
        private System.Windows.Forms.Button Btn_Login;
        private System.Windows.Forms.Button Btn_LogOut;
        private System.Windows.Forms.DataGridView dataGridViewCustomers;
        private System.Windows.Forms.Button Btn_AddCustomer;
        private System.Windows.Forms.Button Btn_UpdateCustInfo;
        private System.Windows.Forms.Button Btn_DeleteCustomer;
        private System.Windows.Forms.Button Btn_Exit;
        private System.Windows.Forms.DataGridView dataGridView_UserAppointments;
        private System.Windows.Forms.Button Btn_DeleteAppointment;
        private System.Windows.Forms.Button Btn_UpdateAppointment;
        private System.Windows.Forms.Button Btn_AddAppointment;
        private System.Windows.Forms.Button Btn_NewUser;
        private System.Windows.Forms.Label Lbl_Clock;
        private System.Windows.Forms.Button Btn_Reports;
        private System.Windows.Forms.Label Lbl_Loading;
        private System.Windows.Forms.MonthCalendar monthCalendar_Main;
        private System.Windows.Forms.GroupBox groupBox_AppointmentView;
        private System.Windows.Forms.RadioButton RBtn_WeekView;
        private System.Windows.Forms.RadioButton RBtn_CalendarView;
        private System.Windows.Forms.GroupBox groupBox_Customers;
        private System.Windows.Forms.ProgressBar progressBar_Main;
    }
}

