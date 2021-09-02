namespace MoeYanPOS
{
    partial class frmSaleOrder
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSaleOrder));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgvsaleorder = new System.Windows.Forms.DataGridView();
            this.colno = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colItem = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.coldescription = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colType = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.colqty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colsaleprice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTotal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OrderDetailID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label7 = new System.Windows.Forms.Label();
            this.txttotal = new System.Windows.Forms.TextBox();
            this.btnsave = new System.Windows.Forms.Button();
            this.btnpreview = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.btnnew = new System.Windows.Forms.Button();
            this.btnclose = new System.Windows.Forms.Button();
            this.pnlSaleEntry = new System.Windows.Forms.Panel();
            this.picClose1 = new System.Windows.Forms.PictureBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtadvance = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtbalance = new System.Windows.Forms.TextBox();
            this.lblTransactionID = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtvoucherno = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dtporderdate = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lblsaleorderid = new System.Windows.Forms.Label();
            this.txtitem = new System.Windows.Forms.TextBox();
            this.txtsystemvoucherno = new System.Windows.Forms.TextBox();
            this.lbluserid = new System.Windows.Forms.Label();
            this.txtCustomer = new System.Windows.Forms.TextBox();
            this.btnCustomer = new System.Windows.Forms.Button();
            this.lblCustomerID = new System.Windows.Forms.Label();
            this.dtpdeliverydate = new System.Windows.Forms.DateTimePicker();
            this.txtremark = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dgvItemCode = new System.Windows.Forms.DataGridView();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ItemCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NameinEng = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NameInMM = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Price = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label13 = new System.Windows.Forms.Label();
            this.dgvCustomer = new System.Windows.Forms.DataGridView();
            this.colID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCustomerID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCustomerName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAddress = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colWholeSale = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cbopaymenttype = new System.Windows.Forms.ComboBox();
            this.lblCustomerName = new System.Windows.Forms.Label();
            this.chkwholesale = new System.Windows.Forms.CheckBox();
            this.cboLocation = new System.Windows.Forms.ComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvsaleorder)).BeginInit();
            this.pnlSaleEntry.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picClose1)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItemCode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCustomer)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvsaleorder
            // 
            this.dgvsaleorder.AllowUserToAddRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Zawgyi-One", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Black;
            this.dgvsaleorder.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvsaleorder.BackgroundColor = System.Drawing.Color.Lavender;
            this.dgvsaleorder.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvsaleorder.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colno,
            this.colItem,
            this.coldescription,
            this.colType,
            this.colqty,
            this.colsaleprice,
            this.colTotal,
            this.OrderDetailID});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Zawgyi-One", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvsaleorder.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvsaleorder.Location = new System.Drawing.Point(12, 294);
            this.dgvsaleorder.MultiSelect = false;
            this.dgvsaleorder.Name = "dgvsaleorder";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvsaleorder.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvsaleorder.Size = new System.Drawing.Size(857, 288);
            this.dgvsaleorder.TabIndex = 12;
            this.dgvsaleorder.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvsaleorder_CellEndEdit);
            this.dgvsaleorder.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgvsaleorder_KeyDown);
            // 
            // colno
            // 
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Zawgyi-One", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.colno.DefaultCellStyle = dataGridViewCellStyle2;
            this.colno.HeaderText = "No";
            this.colno.Name = "colno";
            this.colno.ReadOnly = true;
            this.colno.Width = 80;
            // 
            // colItem
            // 
            this.colItem.HeaderText = "Item/Code";
            this.colItem.Name = "colItem";
            this.colItem.Width = 90;
            // 
            // coldescription
            // 
            this.coldescription.HeaderText = "Description";
            this.coldescription.Name = "coldescription";
            this.coldescription.ReadOnly = true;
            this.coldescription.Width = 220;
            // 
            // colType
            // 
            this.colType.HeaderText = "Type";
            this.colType.Name = "colType";
            this.colType.Visible = false;
            // 
            // colqty
            // 
            this.colqty.HeaderText = "Qty";
            this.colqty.Name = "colqty";
            this.colqty.Width = 90;
            // 
            // colsaleprice
            // 
            this.colsaleprice.HeaderText = "SalePrice";
            this.colsaleprice.Name = "colsaleprice";
            this.colsaleprice.Width = 90;
            // 
            // colTotal
            // 
            this.colTotal.HeaderText = "Total";
            this.colTotal.Name = "colTotal";
            // 
            // OrderDetailID
            // 
            this.OrderDetailID.HeaderText = "OrderDetailID";
            this.OrderDetailID.Name = "OrderDetailID";
            this.OrderDetailID.Visible = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Zawgyi-One", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(683, 592);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(39, 21);
            this.label7.TabIndex = 13;
            this.label7.Text = "Total";
            // 
            // txttotal
            // 
            this.txttotal.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txttotal.Font = new System.Drawing.Font("Zawgyi-One", 9F);
            this.txttotal.Location = new System.Drawing.Point(731, 590);
            this.txttotal.Multiline = true;
            this.txttotal.Name = "txttotal";
            this.txttotal.Size = new System.Drawing.Size(138, 31);
            this.txttotal.TabIndex = 14;
            this.txttotal.Text = "0";
            this.txttotal.TextChanged += new System.EventHandler(this.txttotal_TextChanged_1);
            this.txttotal.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txttotal_KeyDown);
            // 
            // btnsave
            // 
            this.btnsave.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnsave.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnsave.BackgroundImage")));
            this.btnsave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnsave.Font = new System.Drawing.Font("Zawgyi-One", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnsave.ForeColor = System.Drawing.Color.White;
            this.btnsave.Location = new System.Drawing.Point(796, 47);
            this.btnsave.Name = "btnsave";
            this.btnsave.Size = new System.Drawing.Size(102, 32);
            this.btnsave.TabIndex = 18;
            this.btnsave.Text = "&Save";
            this.btnsave.UseVisualStyleBackColor = false;
            this.btnsave.Click += new System.EventHandler(this.btnsave_Click);
            // 
            // btnpreview
            // 
            this.btnpreview.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnpreview.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnpreview.BackgroundImage")));
            this.btnpreview.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnpreview.Font = new System.Drawing.Font("Zawgyi-One", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnpreview.ForeColor = System.Drawing.Color.White;
            this.btnpreview.Location = new System.Drawing.Point(796, 77);
            this.btnpreview.Name = "btnpreview";
            this.btnpreview.Size = new System.Drawing.Size(102, 32);
            this.btnpreview.TabIndex = 18;
            this.btnpreview.Text = "&Preview";
            this.btnpreview.UseVisualStyleBackColor = false;
            this.btnpreview.Click += new System.EventHandler(this.btnpreview_Click);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.BackColor = System.Drawing.Color.Transparent;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label11.Location = new System.Drawing.Point(11, 4);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(165, 24);
            this.label11.TabIndex = 21;
            this.label11.Text = "Sale Order Entry";
            this.label11.Click += new System.EventHandler(this.label11_Click);
            // 
            // btnnew
            // 
            this.btnnew.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnnew.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnnew.BackgroundImage")));
            this.btnnew.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnnew.Font = new System.Drawing.Font("Zawgyi-One", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnnew.ForeColor = System.Drawing.Color.White;
            this.btnnew.Location = new System.Drawing.Point(796, 108);
            this.btnnew.Name = "btnnew";
            this.btnnew.Size = new System.Drawing.Size(102, 32);
            this.btnnew.TabIndex = 18;
            this.btnnew.Text = "&Clear";
            this.btnnew.UseVisualStyleBackColor = false;
            this.btnnew.Click += new System.EventHandler(this.btnnew_Click);
            // 
            // btnclose
            // 
            this.btnclose.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnclose.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnclose.BackgroundImage")));
            this.btnclose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnclose.Font = new System.Drawing.Font("Zawgyi-One", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnclose.ForeColor = System.Drawing.Color.White;
            this.btnclose.Location = new System.Drawing.Point(796, 139);
            this.btnclose.Name = "btnclose";
            this.btnclose.Size = new System.Drawing.Size(102, 32);
            this.btnclose.TabIndex = 18;
            this.btnclose.Text = "&Close";
            this.btnclose.UseVisualStyleBackColor = false;
            this.btnclose.Click += new System.EventHandler(this.btnclose_Click);
            // 
            // pnlSaleEntry
            // 
            this.pnlSaleEntry.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.pnlSaleEntry.BackgroundImage = global::MoeYanPOS.Properties.Resources.titlebar;
            this.pnlSaleEntry.Controls.Add(this.picClose1);
            this.pnlSaleEntry.Controls.Add(this.label11);
            this.pnlSaleEntry.Location = new System.Drawing.Point(1, 1);
            this.pnlSaleEntry.Name = "pnlSaleEntry";
            this.pnlSaleEntry.Size = new System.Drawing.Size(910, 36);
            this.pnlSaleEntry.TabIndex = 36;
            // 
            // picClose1
            // 
            this.picClose1.BackColor = System.Drawing.Color.Transparent;
            this.picClose1.BackgroundImage = global::MoeYanPOS.Properties.Resources.test3;
            this.picClose1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.picClose1.Image = global::MoeYanPOS.Properties.Resources.close3;
            this.picClose1.Location = new System.Drawing.Point(874, 1);
            this.picClose1.Name = "picClose1";
            this.picClose1.Size = new System.Drawing.Size(34, 34);
            this.picClose1.TabIndex = 39;
            this.picClose1.TabStop = false;
            this.picClose1.Click += new System.EventHandler(this.picClose1_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Zawgyi-One", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(668, 640);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(58, 21);
            this.label8.TabIndex = 13;
            this.label8.Text = "Advance";
            // 
            // txtadvance
            // 
            this.txtadvance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtadvance.Font = new System.Drawing.Font("Zawgyi-One", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtadvance.Location = new System.Drawing.Point(731, 638);
            this.txtadvance.Name = "txtadvance";
            this.txtadvance.Size = new System.Drawing.Size(138, 27);
            this.txtadvance.TabIndex = 37;
            this.txtadvance.Text = "0";
            this.txtadvance.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtadvance_KeyDown);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Zawgyi-One", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(672, 685);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(54, 21);
            this.label9.TabIndex = 13;
            this.label9.Text = "Balance";
            // 
            // txtbalance
            // 
            this.txtbalance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.txtbalance.Font = new System.Drawing.Font("Zawgyi-One", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtbalance.Location = new System.Drawing.Point(731, 683);
            this.txtbalance.Name = "txtbalance";
            this.txtbalance.Size = new System.Drawing.Size(138, 27);
            this.txtbalance.TabIndex = 38;
            this.txtbalance.Text = "0";
            // 
            // lblTransactionID
            // 
            this.lblTransactionID.AutoSize = true;
            this.lblTransactionID.Location = new System.Drawing.Point(13, 88);
            this.lblTransactionID.Name = "lblTransactionID";
            this.lblTransactionID.Size = new System.Drawing.Size(13, 13);
            this.lblTransactionID.TabIndex = 34;
            this.lblTransactionID.Text = "0";
            this.lblTransactionID.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(47, 89);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(81, 15);
            this.label2.TabIndex = 45;
            this.label2.Text = "Voucher No";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(364, 14);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 15);
            this.label3.TabIndex = 44;
            this.label3.Text = "Item";
            // 
            // txtvoucherno
            // 
            this.txtvoucherno.Font = new System.Drawing.Font("Zawgyi-One", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtvoucherno.Location = new System.Drawing.Point(134, 83);
            this.txtvoucherno.Name = "txtvoucherno";
            this.txtvoucherno.Size = new System.Drawing.Size(162, 27);
            this.txtvoucherno.TabIndex = 46;
            this.txtvoucherno.Visible = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(47, 19);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 15);
            this.label4.TabIndex = 47;
            this.label4.Text = "Order Date";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(32, 124);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 15);
            this.label1.TabIndex = 47;
            this.label1.Text = "Delivery Date";
            this.label1.Click += new System.EventHandler(this.label4_Click);
            // 
            // dtporderdate
            // 
            this.dtporderdate.CustomFormat = "dd-MM-yyyy";
            this.dtporderdate.Font = new System.Drawing.Font("Zawgyi-One", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtporderdate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtporderdate.Location = new System.Drawing.Point(134, 13);
            this.dtporderdate.Name = "dtporderdate";
            this.dtporderdate.Size = new System.Drawing.Size(162, 27);
            this.dtporderdate.TabIndex = 48;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(331, 54);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(68, 15);
            this.label5.TabIndex = 49;
            this.label5.Text = "Customer";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(342, 96);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(57, 15);
            this.label6.TabIndex = 49;
            this.label6.Text = "Remark";
            // 
            // lblsaleorderid
            // 
            this.lblsaleorderid.AutoSize = true;
            this.lblsaleorderid.Location = new System.Drawing.Point(478, 78);
            this.lblsaleorderid.Name = "lblsaleorderid";
            this.lblsaleorderid.Size = new System.Drawing.Size(13, 13);
            this.lblsaleorderid.TabIndex = 50;
            this.lblsaleorderid.Text = "0";
            this.lblsaleorderid.Visible = false;
            // 
            // txtitem
            // 
            this.txtitem.Font = new System.Drawing.Font("Zawgyi-One", 9F);
            this.txtitem.Location = new System.Drawing.Point(416, 10);
            this.txtitem.Name = "txtitem";
            this.txtitem.Size = new System.Drawing.Size(163, 27);
            this.txtitem.TabIndex = 1;
            this.txtitem.TextChanged += new System.EventHandler(this.txtitem_TextChanged);
            this.txtitem.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtitem_KeyDown);
            // 
            // txtsystemvoucherno
            // 
            this.txtsystemvoucherno.Enabled = false;
            this.txtsystemvoucherno.Font = new System.Drawing.Font("Zawgyi-One", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtsystemvoucherno.Location = new System.Drawing.Point(134, 48);
            this.txtsystemvoucherno.Name = "txtsystemvoucherno";
            this.txtsystemvoucherno.Size = new System.Drawing.Size(162, 27);
            this.txtsystemvoucherno.TabIndex = 52;
            this.txtsystemvoucherno.Text = "0";
            // 
            // lbluserid
            // 
            this.lbluserid.AutoSize = true;
            this.lbluserid.Location = new System.Drawing.Point(99, 107);
            this.lbluserid.Name = "lbluserid";
            this.lbluserid.Size = new System.Drawing.Size(13, 13);
            this.lbluserid.TabIndex = 53;
            this.lbluserid.Text = "0";
            this.lbluserid.Visible = false;
            // 
            // txtCustomer
            // 
            this.txtCustomer.Font = new System.Drawing.Font("Zawgyi-One", 9F);
            this.txtCustomer.Location = new System.Drawing.Point(416, 50);
            this.txtCustomer.Name = "txtCustomer";
            this.txtCustomer.Size = new System.Drawing.Size(163, 27);
            this.txtCustomer.TabIndex = 54;
            this.txtCustomer.TextChanged += new System.EventHandler(this.txtCustomer_TextChanged);
            this.txtCustomer.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCustomer_KeyDown);
            // 
            // btnCustomer
            // 
            this.btnCustomer.Location = new System.Drawing.Point(583, 47);
            this.btnCustomer.Name = "btnCustomer";
            this.btnCustomer.Size = new System.Drawing.Size(24, 29);
            this.btnCustomer.TabIndex = 56;
            this.btnCustomer.Text = "+";
            this.btnCustomer.UseVisualStyleBackColor = true;
            this.btnCustomer.Click += new System.EventHandler(this.btnCustomer_Click);
            // 
            // lblCustomerID
            // 
            this.lblCustomerID.AutoSize = true;
            this.lblCustomerID.Location = new System.Drawing.Point(42, 107);
            this.lblCustomerID.Name = "lblCustomerID";
            this.lblCustomerID.Size = new System.Drawing.Size(13, 13);
            this.lblCustomerID.TabIndex = 57;
            this.lblCustomerID.Text = "0";
            this.lblCustomerID.Visible = false;
            // 
            // dtpdeliverydate
            // 
            this.dtpdeliverydate.CustomFormat = "dd-MM-yyyy";
            this.dtpdeliverydate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpdeliverydate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpdeliverydate.Location = new System.Drawing.Point(134, 124);
            this.dtpdeliverydate.Name = "dtpdeliverydate";
            this.dtpdeliverydate.Size = new System.Drawing.Size(162, 21);
            this.dtpdeliverydate.TabIndex = 61;
            // 
            // txtremark
            // 
            this.txtremark.Font = new System.Drawing.Font("Zawgyi-One", 9F);
            this.txtremark.Location = new System.Drawing.Point(416, 92);
            this.txtremark.Multiline = true;
            this.txtremark.Name = "txtremark";
            this.txtremark.Size = new System.Drawing.Size(342, 84);
            this.txtremark.TabIndex = 62;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.dgvItemCode);
            this.panel1.Controls.Add(this.label13);
            this.panel1.Controls.Add(this.dgvCustomer);
            this.panel1.Controls.Add(this.cbopaymenttype);
            this.panel1.Controls.Add(this.lblCustomerName);
            this.panel1.Controls.Add(this.chkwholesale);
            this.panel1.Controls.Add(this.cboLocation);
            this.panel1.Controls.Add(this.label12);
            this.panel1.Controls.Add(this.label10);
            this.panel1.Controls.Add(this.txtremark);
            this.panel1.Controls.Add(this.dtpdeliverydate);
            this.panel1.Controls.Add(this.lblCustomerID);
            this.panel1.Controls.Add(this.btnCustomer);
            this.panel1.Controls.Add(this.txtCustomer);
            this.panel1.Controls.Add(this.lbluserid);
            this.panel1.Controls.Add(this.txtsystemvoucherno);
            this.panel1.Controls.Add(this.txtitem);
            this.panel1.Controls.Add(this.lblsaleorderid);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.dtporderdate);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.txtvoucherno);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.lblTransactionID);
            this.panel1.Location = new System.Drawing.Point(12, 50);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(778, 238);
            this.panel1.TabIndex = 35;
            // 
            // dgvItemCode
            // 
            this.dgvItemCode.AllowUserToAddRows = false;
            this.dgvItemCode.AllowUserToDeleteRows = false;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.InactiveBorder;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Zawgyi-One", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvItemCode.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle5;
            this.dgvItemCode.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dgvItemCode.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvItemCode.ColumnHeadersVisible = false;
            this.dgvItemCode.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ID,
            this.ItemCode,
            this.NameinEng,
            this.NameInMM,
            this.Price});
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Zawgyi-One", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvItemCode.DefaultCellStyle = dataGridViewCellStyle6;
            this.dgvItemCode.Location = new System.Drawing.Point(416, 36);
            this.dgvItemCode.Name = "dgvItemCode";
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Zawgyi-One", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvItemCode.RowHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.dgvItemCode.RowHeadersVisible = false;
            this.dgvItemCode.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvItemCode.Size = new System.Drawing.Size(359, 103);
            this.dgvItemCode.TabIndex = 60;
            this.dgvItemCode.Visible = false;
            this.dgvItemCode.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvItemCode_CellContentClick);
            this.dgvItemCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgvItemCode_KeyDown);
            // 
            // ID
            // 
            this.ID.HeaderText = "ID";
            this.ID.Name = "ID";
            this.ID.Visible = false;
            // 
            // ItemCode
            // 
            this.ItemCode.HeaderText = "ItemCode";
            this.ItemCode.Name = "ItemCode";
            this.ItemCode.Width = 90;
            // 
            // NameinEng
            // 
            this.NameinEng.HeaderText = "NameInEng";
            this.NameinEng.Name = "NameinEng";
            // 
            // NameInMM
            // 
            this.NameInMM.HeaderText = "NameInMM";
            this.NameInMM.Name = "NameInMM";
            this.NameInMM.Width = 150;
            // 
            // Price
            // 
            this.Price.HeaderText = "Price";
            this.Price.Name = "Price";
            this.Price.Width = 90;
            // 
            // label13
            // 
            this.label13.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(-3, 52);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(131, 15);
            this.label13.TabIndex = 77;
            this.label13.Text = "System Voucher No";
            // 
            // dgvCustomer
            // 
            this.dgvCustomer.AllowUserToAddRows = false;
            this.dgvCustomer.AllowUserToDeleteRows = false;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Zawgyi-One", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvCustomer.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle8;
            this.dgvCustomer.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dgvCustomer.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCustomer.ColumnHeadersVisible = false;
            this.dgvCustomer.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colID,
            this.colCustomerID,
            this.colCustomerName,
            this.colAddress,
            this.colWholeSale});
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Zawgyi-One", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvCustomer.DefaultCellStyle = dataGridViewCellStyle9;
            this.dgvCustomer.Location = new System.Drawing.Point(416, 78);
            this.dgvCustomer.MultiSelect = false;
            this.dgvCustomer.Name = "dgvCustomer";
            this.dgvCustomer.RowHeadersVisible = false;
            this.dgvCustomer.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvCustomer.Size = new System.Drawing.Size(359, 130);
            this.dgvCustomer.TabIndex = 76;
            this.dgvCustomer.Visible = false;
            this.dgvCustomer.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgvCustomer_KeyDown);
            // 
            // colID
            // 
            this.colID.HeaderText = "ID";
            this.colID.Name = "colID";
            this.colID.ReadOnly = true;
            this.colID.Visible = false;
            // 
            // colCustomerID
            // 
            this.colCustomerID.HeaderText = "CustomerID";
            this.colCustomerID.Name = "colCustomerID";
            this.colCustomerID.ReadOnly = true;
            // 
            // colCustomerName
            // 
            this.colCustomerName.HeaderText = "Name";
            this.colCustomerName.Name = "colCustomerName";
            this.colCustomerName.ReadOnly = true;
            this.colCustomerName.Width = 150;
            // 
            // colAddress
            // 
            this.colAddress.HeaderText = "Address";
            this.colAddress.Name = "colAddress";
            this.colAddress.ReadOnly = true;
            this.colAddress.Visible = false;
            // 
            // colWholeSale
            // 
            this.colWholeSale.HeaderText = "WholeSale";
            this.colWholeSale.Name = "colWholeSale";
            this.colWholeSale.Visible = false;
            this.colWholeSale.Width = 90;
            // 
            // cbopaymenttype
            // 
            this.cbopaymenttype.Font = new System.Drawing.Font("Zawgyi-One", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbopaymenttype.FormattingEnabled = true;
            this.cbopaymenttype.Items.AddRange(new object[] {
            "Cash",
            "Credit"});
            this.cbopaymenttype.Location = new System.Drawing.Point(135, 200);
            this.cbopaymenttype.Name = "cbopaymenttype";
            this.cbopaymenttype.Size = new System.Drawing.Size(161, 29);
            this.cbopaymenttype.TabIndex = 75;
            this.cbopaymenttype.SelectedIndexChanged += new System.EventHandler(this.cbopaymenttype_SelectedIndexChanged);
            // 
            // lblCustomerName
            // 
            this.lblCustomerName.AutoSize = true;
            this.lblCustomerName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCustomerName.Location = new System.Drawing.Point(669, 214);
            this.lblCustomerName.Name = "lblCustomerName";
            this.lblCustomerName.Size = new System.Drawing.Size(106, 15);
            this.lblCustomerName.TabIndex = 74;
            this.lblCustomerName.Text = "CustomerName";
            this.lblCustomerName.Visible = false;
            // 
            // chkwholesale
            // 
            this.chkwholesale.AutoSize = true;
            this.chkwholesale.Font = new System.Drawing.Font("Zawgyi-One", 9.75F, System.Drawing.FontStyle.Bold);
            this.chkwholesale.Location = new System.Drawing.Point(613, 50);
            this.chkwholesale.Name = "chkwholesale";
            this.chkwholesale.Size = new System.Drawing.Size(132, 25);
            this.chkwholesale.TabIndex = 73;
            this.chkwholesale.Text = "WholeSalePrice";
            this.chkwholesale.UseVisualStyleBackColor = true;
            // 
            // cboLocation
            // 
            this.cboLocation.Font = new System.Drawing.Font("Zawgyi-One", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboLocation.FormattingEnabled = true;
            this.cboLocation.Location = new System.Drawing.Point(134, 159);
            this.cboLocation.Name = "cboLocation";
            this.cboLocation.Size = new System.Drawing.Size(162, 29);
            this.cboLocation.TabIndex = 64;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(43, 205);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(81, 15);
            this.label12.TabIndex = 63;
            this.label12.Text = "Payment By";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(62, 164);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(62, 15);
            this.label10.TabIndex = 63;
            this.label10.Text = "Location";
            // 
            // frmSaleOrder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(910, 747);
            this.Controls.Add(this.txtbalance);
            this.Controls.Add(this.txtadvance);
            this.Controls.Add(this.pnlSaleEntry);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnclose);
            this.Controls.Add(this.btnnew);
            this.Controls.Add(this.btnpreview);
            this.Controls.Add(this.btnsave);
            this.Controls.Add(this.txttotal);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.dgvsaleorder);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmSaleOrder";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Sale Order";
            this.Load += new System.EventHandler(this.frmSaleOrder_Load);
            this.Click += new System.EventHandler(this.frmSaleOrder_Click);
            ((System.ComponentModel.ISupportInitialize)(this.dgvsaleorder)).EndInit();
            this.pnlSaleEntry.ResumeLayout(false);
            this.pnlSaleEntry.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picClose1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItemCode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCustomer)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvsaleorder;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txttotal;
        private System.Windows.Forms.Button btnsave;
        private System.Windows.Forms.Button btnpreview;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button btnnew;
        private System.Windows.Forms.Button btnclose;
        private System.Windows.Forms.Panel pnlSaleEntry;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtadvance;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtbalance;
        private System.Windows.Forms.Label lblTransactionID;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtvoucherno;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtporderdate;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblsaleorderid;
        private System.Windows.Forms.TextBox txtitem;
        private System.Windows.Forms.TextBox txtsystemvoucherno;
        private System.Windows.Forms.Label lbluserid;
        private System.Windows.Forms.TextBox txtCustomer;
        private System.Windows.Forms.Button btnCustomer;
        private System.Windows.Forms.Label lblCustomerID;
        private System.Windows.Forms.DateTimePicker dtpdeliverydate;
        private System.Windows.Forms.TextBox txtremark;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView dgvItemCode;
        private System.Windows.Forms.ComboBox cboLocation;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.PictureBox picClose1;
        private System.Windows.Forms.CheckBox chkwholesale;
        private System.Windows.Forms.Label lblCustomerName;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.ComboBox cbopaymenttype;
        private System.Windows.Forms.DataGridView dgvCustomer;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn ItemCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn NameinEng;
        private System.Windows.Forms.DataGridViewTextBoxColumn NameInMM;
        private System.Windows.Forms.DataGridViewTextBoxColumn Price;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.DataGridViewTextBoxColumn colID;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCustomerID;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCustomerName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAddress;
        private System.Windows.Forms.DataGridViewTextBoxColumn colWholeSale;
        private System.Windows.Forms.DataGridViewTextBoxColumn colno;
        private System.Windows.Forms.DataGridViewTextBoxColumn colItem;
        private System.Windows.Forms.DataGridViewTextBoxColumn coldescription;
        private System.Windows.Forms.DataGridViewComboBoxColumn colType;
        private System.Windows.Forms.DataGridViewTextBoxColumn colqty;
        private System.Windows.Forms.DataGridViewTextBoxColumn colsaleprice;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTotal;
        private System.Windows.Forms.DataGridViewTextBoxColumn OrderDetailID;
    }
}