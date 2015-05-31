﻿using System.ComponentModel;
using System.Windows.Forms;

namespace CxTaxiSlimClient
{
    partial class BonusOperationsForm
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
            this.loTable = new System.Windows.Forms.TableLayoutPanel();
            this.teName = new System.Windows.Forms.TextBox();
            this.lblName = new System.Windows.Forms.Label();
            this.lblPhone = new System.Windows.Forms.Label();
            this.lblOperationSumm = new System.Windows.Forms.Label();
            this.lblCode = new System.Windows.Forms.Label();
            this.tePhone = new System.Windows.Forms.TextBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.gbSendMethod = new System.Windows.Forms.GroupBox();
            this.rbCall = new System.Windows.Forms.RadioButton();
            this.rbSMS = new System.Windows.Forms.RadioButton();
            this.btnSendAgain = new System.Windows.Forms.Button();
            this.teCode = new System.Windows.Forms.MaskedTextBox();
            this.lblSumm = new System.Windows.Forms.Label();
            this.teSumm = new CxTaxiSlimClient.SummEdit();
            this.teOpSumm = new CxTaxiSlimClient.SummEdit();
            this.loTable.SuspendLayout();
            this.gbSendMethod.SuspendLayout();
            this.SuspendLayout();
            // 
            // loTable
            // 
            this.loTable.ColumnCount = 2;
            this.loTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.loTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.loTable.Controls.Add(this.teName, 1, 0);
            this.loTable.Controls.Add(this.lblName, 0, 0);
            this.loTable.Controls.Add(this.lblPhone, 0, 1);
            this.loTable.Controls.Add(this.lblOperationSumm, 0, 3);
            this.loTable.Controls.Add(this.lblCode, 0, 4);
            this.loTable.Controls.Add(this.tePhone, 1, 1);
            this.loTable.Controls.Add(this.btnCancel, 0, 7);
            this.loTable.Controls.Add(this.btnOk, 1, 7);
            this.loTable.Controls.Add(this.gbSendMethod, 0, 6);
            this.loTable.Controls.Add(this.btnSendAgain, 1, 5);
            this.loTable.Controls.Add(this.teCode, 1, 4);
            this.loTable.Controls.Add(this.lblSumm, 0, 2);
            this.loTable.Controls.Add(this.teSumm, 1, 2);
            this.loTable.Controls.Add(this.teOpSumm, 1, 3);
            this.loTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.loTable.Location = new System.Drawing.Point(0, 0);
            this.loTable.Name = "loTable";
            this.loTable.RowCount = 8;
            this.loTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.loTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.loTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.loTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.loTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.loTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.loTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.loTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.loTable.Size = new System.Drawing.Size(344, 258);
            this.loTable.TabIndex = 0;
            // 
            // teName
            // 
            this.teName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.teName.Location = new System.Drawing.Point(140, 3);
            this.teName.Name = "teName";
            this.teName.ReadOnly = true;
            this.teName.Size = new System.Drawing.Size(201, 20);
            this.teName.TabIndex = 0;
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(3, 6);
            this.lblName.Margin = new System.Windows.Forms.Padding(3, 6, 3, 0);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(131, 13);
            this.lblName.TabIndex = 1;
            this.lblName.Text = "Фамилия Имя Отчество";
            // 
            // lblPhone
            // 
            this.lblPhone.AutoSize = true;
            this.lblPhone.Location = new System.Drawing.Point(3, 32);
            this.lblPhone.Margin = new System.Windows.Forms.Padding(3, 6, 3, 0);
            this.lblPhone.Name = "lblPhone";
            this.lblPhone.Size = new System.Drawing.Size(93, 13);
            this.lblPhone.TabIndex = 2;
            this.lblPhone.Text = "Номер телефона";
            // 
            // lblOperationSumm
            // 
            this.lblOperationSumm.AutoSize = true;
            this.lblOperationSumm.Location = new System.Drawing.Point(3, 84);
            this.lblOperationSumm.Margin = new System.Windows.Forms.Padding(3, 6, 3, 0);
            this.lblOperationSumm.Name = "lblOperationSumm";
            this.lblOperationSumm.Size = new System.Drawing.Size(121, 13);
            this.lblOperationSumm.TabIndex = 3;
            this.lblOperationSumm.Text = "Списание/Зачисление";
            // 
            // lblCode
            // 
            this.lblCode.AutoSize = true;
            this.lblCode.Location = new System.Drawing.Point(3, 110);
            this.lblCode.Margin = new System.Windows.Forms.Padding(3, 6, 3, 0);
            this.lblCode.Name = "lblCode";
            this.lblCode.Size = new System.Drawing.Size(77, 13);
            this.lblCode.TabIndex = 4;
            this.lblCode.Text = "Код операции";
            // 
            // tePhone
            // 
            this.tePhone.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tePhone.Location = new System.Drawing.Point(140, 29);
            this.tePhone.Name = "tePhone";
            this.tePhone.Size = new System.Drawing.Size(201, 20);
            this.tePhone.TabIndex = 5;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCancel.Location = new System.Drawing.Point(3, 232);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 8;
            this.btnCancel.Text = "Отмена";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.Location = new System.Drawing.Point(227, 232);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(114, 23);
            this.btnOk.TabIndex = 9;
            this.btnOk.Text = "Отправить код";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // gbSendMethod
            // 
            this.loTable.SetColumnSpan(this.gbSendMethod, 2);
            this.gbSendMethod.Controls.Add(this.rbCall);
            this.gbSendMethod.Controls.Add(this.rbSMS);
            this.gbSendMethod.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbSendMethod.Location = new System.Drawing.Point(3, 162);
            this.gbSendMethod.Name = "gbSendMethod";
            this.gbSendMethod.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.gbSendMethod.Size = new System.Drawing.Size(338, 64);
            this.gbSendMethod.TabIndex = 11;
            this.gbSendMethod.TabStop = false;
            this.gbSendMethod.Text = "Способ отправки кода";
            // 
            // rbCall
            // 
            this.rbCall.AutoSize = true;
            this.rbCall.Location = new System.Drawing.Point(7, 41);
            this.rbCall.Name = "rbCall";
            this.rbCall.Size = new System.Drawing.Size(62, 17);
            this.rbCall.TabIndex = 1;
            this.rbCall.Text = "Звонок";
            this.rbCall.UseVisualStyleBackColor = true;
            this.rbCall.CheckedChanged += new System.EventHandler(this.rbCall_CheckedChanged);
            // 
            // rbSMS
            // 
            this.rbSMS.AutoSize = true;
            this.rbSMS.Checked = true;
            this.rbSMS.Location = new System.Drawing.Point(7, 17);
            this.rbSMS.Name = "rbSMS";
            this.rbSMS.Size = new System.Drawing.Size(48, 17);
            this.rbSMS.TabIndex = 0;
            this.rbSMS.TabStop = true;
            this.rbSMS.Text = "СМС";
            this.rbSMS.UseVisualStyleBackColor = true;
            this.rbSMS.CheckedChanged += new System.EventHandler(this.rbSMS_CheckedChanged);
            // 
            // btnSendAgain
            // 
            this.btnSendAgain.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSendAgain.Location = new System.Drawing.Point(140, 133);
            this.btnSendAgain.Name = "btnSendAgain";
            this.btnSendAgain.Size = new System.Drawing.Size(201, 23);
            this.btnSendAgain.TabIndex = 12;
            this.btnSendAgain.Text = "Повторить отправку кода";
            this.btnSendAgain.UseVisualStyleBackColor = true;
            this.btnSendAgain.Click += new System.EventHandler(this.btnSendAgain_Click);
            // 
            // teCode
            // 
            this.teCode.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.teCode.Location = new System.Drawing.Point(140, 107);
            this.teCode.Mask = "000";
            this.teCode.Name = "teCode";
            this.teCode.Size = new System.Drawing.Size(201, 20);
            this.teCode.TabIndex = 13;
            this.teCode.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lblSumm
            // 
            this.lblSumm.AutoSize = true;
            this.lblSumm.Location = new System.Drawing.Point(3, 58);
            this.lblSumm.Margin = new System.Windows.Forms.Padding(3, 6, 3, 0);
            this.lblSumm.Name = "lblSumm";
            this.lblSumm.Size = new System.Drawing.Size(41, 13);
            this.lblSumm.TabIndex = 14;
            this.lblSumm.Text = "Сумма";
            // 
            // teSumm
            // 
            this.teSumm.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.teSumm.Location = new System.Drawing.Point(140, 55);
            this.teSumm.MaxValue = 9999999D;
            this.teSumm.Name = "teSumm";
            this.teSumm.Size = new System.Drawing.Size(201, 20);
            this.teSumm.TabIndex = 17;
            this.teSumm.TextChanged += new System.EventHandler(this.teSumm_TextChanged);
            // 
            // teOpSumm
            // 
            this.teOpSumm.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.teOpSumm.Location = new System.Drawing.Point(140, 81);
            this.teOpSumm.MaxValue = 9999999D;
            this.teOpSumm.Name = "teOpSumm";
            this.teOpSumm.Size = new System.Drawing.Size(201, 20);
            this.teOpSumm.TabIndex = 18;
            // 
            // BonusOperationsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(344, 258);
            this.Controls.Add(this.loTable);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(360, 270);
            this.Name = "BonusOperationsForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Списание/Зачисление";
            this.loTable.ResumeLayout(false);
            this.loTable.PerformLayout();
            this.gbSendMethod.ResumeLayout(false);
            this.gbSendMethod.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private TableLayoutPanel loTable;
        private TextBox teName;
        private Label lblName;
        private Label lblPhone;
        private Label lblOperationSumm;
        private Label lblCode;
        private TextBox tePhone;
        private Button btnCancel;
        private Button btnOk;
        private GroupBox gbSendMethod;
        private RadioButton rbCall;
        private RadioButton rbSMS;
        private Button btnSendAgain;
        private MaskedTextBox teCode;
        private Label lblSumm;
        private SummEdit teSumm;
        private SummEdit teOpSumm;
    }
}