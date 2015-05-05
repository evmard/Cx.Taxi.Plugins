using System.ComponentModel;
using System.Windows.Forms;

namespace CxTaxiSlimClient
{
    partial class LoginForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.lblUserName = new System.Windows.Forms.Label();
            this.lblPass = new System.Windows.Forms.Label();
            this.teLogin = new System.Windows.Forms.TextBox();
            this.tePass = new System.Windows.Forms.TextBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnLogin = new System.Windows.Forms.Button();
            this.lblRole = new System.Windows.Forms.Label();
            this.cbRole = new System.Windows.Forms.ComboBox();
            this.chSaveUser = new System.Windows.Forms.CheckBox();
            this.tabLogon = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnNetCancel = new System.Windows.Forms.Button();
            this.teIp = new System.Windows.Forms.TextBox();
            this.btnNetSave = new System.Windows.Forms.Button();
            this.tePort = new System.Windows.Forms.MaskedTextBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.tabLogon.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.lblUserName, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.lblPass, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.teLogin, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.tePass, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.btnCancel, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.btnLogin, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.lblRole, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.cbRole, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.chSaveUser, 1, 3);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(310, 135);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // lblUserName
            // 
            this.lblUserName.AutoSize = true;
            this.lblUserName.Location = new System.Drawing.Point(3, 35);
            this.lblUserName.Margin = new System.Windows.Forms.Padding(3, 8, 3, 0);
            this.lblUserName.Name = "lblUserName";
            this.lblUserName.Size = new System.Drawing.Size(103, 13);
            this.lblUserName.TabIndex = 0;
            this.lblUserName.Text = "Имя пользователя";
            // 
            // lblPass
            // 
            this.lblPass.AutoSize = true;
            this.lblPass.Location = new System.Drawing.Point(3, 61);
            this.lblPass.Margin = new System.Windows.Forms.Padding(3, 8, 3, 0);
            this.lblPass.Name = "lblPass";
            this.lblPass.Size = new System.Drawing.Size(45, 13);
            this.lblPass.TabIndex = 1;
            this.lblPass.Text = "Пароль";
            // 
            // teLogin
            // 
            this.teLogin.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.teLogin.Location = new System.Drawing.Point(112, 30);
            this.teLogin.Name = "teLogin";
            this.teLogin.Size = new System.Drawing.Size(195, 20);
            this.teLogin.TabIndex = 1;
            // 
            // tePass
            // 
            this.tePass.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tePass.Location = new System.Drawing.Point(112, 56);
            this.tePass.Name = "tePass";
            this.tePass.PasswordChar = '*';
            this.tePass.Size = new System.Drawing.Size(195, 20);
            this.tePass.TabIndex = 2;
            this.tePass.UseSystemPasswordChar = true;
            this.tePass.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tePass_KeyPress);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(3, 108);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(103, 23);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "Закрыть";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnLogin
            // 
            this.btnLogin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLogin.Location = new System.Drawing.Point(204, 108);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(103, 23);
            this.btnLogin.TabIndex = 4;
            this.btnLogin.Text = "Войти";
            this.btnLogin.UseVisualStyleBackColor = true;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // lblRole
            // 
            this.lblRole.AutoSize = true;
            this.lblRole.Location = new System.Drawing.Point(3, 8);
            this.lblRole.Margin = new System.Windows.Forms.Padding(3, 8, 3, 0);
            this.lblRole.Name = "lblRole";
            this.lblRole.Size = new System.Drawing.Size(32, 13);
            this.lblRole.TabIndex = 7;
            this.lblRole.Text = "Роль";
            // 
            // cbRole
            // 
            this.cbRole.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbRole.FormattingEnabled = true;
            this.cbRole.Items.AddRange(new object[] {
            "Зачисление на счет",
            "Списание со счета"});
            this.cbRole.Location = new System.Drawing.Point(112, 3);
            this.cbRole.Name = "cbRole";
            this.cbRole.Size = new System.Drawing.Size(195, 21);
            this.cbRole.TabIndex = 0;
            // 
            // chSaveUser
            // 
            this.chSaveUser.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chSaveUser.AutoSize = true;
            this.chSaveUser.Location = new System.Drawing.Point(112, 82);
            this.chSaveUser.Name = "chSaveUser";
            this.chSaveUser.Size = new System.Drawing.Size(195, 17);
            this.chSaveUser.TabIndex = 3;
            this.chSaveUser.TabStop = false;
            this.chSaveUser.Text = "Запомнить пользователя и роль";
            this.chSaveUser.UseVisualStyleBackColor = true;
            // 
            // tabLogon
            // 
            this.tabLogon.Controls.Add(this.tabPage1);
            this.tabLogon.Controls.Add(this.tabPage2);
            this.tabLogon.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabLogon.Location = new System.Drawing.Point(0, 0);
            this.tabLogon.Name = "tabLogon";
            this.tabLogon.SelectedIndex = 0;
            this.tabLogon.Size = new System.Drawing.Size(324, 167);
            this.tabLogon.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.tableLayoutPanel1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(316, 141);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Вход в систему";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.tableLayoutPanel2);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(316, 141);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Настройки сети";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.btnNetCancel, 0, 3);
            this.tableLayoutPanel2.Controls.Add(this.teIp, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnNetSave, 1, 3);
            this.tableLayoutPanel2.Controls.Add(this.tePort, 1, 1);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 4;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.Size = new System.Drawing.Size(310, 135);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 8);
            this.label1.Margin = new System.Windows.Forms.Padding(3, 8, 3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "IP Адрес";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 34);
            this.label2.Margin = new System.Windows.Forms.Padding(3, 8, 3, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(32, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Порт";
            // 
            // btnNetCancel
            // 
            this.btnNetCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnNetCancel.Location = new System.Drawing.Point(3, 109);
            this.btnNetCancel.Name = "btnNetCancel";
            this.btnNetCancel.Size = new System.Drawing.Size(103, 23);
            this.btnNetCancel.TabIndex = 3;
            this.btnNetCancel.Text = "Отмена";
            this.btnNetCancel.UseVisualStyleBackColor = true;
            this.btnNetCancel.Click += new System.EventHandler(this.btnNetCancel_Click);
            // 
            // teIp
            // 
            this.teIp.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.teIp.Location = new System.Drawing.Point(112, 3);
            this.teIp.Name = "teIp";
            this.teIp.Size = new System.Drawing.Size(195, 20);
            this.teIp.TabIndex = 0;
            // 
            // btnNetSave
            // 
            this.btnNetSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNetSave.Location = new System.Drawing.Point(204, 109);
            this.btnNetSave.Name = "btnNetSave";
            this.btnNetSave.Size = new System.Drawing.Size(103, 23);
            this.btnNetSave.TabIndex = 2;
            this.btnNetSave.Text = "Сохранить";
            this.btnNetSave.UseVisualStyleBackColor = true;
            this.btnNetSave.Click += new System.EventHandler(this.btnNetSave_Click);
            // 
            // tePort
            // 
            this.tePort.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tePort.Location = new System.Drawing.Point(112, 29);
            this.tePort.Mask = "00000";
            this.tePort.Name = "tePort";
            this.tePort.Size = new System.Drawing.Size(195, 20);
            this.tePort.TabIndex = 1;
            this.tePort.ValidatingType = typeof(int);
            // 
            // LoginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(324, 167);
            this.Controls.Add(this.tabLogon);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(400, 250);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(340, 205);
            this.Name = "LoginForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Вход в Infinity";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tabLogon.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        private Label lblUserName;
        private Label lblPass;
        private TextBox teLogin;
        private TextBox tePass;
        private Button btnCancel;
        private Button btnLogin;
        private Label lblRole;
        private ComboBox cbRole;
        private TabControl tabLogon;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private TableLayoutPanel tableLayoutPanel2;
        private Label label1;
        private Label label2;
        private Button btnNetCancel;
        private Button btnNetSave;
        private TextBox teIp;
        private CheckBox chSaveUser;
        private MaskedTextBox tePort;
    }
}

