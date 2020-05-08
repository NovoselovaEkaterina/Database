namespace kurs
{
    partial class BalancesScreen
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
            this.dataGrid1 = new System.Windows.Forms.DataGrid();
            this.delBalance = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.createBalance = new System.Windows.Forms.Button();
            this.beginDate = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.endDate = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.number = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGrid1
            // 
            this.dataGrid1.CaptionVisible = false;
            this.dataGrid1.DataMember = "";
            this.dataGrid1.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.dataGrid1.Location = new System.Drawing.Point(24, 12);
            this.dataGrid1.Name = "dataGrid1";
            this.dataGrid1.Size = new System.Drawing.Size(752, 287);
            this.dataGrid1.TabIndex = 16;
            // 
            // delBalance
            // 
            this.delBalance.Location = new System.Drawing.Point(495, 319);
            this.delBalance.Name = "delBalance";
            this.delBalance.Size = new System.Drawing.Size(124, 36);
            this.delBalance.TabIndex = 18;
            this.delBalance.Text = "Удалить баланс";
            this.delBalance.UseVisualStyleBackColor = true;
            this.delBalance.Click += new System.EventHandler(this.delBalance_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(635, 316);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(86, 13);
            this.label3.TabIndex = 26;
            this.label3.Text = "Номер баланса";
            // 
            // createBalance
            // 
            this.createBalance.Location = new System.Drawing.Point(24, 319);
            this.createBalance.Name = "createBalance";
            this.createBalance.Size = new System.Drawing.Size(124, 36);
            this.createBalance.TabIndex = 27;
            this.createBalance.Text = "Сформировать баланс";
            this.createBalance.UseVisualStyleBackColor = true;
            this.createBalance.Click += new System.EventHandler(this.createBalance_Click);
            // 
            // beginDate
            // 
            this.beginDate.Location = new System.Drawing.Point(175, 335);
            this.beginDate.Name = "beginDate";
            this.beginDate.Size = new System.Drawing.Size(117, 20);
            this.beginDate.TabIndex = 31;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(172, 319);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(71, 13);
            this.label4.TabIndex = 30;
            this.label4.Text = "Дата начала";
            // 
            // endDate
            // 
            this.endDate.Location = new System.Drawing.Point(308, 335);
            this.endDate.Name = "endDate";
            this.endDate.Size = new System.Drawing.Size(117, 20);
            this.endDate.TabIndex = 29;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(305, 319);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 13);
            this.label1.TabIndex = 28;
            this.label1.Text = "Дата конца";
            // 
            // number
            // 
            this.number.Location = new System.Drawing.Point(638, 335);
            this.number.Name = "number";
            this.number.Size = new System.Drawing.Size(117, 20);
            this.number.TabIndex = 32;
            // 
            // BalancesScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 371);
            this.Controls.Add(this.number);
            this.Controls.Add(this.beginDate);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.endDate);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.createBalance);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.delBalance);
            this.Controls.Add(this.dataGrid1);
            this.Name = "BalancesScreen";
            this.Text = "Журнал анализа бюджета";
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGrid dataGrid1;
        private System.Windows.Forms.Button delBalance;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button createBalance;
        private System.Windows.Forms.TextBox beginDate;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox endDate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox number;
    }
}