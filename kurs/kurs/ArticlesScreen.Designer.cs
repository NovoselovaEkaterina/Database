namespace kurs
{
    partial class ArticlesScreen
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
            this.Add = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.nameArt = new System.Windows.Forms.TextBox();
            this.dataGrid1 = new System.Windows.Forms.DataGrid();
            this.Modify = new System.Windows.Forms.Button();
            this.Del = new System.Windows.Forms.Button();
            this.IdArt = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid1)).BeginInit();
            this.SuspendLayout();
            // 
            // Add
            // 
            this.Add.Location = new System.Drawing.Point(68, 264);
            this.Add.Name = "Add";
            this.Add.Size = new System.Drawing.Size(124, 36);
            this.Add.TabIndex = 0;
            this.Add.Text = "Добавить статью";
            this.Add.UseVisualStyleBackColor = true;
            this.Add.Click += new System.EventHandler(this.Add_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(219, 346);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Название статьи";
            // 
            // nameArt
            // 
            this.nameArt.Location = new System.Drawing.Point(222, 362);
            this.nameArt.Name = "nameArt";
            this.nameArt.Size = new System.Drawing.Size(180, 20);
            this.nameArt.TabIndex = 2;
            // 
            // dataGrid1
            // 
            this.dataGrid1.CaptionVisible = false;
            this.dataGrid1.DataMember = "";
            this.dataGrid1.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.dataGrid1.Location = new System.Drawing.Point(23, 12);
            this.dataGrid1.Name = "dataGrid1";
            this.dataGrid1.Size = new System.Drawing.Size(419, 228);
            this.dataGrid1.TabIndex = 3;
            // 
            // Modify
            // 
            this.Modify.Location = new System.Drawing.Point(68, 318);
            this.Modify.Name = "Modify";
            this.Modify.Size = new System.Drawing.Size(124, 36);
            this.Modify.TabIndex = 4;
            this.Modify.Text = "Изменить название статьи";
            this.Modify.UseVisualStyleBackColor = true;
            this.Modify.Click += new System.EventHandler(this.Modify_Click);
            // 
            // Del
            // 
            this.Del.Location = new System.Drawing.Point(68, 374);
            this.Del.Name = "Del";
            this.Del.Size = new System.Drawing.Size(124, 36);
            this.Del.TabIndex = 5;
            this.Del.Text = "Удалить статью";
            this.Del.UseVisualStyleBackColor = true;
            this.Del.Click += new System.EventHandler(this.Del_Click);
            // 
            // IdArt
            // 
            this.IdArt.Location = new System.Drawing.Point(222, 306);
            this.IdArt.Name = "IdArt";
            this.IdArt.Size = new System.Drawing.Size(180, 20);
            this.IdArt.TabIndex = 11;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(219, 290);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(78, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Номер статьи";
            // 
            // ArticlesScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(464, 450);
            this.Controls.Add(this.IdArt);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.Del);
            this.Controls.Add(this.Modify);
            this.Controls.Add(this.dataGrid1);
            this.Controls.Add(this.nameArt);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Add);
            this.Name = "ArticlesScreen";
            this.Text = "Статьи";
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Add;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox nameArt;
        private System.Windows.Forms.DataGrid dataGrid1;
        private System.Windows.Forms.Button Modify;
        private System.Windows.Forms.Button Del;
        private System.Windows.Forms.TextBox IdArt;
        private System.Windows.Forms.Label label4;
    }
}