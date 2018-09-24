namespace Software2Project_DakodaWillden_V1
{
    partial class Frm_NewUser
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
            this.Txt_NewUserName = new System.Windows.Forms.TextBox();
            this.Lbl_NewUserName = new System.Windows.Forms.Label();
            this.Lbl_NewPassword = new System.Windows.Forms.Label();
            this.Txt_Password = new System.Windows.Forms.TextBox();
            this.Btn_Cancel = new System.Windows.Forms.Button();
            this.Btn_Add = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Txt_NewUserName
            // 
            this.Txt_NewUserName.Location = new System.Drawing.Point(182, 72);
            this.Txt_NewUserName.Name = "Txt_NewUserName";
            this.Txt_NewUserName.Size = new System.Drawing.Size(264, 31);
            this.Txt_NewUserName.TabIndex = 0;
            // 
            // Lbl_NewUserName
            // 
            this.Lbl_NewUserName.AutoSize = true;
            this.Lbl_NewUserName.Location = new System.Drawing.Point(57, 75);
            this.Lbl_NewUserName.Name = "Lbl_NewUserName";
            this.Lbl_NewUserName.Size = new System.Drawing.Size(119, 25);
            this.Lbl_NewUserName.TabIndex = 1;
            this.Lbl_NewUserName.Text = "User Name";
            // 
            // Lbl_NewPassword
            // 
            this.Lbl_NewPassword.AutoSize = true;
            this.Lbl_NewPassword.Location = new System.Drawing.Point(57, 122);
            this.Lbl_NewPassword.Name = "Lbl_NewPassword";
            this.Lbl_NewPassword.Size = new System.Drawing.Size(106, 25);
            this.Lbl_NewPassword.TabIndex = 3;
            this.Lbl_NewPassword.Text = "Password";
            // 
            // Txt_Password
            // 
            this.Txt_Password.Location = new System.Drawing.Point(182, 119);
            this.Txt_Password.Name = "Txt_Password";
            this.Txt_Password.PasswordChar = '*';
            this.Txt_Password.Size = new System.Drawing.Size(264, 31);
            this.Txt_Password.TabIndex = 2;
            // 
            // Btn_Cancel
            // 
            this.Btn_Cancel.Location = new System.Drawing.Point(454, 213);
            this.Btn_Cancel.Name = "Btn_Cancel";
            this.Btn_Cancel.Size = new System.Drawing.Size(107, 41);
            this.Btn_Cancel.TabIndex = 4;
            this.Btn_Cancel.Text = "Cancel";
            this.Btn_Cancel.UseVisualStyleBackColor = true;
            this.Btn_Cancel.Click += new System.EventHandler(this.Btn_Cancel_Click);
            // 
            // Btn_Add
            // 
            this.Btn_Add.Location = new System.Drawing.Point(339, 213);
            this.Btn_Add.Name = "Btn_Add";
            this.Btn_Add.Size = new System.Drawing.Size(107, 41);
            this.Btn_Add.TabIndex = 5;
            this.Btn_Add.Text = "Add";
            this.Btn_Add.UseVisualStyleBackColor = true;
            this.Btn_Add.Click += new System.EventHandler(this.Btn_Add_Click);
            // 
            // NewUser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(573, 266);
            this.Controls.Add(this.Btn_Add);
            this.Controls.Add(this.Btn_Cancel);
            this.Controls.Add(this.Lbl_NewPassword);
            this.Controls.Add(this.Txt_Password);
            this.Controls.Add(this.Lbl_NewUserName);
            this.Controls.Add(this.Txt_NewUserName);
            this.Name = "NewUser";
            this.Text = "Add New User";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox Txt_NewUserName;
        private System.Windows.Forms.Label Lbl_NewUserName;
        private System.Windows.Forms.Label Lbl_NewPassword;
        private System.Windows.Forms.TextBox Txt_Password;
        private System.Windows.Forms.Button Btn_Cancel;
        private System.Windows.Forms.Button Btn_Add;
    }
}