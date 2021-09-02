namespace MoeYanPOS.UI
{
    partial class frmPurchaseHistory
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPurchaseHistory));
            this.pnlSaleEntry = new System.Windows.Forms.Panel();
            this.picClose1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dgvitem = new System.Windows.Forms.DataGridView();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colItemCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NameinEng = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NameinMM = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvsupplier = new System.Windows.Forms.DataGridView();
            this.SupplierISD = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SupplierName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Address = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Phone = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cbolocation = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.cbouser = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.cboCategory = new System.Windows.Forms.ComboBox();
            this.cboClass = new System.Windows.Forms.ComboBox();
            this.dtpto = new System.Windows.Forms.DateTimePicker();
            this.dtpfrom = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtitem = new System.Windows.Forms.TextBox();
            this.lblsupplierid = new System.Windows.Forms.Label();
            this.txtvoucher = new System.Windows.Forms.TextBox();
            this.txtsupplier = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnsearch = new System.Windows.Forms.Button();
            this.btnPurchaseReport = new System.Windows.Forms.Button();
            this.btndetailreport = new System.Windows.Forms.Button();
            this.dgvpurchasedetail = new System.Windows.Forms.DataGridView();
            this.colitem = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Description = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Type = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.Qty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SalePrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.coltotal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FOC = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.ItemDispercent = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ItemDis = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnclose = new System.Windows.Forms.Button();
            this.PurchaseID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvpurchase = new System.Windows.Forms.DataGridView();
            this.PID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.User = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Supplier = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colVoucherNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PaymentBy = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.currency = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Total = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Advance = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Discount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GrandTotal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnEdit = new System.Windows.Forms.DataGridViewButtonColumn();
            this.btndelete = new System.Windows.Forms.DataGridViewButtonColumn();
            this.pnlSaleEntry.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picClose1)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvitem)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvsupplier)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvpurchasedetail)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvpurchase)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlSaleEntry
            // 
            this.pnlSaleEntry.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.pnlSaleEntry.BackgroundImage = global::MoeYanPOS.Properties.Resources.titlebar;
            this.pnlSaleEntry.Controls.Add(this.picClose1);
            this.pnlSaleEntry.Controls.Add(this.label1);
            this.pnlSaleEntry.Location = new System.Drawing.Point(1, 0);
            this.pnlSaleEntry.Name = "pnlSaleEntry";
            this.pnlSaleEntry.Size = new System.Drawing.Size(1003, 36);
            this.pnlSaleEntry.TabIndex = 2;
            // 
            // picClose1
            // 
            this.picClose1.BackColor = System.Drawing.Color.Transparent;
            this.picClose1.BackgroundImage = global::MoeYanPOS.Properties.Resources.test3;
            this.picClose1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.picClose1.Image = global::MoeYanPOS.Properties.Resources.close3;
            this.picClose1.Location = new System.Drawing.Point(966, 0);
            this.picClose1.Name = "picClose1";
            this.picClose1.Size = new System.Drawing.Size(34, 34);
            this.picClose1.TabIndex = 4;
            this.picClose1.TabStop = false;
            this.picClose1.Click += new System.EventHandler(this.picClose1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label1.Location = new System.Drawing.Point(3, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(168, 24);
            this.label1.TabIndex = 0;
            this.label1.Text = "Purchase History";
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.White;
            this.groupBox1.Controls.Add(this.dgvitem);
            this.groupBox1.Controls.Add(this.dgvsupplier);
            this.groupBox1.Controls.Add(this.cbolocation);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.cbouser);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.cboCategory);
            this.groupBox1.Controls.Add(this.cboClass);
            this.groupBox1.Controls.Add(this.dtpto);
            this.groupBox1.Controls.Add(this.dtpfrom);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtitem);
            this.groupBox1.Controls.Add(this.lblsupplierid);
            this.groupBox1.Controls.Add(this.txtvoucher);
            this.groupBox1.Controls.Add(this.txtsupplier);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Location = new System.Drawing.Point(13, 42);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(861, 197);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Search By";
            // 
            // dgvitem
            // 
            this.dgvitem.AllowUserToAddRows = false;
            this.dgvitem.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Zawgyi-One", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvitem.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvitem.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dgvitem.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvitem.ColumnHeadersVisible = false;
            this.dgvitem.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ID,
            this.colItemCode,
            this.NameinEng,
            this.NameinMM,
            this.colPrice});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Zawgyi-One", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvitem.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvitem.Location = new System.Drawing.Point(331, 97);
            this.dgvitem.MultiSelect = false;
            this.dgvitem.Name = "dgvitem";
            this.dgvitem.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvitem.Size = new System.Drawing.Size(488, 94);
            this.dgvitem.TabIndex = 9;
            this.dgvitem.Visible = false;
            this.dgvitem.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgvitem_KeyDown);
            // 
            // ID
            // 
            this.ID.HeaderText = "ID";
            this.ID.Name = "ID";
            this.ID.Visible = false;
            // 
            // colItemCode
            // 
            this.colItemCode.HeaderText = "ItemCode";
            this.colItemCode.Name = "colItemCode";
            this.colItemCode.Width = 90;
            // 
            // NameinEng
            // 
            this.NameinEng.HeaderText = "NameinEng";
            this.NameinEng.Name = "NameinEng";
            // 
            // NameinMM
            // 
            this.NameinMM.HeaderText = "NameinMM";
            this.NameinMM.Name = "NameinMM";
            this.NameinMM.Width = 150;
            // 
            // colPrice
            // 
            this.colPrice.HeaderText = "PurchasePrice";
            this.colPrice.Name = "colPrice";
            this.colPrice.Width = 90;
            // 
            // dgvsupplier
            // 
            this.dgvsupplier.AllowUserToAddRows = false;
            this.dgvsupplier.AllowUserToDeleteRows = false;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Zawgyi-One", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvsupplier.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvsupplier.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dgvsupplier.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvsupplier.ColumnHeadersVisible = false;
            this.dgvsupplier.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SupplierISD,
            this.SupplierName,
            this.Address,
            this.Phone});
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Zawgyi-One", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvsupplier.DefaultCellStyle = dataGridViewCellStyle4;
            this.dgvsupplier.Location = new System.Drawing.Point(567, 58);
            this.dgvsupplier.MultiSelect = false;
            this.dgvsupplier.Name = "dgvsupplier";
            this.dgvsupplier.RowHeadersVisible = false;
            this.dgvsupplier.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvsupplier.Size = new System.Drawing.Size(288, 133);
            this.dgvsupplier.TabIndex = 44;
            this.dgvsupplier.Visible = false;
            this.dgvsupplier.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgvsupplier_KeyDown);
            // 
            // SupplierISD
            // 
            this.SupplierISD.HeaderText = "SupplierID";
            this.SupplierISD.Name = "SupplierISD";
            this.SupplierISD.Visible = false;
            // 
            // SupplierName
            // 
            this.SupplierName.HeaderText = "SupplierName";
            this.SupplierName.Name = "SupplierName";
            // 
            // Address
            // 
            this.Address.HeaderText = "Address";
            this.Address.Name = "Address";
            // 
            // Phone
            // 
            this.Phone.HeaderText = "Phone";
            this.Phone.Name = "Phone";
            // 
            // cbolocation
            // 
            this.cbolocation.Font = new System.Drawing.Font("Zawgyi-One", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbolocation.FormattingEnabled = true;
            this.cbolocation.Location = new System.Drawing.Point(73, 113);
            this.cbolocation.Name = "cbolocation";
            this.cbolocation.Size = new System.Drawing.Size(166, 29);
            this.cbolocation.TabIndex = 43;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(527, 122);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(33, 13);
            this.label9.TabIndex = 42;
            this.label9.Text = "User";
            // 
            // cbouser
            // 
            this.cbouser.Font = new System.Drawing.Font("Zawgyi-One", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbouser.FormattingEnabled = true;
            this.cbouser.Location = new System.Drawing.Point(567, 114);
            this.cbouser.Name = "cbouser";
            this.cbouser.Size = new System.Drawing.Size(164, 29);
            this.cbouser.TabIndex = 41;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Zawgyi-One", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(6, 120);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(63, 20);
            this.label10.TabIndex = 39;
            this.label10.Text = "Location";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Zawgyi-One", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(256, 120);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(66, 20);
            this.label8.TabIndex = 39;
            this.label8.Text = "Category";
            // 
            // cboCategory
            // 
            this.cboCategory.Font = new System.Drawing.Font("Zawgyi-One", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboCategory.FormattingEnabled = true;
            this.cboCategory.Location = new System.Drawing.Point(331, 113);
            this.cboCategory.Name = "cboCategory";
            this.cboCategory.Size = new System.Drawing.Size(163, 29);
            this.cboCategory.TabIndex = 38;
            // 
            // cboClass
            // 
            this.cboClass.Font = new System.Drawing.Font("Zawgyi-One", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboClass.FormattingEnabled = true;
            this.cboClass.Location = new System.Drawing.Point(567, 68);
            this.cboClass.Name = "cboClass";
            this.cboClass.Size = new System.Drawing.Size(164, 29);
            this.cboClass.TabIndex = 36;
            // 
            // dtpto
            // 
            this.dtpto.CustomFormat = "dd-MM-yyyy";
            this.dtpto.Font = new System.Drawing.Font("Zawgyi-One", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpto.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpto.Location = new System.Drawing.Point(73, 71);
            this.dtpto.Name = "dtpto";
            this.dtpto.Size = new System.Drawing.Size(166, 27);
            this.dtpto.TabIndex = 13;
            // 
            // dtpfrom
            // 
            this.dtpfrom.CustomFormat = "dd-MM-yyyy";
            this.dtpfrom.Font = new System.Drawing.Font("Zawgyi-One", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpfrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpfrom.Location = new System.Drawing.Point(73, 28);
            this.dtpfrom.Name = "dtpfrom";
            this.dtpfrom.Size = new System.Drawing.Size(166, 27);
            this.dtpfrom.TabIndex = 12;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Zawgyi-One", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(30, 74);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(26, 20);
            this.label3.TabIndex = 10;
            this.label3.Text = "To";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Zawgyi-One", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(18, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 20);
            this.label2.TabIndex = 11;
            this.label2.Text = "From";
            // 
            // txtitem
            // 
            this.txtitem.Font = new System.Drawing.Font("Zawgyi-One", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtitem.Location = new System.Drawing.Point(331, 70);
            this.txtitem.Name = "txtitem";
            this.txtitem.Size = new System.Drawing.Size(163, 27);
            this.txtitem.TabIndex = 8;
            this.txtitem.TextChanged += new System.EventHandler(this.txtitem_TextChanged);
            // 
            // lblsupplierid
            // 
            this.lblsupplierid.AutoSize = true;
            this.lblsupplierid.Location = new System.Drawing.Point(347, 8);
            this.lblsupplierid.Name = "lblsupplierid";
            this.lblsupplierid.Size = new System.Drawing.Size(13, 13);
            this.lblsupplierid.TabIndex = 7;
            this.lblsupplierid.Text = "0";
            this.lblsupplierid.Visible = false;
            // 
            // txtvoucher
            // 
            this.txtvoucher.Font = new System.Drawing.Font("Zawgyi-One", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtvoucher.Location = new System.Drawing.Point(331, 28);
            this.txtvoucher.Name = "txtvoucher";
            this.txtvoucher.Size = new System.Drawing.Size(163, 27);
            this.txtvoucher.TabIndex = 5;
            // 
            // txtsupplier
            // 
            this.txtsupplier.Font = new System.Drawing.Font("Zawgyi-One", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtsupplier.Location = new System.Drawing.Point(567, 28);
            this.txtsupplier.Name = "txtsupplier";
            this.txtsupplier.Size = new System.Drawing.Size(164, 27);
            this.txtsupplier.TabIndex = 4;
            this.txtsupplier.TextChanged += new System.EventHandler(this.txtsupplier_TextChanged);
            this.txtsupplier.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtsupplier_KeyDown);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Zawgyi-One", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(283, 74);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(39, 20);
            this.label6.TabIndex = 3;
            this.label6.Text = "Item";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Zawgyi-One", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(260, 31);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(62, 20);
            this.label5.TabIndex = 3;
            this.label5.Text = "Voucher";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Zawgyi-One", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(521, 74);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(39, 20);
            this.label7.TabIndex = 3;
            this.label7.Text = "Class";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Zawgyi-One", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(500, 30);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(60, 20);
            this.label4.TabIndex = 3;
            this.label4.Text = "Supplier";
            // 
            // btnsearch
            // 
            this.btnsearch.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnsearch.BackgroundImage = global::MoeYanPOS.Properties.Resources.steelblue;
            this.btnsearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnsearch.Font = new System.Drawing.Font("Zawgyi-One", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnsearch.ForeColor = System.Drawing.Color.White;
            this.btnsearch.Location = new System.Drawing.Point(880, 48);
            this.btnsearch.Name = "btnsearch";
            this.btnsearch.Size = new System.Drawing.Size(110, 30);
            this.btnsearch.TabIndex = 7;
            this.btnsearch.Text = "&Search";
            this.btnsearch.UseVisualStyleBackColor = false;
            this.btnsearch.Click += new System.EventHandler(this.btnsearch_Click);
            // 
            // btnPurchaseReport
            // 
            this.btnPurchaseReport.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnPurchaseReport.BackgroundImage = global::MoeYanPOS.Properties.Resources.steelblue;
            this.btnPurchaseReport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPurchaseReport.Font = new System.Drawing.Font("Zawgyi-One", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPurchaseReport.ForeColor = System.Drawing.Color.White;
            this.btnPurchaseReport.Location = new System.Drawing.Point(880, 81);
            this.btnPurchaseReport.Name = "btnPurchaseReport";
            this.btnPurchaseReport.Size = new System.Drawing.Size(110, 30);
            this.btnPurchaseReport.TabIndex = 7;
            this.btnPurchaseReport.Text = "&Preview";
            this.btnPurchaseReport.UseVisualStyleBackColor = false;
            this.btnPurchaseReport.Click += new System.EventHandler(this.btnPurchaseReport_Click);
            // 
            // btndetailreport
            // 
            this.btndetailreport.BackColor = System.Drawing.Color.DodgerBlue;
            this.btndetailreport.BackgroundImage = global::MoeYanPOS.Properties.Resources.steelblue;
            this.btndetailreport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btndetailreport.Font = new System.Drawing.Font("Zawgyi-One", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btndetailreport.ForeColor = System.Drawing.Color.White;
            this.btndetailreport.Location = new System.Drawing.Point(880, 114);
            this.btndetailreport.Name = "btndetailreport";
            this.btndetailreport.Size = new System.Drawing.Size(110, 30);
            this.btndetailreport.TabIndex = 7;
            this.btndetailreport.Text = "&Detail";
            this.btndetailreport.UseVisualStyleBackColor = false;
            this.btndetailreport.Click += new System.EventHandler(this.btndetailreport_Click);
            // 
            // dgvpurchasedetail
            // 
            this.dgvpurchasedetail.AllowUserToAddRows = false;
            this.dgvpurchasedetail.AllowUserToDeleteRows = false;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Zawgyi-One", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.Black;
            this.dgvpurchasedetail.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle5;
            this.dgvpurchasedetail.BackgroundColor = System.Drawing.Color.Lavender;
            this.dgvpurchasedetail.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvpurchasedetail.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colitem,
            this.Description,
            this.Type,
            this.Qty,
            this.SalePrice,
            this.coltotal,
            this.FOC,
            this.ItemDispercent,
            this.ItemDis});
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Zawgyi-One", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvpurchasedetail.DefaultCellStyle = dataGridViewCellStyle6;
            this.dgvpurchasedetail.Location = new System.Drawing.Point(13, 410);
            this.dgvpurchasedetail.Name = "dgvpurchasedetail";
            this.dgvpurchasedetail.ReadOnly = true;
            this.dgvpurchasedetail.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvpurchasedetail.Size = new System.Drawing.Size(985, 201);
            this.dgvpurchasedetail.TabIndex = 9;
            // 
            // colitem
            // 
            this.colitem.HeaderText = "ItemCode";
            this.colitem.Name = "colitem";
            this.colitem.ReadOnly = true;
            // 
            // Description
            // 
            this.Description.HeaderText = "Description";
            this.Description.Name = "Description";
            this.Description.ReadOnly = true;
            this.Description.Width = 230;
            // 
            // Type
            // 
            this.Type.HeaderText = "Type";
            this.Type.Name = "Type";
            this.Type.ReadOnly = true;
            this.Type.Visible = false;
            // 
            // Qty
            // 
            this.Qty.HeaderText = "Qty";
            this.Qty.Name = "Qty";
            this.Qty.ReadOnly = true;
            this.Qty.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Qty.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Qty.Width = 90;
            // 
            // SalePrice
            // 
            this.SalePrice.HeaderText = "SalePrice";
            this.SalePrice.Name = "SalePrice";
            this.SalePrice.ReadOnly = true;
            this.SalePrice.Width = 90;
            // 
            // coltotal
            // 
            this.coltotal.HeaderText = "Total";
            this.coltotal.Name = "coltotal";
            this.coltotal.ReadOnly = true;
            // 
            // FOC
            // 
            this.FOC.HeaderText = "FOC";
            this.FOC.Items.AddRange(new object[] {
            "True",
            "False"});
            this.FOC.Name = "FOC";
            this.FOC.ReadOnly = true;
            // 
            // ItemDispercent
            // 
            this.ItemDispercent.HeaderText = "ItemDis%";
            this.ItemDispercent.Name = "ItemDispercent";
            this.ItemDispercent.ReadOnly = true;
            // 
            // ItemDis
            // 
            this.ItemDis.HeaderText = "ItemDis";
            this.ItemDis.Name = "ItemDis";
            this.ItemDis.ReadOnly = true;
            // 
            // btnclose
            // 
            this.btnclose.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnclose.BackgroundImage = global::MoeYanPOS.Properties.Resources.steelblue;
            this.btnclose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnclose.Font = new System.Drawing.Font("Zawgyi-One", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnclose.ForeColor = System.Drawing.Color.White;
            this.btnclose.Location = new System.Drawing.Point(880, 147);
            this.btnclose.Name = "btnclose";
            this.btnclose.Size = new System.Drawing.Size(110, 30);
            this.btnclose.TabIndex = 7;
            this.btnclose.Text = "Close";
            this.btnclose.UseVisualStyleBackColor = false;
            this.btnclose.Click += new System.EventHandler(this.btnclose_Click);
            // 
            // PurchaseID
            // 
            this.PurchaseID.HeaderText = "PurchaseID";
            this.PurchaseID.Name = "PurchaseID";
            this.PurchaseID.Width = 70;
            // 
            // dgvpurchase
            // 
            this.dgvpurchase.AllowUserToAddRows = false;
            this.dgvpurchase.AllowUserToDeleteRows = false;
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Zawgyi-One", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.Color.Black;
            this.dgvpurchase.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle7;
            this.dgvpurchase.BackgroundColor = System.Drawing.Color.Lavender;
            this.dgvpurchase.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvpurchase.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.PID,
            this.User,
            this.Supplier,
            this.colVoucherNo,
            this.PaymentBy,
            this.currency,
            this.Total,
            this.Advance,
            this.Discount,
            this.GrandTotal,
            this.btnEdit,
            this.btndelete});
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Zawgyi-One", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvpurchase.DefaultCellStyle = dataGridViewCellStyle8;
            this.dgvpurchase.Location = new System.Drawing.Point(12, 246);
            this.dgvpurchase.Name = "dgvpurchase";
            this.dgvpurchase.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvpurchase.Size = new System.Drawing.Size(986, 158);
            this.dgvpurchase.TabIndex = 10;
            this.dgvpurchase.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvpurchase_CellClick);
            this.dgvpurchase.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvpurchase_CellContentClick);
            this.dgvpurchase.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvpurchase_CellEnter);
            // 
            // PID
            // 
            this.PID.HeaderText = "PurchaseID";
            this.PID.Name = "PID";
            this.PID.Visible = false;
            this.PID.Width = 70;
            // 
            // User
            // 
            this.User.HeaderText = "User";
            this.User.Name = "User";
            // 
            // Supplier
            // 
            this.Supplier.HeaderText = "Supplier";
            this.Supplier.Name = "Supplier";
            // 
            // colVoucherNo
            // 
            this.colVoucherNo.HeaderText = "VoucherNo";
            this.colVoucherNo.Name = "colVoucherNo";
            this.colVoucherNo.Width = 150;
            // 
            // PaymentBy
            // 
            this.PaymentBy.HeaderText = "PaymentBy";
            this.PaymentBy.Name = "PaymentBy";
            this.PaymentBy.Visible = false;
            this.PaymentBy.Width = 70;
            // 
            // currency
            // 
            this.currency.HeaderText = "Currency";
            this.currency.Name = "currency";
            this.currency.Visible = false;
            this.currency.Width = 70;
            // 
            // Total
            // 
            this.Total.HeaderText = "Total";
            this.Total.Name = "Total";
            this.Total.Width = 90;
            // 
            // Advance
            // 
            this.Advance.HeaderText = "Advance";
            this.Advance.Name = "Advance";
            this.Advance.Width = 70;
            // 
            // Discount
            // 
            this.Discount.HeaderText = "Discount";
            this.Discount.Name = "Discount";
            this.Discount.Width = 70;
            // 
            // GrandTotal
            // 
            this.GrandTotal.HeaderText = "GrandTotal";
            this.GrandTotal.Name = "GrandTotal";
            // 
            // btnEdit
            // 
            this.btnEdit.HeaderText = "";
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Text = "Edit";
            this.btnEdit.UseColumnTextForButtonValue = true;
            this.btnEdit.Width = 70;
            // 
            // btndelete
            // 
            this.btndelete.HeaderText = "";
            this.btndelete.Name = "btndelete";
            this.btndelete.Text = "Delete";
            this.btndelete.UseColumnTextForButtonValue = true;
            this.btndelete.Width = 70;
            // 
            // frmPurchaseHistory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1002, 617);
            this.Controls.Add(this.dgvpurchase);
            this.Controls.Add(this.dgvpurchasedetail);
            this.Controls.Add(this.btnclose);
            this.Controls.Add(this.btndetailreport);
            this.Controls.Add(this.btnsearch);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.pnlSaleEntry);
            this.Controls.Add(this.btnPurchaseReport);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmPurchaseHistory";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmPurchaseHistory";
            this.Load += new System.EventHandler(this.frmPurchaseHistory_Load);
            this.Click += new System.EventHandler(this.frmPurchaseHistory_Click);
            this.pnlSaleEntry.ResumeLayout(false);
            this.pnlSaleEntry.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picClose1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvitem)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvsupplier)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvpurchasedetail)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvpurchase)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlSaleEntry;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtsupplier;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtvoucher;
        private System.Windows.Forms.Button btnsearch;
        private System.Windows.Forms.Button btnPurchaseReport;
        private System.Windows.Forms.Button btndetailreport;
        private System.Windows.Forms.Label lblsupplierid;
        private System.Windows.Forms.TextBox txtitem;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DataGridView dgvitem;
        private System.Windows.Forms.DataGridView dgvpurchasedetail;
        private System.Windows.Forms.DateTimePicker dtpto;
        private System.Windows.Forms.DateTimePicker dtpfrom;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnclose;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cboClass;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cboCategory;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cbouser;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox cbolocation;
        private System.Windows.Forms.PictureBox picClose1;
        private System.Windows.Forms.DataGridView dgvsupplier;
        private System.Windows.Forms.DataGridViewTextBoxColumn SupplierISD;
        private System.Windows.Forms.DataGridViewTextBoxColumn SupplierName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Address;
        private System.Windows.Forms.DataGridViewTextBoxColumn Phone;
        private System.Windows.Forms.DataGridViewTextBoxColumn PurchaseID;
        private System.Windows.Forms.DataGridView dgvpurchase;
        private System.Windows.Forms.DataGridViewTextBoxColumn colitem;
        private System.Windows.Forms.DataGridViewTextBoxColumn Description;
        private System.Windows.Forms.DataGridViewComboBoxColumn Type;
        private System.Windows.Forms.DataGridViewTextBoxColumn Qty;
        private System.Windows.Forms.DataGridViewTextBoxColumn SalePrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn coltotal;
        private System.Windows.Forms.DataGridViewComboBoxColumn FOC;
        private System.Windows.Forms.DataGridViewTextBoxColumn ItemDispercent;
        private System.Windows.Forms.DataGridViewTextBoxColumn ItemDis;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn colItemCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn NameinEng;
        private System.Windows.Forms.DataGridViewTextBoxColumn NameinMM;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn PID;
        private System.Windows.Forms.DataGridViewTextBoxColumn User;
        private System.Windows.Forms.DataGridViewTextBoxColumn Supplier;
        private System.Windows.Forms.DataGridViewTextBoxColumn colVoucherNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn PaymentBy;
        private System.Windows.Forms.DataGridViewTextBoxColumn currency;
        private System.Windows.Forms.DataGridViewTextBoxColumn Total;
        private System.Windows.Forms.DataGridViewTextBoxColumn Advance;
        private System.Windows.Forms.DataGridViewTextBoxColumn Discount;
        private System.Windows.Forms.DataGridViewTextBoxColumn GrandTotal;
        private System.Windows.Forms.DataGridViewButtonColumn btnEdit;
        private System.Windows.Forms.DataGridViewButtonColumn btndelete;
    }
}