namespace AntiVirus
{
    partial class FormMain
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem("");
            this.delightTabControl1 = new DelightTabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.LabelPing = new DelightLabel();
            this.LabelUpload = new DelightLabel();
            this.delightLabel7 = new DelightLabel();
            this.delightLabel5 = new DelightLabel();
            this.ButtonCheck = new DelightButton();
            this.ButtonScan = new DelightButton();
            this.delightLabel4 = new DelightLabel();
            this.ProgressDisk = new DelightCircleProgressBar();
            this.delightLabel3 = new DelightLabel();
            this.ProgressMemory = new DelightCircleProgressBar();
            this.ListViewProcess = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ProcessMenu = new DelightContextMenuStrip();
            this.refreshToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.killToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.scanToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.delightLabel2 = new DelightLabel();
            this.ProgressCPU = new DelightCircleProgressBar();
            this.ButtonRefresh = new DelightButton();
            this.delightLabel1 = new DelightLabel();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.ButtonStartup = new DelightButton();
            this.ListviewStartup = new System.Windows.Forms.ListView();
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader8 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader9 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader10 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tmrCheck = new System.Windows.Forms.Timer(this.components);
            this.delightTabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.ProcessMenu.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // delightTabControl1
            // 
            this.delightTabControl1.Alignment = System.Windows.Forms.TabAlignment.Left;
            this.delightTabControl1.ArrowsColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(146)))));
            this.delightTabControl1.ArrowsLocation = new System.Drawing.Point(25, 15);
            this.delightTabControl1.ChangeSize = new System.Drawing.Size(40, 210);
            this.delightTabControl1.Controls.Add(this.tabPage1);
            this.delightTabControl1.Controls.Add(this.tabPage2);
            this.delightTabControl1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.delightTabControl1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.delightTabControl1.HeaderTextLocation = new System.Drawing.Point(25, 5);
            this.delightTabControl1.ImageBackArea = new System.Drawing.Point(30, 7);
            this.delightTabControl1.ImageLocation = new System.Drawing.Point(35, 11);
            this.delightTabControl1.ItemSize = new System.Drawing.Size(40, 210);
            this.delightTabControl1.Location = new System.Drawing.Point(0, 0);
            this.delightTabControl1.Multiline = true;
            this.delightTabControl1.Name = "delightTabControl1";
            this.delightTabControl1.SelectedIndex = 0;
            this.delightTabControl1.SelectedTabSmallRectColor = System.Drawing.Color.White;
            this.delightTabControl1.Size = new System.Drawing.Size(1133, 549);
            this.delightTabControl1.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.delightTabControl1.SmallRectColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(31)))), ((int)(((byte)(42)))));
            this.delightTabControl1.TabBackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(245)))), ((int)(((byte)(247)))));
            this.delightTabControl1.TabColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(38)))), ((int)(((byte)(52)))));
            this.delightTabControl1.TabIndex = 0;
            this.delightTabControl1.TabLinesColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.delightTabControl1.TabPageColor = System.Drawing.Color.White;
            this.delightTabControl1.TabSelectedTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(195)))), ((int)(((byte)(193)))), ((int)(((byte)(232)))));
            this.delightTabControl1.TabTextHeaderColor = System.Drawing.Color.FromArgb(((int)(((byte)(144)))), ((int)(((byte)(143)))), ((int)(((byte)(150)))));
            this.delightTabControl1.TabUnSelectedTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(146)))));
            this.delightTabControl1.TextLocation = new System.Drawing.Point(60, 10);
            this.delightTabControl1.UseAnimation = true;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.White;
            this.tabPage1.Controls.Add(this.LabelPing);
            this.tabPage1.Controls.Add(this.LabelUpload);
            this.tabPage1.Controls.Add(this.delightLabel7);
            this.tabPage1.Controls.Add(this.delightLabel5);
            this.tabPage1.Controls.Add(this.ButtonCheck);
            this.tabPage1.Controls.Add(this.ButtonScan);
            this.tabPage1.Controls.Add(this.delightLabel4);
            this.tabPage1.Controls.Add(this.ProgressDisk);
            this.tabPage1.Controls.Add(this.delightLabel3);
            this.tabPage1.Controls.Add(this.ProgressMemory);
            this.tabPage1.Controls.Add(this.ListViewProcess);
            this.tabPage1.Controls.Add(this.delightLabel2);
            this.tabPage1.Controls.Add(this.ProgressCPU);
            this.tabPage1.Controls.Add(this.ButtonRefresh);
            this.tabPage1.Controls.Add(this.delightLabel1);
            this.tabPage1.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.tabPage1.Location = new System.Drawing.Point(214, 4);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(915, 541);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Main";
            // 
            // LabelPing
            // 
            this.LabelPing.BackColor = System.Drawing.Color.Transparent;
            this.LabelPing.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.LabelPing.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(72)))), ((int)(((byte)(85)))));
            this.LabelPing.Location = new System.Drawing.Point(51, 481);
            this.LabelPing.Name = "LabelPing";
            this.LabelPing.Size = new System.Drawing.Size(75, 18);
            this.LabelPing.TabIndex = 17;
            this.LabelPing.Text = "0";
            // 
            // LabelUpload
            // 
            this.LabelUpload.BackColor = System.Drawing.Color.Transparent;
            this.LabelUpload.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.LabelUpload.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(72)))), ((int)(((byte)(85)))));
            this.LabelUpload.Location = new System.Drawing.Point(63, 457);
            this.LabelUpload.Name = "LabelUpload";
            this.LabelUpload.Size = new System.Drawing.Size(75, 18);
            this.LabelUpload.TabIndex = 16;
            this.LabelUpload.Text = "0.0";
            // 
            // delightLabel7
            // 
            this.delightLabel7.BackColor = System.Drawing.Color.Transparent;
            this.delightLabel7.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.delightLabel7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(72)))), ((int)(((byte)(85)))));
            this.delightLabel7.Location = new System.Drawing.Point(6, 481);
            this.delightLabel7.Name = "delightLabel7";
            this.delightLabel7.Size = new System.Drawing.Size(75, 18);
            this.delightLabel7.TabIndex = 15;
            this.delightLabel7.Text = "Ping:";
            // 
            // delightLabel5
            // 
            this.delightLabel5.BackColor = System.Drawing.Color.Transparent;
            this.delightLabel5.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.delightLabel5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(72)))), ((int)(((byte)(85)))));
            this.delightLabel5.Location = new System.Drawing.Point(6, 457);
            this.delightLabel5.Name = "delightLabel5";
            this.delightLabel5.Size = new System.Drawing.Size(75, 18);
            this.delightLabel5.TabIndex = 13;
            this.delightLabel5.Text = "Internet:";
            // 
            // ButtonCheck
            // 
            this.ButtonCheck.BackColor = System.Drawing.Color.Transparent;
            this.ButtonCheck.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.ButtonCheck.HoverBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(243)))), ((int)(((byte)(249)))));
            this.ButtonCheck.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(243)))), ((int)(((byte)(249)))));
            this.ButtonCheck.HoverTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(47)))), ((int)(((byte)(51)))));
            this.ButtonCheck.IsEnabled = true;
            this.ButtonCheck.Location = new System.Drawing.Point(258, 399);
            this.ButtonCheck.Name = "ButtonCheck";
            this.ButtonCheck.NormalBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(241)))), ((int)(((byte)(246)))));
            this.ButtonCheck.NormalColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(254)))), ((int)(((byte)(255)))));
            this.ButtonCheck.NormalTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(87)))), ((int)(((byte)(91)))), ((int)(((byte)(97)))));
            this.ButtonCheck.PushedBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(216)))), ((int)(((byte)(231)))));
            this.ButtonCheck.PushedColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(243)))), ((int)(((byte)(249)))));
            this.ButtonCheck.PushedTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(47)))), ((int)(((byte)(51)))));
            this.ButtonCheck.RoundRadius = 0;
            this.ButtonCheck.Size = new System.Drawing.Size(120, 52);
            this.ButtonCheck.TabIndex = 12;
            this.ButtonCheck.Text = "Scan Connections";
            // 
            // ButtonScan
            // 
            this.ButtonScan.BackColor = System.Drawing.Color.Transparent;
            this.ButtonScan.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.ButtonScan.HoverBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(243)))), ((int)(((byte)(249)))));
            this.ButtonScan.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(243)))), ((int)(((byte)(249)))));
            this.ButtonScan.HoverTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(47)))), ((int)(((byte)(51)))));
            this.ButtonScan.IsEnabled = true;
            this.ButtonScan.Location = new System.Drawing.Point(132, 399);
            this.ButtonScan.Name = "ButtonScan";
            this.ButtonScan.NormalBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(241)))), ((int)(((byte)(246)))));
            this.ButtonScan.NormalColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(254)))), ((int)(((byte)(255)))));
            this.ButtonScan.NormalTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(87)))), ((int)(((byte)(91)))), ((int)(((byte)(97)))));
            this.ButtonScan.PushedBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(216)))), ((int)(((byte)(231)))));
            this.ButtonScan.PushedColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(243)))), ((int)(((byte)(249)))));
            this.ButtonScan.PushedTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(47)))), ((int)(((byte)(51)))));
            this.ButtonScan.RoundRadius = 0;
            this.ButtonScan.Size = new System.Drawing.Size(120, 52);
            this.ButtonScan.TabIndex = 11;
            this.ButtonScan.Text = "Scan Computer";
            this.ButtonScan.Click += new System.EventHandler(this.ButtonScan_Click);
            // 
            // delightLabel4
            // 
            this.delightLabel4.BackColor = System.Drawing.Color.Transparent;
            this.delightLabel4.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.delightLabel4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(72)))), ((int)(((byte)(85)))));
            this.delightLabel4.Location = new System.Drawing.Point(610, 411);
            this.delightLabel4.Name = "delightLabel4";
            this.delightLabel4.Size = new System.Drawing.Size(75, 18);
            this.delightLabel4.TabIndex = 10;
            this.delightLabel4.Text = "Disk:";
            // 
            // ProgressDisk
            // 
            this.ProgressDisk.BackColor = System.Drawing.Color.Transparent;
            this.ProgressDisk.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(241)))), ((int)(((byte)(246)))));
            this.ProgressDisk.EndStyle = System.Drawing.Drawing2D.LineCap.Custom;
            this.ProgressDisk.FillInside = false;
            this.ProgressDisk.InsideColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(254)))), ((int)(((byte)(255)))));
            this.ProgressDisk.Location = new System.Drawing.Point(610, 435);
            this.ProgressDisk.Maximum = 100;
            this.ProgressDisk.Name = "ProgressDisk";
            this.ProgressDisk.ProgressColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(38)))), ((int)(((byte)(52)))));
            this.ProgressDisk.ProgressTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(38)))), ((int)(((byte)(52)))));
            this.ProgressDisk.ShowBase = true;
            this.ProgressDisk.ShowProgressValue = true;
            this.ProgressDisk.Size = new System.Drawing.Size(95, 95);
            this.ProgressDisk.StartStyle = System.Drawing.Drawing2D.LineCap.Custom;
            this.ProgressDisk.TabIndex = 9;
            this.ProgressDisk.Thickness = 12;
            this.ProgressDisk.Value = 0;
            // 
            // delightLabel3
            // 
            this.delightLabel3.BackColor = System.Drawing.Color.Transparent;
            this.delightLabel3.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.delightLabel3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(72)))), ((int)(((byte)(85)))));
            this.delightLabel3.Location = new System.Drawing.Point(711, 411);
            this.delightLabel3.Name = "delightLabel3";
            this.delightLabel3.Size = new System.Drawing.Size(75, 18);
            this.delightLabel3.TabIndex = 8;
            this.delightLabel3.Text = "Memory:";
            // 
            // ProgressMemory
            // 
            this.ProgressMemory.BackColor = System.Drawing.Color.Transparent;
            this.ProgressMemory.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(241)))), ((int)(((byte)(246)))));
            this.ProgressMemory.EndStyle = System.Drawing.Drawing2D.LineCap.Custom;
            this.ProgressMemory.FillInside = false;
            this.ProgressMemory.InsideColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(254)))), ((int)(((byte)(255)))));
            this.ProgressMemory.Location = new System.Drawing.Point(711, 435);
            this.ProgressMemory.Maximum = 100;
            this.ProgressMemory.Name = "ProgressMemory";
            this.ProgressMemory.ProgressColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(38)))), ((int)(((byte)(52)))));
            this.ProgressMemory.ProgressTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(38)))), ((int)(((byte)(52)))));
            this.ProgressMemory.ShowBase = true;
            this.ProgressMemory.ShowProgressValue = true;
            this.ProgressMemory.Size = new System.Drawing.Size(95, 95);
            this.ProgressMemory.StartStyle = System.Drawing.Drawing2D.LineCap.Custom;
            this.ProgressMemory.TabIndex = 7;
            this.ProgressMemory.Thickness = 12;
            this.ProgressMemory.Value = 0;
            // 
            // ListViewProcess
            // 
            this.ListViewProcess.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader5,
            this.columnHeader4});
            this.ListViewProcess.ContextMenuStrip = this.ProcessMenu;
            this.ListViewProcess.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1});
            this.ListViewProcess.Location = new System.Drawing.Point(6, 38);
            this.ListViewProcess.Name = "ListViewProcess";
            this.ListViewProcess.Size = new System.Drawing.Size(901, 355);
            this.ListViewProcess.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.ListViewProcess.TabIndex = 6;
            this.ListViewProcess.UseCompatibleStateImageBehavior = false;
            this.ListViewProcess.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Processname";
            this.columnHeader1.Width = 164;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Process ID";
            this.columnHeader2.Width = 126;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Process CPU";
            this.columnHeader3.Width = 163;
            // 
            // columnHeader5
            // 
            this.columnHeader5.DisplayIndex = 4;
            this.columnHeader5.Text = "Memory";
            this.columnHeader5.Width = 92;
            // 
            // columnHeader4
            // 
            this.columnHeader4.DisplayIndex = 3;
            this.columnHeader4.Text = "Checked";
            this.columnHeader4.Width = 104;
            // 
            // ProcessMenu
            // 
            this.ProcessMenu.BackColor = System.Drawing.Color.White;
            this.ProcessMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.refreshToolStripMenuItem,
            this.killToolStripMenuItem,
            this.scanToolStripMenuItem});
            this.ProcessMenu.Name = "ProcessMenu";
            this.ProcessMenu.Size = new System.Drawing.Size(114, 70);
            // 
            // refreshToolStripMenuItem
            // 
            this.refreshToolStripMenuItem.Name = "refreshToolStripMenuItem";
            this.refreshToolStripMenuItem.Size = new System.Drawing.Size(113, 22);
            this.refreshToolStripMenuItem.Text = "Refresh";
            this.refreshToolStripMenuItem.Click += new System.EventHandler(this.refreshToolStripMenuItem_Click);
            // 
            // killToolStripMenuItem
            // 
            this.killToolStripMenuItem.Name = "killToolStripMenuItem";
            this.killToolStripMenuItem.Size = new System.Drawing.Size(113, 22);
            this.killToolStripMenuItem.Text = "Kill";
            this.killToolStripMenuItem.Click += new System.EventHandler(this.killToolStripMenuItem_Click);
            // 
            // scanToolStripMenuItem
            // 
            this.scanToolStripMenuItem.Name = "scanToolStripMenuItem";
            this.scanToolStripMenuItem.Size = new System.Drawing.Size(113, 22);
            this.scanToolStripMenuItem.Text = "Scan";
            // 
            // delightLabel2
            // 
            this.delightLabel2.BackColor = System.Drawing.Color.Transparent;
            this.delightLabel2.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.delightLabel2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(72)))), ((int)(((byte)(85)))));
            this.delightLabel2.Location = new System.Drawing.Point(812, 411);
            this.delightLabel2.Name = "delightLabel2";
            this.delightLabel2.Size = new System.Drawing.Size(75, 18);
            this.delightLabel2.TabIndex = 5;
            this.delightLabel2.Text = "CPU:";
            // 
            // ProgressCPU
            // 
            this.ProgressCPU.BackColor = System.Drawing.Color.Transparent;
            this.ProgressCPU.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(241)))), ((int)(((byte)(246)))));
            this.ProgressCPU.EndStyle = System.Drawing.Drawing2D.LineCap.Custom;
            this.ProgressCPU.FillInside = false;
            this.ProgressCPU.InsideColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(254)))), ((int)(((byte)(255)))));
            this.ProgressCPU.Location = new System.Drawing.Point(812, 435);
            this.ProgressCPU.Maximum = 100;
            this.ProgressCPU.Name = "ProgressCPU";
            this.ProgressCPU.ProgressColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(38)))), ((int)(((byte)(52)))));
            this.ProgressCPU.ProgressTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(38)))), ((int)(((byte)(52)))));
            this.ProgressCPU.ShowBase = true;
            this.ProgressCPU.ShowProgressValue = true;
            this.ProgressCPU.Size = new System.Drawing.Size(95, 95);
            this.ProgressCPU.StartStyle = System.Drawing.Drawing2D.LineCap.Custom;
            this.ProgressCPU.TabIndex = 4;
            this.ProgressCPU.Thickness = 12;
            this.ProgressCPU.Value = 0;
            // 
            // ButtonRefresh
            // 
            this.ButtonRefresh.BackColor = System.Drawing.Color.Transparent;
            this.ButtonRefresh.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.ButtonRefresh.HoverBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(243)))), ((int)(((byte)(249)))));
            this.ButtonRefresh.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(243)))), ((int)(((byte)(249)))));
            this.ButtonRefresh.HoverTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(47)))), ((int)(((byte)(51)))));
            this.ButtonRefresh.IsEnabled = true;
            this.ButtonRefresh.Location = new System.Drawing.Point(6, 399);
            this.ButtonRefresh.Name = "ButtonRefresh";
            this.ButtonRefresh.NormalBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(241)))), ((int)(((byte)(246)))));
            this.ButtonRefresh.NormalColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(254)))), ((int)(((byte)(255)))));
            this.ButtonRefresh.NormalTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(87)))), ((int)(((byte)(91)))), ((int)(((byte)(97)))));
            this.ButtonRefresh.PushedBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(216)))), ((int)(((byte)(231)))));
            this.ButtonRefresh.PushedColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(243)))), ((int)(((byte)(249)))));
            this.ButtonRefresh.PushedTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(47)))), ((int)(((byte)(51)))));
            this.ButtonRefresh.RoundRadius = 0;
            this.ButtonRefresh.Size = new System.Drawing.Size(120, 52);
            this.ButtonRefresh.TabIndex = 2;
            this.ButtonRefresh.Text = "Refresh";
            this.ButtonRefresh.Click += new System.EventHandler(this.ButtonRefresh_Click);
            // 
            // delightLabel1
            // 
            this.delightLabel1.BackColor = System.Drawing.Color.Transparent;
            this.delightLabel1.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.delightLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(72)))), ((int)(((byte)(85)))));
            this.delightLabel1.Location = new System.Drawing.Point(6, 14);
            this.delightLabel1.Name = "delightLabel1";
            this.delightLabel1.Size = new System.Drawing.Size(75, 18);
            this.delightLabel1.TabIndex = 1;
            this.delightLabel1.Text = "Processes";
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.Color.White;
            this.tabPage2.Controls.Add(this.ButtonStartup);
            this.tabPage2.Controls.Add(this.ListviewStartup);
            this.tabPage2.Location = new System.Drawing.Point(214, 4);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(915, 541);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Boot Settings";
            // 
            // ButtonStartup
            // 
            this.ButtonStartup.BackColor = System.Drawing.Color.Transparent;
            this.ButtonStartup.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.ButtonStartup.HoverBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(243)))), ((int)(((byte)(249)))));
            this.ButtonStartup.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(243)))), ((int)(((byte)(249)))));
            this.ButtonStartup.HoverTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(47)))), ((int)(((byte)(51)))));
            this.ButtonStartup.IsEnabled = true;
            this.ButtonStartup.Location = new System.Drawing.Point(8, 341);
            this.ButtonStartup.Name = "ButtonStartup";
            this.ButtonStartup.NormalBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(241)))), ((int)(((byte)(246)))));
            this.ButtonStartup.NormalColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(254)))), ((int)(((byte)(255)))));
            this.ButtonStartup.NormalTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(87)))), ((int)(((byte)(91)))), ((int)(((byte)(97)))));
            this.ButtonStartup.PushedBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(216)))), ((int)(((byte)(231)))));
            this.ButtonStartup.PushedColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(243)))), ((int)(((byte)(249)))));
            this.ButtonStartup.PushedTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(47)))), ((int)(((byte)(51)))));
            this.ButtonStartup.RoundRadius = 0;
            this.ButtonStartup.Size = new System.Drawing.Size(124, 50);
            this.ButtonStartup.TabIndex = 1;
            this.ButtonStartup.Text = "Check";
            // 
            // ListviewStartup
            // 
            this.ListviewStartup.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader6,
            this.columnHeader7,
            this.columnHeader8,
            this.columnHeader9,
            this.columnHeader10});
            this.ListviewStartup.Location = new System.Drawing.Point(8, 8);
            this.ListviewStartup.Name = "ListviewStartup";
            this.ListviewStartup.Size = new System.Drawing.Size(901, 327);
            this.ListviewStartup.TabIndex = 0;
            this.ListviewStartup.UseCompatibleStateImageBehavior = false;
            this.ListviewStartup.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "Item Name";
            this.columnHeader6.Width = 125;
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "Filename";
            this.columnHeader7.Width = 173;
            // 
            // columnHeader8
            // 
            this.columnHeader8.Text = "Location";
            this.columnHeader8.Width = 204;
            // 
            // columnHeader9
            // 
            this.columnHeader9.Text = "Status";
            this.columnHeader9.Width = 104;
            // 
            // columnHeader10
            // 
            this.columnHeader10.Text = "Scanned";
            this.columnHeader10.Width = 76;
            // 
            // tmrCheck
            // 
            this.tmrCheck.Interval = 900;
            this.tmrCheck.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1133, 546);
            this.Controls.Add(this.delightTabControl1);
            this.Name = "FormMain";
            this.Text = "WaRo POC Anti Virus";
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.delightTabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.ProcessMenu.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DelightTabControl delightTabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private DelightLabel delightLabel1;
        private System.Windows.Forms.TabPage tabPage2;
        private DelightButton ButtonRefresh;
        private DelightLabel delightLabel2;
        private DelightCircleProgressBar ProgressCPU;
        private System.Windows.Forms.Timer tmrCheck;
        private System.Windows.Forms.ListView ListViewProcess;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private DelightContextMenuStrip ProcessMenu;
        private System.Windows.Forms.ToolStripMenuItem refreshToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem killToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem scanToolStripMenuItem;
        private DelightLabel delightLabel3;
        private DelightCircleProgressBar ProgressMemory;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private DelightLabel delightLabel4;
        private DelightCircleProgressBar ProgressDisk;
        private DelightButton ButtonCheck;
        private DelightButton ButtonScan;
        private DelightLabel LabelUpload;
        private DelightLabel delightLabel7;
        private DelightLabel delightLabel5;
        private DelightLabel LabelPing;
        private DelightButton ButtonStartup;
        private System.Windows.Forms.ListView ListviewStartup;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.ColumnHeader columnHeader8;
        private System.Windows.Forms.ColumnHeader columnHeader9;
        private System.Windows.Forms.ColumnHeader columnHeader10;
    }
}

