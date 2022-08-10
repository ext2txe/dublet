namespace dublet
{
    partial class MainBrowserForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainBrowserForm));
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tsStatusText = new System.Windows.Forms.ToolStripStatusLabel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tabControlLeft = new System.Windows.Forms.TabControl();
            this.tabYouTube = new System.Windows.Forms.TabPage();
            this.pnlMain1 = new System.Windows.Forms.Panel();
            this.wv1 = new Microsoft.Web.WebView2.WinForms.WebView2();
            this.pnlTop1 = new System.Windows.Forms.Panel();
            this.btnGo1 = new System.Windows.Forms.Button();
            this.textUrl1 = new System.Windows.Forms.TextBox();
            this.tabReddit = new System.Windows.Forms.TabPage();
            this.pnlMain2 = new System.Windows.Forms.Panel();
            this.wv2 = new Microsoft.Web.WebView2.WinForms.WebView2();
            this.pnlTop2 = new System.Windows.Forms.Panel();
            this.btnGo2 = new System.Windows.Forms.Button();
            this.textUrl2 = new System.Windows.Forms.TextBox();
            this.tabUpwork = new System.Windows.Forms.TabPage();
            this.panel5 = new System.Windows.Forms.Panel();
            this.wv5 = new Microsoft.Web.WebView2.WinForms.WebView2();
            this.pnlUpwork = new System.Windows.Forms.Panel();
            this.btnGo5 = new System.Windows.Forms.Button();
            this.textUrl5 = new System.Windows.Forms.TextBox();
            this.tabNewTab = new System.Windows.Forms.TabPage();
            this.panel6 = new System.Windows.Forms.Panel();
            this.btnNewTabGo = new System.Windows.Forms.Button();
            this.textNewTabUrl = new System.Windows.Forms.TextBox();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabScratch1 = new System.Windows.Forms.TabPage();
            this.panel1 = new System.Windows.Forms.Panel();
            this.wv3 = new Microsoft.Web.WebView2.WinForms.WebView2();
            this.panel2 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.textUrl3 = new System.Windows.Forms.TextBox();
            this.tabScratch2 = new System.Windows.Forms.TabPage();
            this.panel3 = new System.Windows.Forms.Panel();
            this.wv4 = new Microsoft.Web.WebView2.WinForms.WebView2();
            this.panel4 = new System.Windows.Forms.Panel();
            this.button2 = new System.Windows.Forms.Button();
            this.textUrl4 = new System.Windows.Forms.TextBox();
            this.textLog = new System.Windows.Forms.TextBox();
            this.btnShortcut = new System.Windows.Forms.Button();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tabControlLeft.SuspendLayout();
            this.tabYouTube.SuspendLayout();
            this.pnlMain1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.wv1)).BeginInit();
            this.pnlTop1.SuspendLayout();
            this.tabReddit.SuspendLayout();
            this.pnlMain2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.wv2)).BeginInit();
            this.pnlTop2.SuspendLayout();
            this.tabUpwork.SuspendLayout();
            this.panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.wv5)).BeginInit();
            this.pnlUpwork.SuspendLayout();
            this.tabNewTab.SuspendLayout();
            this.panel6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabScratch1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.wv3)).BeginInit();
            this.panel2.SuspendLayout();
            this.tabScratch2.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.wv4)).BeginInit();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsStatusText});
            this.statusStrip1.Location = new System.Drawing.Point(0, 239);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(784, 22);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            this.statusStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.statusStrip1_ItemClicked);
            // 
            // tsStatusText
            // 
            this.tsStatusText.Name = "tsStatusText";
            this.tsStatusText.Size = new System.Drawing.Size(118, 17);
            this.tsStatusText.Text = "toolStripStatusLabel1";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tabControlLeft);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(784, 239);
            this.splitContainer1.SplitterDistance = 336;
            this.splitContainer1.TabIndex = 1;
            // 
            // tabControlLeft
            // 
            this.tabControlLeft.Controls.Add(this.tabYouTube);
            this.tabControlLeft.Controls.Add(this.tabReddit);
            this.tabControlLeft.Controls.Add(this.tabUpwork);
            this.tabControlLeft.Controls.Add(this.tabNewTab);
            this.tabControlLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlLeft.Location = new System.Drawing.Point(0, 0);
            this.tabControlLeft.Name = "tabControlLeft";
            this.tabControlLeft.SelectedIndex = 0;
            this.tabControlLeft.Size = new System.Drawing.Size(336, 239);
            this.tabControlLeft.TabIndex = 0;
            // 
            // tabYouTube
            // 
            this.tabYouTube.Controls.Add(this.pnlMain1);
            this.tabYouTube.Controls.Add(this.pnlTop1);
            this.tabYouTube.Location = new System.Drawing.Point(4, 22);
            this.tabYouTube.Name = "tabYouTube";
            this.tabYouTube.Padding = new System.Windows.Forms.Padding(3);
            this.tabYouTube.Size = new System.Drawing.Size(735, 431);
            this.tabYouTube.TabIndex = 0;
            this.tabYouTube.Text = "YouTube";
            this.tabYouTube.UseVisualStyleBackColor = true;
            // 
            // pnlMain1
            // 
            this.pnlMain1.Controls.Add(this.wv1);
            this.pnlMain1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain1.Location = new System.Drawing.Point(3, 47);
            this.pnlMain1.Name = "pnlMain1";
            this.pnlMain1.Size = new System.Drawing.Size(729, 381);
            this.pnlMain1.TabIndex = 1;
            // 
            // wv1
            // 
            this.wv1.AllowExternalDrop = true;
            this.wv1.CreationProperties = null;
            this.wv1.DefaultBackgroundColor = System.Drawing.Color.White;
            this.wv1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wv1.Location = new System.Drawing.Point(0, 0);
            this.wv1.Name = "wv1";
            this.wv1.Size = new System.Drawing.Size(729, 381);
            this.wv1.TabIndex = 0;
            this.wv1.ZoomFactor = 1D;
            // 
            // pnlTop1
            // 
            this.pnlTop1.Controls.Add(this.btnGo1);
            this.pnlTop1.Controls.Add(this.textUrl1);
            this.pnlTop1.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop1.Location = new System.Drawing.Point(3, 3);
            this.pnlTop1.Name = "pnlTop1";
            this.pnlTop1.Size = new System.Drawing.Size(729, 44);
            this.pnlTop1.TabIndex = 0;
            // 
            // btnGo1
            // 
            this.btnGo1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGo1.Location = new System.Drawing.Point(683, 11);
            this.btnGo1.Name = "btnGo1";
            this.btnGo1.Size = new System.Drawing.Size(40, 23);
            this.btnGo1.TabIndex = 3;
            this.btnGo1.Text = "Go";
            this.btnGo1.UseVisualStyleBackColor = true;
            this.btnGo1.Click += new System.EventHandler(this.btnGo1_Click);
            // 
            // textUrl1
            // 
            this.textUrl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textUrl1.Location = new System.Drawing.Point(6, 12);
            this.textUrl1.Name = "textUrl1";
            this.textUrl1.Size = new System.Drawing.Size(677, 20);
            this.textUrl1.TabIndex = 2;
            this.textUrl1.Text = "https://youtube.com";
            // 
            // tabReddit
            // 
            this.tabReddit.Controls.Add(this.pnlMain2);
            this.tabReddit.Controls.Add(this.pnlTop2);
            this.tabReddit.Location = new System.Drawing.Point(4, 22);
            this.tabReddit.Name = "tabReddit";
            this.tabReddit.Padding = new System.Windows.Forms.Padding(3);
            this.tabReddit.Size = new System.Drawing.Size(735, 431);
            this.tabReddit.TabIndex = 1;
            this.tabReddit.Text = "Reddit";
            this.tabReddit.UseVisualStyleBackColor = true;
            // 
            // pnlMain2
            // 
            this.pnlMain2.Controls.Add(this.wv2);
            this.pnlMain2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain2.Location = new System.Drawing.Point(3, 47);
            this.pnlMain2.Name = "pnlMain2";
            this.pnlMain2.Size = new System.Drawing.Size(729, 381);
            this.pnlMain2.TabIndex = 3;
            // 
            // wv2
            // 
            this.wv2.AllowExternalDrop = true;
            this.wv2.CreationProperties = null;
            this.wv2.DefaultBackgroundColor = System.Drawing.Color.White;
            this.wv2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wv2.Location = new System.Drawing.Point(0, 0);
            this.wv2.Name = "wv2";
            this.wv2.Size = new System.Drawing.Size(729, 381);
            this.wv2.TabIndex = 4;
            this.wv2.ZoomFactor = 1D;
            // 
            // pnlTop2
            // 
            this.pnlTop2.Controls.Add(this.btnGo2);
            this.pnlTop2.Controls.Add(this.textUrl2);
            this.pnlTop2.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop2.Location = new System.Drawing.Point(3, 3);
            this.pnlTop2.Name = "pnlTop2";
            this.pnlTop2.Size = new System.Drawing.Size(729, 44);
            this.pnlTop2.TabIndex = 2;
            // 
            // btnGo2
            // 
            this.btnGo2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGo2.Location = new System.Drawing.Point(687, 5);
            this.btnGo2.Name = "btnGo2";
            this.btnGo2.Size = new System.Drawing.Size(40, 23);
            this.btnGo2.TabIndex = 1;
            this.btnGo2.Text = "Go";
            this.btnGo2.UseVisualStyleBackColor = true;
            this.btnGo2.Click += new System.EventHandler(this.btnGo2_Click);
            // 
            // textUrl2
            // 
            this.textUrl2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textUrl2.Location = new System.Drawing.Point(10, 6);
            this.textUrl2.Name = "textUrl2";
            this.textUrl2.Size = new System.Drawing.Size(677, 20);
            this.textUrl2.TabIndex = 0;
            this.textUrl2.Text = "https://old.reddit.com/r/programming/";
            // 
            // tabUpwork
            // 
            this.tabUpwork.Controls.Add(this.panel5);
            this.tabUpwork.Controls.Add(this.pnlUpwork);
            this.tabUpwork.Location = new System.Drawing.Point(4, 22);
            this.tabUpwork.Name = "tabUpwork";
            this.tabUpwork.Padding = new System.Windows.Forms.Padding(3);
            this.tabUpwork.Size = new System.Drawing.Size(735, 431);
            this.tabUpwork.TabIndex = 2;
            this.tabUpwork.Text = "Upwork";
            this.tabUpwork.UseVisualStyleBackColor = true;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.wv5);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel5.Location = new System.Drawing.Point(3, 47);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(729, 381);
            this.panel5.TabIndex = 5;
            // 
            // wv5
            // 
            this.wv5.AllowExternalDrop = true;
            this.wv5.CreationProperties = null;
            this.wv5.DefaultBackgroundColor = System.Drawing.Color.White;
            this.wv5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wv5.Location = new System.Drawing.Point(0, 0);
            this.wv5.Name = "wv5";
            this.wv5.Size = new System.Drawing.Size(729, 381);
            this.wv5.TabIndex = 4;
            this.wv5.ZoomFactor = 1D;
            // 
            // pnlUpwork
            // 
            this.pnlUpwork.Controls.Add(this.btnGo5);
            this.pnlUpwork.Controls.Add(this.textUrl5);
            this.pnlUpwork.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlUpwork.Location = new System.Drawing.Point(3, 3);
            this.pnlUpwork.Name = "pnlUpwork";
            this.pnlUpwork.Size = new System.Drawing.Size(729, 44);
            this.pnlUpwork.TabIndex = 4;
            // 
            // btnGo5
            // 
            this.btnGo5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGo5.Location = new System.Drawing.Point(687, 5);
            this.btnGo5.Name = "btnGo5";
            this.btnGo5.Size = new System.Drawing.Size(40, 23);
            this.btnGo5.TabIndex = 1;
            this.btnGo5.Text = "Go";
            this.btnGo5.UseVisualStyleBackColor = true;
            this.btnGo5.Click += new System.EventHandler(this.btnGo5_Click);
            // 
            // textUrl5
            // 
            this.textUrl5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textUrl5.Location = new System.Drawing.Point(10, 6);
            this.textUrl5.Name = "textUrl5";
            this.textUrl5.Size = new System.Drawing.Size(677, 20);
            this.textUrl5.TabIndex = 0;
            this.textUrl5.Text = "https://upwork.com";
            // 
            // tabNewTab
            // 
            this.tabNewTab.Controls.Add(this.panel6);
            this.tabNewTab.Location = new System.Drawing.Point(4, 22);
            this.tabNewTab.Name = "tabNewTab";
            this.tabNewTab.Padding = new System.Windows.Forms.Padding(3);
            this.tabNewTab.Size = new System.Drawing.Size(328, 213);
            this.tabNewTab.TabIndex = 3;
            this.tabNewTab.Text = "New Tab";
            this.tabNewTab.UseVisualStyleBackColor = true;
            this.tabNewTab.Click += new System.EventHandler(this.tabNewTab_Click);
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.btnShortcut);
            this.panel6.Controls.Add(this.btnNewTabGo);
            this.panel6.Controls.Add(this.textNewTabUrl);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel6.Location = new System.Drawing.Point(3, 3);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(322, 44);
            this.panel6.TabIndex = 1;
            // 
            // btnNewTabGo
            // 
            this.btnNewTabGo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNewTabGo.Location = new System.Drawing.Point(276, 11);
            this.btnNewTabGo.Name = "btnNewTabGo";
            this.btnNewTabGo.Size = new System.Drawing.Size(40, 23);
            this.btnNewTabGo.TabIndex = 3;
            this.btnNewTabGo.Text = "Go";
            this.btnNewTabGo.UseVisualStyleBackColor = true;
            this.btnNewTabGo.Click += new System.EventHandler(this.btnNewTabGo_Click);
            // 
            // textNewTabUrl
            // 
            this.textNewTabUrl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textNewTabUrl.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.textNewTabUrl.CausesValidation = false;
            this.textNewTabUrl.Location = new System.Drawing.Point(5, 12);
            this.textNewTabUrl.Name = "textNewTabUrl";
            this.textNewTabUrl.Size = new System.Drawing.Size(219, 20);
            this.textNewTabUrl.TabIndex = 2;
            this.textNewTabUrl.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textNewTabUrl_KeyDown);
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.tabControl1);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.textLog);
            this.splitContainer2.Size = new System.Drawing.Size(444, 239);
            this.splitContainer2.SplitterDistance = 364;
            this.splitContainer2.TabIndex = 0;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabScratch1);
            this.tabControl1.Controls.Add(this.tabScratch2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(364, 239);
            this.tabControl1.TabIndex = 1;
            // 
            // tabScratch1
            // 
            this.tabScratch1.Controls.Add(this.panel1);
            this.tabScratch1.Controls.Add(this.panel2);
            this.tabScratch1.Location = new System.Drawing.Point(4, 22);
            this.tabScratch1.Name = "tabScratch1";
            this.tabScratch1.Padding = new System.Windows.Forms.Padding(3);
            this.tabScratch1.Size = new System.Drawing.Size(356, 213);
            this.tabScratch1.TabIndex = 0;
            this.tabScratch1.Text = "Scratch 1";
            this.tabScratch1.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.wv3);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 47);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(350, 163);
            this.panel1.TabIndex = 1;
            // 
            // wv3
            // 
            this.wv3.AllowExternalDrop = true;
            this.wv3.CreationProperties = null;
            this.wv3.DefaultBackgroundColor = System.Drawing.Color.White;
            this.wv3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wv3.Location = new System.Drawing.Point(0, 0);
            this.wv3.Name = "wv3";
            this.wv3.Size = new System.Drawing.Size(350, 163);
            this.wv3.TabIndex = 0;
            this.wv3.ZoomFactor = 1D;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.button1);
            this.panel2.Controls.Add(this.textUrl3);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(3, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(350, 44);
            this.panel2.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(304, 11);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(40, 23);
            this.button1.TabIndex = 3;
            this.button1.Text = "Go";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textUrl3
            // 
            this.textUrl3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textUrl3.Location = new System.Drawing.Point(6, 12);
            this.textUrl3.Name = "textUrl3";
            this.textUrl3.Size = new System.Drawing.Size(298, 20);
            this.textUrl3.TabIndex = 2;
            // 
            // tabScratch2
            // 
            this.tabScratch2.Controls.Add(this.panel3);
            this.tabScratch2.Controls.Add(this.panel4);
            this.tabScratch2.Location = new System.Drawing.Point(4, 22);
            this.tabScratch2.Name = "tabScratch2";
            this.tabScratch2.Padding = new System.Windows.Forms.Padding(3);
            this.tabScratch2.Size = new System.Drawing.Size(798, 431);
            this.tabScratch2.TabIndex = 1;
            this.tabScratch2.Text = "Scratch 2";
            this.tabScratch2.UseVisualStyleBackColor = true;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.wv4);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(3, 47);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(792, 381);
            this.panel3.TabIndex = 3;
            // 
            // wv4
            // 
            this.wv4.AllowExternalDrop = true;
            this.wv4.CreationProperties = null;
            this.wv4.DefaultBackgroundColor = System.Drawing.Color.White;
            this.wv4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wv4.Location = new System.Drawing.Point(0, 0);
            this.wv4.Name = "wv4";
            this.wv4.Size = new System.Drawing.Size(792, 381);
            this.wv4.TabIndex = 4;
            this.wv4.ZoomFactor = 1D;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.button2);
            this.panel4.Controls.Add(this.textUrl4);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(3, 3);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(792, 44);
            this.panel4.TabIndex = 2;
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.Location = new System.Drawing.Point(750, 5);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(40, 23);
            this.button2.TabIndex = 1;
            this.button2.Text = "Go";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // textUrl4
            // 
            this.textUrl4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textUrl4.Location = new System.Drawing.Point(10, 6);
            this.textUrl4.Name = "textUrl4";
            this.textUrl4.Size = new System.Drawing.Size(740, 20);
            this.textUrl4.TabIndex = 0;
            // 
            // textLog
            // 
            this.textLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textLog.Location = new System.Drawing.Point(0, 0);
            this.textLog.Multiline = true;
            this.textLog.Name = "textLog";
            this.textLog.Size = new System.Drawing.Size(76, 239);
            this.textLog.TabIndex = 0;
            // 
            // btnShortcut
            // 
            this.btnShortcut.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnShortcut.Location = new System.Drawing.Point(230, 11);
            this.btnShortcut.Name = "btnShortcut";
            this.btnShortcut.Size = new System.Drawing.Size(40, 23);
            this.btnShortcut.TabIndex = 4;
            this.btnShortcut.Text = "*";
            this.btnShortcut.UseVisualStyleBackColor = true;
            // 
            // MainBrowserForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 261);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.statusStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainBrowserForm";
            this.Text = "Twin Browser";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainBrowserForm_FormClosing);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tabControlLeft.ResumeLayout(false);
            this.tabYouTube.ResumeLayout(false);
            this.pnlMain1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.wv1)).EndInit();
            this.pnlTop1.ResumeLayout(false);
            this.pnlTop1.PerformLayout();
            this.tabReddit.ResumeLayout(false);
            this.pnlMain2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.wv2)).EndInit();
            this.pnlTop2.ResumeLayout(false);
            this.pnlTop2.PerformLayout();
            this.tabUpwork.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.wv5)).EndInit();
            this.pnlUpwork.ResumeLayout(false);
            this.pnlUpwork.PerformLayout();
            this.tabNewTab.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabScratch1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.wv3)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.tabScratch2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.wv4)).EndInit();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TabControl tabControlLeft;
        private System.Windows.Forms.TabPage tabYouTube;
        private System.Windows.Forms.Panel pnlTop1;
        private System.Windows.Forms.TabPage tabReddit;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.Panel pnlMain1;
        private System.Windows.Forms.Panel pnlMain2;
        private System.Windows.Forms.Panel pnlTop2;
        private Microsoft.Web.WebView2.WinForms.WebView2 wv1;
        private System.Windows.Forms.Button btnGo1;
        private System.Windows.Forms.TextBox textUrl1;
        private Microsoft.Web.WebView2.WinForms.WebView2 wv2;
        private System.Windows.Forms.Button btnGo2;
        private System.Windows.Forms.TextBox textUrl2;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabScratch1;
        private System.Windows.Forms.Panel panel1;
        private Microsoft.Web.WebView2.WinForms.WebView2 wv3;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textUrl3;
        private System.Windows.Forms.TabPage tabScratch2;
        private System.Windows.Forms.Panel panel3;
        private Microsoft.Web.WebView2.WinForms.WebView2 wv4;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox textUrl4;
        private System.Windows.Forms.TextBox textLog;
        private System.Windows.Forms.TabPage tabUpwork;
        private System.Windows.Forms.Panel panel5;
        private Microsoft.Web.WebView2.WinForms.WebView2 wv5;
        private System.Windows.Forms.Panel pnlUpwork;
        private System.Windows.Forms.Button btnGo5;
        private System.Windows.Forms.TextBox textUrl5;
        private System.Windows.Forms.TabPage tabNewTab;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Button btnNewTabGo;
        private System.Windows.Forms.TextBox textNewTabUrl;
        private System.Windows.Forms.ToolStripStatusLabel tsStatusText;
        private System.Windows.Forms.Button btnShortcut;
    }
}

