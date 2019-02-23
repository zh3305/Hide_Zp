using System.Windows.Forms.AbnormityFrame;
namespace HongHu.UI
{
    partial class FormMoveRun
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMoveRun));
            this.pictureBox_Set_O = new System.Windows.Forms.PictureBox();
            this.pictureBox_Title = new System.Windows.Forms.PictureBox();
            this.pictureBox_About = new System.Windows.Forms.PictureBox();
            this.pictureBox_DaoRu = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Set_O)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Title)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_About)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_DaoRu)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox_Set_O
            // 
            this.pictureBox_Set_O.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox_Set_O.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox_Set_O.Image")));
            this.pictureBox_Set_O.Location = new System.Drawing.Point(12, 148);
            this.pictureBox_Set_O.Name = "pictureBox_Set_O";
            this.pictureBox_Set_O.Size = new System.Drawing.Size(106, 40);
            this.pictureBox_Set_O.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox_Set_O.TabIndex = 23;
            this.pictureBox_Set_O.TabStop = false;
            // 
            // pictureBox_Title
            // 
            this.pictureBox_Title.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox_Title.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox_Title.Image")));
            this.pictureBox_Title.Location = new System.Drawing.Point(12, 12);
            this.pictureBox_Title.Name = "pictureBox_Title";
            this.pictureBox_Title.Size = new System.Drawing.Size(408, 54);
            this.pictureBox_Title.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox_Title.TabIndex = 23;
            this.pictureBox_Title.TabStop = false;
            // 
            // pictureBox_About
            // 
            this.pictureBox_About.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox_About.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox_About.Image")));
            this.pictureBox_About.Location = new System.Drawing.Point(12, 205);
            this.pictureBox_About.Name = "pictureBox_About";
            this.pictureBox_About.Size = new System.Drawing.Size(106, 40);
            this.pictureBox_About.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox_About.TabIndex = 23;
            this.pictureBox_About.TabStop = false;
            this.pictureBox_About.Click += new System.EventHandler(this.pictureBox_About_Click);
            // 
            // pictureBox_DaoRu
            // 
            this.pictureBox_DaoRu.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox_DaoRu.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox_DaoRu.Image")));
            this.pictureBox_DaoRu.Location = new System.Drawing.Point(12, 93);
            this.pictureBox_DaoRu.Name = "pictureBox_DaoRu";
            this.pictureBox_DaoRu.Size = new System.Drawing.Size(106, 40);
            this.pictureBox_DaoRu.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox_DaoRu.TabIndex = 23;
            this.pictureBox_DaoRu.TabStop = false;
            this.pictureBox_DaoRu.Click += new System.EventHandler(this.pictureBox_DaoRu_Click);
            // 
            // FormMoveRun
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::HongHu.Properties.Resources.MoveForm_backimage2;
            this.ClientSize = new System.Drawing.Size(680, 385);
            this.Controls.Add(this.pictureBox_Title);
            this.Controls.Add(this.pictureBox_About);
            this.Controls.Add(this.pictureBox_DaoRu);
            this.Controls.Add(this.pictureBox_Set_O);
            this.Name = "FormMoveRun";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "用友通移动应用工具";
            this.Controls.SetChildIndex(this.pictureBox_Set_O, 0);
            this.Controls.SetChildIndex(this.pictureBox_DaoRu, 0);
            this.Controls.SetChildIndex(this.pictureBox_About, 0);
            this.Controls.SetChildIndex(this.pictureBox_Title, 0);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Set_O)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Title)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_About)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_DaoRu)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private  System.Windows.Forms.PictureBox  pictureBox_Set_O;
        private System.Windows.Forms.PictureBox pictureBox_Title;
        private System.Windows.Forms.PictureBox pictureBox_About;
        private System.Windows.Forms.PictureBox pictureBox_DaoRu;
    }
}