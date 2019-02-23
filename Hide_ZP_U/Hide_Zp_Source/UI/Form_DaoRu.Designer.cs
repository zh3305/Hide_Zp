namespace HongHu.UI
{
    partial class Form_DaoRu
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_DaoRu));
            this.checkedListBox1 = new System.Windows.Forms.CheckedListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.Button_DaoRu = new Glass.GlassButton();
            this.glassButton2 = new Glass.GlassButton();
            this.Button_ShuaXin = new Glass.GlassButton();
            this.SuspendLayout();
            // 
            // checkedListBox1
            // 
            this.checkedListBox1.FormattingEnabled = true;
            this.checkedListBox1.Location = new System.Drawing.Point(12, 28);
            this.checkedListBox1.Name = "checkedListBox1";
            this.checkedListBox1.Size = new System.Drawing.Size(233, 212);
            this.checkedListBox1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(125, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "请选择需要导入的帐套";
            // 
            // Button_DaoRu
            // 
            this.Button_DaoRu.BackColor = System.Drawing.Color.LightGray;
            this.Button_DaoRu.ForeColor = System.Drawing.Color.Black;
            this.Button_DaoRu.Location = new System.Drawing.Point(89, 246);
            this.Button_DaoRu.Name = "Button_DaoRu";
            this.Button_DaoRu.Size = new System.Drawing.Size(75, 23);
            this.Button_DaoRu.TabIndex = 2;
            this.Button_DaoRu.Text = "导   入";
            this.Button_DaoRu.Click += new System.EventHandler(this.Button_DaoRu_Click);
            // 
            // glassButton2
            // 
            this.glassButton2.BackColor = System.Drawing.Color.LightGray;
            this.glassButton2.ForeColor = System.Drawing.Color.Black;
            this.glassButton2.Location = new System.Drawing.Point(170, 246);
            this.glassButton2.Name = "glassButton2";
            this.glassButton2.Size = new System.Drawing.Size(75, 23);
            this.glassButton2.TabIndex = 2;
            this.glassButton2.Text = "取   消";
            this.glassButton2.Click += new System.EventHandler(this.glassButton2_Click);
            // 
            // Button_ShuaXin
            // 
            this.Button_ShuaXin.BackColor = System.Drawing.Color.LightGray;
            this.Button_ShuaXin.ForeColor = System.Drawing.Color.Black;
            this.Button_ShuaXin.Location = new System.Drawing.Point(8, 246);
            this.Button_ShuaXin.Name = "Button_ShuaXin";
            this.Button_ShuaXin.Size = new System.Drawing.Size(75, 23);
            this.Button_ShuaXin.TabIndex = 2;
            this.Button_ShuaXin.Text = "刷   新";
            this.Button_ShuaXin.Click += new System.EventHandler(this.Button_ShuaXin_Click);
            // 
            // Form_DaoRu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(257, 276);
            this.Controls.Add(this.glassButton2);
            this.Controls.Add(this.Button_ShuaXin);
            this.Controls.Add(this.Button_DaoRu);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.checkedListBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form_DaoRu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "导入帐套";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckedListBox checkedListBox1;
        private System.Windows.Forms.Label label1;
        private Glass.GlassButton Button_DaoRu;
        private Glass.GlassButton glassButton2;
        private Glass.GlassButton Button_ShuaXin;
    }
}