namespace Ryr.SolutionHistory
{
    partial class SolutionHistory
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SolutionHistory));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lvSolutionImports = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lvSolutionComponents = new System.Windows.Forms.ListView();
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.lvSolutionComponentDetail = new System.Windows.Forms.ListView();
            this.columnHeader8 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.tsMain = new System.Windows.Forms.ToolStrip();
            this.tsbCloseThisTab = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbSolutionHistory = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbExportSolutionLog = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.columnHeader9 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader10 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader11 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader12 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.tsMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lvSolutionImports);
            this.groupBox1.Location = new System.Drawing.Point(21, 52);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(1166, 980);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Solution Imports";
            // 
            // lvSolutionImports
            // 
            this.lvSolutionImports.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader9,
            this.columnHeader10,
            this.columnHeader11,
            this.columnHeader12});
            this.lvSolutionImports.FullRowSelect = true;
            this.lvSolutionImports.HideSelection = false;
            this.lvSolutionImports.Location = new System.Drawing.Point(36, 33);
            this.lvSolutionImports.Margin = new System.Windows.Forms.Padding(4);
            this.lvSolutionImports.MultiSelect = false;
            this.lvSolutionImports.Name = "lvSolutionImports";
            this.lvSolutionImports.Size = new System.Drawing.Size(1106, 932);
            this.lvSolutionImports.TabIndex = 0;
            this.lvSolutionImports.UseCompatibleStateImageBehavior = false;
            this.lvSolutionImports.View = System.Windows.Forms.View.Details;
            this.lvSolutionImports.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lvSolutionImports_ColumnClick);
            this.lvSolutionImports.SelectedIndexChanged += new System.EventHandler(this.lvSolutionImports_SelectedIndexChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Started On";
            this.columnHeader1.Width = 139;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Solution Name";
            this.columnHeader2.Width = 172;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Type";
            this.columnHeader3.Width = 201;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lvSolutionComponents);
            this.groupBox2.Location = new System.Drawing.Point(1214, 52);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox2.Size = new System.Drawing.Size(574, 980);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Solution Components";
            // 
            // lvSolutionComponents
            // 
            this.lvSolutionComponents.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader4,
            this.columnHeader5});
            this.lvSolutionComponents.FullRowSelect = true;
            this.lvSolutionComponents.HideSelection = false;
            this.lvSolutionComponents.Location = new System.Drawing.Point(28, 33);
            this.lvSolutionComponents.Margin = new System.Windows.Forms.Padding(4);
            this.lvSolutionComponents.MultiSelect = false;
            this.lvSolutionComponents.Name = "lvSolutionComponents";
            this.lvSolutionComponents.Size = new System.Drawing.Size(525, 932);
            this.lvSolutionComponents.TabIndex = 0;
            this.lvSolutionComponents.UseCompatibleStateImageBehavior = false;
            this.lvSolutionComponents.View = System.Windows.Forms.View.Details;
            this.lvSolutionComponents.SelectedIndexChanged += new System.EventHandler(this.lvSolutionComponents_SelectedIndexChanged);
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Type";
            this.columnHeader4.Width = 213;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Count";
            this.columnHeader5.Width = 146;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.lvSolutionComponentDetail);
            this.groupBox3.Location = new System.Drawing.Point(1814, 52);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox3.Size = new System.Drawing.Size(601, 980);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Component Detail";
            // 
            // lvSolutionComponentDetail
            // 
            this.lvSolutionComponentDetail.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader8,
            this.columnHeader6,
            this.columnHeader7});
            this.lvSolutionComponentDetail.FullRowSelect = true;
            this.lvSolutionComponentDetail.HideSelection = false;
            this.lvSolutionComponentDetail.Location = new System.Drawing.Point(23, 33);
            this.lvSolutionComponentDetail.Margin = new System.Windows.Forms.Padding(4);
            this.lvSolutionComponentDetail.MultiSelect = false;
            this.lvSolutionComponentDetail.Name = "lvSolutionComponentDetail";
            this.lvSolutionComponentDetail.Size = new System.Drawing.Size(541, 933);
            this.lvSolutionComponentDetail.SmallImageList = this.imageList1;
            this.lvSolutionComponentDetail.TabIndex = 0;
            this.lvSolutionComponentDetail.UseCompatibleStateImageBehavior = false;
            this.lvSolutionComponentDetail.View = System.Windows.Forms.View.Details;
            this.lvSolutionComponentDetail.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lvSolutionComponentDetail_ColumnClick);
            this.lvSolutionComponentDetail.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lvSolutionComponentDetail_MouseDoubleClick);
            // 
            // columnHeader8
            // 
            this.columnHeader8.Text = "     ";
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "Display Name";
            this.columnHeader6.Width = 208;
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "Id";
            this.columnHeader7.Width = 206;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "16_succeeded.png");
            this.imageList1.Images.SetKeyName(1, "bar_error_16.png");
            this.imageList1.Images.SetKeyName(2, "bar_warn_16.png");
            // 
            // tsMain
            // 
            this.tsMain.AutoSize = false;
            this.tsMain.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.tsMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbCloseThisTab,
            this.toolStripSeparator2,
            this.tsbSolutionHistory,
            this.toolStripSeparator1,
            this.tsbExportSolutionLog,
            this.toolStripSeparator3});
            this.tsMain.Location = new System.Drawing.Point(0, 0);
            this.tsMain.Name = "tsMain";
            this.tsMain.Padding = new System.Windows.Forms.Padding(0, 0, 2, 0);
            this.tsMain.Size = new System.Drawing.Size(2440, 48);
            this.tsMain.TabIndex = 86;
            this.tsMain.Text = "toolStrip1";
            // 
            // tsbCloseThisTab
            // 
            this.tsbCloseThisTab.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbCloseThisTab.Image = ((System.Drawing.Image)(resources.GetObject("tsbCloseThisTab.Image")));
            this.tsbCloseThisTab.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbCloseThisTab.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbCloseThisTab.Name = "tsbCloseThisTab";
            this.tsbCloseThisTab.Size = new System.Drawing.Size(23, 45);
            this.tsbCloseThisTab.Text = "Close this tab";
            this.tsbCloseThisTab.Click += new System.EventHandler(this.tsbCloseThisTab_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 48);
            // 
            // tsbSolutionHistory
            // 
            this.tsbSolutionHistory.Image = ((System.Drawing.Image)(resources.GetObject("tsbSolutionHistory.Image")));
            this.tsbSolutionHistory.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbSolutionHistory.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbSolutionHistory.Name = "tsbSolutionHistory";
            this.tsbSolutionHistory.Size = new System.Drawing.Size(264, 45);
            this.tsbSolutionHistory.Text = "Load Solution History";
            this.tsbSolutionHistory.Click += new System.EventHandler(this.tsbSolutionHistory_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 48);
            // 
            // tsbExportSolutionLog
            // 
            this.tsbExportSolutionLog.Enabled = false;
            this.tsbExportSolutionLog.Image = ((System.Drawing.Image)(resources.GetObject("tsbExportSolutionLog.Image")));
            this.tsbExportSolutionLog.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbExportSolutionLog.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbExportSolutionLog.Name = "tsbExportSolutionLog";
            this.tsbExportSolutionLog.Size = new System.Drawing.Size(305, 45);
            this.tsbExportSolutionLog.Text = "Save Solution Import Log";
            this.tsbExportSolutionLog.ToolTipText = "This exports additional details about the currently selected solution row";
            this.tsbExportSolutionLog.Click += new System.EventHandler(this.tsbExportSolutionLog_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 48);
            // 
            // columnHeader9
            // 
            this.columnHeader9.Text = "Imported By";
            this.columnHeader9.Width = 241;
            // 
            // columnHeader10
            // 
            this.columnHeader10.Text = "Current Version";
            this.columnHeader10.Width = 186;
            // 
            // columnHeader11
            // 
            this.columnHeader11.Text = "Prev. Version";
            this.columnHeader11.Width = 151;
            // 
            // columnHeader12
            // 
            this.columnHeader12.Text = "Publisher";
            this.columnHeader12.Width = 198;
            // 
            // SolutionHistory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tsMain);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "SolutionHistory";
            this.Size = new System.Drawing.Size(2440, 1052);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.tsMain.ResumeLayout(false);
            this.tsMain.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ListView lvSolutionImports;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ListView lvSolutionComponents;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ListView lvSolutionComponentDetail;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.ToolStrip tsMain;
        private System.Windows.Forms.ToolStripButton tsbCloseThisTab;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton tsbSolutionHistory;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton tsbExportSolutionLog;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ColumnHeader columnHeader8;
        private System.Windows.Forms.ColumnHeader columnHeader9;
        private System.Windows.Forms.ColumnHeader columnHeader10;
        private System.Windows.Forms.ColumnHeader columnHeader11;
        private System.Windows.Forms.ColumnHeader columnHeader12;
    }
}
