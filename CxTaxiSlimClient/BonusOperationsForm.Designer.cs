using System.ComponentModel;
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
            this.lblSumm = new System.Windows.Forms.Label();
            this.lblCode = new System.Windows.Forms.Label();
            this.tePhone = new System.Windows.Forms.TextBox();
            this.teCode = new System.Windows.Forms.TextBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.teSumm = new System.Windows.Forms.NumericUpDown();
            this.loTable.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.teSumm)).BeginInit();
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
            this.loTable.Controls.Add(this.lblSumm, 0, 2);
            this.loTable.Controls.Add(this.lblCode, 0, 3);
            this.loTable.Controls.Add(this.tePhone, 1, 1);
            this.loTable.Controls.Add(this.teCode, 1, 3);
            this.loTable.Controls.Add(this.btnCancel, 0, 5);
            this.loTable.Controls.Add(this.btnOk, 1, 5);
            this.loTable.Controls.Add(this.teSumm, 1, 2);
            this.loTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.loTable.Location = new System.Drawing.Point(0, 0);
            this.loTable.Name = "loTable";
            this.loTable.RowCount = 6;
            this.loTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.loTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.loTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.loTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.loTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.loTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.loTable.Size = new System.Drawing.Size(344, 142);
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
            // lblSumm
            // 
            this.lblSumm.AutoSize = true;
            this.lblSumm.Location = new System.Drawing.Point(3, 58);
            this.lblSumm.Margin = new System.Windows.Forms.Padding(3, 6, 3, 0);
            this.lblSumm.Name = "lblSumm";
            this.lblSumm.Size = new System.Drawing.Size(41, 13);
            this.lblSumm.TabIndex = 3;
            this.lblSumm.Text = "Сумма";
            // 
            // lblCode
            // 
            this.lblCode.AutoSize = true;
            this.lblCode.Location = new System.Drawing.Point(3, 84);
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
            // teCode
            // 
            this.teCode.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.teCode.Location = new System.Drawing.Point(140, 81);
            this.teCode.Name = "teCode";
            this.teCode.Size = new System.Drawing.Size(201, 20);
            this.teCode.TabIndex = 7;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCancel.Location = new System.Drawing.Point(3, 116);
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
            this.btnOk.Location = new System.Drawing.Point(227, 116);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(114, 23);
            this.btnOk.TabIndex = 9;
            this.btnOk.Text = "Отправить код";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // teSumm
            // 
            this.teSumm.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.teSumm.Location = new System.Drawing.Point(140, 55);
            this.teSumm.Maximum = new decimal(new int[] {
            999999,
            0,
            0,
            0});
            this.teSumm.Name = "teSumm";
            this.teSumm.Size = new System.Drawing.Size(201, 20);
            this.teSumm.TabIndex = 10;
            // 
            // BonusOperationsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(344, 142);
            this.Controls.Add(this.loTable);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(360, 180);
            this.Name = "BonusOperationsForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "BonusOperationsForm";
            this.loTable.ResumeLayout(false);
            this.loTable.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.teSumm)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private TableLayoutPanel loTable;
        private TextBox teName;
        private Label lblName;
        private Label lblPhone;
        private Label lblSumm;
        private Label lblCode;
        private TextBox tePhone;
        private TextBox teCode;
        private Button btnCancel;
        private Button btnOk;
        private NumericUpDown teSumm;
    }
}