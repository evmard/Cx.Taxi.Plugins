namespace CxTaxiSlimClient
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.mainTool = new System.Windows.Forms.ToolStrip();
            this.tsPhoneEdit = new System.Windows.Forms.ToolStripTextBox();
            this.tsSearchBtn = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsPayInBtn = new System.Windows.Forms.ToolStripButton();
            this.tsPayoutBtn = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsRelogin = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.lblName = new System.Windows.Forms.Label();
            this.lblBirthday = new System.Windows.Forms.Label();
            this.teName = new System.Windows.Forms.TextBox();
            this.teBirthday = new System.Windows.Forms.TextBox();
            this.lblBullingNumber = new System.Windows.Forms.Label();
            this.lblBalance = new System.Windows.Forms.Label();
            this.teBillingNumber = new System.Windows.Forms.TextBox();
            this.teBalance = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.mainTool.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainTool
            // 
            this.mainTool.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsPhoneEdit,
            this.tsSearchBtn,
            this.toolStripSeparator1,
            this.tsPayInBtn,
            this.tsPayoutBtn,
            this.toolStripSeparator2,
            this.tsRelogin,
            this.toolStripSeparator3});
            this.mainTool.Location = new System.Drawing.Point(0, 0);
            this.mainTool.Margin = new System.Windows.Forms.Padding(2);
            this.mainTool.Name = "mainTool";
            this.mainTool.Padding = new System.Windows.Forms.Padding(0);
            this.mainTool.Size = new System.Drawing.Size(623, 25);
            this.mainTool.TabIndex = 1;
            this.mainTool.Text = "toolStrip1";
            // 
            // tsPhoneEdit
            // 
            this.tsPhoneEdit.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tsPhoneEdit.Name = "tsPhoneEdit";
            this.tsPhoneEdit.Size = new System.Drawing.Size(150, 25);
            this.tsPhoneEdit.ToolTipText = "Номер телефона клиента";
            // 
            // tsSearchBtn
            // 
            this.tsSearchBtn.Image = ((System.Drawing.Image)(resources.GetObject("tsSearchBtn.Image")));
            this.tsSearchBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsSearchBtn.Name = "tsSearchBtn";
            this.tsSearchBtn.Size = new System.Drawing.Size(62, 22);
            this.tsSearchBtn.Text = "Поиск";
            this.tsSearchBtn.ToolTipText = "Поиск клиента по номеру телефона";
            this.tsSearchBtn.Click += new System.EventHandler(this.tsSearchBtn_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // tsPayInBtn
            // 
            this.tsPayInBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsPayInBtn.Image = ((System.Drawing.Image)(resources.GetObject("tsPayInBtn.Image")));
            this.tsPayInBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsPayInBtn.Name = "tsPayInBtn";
            this.tsPayInBtn.Size = new System.Drawing.Size(23, 22);
            this.tsPayInBtn.Text = "toolStripButton1";
            this.tsPayInBtn.ToolTipText = "Зачислить на счет";
            this.tsPayInBtn.Click += new System.EventHandler(this.tsPayInBtn_Click);
            // 
            // tsPayoutBtn
            // 
            this.tsPayoutBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsPayoutBtn.Image = ((System.Drawing.Image)(resources.GetObject("tsPayoutBtn.Image")));
            this.tsPayoutBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsPayoutBtn.Name = "tsPayoutBtn";
            this.tsPayoutBtn.Size = new System.Drawing.Size(23, 22);
            this.tsPayoutBtn.Text = "toolStripButton2";
            this.tsPayoutBtn.ToolTipText = "Списать со счета";
            this.tsPayoutBtn.Click += new System.EventHandler(this.tsPayoutBtn_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // tsRelogin
            // 
            this.tsRelogin.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsRelogin.Image = ((System.Drawing.Image)(resources.GetObject("tsRelogin.Image")));
            this.tsRelogin.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsRelogin.Name = "tsRelogin";
            this.tsRelogin.Size = new System.Drawing.Size(85, 22);
            this.tsRelogin.Text = "Перезайти";
            this.tsRelogin.ToolTipText = "Войти в систему под другим пользователем";
            this.tsRelogin.Click += new System.EventHandler(this.tsRelogin_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 5;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.lblName, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblBirthday, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.teName, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.teBirthday, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.lblBullingNumber, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblBalance, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.teBillingNumber, 4, 0);
            this.tableLayoutPanel1.Controls.Add(this.teBalance, 4, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(621, 240);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(3, 6);
            this.lblName.Margin = new System.Windows.Forms.Padding(3, 6, 3, 0);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(131, 13);
            this.lblName.TabIndex = 0;
            this.lblName.Text = "Фамилия Имя Отчество";
            // 
            // lblBirthday
            // 
            this.lblBirthday.AutoSize = true;
            this.lblBirthday.Location = new System.Drawing.Point(3, 32);
            this.lblBirthday.Margin = new System.Windows.Forms.Padding(3, 6, 3, 0);
            this.lblBirthday.Name = "lblBirthday";
            this.lblBirthday.Size = new System.Drawing.Size(86, 13);
            this.lblBirthday.TabIndex = 1;
            this.lblBirthday.Text = "Дата рождения";
            // 
            // teName
            // 
            this.teName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.teName.Location = new System.Drawing.Point(140, 3);
            this.teName.Name = "teName";
            this.teName.ReadOnly = true;
            this.teName.Size = new System.Drawing.Size(187, 20);
            this.teName.TabIndex = 2;
            // 
            // teBirthday
            // 
            this.teBirthday.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.teBirthday.Location = new System.Drawing.Point(140, 29);
            this.teBirthday.Name = "teBirthday";
            this.teBirthday.ReadOnly = true;
            this.teBirthday.Size = new System.Drawing.Size(187, 20);
            this.teBirthday.TabIndex = 3;
            // 
            // lblBullingNumber
            // 
            this.lblBullingNumber.AutoSize = true;
            this.lblBullingNumber.Location = new System.Drawing.Point(353, 5);
            this.lblBullingNumber.Margin = new System.Windows.Forms.Padding(3, 5, 3, 0);
            this.lblBullingNumber.Name = "lblBullingNumber";
            this.lblBullingNumber.Size = new System.Drawing.Size(72, 13);
            this.lblBullingNumber.TabIndex = 4;
            this.lblBullingNumber.Text = "Номер счета";
            // 
            // lblBalance
            // 
            this.lblBalance.AutoSize = true;
            this.lblBalance.Location = new System.Drawing.Point(353, 31);
            this.lblBalance.Margin = new System.Windows.Forms.Padding(3, 5, 3, 0);
            this.lblBalance.Name = "lblBalance";
            this.lblBalance.Size = new System.Drawing.Size(44, 13);
            this.lblBalance.TabIndex = 5;
            this.lblBalance.Text = "Баланс";
            // 
            // teBillingNumber
            // 
            this.teBillingNumber.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.teBillingNumber.Location = new System.Drawing.Point(431, 3);
            this.teBillingNumber.Name = "teBillingNumber";
            this.teBillingNumber.ReadOnly = true;
            this.teBillingNumber.Size = new System.Drawing.Size(187, 20);
            this.teBillingNumber.TabIndex = 6;
            // 
            // teBalance
            // 
            this.teBalance.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.teBalance.Location = new System.Drawing.Point(431, 29);
            this.teBalance.Name = "teBalance";
            this.teBalance.ReadOnly = true;
            this.teBalance.Size = new System.Drawing.Size(187, 20);
            this.teBalance.TabIndex = 7;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.tableLayoutPanel1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 25);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(623, 242);
            this.panel1.TabIndex = 3;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(623, 267);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.mainTool);
            this.MinimumSize = new System.Drawing.Size(570, 200);
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.mainTool.ResumeLayout(false);
            this.mainTool.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip mainTool;
        private System.Windows.Forms.ToolStripTextBox tsPhoneEdit;
        private System.Windows.Forms.ToolStripButton tsSearchBtn;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton tsPayInBtn;
        private System.Windows.Forms.ToolStripButton tsPayoutBtn;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton tsRelogin;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblBirthday;
        private System.Windows.Forms.TextBox teName;
        private System.Windows.Forms.TextBox teBirthday;
        private System.Windows.Forms.Label lblBullingNumber;
        private System.Windows.Forms.Label lblBalance;
        private System.Windows.Forms.TextBox teBillingNumber;
        private System.Windows.Forms.TextBox teBalance;
        private System.Windows.Forms.Panel panel1;

    }
}