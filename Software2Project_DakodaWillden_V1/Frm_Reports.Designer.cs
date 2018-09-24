namespace Software2Project_DakodaWillden_V1
{
    partial class Frm_Reports
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
            this.comboBox_SelectReport = new System.Windows.Forms.ComboBox();
            this.comboBox_VariableSelections = new System.Windows.Forms.ComboBox();
            this.Btn_Exit = new System.Windows.Forms.Button();
            this.Btn_CreateReport = new System.Windows.Forms.Button();
            this.Txt_ShowReport = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // comboBox_SelectReport
            // 
            this.comboBox_SelectReport.FormattingEnabled = true;
            this.comboBox_SelectReport.Location = new System.Drawing.Point(13, 13);
            this.comboBox_SelectReport.Name = "comboBox_SelectReport";
            this.comboBox_SelectReport.Size = new System.Drawing.Size(364, 33);
            this.comboBox_SelectReport.TabIndex = 0;
            this.comboBox_SelectReport.Text = "--Please Select A Report--";
            this.comboBox_SelectReport.TextChanged += new System.EventHandler(this.comboBox_SelectReport_TextChanged);
            // 
            // comboBox_VariableSelections
            // 
            this.comboBox_VariableSelections.Enabled = false;
            this.comboBox_VariableSelections.FormattingEnabled = true;
            this.comboBox_VariableSelections.Location = new System.Drawing.Point(383, 13);
            this.comboBox_VariableSelections.Name = "comboBox_VariableSelections";
            this.comboBox_VariableSelections.Size = new System.Drawing.Size(208, 33);
            this.comboBox_VariableSelections.TabIndex = 1;
            this.comboBox_VariableSelections.TextChanged += new System.EventHandler(this.comboBox_VariableSelections_TextChanged);
            // 
            // Btn_Exit
            // 
            this.Btn_Exit.Location = new System.Drawing.Point(681, 576);
            this.Btn_Exit.Name = "Btn_Exit";
            this.Btn_Exit.Size = new System.Drawing.Size(107, 42);
            this.Btn_Exit.TabIndex = 2;
            this.Btn_Exit.Text = "Exit";
            this.Btn_Exit.UseVisualStyleBackColor = true;
            this.Btn_Exit.Click += new System.EventHandler(this.Btn_Exit_Click);
            // 
            // Btn_CreateReport
            // 
            this.Btn_CreateReport.Enabled = false;
            this.Btn_CreateReport.Location = new System.Drawing.Point(625, 6);
            this.Btn_CreateReport.Name = "Btn_CreateReport";
            this.Btn_CreateReport.Size = new System.Drawing.Size(163, 45);
            this.Btn_CreateReport.TabIndex = 3;
            this.Btn_CreateReport.Text = "Create Report";
            this.Btn_CreateReport.UseVisualStyleBackColor = true;
            this.Btn_CreateReport.Click += new System.EventHandler(this.Btn_CreateReport_Click);
            // 
            // Txt_ShowReport
            // 
            this.Txt_ShowReport.AcceptsReturn = true;
            this.Txt_ShowReport.AcceptsTab = true;
            this.Txt_ShowReport.Location = new System.Drawing.Point(13, 78);
            this.Txt_ShowReport.Multiline = true;
            this.Txt_ShowReport.Name = "Txt_ShowReport";
            this.Txt_ShowReport.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.Txt_ShowReport.Size = new System.Drawing.Size(775, 481);
            this.Txt_ShowReport.TabIndex = 4;
            // 
            // Frm_Reports
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 630);
            this.Controls.Add(this.Txt_ShowReport);
            this.Controls.Add(this.Btn_CreateReport);
            this.Controls.Add(this.Btn_Exit);
            this.Controls.Add(this.comboBox_VariableSelections);
            this.Controls.Add(this.comboBox_SelectReport);
            this.Name = "Frm_Reports";
            this.Text = "Reports";
            this.Load += new System.EventHandler(this.Frm_Reports_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBox_SelectReport;
        private System.Windows.Forms.ComboBox comboBox_VariableSelections;
        private System.Windows.Forms.Button Btn_Exit;
        private System.Windows.Forms.Button Btn_CreateReport;
        private System.Windows.Forms.TextBox Txt_ShowReport;
    }
}