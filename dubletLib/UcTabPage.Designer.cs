namespace dubletLib
{
    partial class UcTabPage
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pnlBack = new System.Windows.Forms.Panel();
            this.pnlTop2 = new System.Windows.Forms.Panel();
            this.btnGo2 = new System.Windows.Forms.Button();
            this.textUrl2 = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.wv2 = new Microsoft.Web.WebView2.WinForms.WebView2();
            this.pnlBack.SuspendLayout();
            this.pnlTop2.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.wv2)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlBack
            // 
            this.pnlBack.Controls.Add(this.pnlTop2);
            this.pnlBack.Controls.Add(this.panel1);
            this.pnlBack.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlBack.Location = new System.Drawing.Point(0, 0);
            this.pnlBack.Name = "pnlBack";
            this.pnlBack.Size = new System.Drawing.Size(716, 691);
            this.pnlBack.TabIndex = 0;
            // 
            // pnlTop2
            // 
            this.pnlTop2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlTop2.Controls.Add(this.btnGo2);
            this.pnlTop2.Controls.Add(this.textUrl2);
            this.pnlTop2.Location = new System.Drawing.Point(0, 0);
            this.pnlTop2.Name = "pnlTop2";
            this.pnlTop2.Size = new System.Drawing.Size(713, 39);
            this.pnlTop2.TabIndex = 3;
            // 
            // btnGo2
            // 
            this.btnGo2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGo2.Location = new System.Drawing.Point(671, 5);
            this.btnGo2.Name = "btnGo2";
            this.btnGo2.Size = new System.Drawing.Size(40, 23);
            this.btnGo2.TabIndex = 1;
            this.btnGo2.Text = "Go";
            this.btnGo2.UseVisualStyleBackColor = true;
            // 
            // textUrl2
            // 
            this.textUrl2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textUrl2.Location = new System.Drawing.Point(10, 6);
            this.textUrl2.Name = "textUrl2";
            this.textUrl2.Size = new System.Drawing.Size(661, 20);
            this.textUrl2.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.wv2);
            this.panel1.Location = new System.Drawing.Point(0, 40);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(713, 649);
            this.panel1.TabIndex = 4;
            // 
            // wv2
            // 
            this.wv2.AllowExternalDrop = true;
            this.wv2.CreationProperties = null;
            this.wv2.DefaultBackgroundColor = System.Drawing.Color.White;
            this.wv2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wv2.Location = new System.Drawing.Point(0, 0);
            this.wv2.Name = "wv2";
            this.wv2.Size = new System.Drawing.Size(713, 649);
            this.wv2.TabIndex = 0;
            this.wv2.ZoomFactor = 1D;
            // 
            // UcTabPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlBack);
            this.Name = "UcTabPage";
            this.Size = new System.Drawing.Size(716, 691);
            this.pnlBack.ResumeLayout(false);
            this.pnlTop2.ResumeLayout(false);
            this.pnlTop2.PerformLayout();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.wv2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlBack;
        private System.Windows.Forms.Panel pnlTop2;
        private System.Windows.Forms.Button btnGo2;
        private System.Windows.Forms.TextBox textUrl2;
        private System.Windows.Forms.Panel panel1;
        public Microsoft.Web.WebView2.WinForms.WebView2 wv2;
    }
}
