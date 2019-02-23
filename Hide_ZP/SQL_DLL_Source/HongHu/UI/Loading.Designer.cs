namespace HongHu.UI
{
    partial class Loading
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.LoadingStr_lab = new System.Windows.Forms.Label();
            this.LoadingImg_picbox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.LoadingImg_picbox)).BeginInit();
            this.SuspendLayout();
            // 
            // LoadingStr_lab
            // 
            this.LoadingStr_lab.AutoSize = true;
            this.LoadingStr_lab.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.LoadingStr_lab.Location = new System.Drawing.Point(57, 11);
            this.LoadingStr_lab.Name = "LoadingStr_lab";
            this.LoadingStr_lab.Size = new System.Drawing.Size(113, 12);
            this.LoadingStr_lab.TabIndex = 0;
            this.LoadingStr_lab.Text = "Loading...........";
            // 
            // LoadingImg_picbox
            // 
            this.LoadingImg_picbox.Image = global::HongHu.Properties.Resource.loading;
            this.LoadingImg_picbox.Location = new System.Drawing.Point(3, 2);
            this.LoadingImg_picbox.Name = "LoadingImg_picbox";
            this.LoadingImg_picbox.Size = new System.Drawing.Size(48, 31);
            this.LoadingImg_picbox.TabIndex = 1;
            this.LoadingImg_picbox.TabStop = false;
            // 
            // Loading
            // 
            this.AllowDrag = true;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(244, 34);
            this.Controls.Add(this.LoadingImg_picbox);
            this.Controls.Add(this.LoadingStr_lab);
            this.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Loading";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Loading";
            this.TopMost = true;
            ((System.ComponentModel.ISupportInitialize)(this.LoadingImg_picbox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

         public System.Windows.Forms.Label LoadingStr_lab;
        private System.Windows.Forms.PictureBox LoadingImg_picbox;
    }
}