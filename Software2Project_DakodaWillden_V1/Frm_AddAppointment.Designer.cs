namespace Software2Project_DakodaWillden_V1
{
    partial class Frm_AddAppointment
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
            this.Lbl_AppointmentId = new System.Windows.Forms.Label();
            this.Txt_AppointmentId = new System.Windows.Forms.TextBox();
            this.Btn_Cancel = new System.Windows.Forms.Button();
            this.Btn_Add = new System.Windows.Forms.Button();
            this.dateTimePicker_AppointmentDateStartTime = new System.Windows.Forms.DateTimePicker();
            this.comboBox_CustomerSelect = new System.Windows.Forms.ComboBox();
            this.Lbl_Title = new System.Windows.Forms.Label();
            this.Txt_Title = new System.Windows.Forms.TextBox();
            this.Lbl_Description = new System.Windows.Forms.Label();
            this.Txt_Description = new System.Windows.Forms.TextBox();
            this.Lbl_ChooseDateStartTime = new System.Windows.Forms.Label();
            this.Lbl_ChooseDateEndTime = new System.Windows.Forms.Label();
            this.dateTimePicker_AppointmentDateEndTime = new System.Windows.Forms.DateTimePicker();
            this.comboBox_SelectLocation = new System.Windows.Forms.ComboBox();
            this.comboBox_AppointmentType = new System.Windows.Forms.ComboBox();
            this.Btn_ViewCustInfo = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Lbl_AppointmentId
            // 
            this.Lbl_AppointmentId.AutoSize = true;
            this.Lbl_AppointmentId.Location = new System.Drawing.Point(12, 21);
            this.Lbl_AppointmentId.Name = "Lbl_AppointmentId";
            this.Lbl_AppointmentId.Size = new System.Drawing.Size(155, 25);
            this.Lbl_AppointmentId.TabIndex = 0;
            this.Lbl_AppointmentId.Text = "Appointment Id";
            // 
            // Txt_AppointmentId
            // 
            this.Txt_AppointmentId.Location = new System.Drawing.Point(173, 18);
            this.Txt_AppointmentId.Name = "Txt_AppointmentId";
            this.Txt_AppointmentId.ReadOnly = true;
            this.Txt_AppointmentId.Size = new System.Drawing.Size(132, 31);
            this.Txt_AppointmentId.TabIndex = 10;
            // 
            // Btn_Cancel
            // 
            this.Btn_Cancel.Location = new System.Drawing.Point(751, 304);
            this.Btn_Cancel.Name = "Btn_Cancel";
            this.Btn_Cancel.Size = new System.Drawing.Size(98, 42);
            this.Btn_Cancel.TabIndex = 9;
            this.Btn_Cancel.Text = "Cancel";
            this.Btn_Cancel.UseVisualStyleBackColor = true;
            this.Btn_Cancel.Click += new System.EventHandler(this.Btn_Cancel_Click);
            // 
            // Btn_Add
            // 
            this.Btn_Add.Location = new System.Drawing.Point(632, 304);
            this.Btn_Add.Name = "Btn_Add";
            this.Btn_Add.Size = new System.Drawing.Size(113, 42);
            this.Btn_Add.TabIndex = 8;
            this.Btn_Add.Text = "Add";
            this.Btn_Add.UseVisualStyleBackColor = true;
            this.Btn_Add.Click += new System.EventHandler(this.Btn_Add_Click);
            // 
            // dateTimePicker_AppointmentDateStartTime
            // 
            this.dateTimePicker_AppointmentDateStartTime.CustomFormat = "MM/dd/yyyy HH:mm  ";
            this.dateTimePicker_AppointmentDateStartTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker_AppointmentDateStartTime.Location = new System.Drawing.Point(17, 210);
            this.dateTimePicker_AppointmentDateStartTime.Name = "dateTimePicker_AppointmentDateStartTime";
            this.dateTimePicker_AppointmentDateStartTime.Size = new System.Drawing.Size(288, 31);
            this.dateTimePicker_AppointmentDateStartTime.TabIndex = 3;
            // 
            // comboBox_CustomerSelect
            // 
            this.comboBox_CustomerSelect.FormattingEnabled = true;
            this.comboBox_CustomerSelect.Location = new System.Drawing.Point(17, 70);
            this.comboBox_CustomerSelect.Name = "comboBox_CustomerSelect";
            this.comboBox_CustomerSelect.Size = new System.Drawing.Size(288, 33);
            this.comboBox_CustomerSelect.TabIndex = 1;
            this.comboBox_CustomerSelect.Text = "--Select A Customer--";
            this.comboBox_CustomerSelect.DropDownClosed += new System.EventHandler(this.comboBox_CustomerSelect_DropDownClosed);
            // 
            // Lbl_Title
            // 
            this.Lbl_Title.AutoSize = true;
            this.Lbl_Title.Location = new System.Drawing.Point(329, 73);
            this.Lbl_Title.Name = "Lbl_Title";
            this.Lbl_Title.Size = new System.Drawing.Size(53, 25);
            this.Lbl_Title.TabIndex = 6;
            this.Lbl_Title.Text = "Title";
            // 
            // Txt_Title
            // 
            this.Txt_Title.Location = new System.Drawing.Point(473, 70);
            this.Txt_Title.Name = "Txt_Title";
            this.Txt_Title.Size = new System.Drawing.Size(376, 31);
            this.Txt_Title.TabIndex = 6;
            // 
            // Lbl_Description
            // 
            this.Lbl_Description.AutoSize = true;
            this.Lbl_Description.Location = new System.Drawing.Point(329, 125);
            this.Lbl_Description.Name = "Lbl_Description";
            this.Lbl_Description.Size = new System.Drawing.Size(120, 25);
            this.Lbl_Description.TabIndex = 9;
            this.Lbl_Description.Text = "Description";
            // 
            // Txt_Description
            // 
            this.Txt_Description.Location = new System.Drawing.Point(473, 122);
            this.Txt_Description.Multiline = true;
            this.Txt_Description.Name = "Txt_Description";
            this.Txt_Description.Size = new System.Drawing.Size(376, 171);
            this.Txt_Description.TabIndex = 7;
            // 
            // Lbl_ChooseDateStartTime
            // 
            this.Lbl_ChooseDateStartTime.AutoSize = true;
            this.Lbl_ChooseDateStartTime.Location = new System.Drawing.Point(12, 182);
            this.Lbl_ChooseDateStartTime.Name = "Lbl_ChooseDateStartTime";
            this.Lbl_ChooseDateStartTime.Size = new System.Drawing.Size(301, 25);
            this.Lbl_ChooseDateStartTime.TabIndex = 11;
            this.Lbl_ChooseDateStartTime.Text = "Choose a Date and Start Time";
            // 
            // Lbl_ChooseDateEndTime
            // 
            this.Lbl_ChooseDateEndTime.AutoSize = true;
            this.Lbl_ChooseDateEndTime.Location = new System.Drawing.Point(11, 264);
            this.Lbl_ChooseDateEndTime.Name = "Lbl_ChooseDateEndTime";
            this.Lbl_ChooseDateEndTime.Size = new System.Drawing.Size(294, 25);
            this.Lbl_ChooseDateEndTime.TabIndex = 13;
            this.Lbl_ChooseDateEndTime.Text = "Choose a Date and End Time";
            // 
            // dateTimePicker_AppointmentDateEndTime
            // 
            this.dateTimePicker_AppointmentDateEndTime.CustomFormat = "MM/dd/yyyy HH:mm  ";
            this.dateTimePicker_AppointmentDateEndTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker_AppointmentDateEndTime.Location = new System.Drawing.Point(17, 292);
            this.dateTimePicker_AppointmentDateEndTime.Name = "dateTimePicker_AppointmentDateEndTime";
            this.dateTimePicker_AppointmentDateEndTime.Size = new System.Drawing.Size(288, 31);
            this.dateTimePicker_AppointmentDateEndTime.TabIndex = 4;
            // 
            // comboBox_SelectLocation
            // 
            this.comboBox_SelectLocation.FormattingEnabled = true;
            this.comboBox_SelectLocation.Location = new System.Drawing.Point(17, 130);
            this.comboBox_SelectLocation.Name = "comboBox_SelectLocation";
            this.comboBox_SelectLocation.Size = new System.Drawing.Size(288, 33);
            this.comboBox_SelectLocation.TabIndex = 2;
            this.comboBox_SelectLocation.Text = "--Select A Location--";
            // 
            // comboBox_AppointmentType
            // 
            this.comboBox_AppointmentType.FormattingEnabled = true;
            this.comboBox_AppointmentType.Location = new System.Drawing.Point(473, 18);
            this.comboBox_AppointmentType.Name = "comboBox_AppointmentType";
            this.comboBox_AppointmentType.Size = new System.Drawing.Size(376, 33);
            this.comboBox_AppointmentType.TabIndex = 5;
            this.comboBox_AppointmentType.Text = "--Select An Appointment Type--";
            // 
            // Btn_ViewCustInfo
            // 
            this.Btn_ViewCustInfo.Location = new System.Drawing.Point(319, 210);
            this.Btn_ViewCustInfo.Name = "Btn_ViewCustInfo";
            this.Btn_ViewCustInfo.Size = new System.Drawing.Size(137, 113);
            this.Btn_ViewCustInfo.TabIndex = 14;
            this.Btn_ViewCustInfo.Text = "View Customer Info";
            this.Btn_ViewCustInfo.UseVisualStyleBackColor = true;
            this.Btn_ViewCustInfo.Visible = false;
            this.Btn_ViewCustInfo.Click += new System.EventHandler(this.Btn_ViewCustInfo_Click);
            // 
            // AddAppointment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(861, 358);
            this.Controls.Add(this.Btn_ViewCustInfo);
            this.Controls.Add(this.comboBox_AppointmentType);
            this.Controls.Add(this.comboBox_SelectLocation);
            this.Controls.Add(this.Lbl_ChooseDateEndTime);
            this.Controls.Add(this.dateTimePicker_AppointmentDateEndTime);
            this.Controls.Add(this.Lbl_ChooseDateStartTime);
            this.Controls.Add(this.Txt_Description);
            this.Controls.Add(this.Lbl_Description);
            this.Controls.Add(this.Txt_Title);
            this.Controls.Add(this.Lbl_Title);
            this.Controls.Add(this.comboBox_CustomerSelect);
            this.Controls.Add(this.dateTimePicker_AppointmentDateStartTime);
            this.Controls.Add(this.Btn_Add);
            this.Controls.Add(this.Btn_Cancel);
            this.Controls.Add(this.Txt_AppointmentId);
            this.Controls.Add(this.Lbl_AppointmentId);
            this.Name = "AddAppointment";
            this.Text = "Schedule An Appointment";
            this.Load += new System.EventHandler(this.AddAppointment_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label Lbl_AppointmentId;
        private System.Windows.Forms.TextBox Txt_AppointmentId;
        private System.Windows.Forms.Button Btn_Cancel;
        private System.Windows.Forms.Button Btn_Add;
        private System.Windows.Forms.DateTimePicker dateTimePicker_AppointmentDateStartTime;
        private System.Windows.Forms.ComboBox comboBox_CustomerSelect;
        private System.Windows.Forms.Label Lbl_Title;
        private System.Windows.Forms.TextBox Txt_Title;
        private System.Windows.Forms.Label Lbl_Description;
        private System.Windows.Forms.TextBox Txt_Description;
        private System.Windows.Forms.Label Lbl_ChooseDateStartTime;
        private System.Windows.Forms.Label Lbl_ChooseDateEndTime;
        private System.Windows.Forms.DateTimePicker dateTimePicker_AppointmentDateEndTime;
        private System.Windows.Forms.ComboBox comboBox_SelectLocation;
        private System.Windows.Forms.ComboBox comboBox_AppointmentType;
        private System.Windows.Forms.Button Btn_ViewCustInfo;
    }
}