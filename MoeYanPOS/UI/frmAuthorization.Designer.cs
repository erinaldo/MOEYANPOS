namespace MoeYanPOS.UI
{
    partial class frmAuthorization
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAuthorization));
            this.chksaleentry = new System.Windows.Forms.CheckBox();
            this.label20 = new System.Windows.Forms.Label();
            this.cbouser = new System.Windows.Forms.ComboBox();
            this.chkpurchaseentry = new System.Windows.Forms.CheckBox();
            this.chksalereturn = new System.Windows.Forms.CheckBox();
            this.chksaleorder = new System.Windows.Forms.CheckBox();
            this.chkstockajustment = new System.Windows.Forms.CheckBox();
            this.chkstocktransfer = new System.Windows.Forms.CheckBox();
            this.chkopeningstock = new System.Windows.Forms.CheckBox();
            this.chkstockbalance = new System.Windows.Forms.CheckBox();
            this.chkpayforcredit = new System.Windows.Forms.CheckBox();
            this.chkpettycash = new System.Windows.Forms.CheckBox();
            this.chksalehistory = new System.Windows.Forms.CheckBox();
            this.chkpurchasehistory = new System.Windows.Forms.CheckBox();
            this.chksaleorderhistory = new System.Windows.Forms.CheckBox();
            this.chksalereturnhistory = new System.Windows.Forms.CheckBox();
            this.chkstockhistory = new System.Windows.Forms.CheckBox();
            this.chkopeningstockhistory = new System.Windows.Forms.CheckBox();
            this.chkstocktransferhistory = new System.Windows.Forms.CheckBox();
            this.chkstockajustmenthistory = new System.Windows.Forms.CheckBox();
            this.chkpayforcredithistory = new System.Windows.Forms.CheckBox();
            this.chkpettycashhistory = new System.Windows.Forms.CheckBox();
            this.chkuser = new System.Windows.Forms.CheckBox();
            this.chkdivision = new System.Windows.Forms.CheckBox();
            this.chkdepartment = new System.Windows.Forms.CheckBox();
            this.chktownship = new System.Windows.Forms.CheckBox();
            this.chklocation = new System.Windows.Forms.CheckBox();
            this.chkcustomer = new System.Windows.Forms.CheckBox();
            this.chksupplier = new System.Windows.Forms.CheckBox();
            this.chkbrand = new System.Windows.Forms.CheckBox();
            this.chkmeasurement = new System.Windows.Forms.CheckBox();
            this.chkclass = new System.Windows.Forms.CheckBox();
            this.chkcategory = new System.Windows.Forms.CheckBox();
            this.chkstock = new System.Windows.Forms.CheckBox();
            this.chkcurrency = new System.Windows.Forms.CheckBox();
            this.chksystemsetting = new System.Windows.Forms.CheckBox();
            this.chkcashreport = new System.Windows.Forms.CheckBox();
            this.chksalereport = new System.Windows.Forms.CheckBox();
            this.chkpurchasereport = new System.Windows.Forms.CheckBox();
            this.chkcustomerreport = new System.Windows.Forms.CheckBox();
            this.chkstockreport = new System.Windows.Forms.CheckBox();
            this.chksupplierreport = new System.Windows.Forms.CheckBox();
            this.picClose = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.btnsave = new System.Windows.Forms.Button();
            this.chksetlocation = new System.Windows.Forms.CheckBox();
            this.chkexpandimp = new System.Windows.Forms.CheckBox();
            this.lblid = new System.Windows.Forms.Label();
            this.btnCheckAll = new System.Windows.Forms.Button();
            this.chkAuthorization = new System.Windows.Forms.CheckBox();
            this.chkVoucherSetting = new System.Windows.Forms.CheckBox();
            this.chkStockAdjustmentType = new System.Windows.Forms.CheckBox();
            this.chkOutletCashHeader = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.picClose)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // chksaleentry
            // 
            this.chksaleentry.AutoSize = true;
            this.chksaleentry.Location = new System.Drawing.Point(52, 100);
            this.chksaleentry.Name = "chksaleentry";
            this.chksaleentry.Size = new System.Drawing.Size(74, 17);
            this.chksaleentry.TabIndex = 0;
            this.chksaleentry.Text = "Sale Entry";
            this.chksaleentry.UseVisualStyleBackColor = true;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("Zawgyi-One", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.Location = new System.Drawing.Point(48, 41);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(40, 21);
            this.label20.TabIndex = 55;
            this.label20.Text = "User";
            // 
            // cbouser
            // 
            this.cbouser.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbouser.Font = new System.Drawing.Font("Zawgyi-One", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbouser.FormattingEnabled = true;
            this.cbouser.Location = new System.Drawing.Point(134, 38);
            this.cbouser.Name = "cbouser";
            this.cbouser.Size = new System.Drawing.Size(291, 29);
            this.cbouser.TabIndex = 54;
            this.cbouser.SelectedIndexChanged += new System.EventHandler(this.cbouser_SelectedIndexChanged);
            // 
            // chkpurchaseentry
            // 
            this.chkpurchaseentry.AutoSize = true;
            this.chkpurchaseentry.Location = new System.Drawing.Point(52, 137);
            this.chkpurchaseentry.Name = "chkpurchaseentry";
            this.chkpurchaseentry.Size = new System.Drawing.Size(98, 17);
            this.chkpurchaseentry.TabIndex = 56;
            this.chkpurchaseentry.Text = "Purchase Entry";
            this.chkpurchaseentry.UseVisualStyleBackColor = true;
            // 
            // chksalereturn
            // 
            this.chksalereturn.AutoSize = true;
            this.chksalereturn.Location = new System.Drawing.Point(52, 174);
            this.chksalereturn.Name = "chksalereturn";
            this.chksalereturn.Size = new System.Drawing.Size(82, 17);
            this.chksalereturn.TabIndex = 57;
            this.chksalereturn.Text = "Sale Return";
            this.chksalereturn.UseVisualStyleBackColor = true;
            // 
            // chksaleorder
            // 
            this.chksaleorder.AutoSize = true;
            this.chksaleorder.Location = new System.Drawing.Point(52, 212);
            this.chksaleorder.Name = "chksaleorder";
            this.chksaleorder.Size = new System.Drawing.Size(76, 17);
            this.chksaleorder.TabIndex = 58;
            this.chksaleorder.Text = "Sale Order";
            this.chksaleorder.UseVisualStyleBackColor = true;
            // 
            // chkstockajustment
            // 
            this.chkstockajustment.AutoSize = true;
            this.chkstockajustment.Location = new System.Drawing.Point(52, 250);
            this.chkstockajustment.Name = "chkstockajustment";
            this.chkstockajustment.Size = new System.Drawing.Size(103, 17);
            this.chkstockajustment.TabIndex = 59;
            this.chkstockajustment.Text = "Stock Ajustment";
            this.chkstockajustment.UseVisualStyleBackColor = true;
            // 
            // chkstocktransfer
            // 
            this.chkstocktransfer.AutoSize = true;
            this.chkstocktransfer.Location = new System.Drawing.Point(52, 287);
            this.chkstocktransfer.Name = "chkstocktransfer";
            this.chkstocktransfer.Size = new System.Drawing.Size(96, 17);
            this.chkstocktransfer.TabIndex = 60;
            this.chkstocktransfer.Text = "Stock Transfer";
            this.chkstocktransfer.UseVisualStyleBackColor = true;
            // 
            // chkopeningstock
            // 
            this.chkopeningstock.AutoSize = true;
            this.chkopeningstock.Location = new System.Drawing.Point(52, 323);
            this.chkopeningstock.Name = "chkopeningstock";
            this.chkopeningstock.Size = new System.Drawing.Size(97, 17);
            this.chkopeningstock.TabIndex = 61;
            this.chkopeningstock.Text = "Opening Stock";
            this.chkopeningstock.UseVisualStyleBackColor = true;
            // 
            // chkstockbalance
            // 
            this.chkstockbalance.AutoSize = true;
            this.chkstockbalance.Location = new System.Drawing.Point(52, 361);
            this.chkstockbalance.Name = "chkstockbalance";
            this.chkstockbalance.Size = new System.Drawing.Size(96, 17);
            this.chkstockbalance.TabIndex = 62;
            this.chkstockbalance.Text = "Stock Balance";
            this.chkstockbalance.UseVisualStyleBackColor = true;
            // 
            // chkpayforcredit
            // 
            this.chkpayforcredit.AutoSize = true;
            this.chkpayforcredit.Location = new System.Drawing.Point(52, 399);
            this.chkpayforcredit.Name = "chkpayforcredit";
            this.chkpayforcredit.Size = new System.Drawing.Size(92, 17);
            this.chkpayforcredit.TabIndex = 63;
            this.chkpayforcredit.Text = "Pay For Credit";
            this.chkpayforcredit.UseVisualStyleBackColor = true;
            // 
            // chkpettycash
            // 
            this.chkpettycash.AutoSize = true;
            this.chkpettycash.Location = new System.Drawing.Point(52, 441);
            this.chkpettycash.Name = "chkpettycash";
            this.chkpettycash.Size = new System.Drawing.Size(77, 17);
            this.chkpettycash.TabIndex = 64;
            this.chkpettycash.Text = "Petty Cash";
            this.chkpettycash.UseVisualStyleBackColor = true;
            // 
            // chksalehistory
            // 
            this.chksalehistory.AutoSize = true;
            this.chksalehistory.Location = new System.Drawing.Point(216, 100);
            this.chksalehistory.Name = "chksalehistory";
            this.chksalehistory.Size = new System.Drawing.Size(82, 17);
            this.chksalehistory.TabIndex = 65;
            this.chksalehistory.Text = "Sale History";
            this.chksalehistory.UseVisualStyleBackColor = true;
            // 
            // chkpurchasehistory
            // 
            this.chkpurchasehistory.AutoSize = true;
            this.chkpurchasehistory.Location = new System.Drawing.Point(216, 137);
            this.chkpurchasehistory.Name = "chkpurchasehistory";
            this.chkpurchasehistory.Size = new System.Drawing.Size(106, 17);
            this.chkpurchasehistory.TabIndex = 66;
            this.chkpurchasehistory.Text = "Purchase History";
            this.chkpurchasehistory.UseVisualStyleBackColor = true;
            // 
            // chksaleorderhistory
            // 
            this.chksaleorderhistory.AutoSize = true;
            this.chksaleorderhistory.Location = new System.Drawing.Point(216, 212);
            this.chksaleorderhistory.Name = "chksaleorderhistory";
            this.chksaleorderhistory.Size = new System.Drawing.Size(111, 17);
            this.chksaleorderhistory.TabIndex = 68;
            this.chksaleorderhistory.Text = "Sale Order History";
            this.chksaleorderhistory.UseVisualStyleBackColor = true;
            // 
            // chksalereturnhistory
            // 
            this.chksalereturnhistory.AutoSize = true;
            this.chksalereturnhistory.Location = new System.Drawing.Point(216, 174);
            this.chksalereturnhistory.Name = "chksalereturnhistory";
            this.chksalereturnhistory.Size = new System.Drawing.Size(117, 17);
            this.chksalereturnhistory.TabIndex = 67;
            this.chksalereturnhistory.Text = "Sale Return History";
            this.chksalereturnhistory.UseVisualStyleBackColor = true;
            // 
            // chkstockhistory
            // 
            this.chkstockhistory.AutoSize = true;
            this.chkstockhistory.Location = new System.Drawing.Point(216, 361);
            this.chkstockhistory.Name = "chkstockhistory";
            this.chkstockhistory.Size = new System.Drawing.Size(89, 17);
            this.chkstockhistory.TabIndex = 72;
            this.chkstockhistory.Text = "Stock History";
            this.chkstockhistory.UseVisualStyleBackColor = true;
            // 
            // chkopeningstockhistory
            // 
            this.chkopeningstockhistory.AutoSize = true;
            this.chkopeningstockhistory.Location = new System.Drawing.Point(216, 323);
            this.chkopeningstockhistory.Name = "chkopeningstockhistory";
            this.chkopeningstockhistory.Size = new System.Drawing.Size(132, 17);
            this.chkopeningstockhistory.TabIndex = 71;
            this.chkopeningstockhistory.Text = "Opening Stock History";
            this.chkopeningstockhistory.UseVisualStyleBackColor = true;
            // 
            // chkstocktransferhistory
            // 
            this.chkstocktransferhistory.AutoSize = true;
            this.chkstocktransferhistory.Location = new System.Drawing.Point(216, 287);
            this.chkstocktransferhistory.Name = "chkstocktransferhistory";
            this.chkstocktransferhistory.Size = new System.Drawing.Size(131, 17);
            this.chkstocktransferhistory.TabIndex = 70;
            this.chkstocktransferhistory.Text = "Stock Transfer History";
            this.chkstocktransferhistory.UseVisualStyleBackColor = true;
            // 
            // chkstockajustmenthistory
            // 
            this.chkstockajustmenthistory.AutoSize = true;
            this.chkstockajustmenthistory.Location = new System.Drawing.Point(216, 250);
            this.chkstockajustmenthistory.Name = "chkstockajustmenthistory";
            this.chkstockajustmenthistory.Size = new System.Drawing.Size(138, 17);
            this.chkstockajustmenthistory.TabIndex = 69;
            this.chkstockajustmenthistory.Text = "Stock Ajustment History";
            this.chkstockajustmenthistory.UseVisualStyleBackColor = true;
            // 
            // chkpayforcredithistory
            // 
            this.chkpayforcredithistory.AutoSize = true;
            this.chkpayforcredithistory.Location = new System.Drawing.Point(216, 399);
            this.chkpayforcredithistory.Name = "chkpayforcredithistory";
            this.chkpayforcredithistory.Size = new System.Drawing.Size(127, 17);
            this.chkpayforcredithistory.TabIndex = 71;
            this.chkpayforcredithistory.Text = "Pay For Credit History";
            this.chkpayforcredithistory.UseVisualStyleBackColor = true;
            // 
            // chkpettycashhistory
            // 
            this.chkpettycashhistory.AutoSize = true;
            this.chkpettycashhistory.Location = new System.Drawing.Point(216, 441);
            this.chkpettycashhistory.Name = "chkpettycashhistory";
            this.chkpettycashhistory.Size = new System.Drawing.Size(112, 17);
            this.chkpettycashhistory.TabIndex = 72;
            this.chkpettycashhistory.Text = "Petty Cash History";
            this.chkpettycashhistory.UseVisualStyleBackColor = true;
            // 
            // chkuser
            // 
            this.chkuser.AutoSize = true;
            this.chkuser.Location = new System.Drawing.Point(424, 100);
            this.chkuser.Name = "chkuser";
            this.chkuser.Size = new System.Drawing.Size(48, 17);
            this.chkuser.TabIndex = 65;
            this.chkuser.Text = "User";
            this.chkuser.UseVisualStyleBackColor = true;
            // 
            // chkdivision
            // 
            this.chkdivision.AutoSize = true;
            this.chkdivision.Location = new System.Drawing.Point(424, 137);
            this.chkdivision.Name = "chkdivision";
            this.chkdivision.Size = new System.Drawing.Size(63, 17);
            this.chkdivision.TabIndex = 66;
            this.chkdivision.Text = "Division";
            this.chkdivision.UseVisualStyleBackColor = true;
            // 
            // chkdepartment
            // 
            this.chkdepartment.AutoSize = true;
            this.chkdepartment.Location = new System.Drawing.Point(424, 174);
            this.chkdepartment.Name = "chkdepartment";
            this.chkdepartment.Size = new System.Drawing.Size(81, 17);
            this.chkdepartment.TabIndex = 67;
            this.chkdepartment.Text = "Department";
            this.chkdepartment.UseVisualStyleBackColor = true;
            // 
            // chktownship
            // 
            this.chktownship.AutoSize = true;
            this.chktownship.Location = new System.Drawing.Point(424, 212);
            this.chktownship.Name = "chktownship";
            this.chktownship.Size = new System.Drawing.Size(72, 17);
            this.chktownship.TabIndex = 68;
            this.chktownship.Text = "Township";
            this.chktownship.UseVisualStyleBackColor = true;
            // 
            // chklocation
            // 
            this.chklocation.AutoSize = true;
            this.chklocation.Location = new System.Drawing.Point(424, 250);
            this.chklocation.Name = "chklocation";
            this.chklocation.Size = new System.Drawing.Size(67, 17);
            this.chklocation.TabIndex = 69;
            this.chklocation.Text = "Location";
            this.chklocation.UseVisualStyleBackColor = true;
            // 
            // chkcustomer
            // 
            this.chkcustomer.AutoSize = true;
            this.chkcustomer.Location = new System.Drawing.Point(424, 287);
            this.chkcustomer.Name = "chkcustomer";
            this.chkcustomer.Size = new System.Drawing.Size(70, 17);
            this.chkcustomer.TabIndex = 70;
            this.chkcustomer.Text = "Customer";
            this.chkcustomer.UseVisualStyleBackColor = true;
            // 
            // chksupplier
            // 
            this.chksupplier.AutoSize = true;
            this.chksupplier.Location = new System.Drawing.Point(424, 323);
            this.chksupplier.Name = "chksupplier";
            this.chksupplier.Size = new System.Drawing.Size(64, 17);
            this.chksupplier.TabIndex = 71;
            this.chksupplier.Text = "Supplier";
            this.chksupplier.UseVisualStyleBackColor = true;
            // 
            // chkbrand
            // 
            this.chkbrand.AutoSize = true;
            this.chkbrand.Location = new System.Drawing.Point(424, 399);
            this.chkbrand.Name = "chkbrand";
            this.chkbrand.Size = new System.Drawing.Size(54, 17);
            this.chkbrand.TabIndex = 71;
            this.chkbrand.Text = "Brand";
            this.chkbrand.UseVisualStyleBackColor = true;
            // 
            // chkmeasurement
            // 
            this.chkmeasurement.AutoSize = true;
            this.chkmeasurement.Location = new System.Drawing.Point(424, 361);
            this.chkmeasurement.Name = "chkmeasurement";
            this.chkmeasurement.Size = new System.Drawing.Size(90, 17);
            this.chkmeasurement.TabIndex = 72;
            this.chkmeasurement.Text = "Measurement";
            this.chkmeasurement.UseVisualStyleBackColor = true;
            // 
            // chkclass
            // 
            this.chkclass.AutoSize = true;
            this.chkclass.Location = new System.Drawing.Point(424, 441);
            this.chkclass.Name = "chkclass";
            this.chkclass.Size = new System.Drawing.Size(51, 17);
            this.chkclass.TabIndex = 72;
            this.chkclass.Text = "Class";
            this.chkclass.UseVisualStyleBackColor = true;
            // 
            // chkcategory
            // 
            this.chkcategory.AutoSize = true;
            this.chkcategory.Location = new System.Drawing.Point(575, 100);
            this.chkcategory.Name = "chkcategory";
            this.chkcategory.Size = new System.Drawing.Size(68, 17);
            this.chkcategory.TabIndex = 65;
            this.chkcategory.Text = "Category";
            this.chkcategory.UseVisualStyleBackColor = true;
            // 
            // chkstock
            // 
            this.chkstock.AutoSize = true;
            this.chkstock.Location = new System.Drawing.Point(575, 137);
            this.chkstock.Name = "chkstock";
            this.chkstock.Size = new System.Drawing.Size(54, 17);
            this.chkstock.TabIndex = 66;
            this.chkstock.Text = "Stock";
            this.chkstock.UseVisualStyleBackColor = true;
            // 
            // chkcurrency
            // 
            this.chkcurrency.AutoSize = true;
            this.chkcurrency.Location = new System.Drawing.Point(575, 174);
            this.chkcurrency.Name = "chkcurrency";
            this.chkcurrency.Size = new System.Drawing.Size(68, 17);
            this.chkcurrency.TabIndex = 67;
            this.chkcurrency.Text = "Currency";
            this.chkcurrency.UseVisualStyleBackColor = true;
            // 
            // chksystemsetting
            // 
            this.chksystemsetting.AutoSize = true;
            this.chksystemsetting.Location = new System.Drawing.Point(575, 212);
            this.chksystemsetting.Name = "chksystemsetting";
            this.chksystemsetting.Size = new System.Drawing.Size(96, 17);
            this.chksystemsetting.TabIndex = 68;
            this.chksystemsetting.Text = "System Setting";
            this.chksystemsetting.UseVisualStyleBackColor = true;
            // 
            // chkcashreport
            // 
            this.chkcashreport.AutoSize = true;
            this.chkcashreport.Location = new System.Drawing.Point(575, 250);
            this.chkcashreport.Name = "chkcashreport";
            this.chkcashreport.Size = new System.Drawing.Size(85, 17);
            this.chkcashreport.TabIndex = 69;
            this.chkcashreport.Text = "Cash Report";
            this.chkcashreport.UseVisualStyleBackColor = true;
            // 
            // chksalereport
            // 
            this.chksalereport.AutoSize = true;
            this.chksalereport.Location = new System.Drawing.Point(575, 287);
            this.chksalereport.Name = "chksalereport";
            this.chksalereport.Size = new System.Drawing.Size(82, 17);
            this.chksalereport.TabIndex = 70;
            this.chksalereport.Text = "Sale Report";
            this.chksalereport.UseVisualStyleBackColor = true;
            // 
            // chkpurchasereport
            // 
            this.chkpurchasereport.AutoSize = true;
            this.chkpurchasereport.Location = new System.Drawing.Point(575, 323);
            this.chkpurchasereport.Name = "chkpurchasereport";
            this.chkpurchasereport.Size = new System.Drawing.Size(106, 17);
            this.chkpurchasereport.TabIndex = 71;
            this.chkpurchasereport.Text = "Purchase Report";
            this.chkpurchasereport.UseVisualStyleBackColor = true;
            // 
            // chkcustomerreport
            // 
            this.chkcustomerreport.AutoSize = true;
            this.chkcustomerreport.Location = new System.Drawing.Point(575, 399);
            this.chkcustomerreport.Name = "chkcustomerreport";
            this.chkcustomerreport.Size = new System.Drawing.Size(105, 17);
            this.chkcustomerreport.TabIndex = 71;
            this.chkcustomerreport.Text = "Customer Report";
            this.chkcustomerreport.UseVisualStyleBackColor = true;
            // 
            // chkstockreport
            // 
            this.chkstockreport.AutoSize = true;
            this.chkstockreport.Location = new System.Drawing.Point(575, 361);
            this.chkstockreport.Name = "chkstockreport";
            this.chkstockreport.Size = new System.Drawing.Size(89, 17);
            this.chkstockreport.TabIndex = 72;
            this.chkstockreport.Text = "Stock Report";
            this.chkstockreport.UseVisualStyleBackColor = true;
            // 
            // chksupplierreport
            // 
            this.chksupplierreport.AutoSize = true;
            this.chksupplierreport.Location = new System.Drawing.Point(575, 441);
            this.chksupplierreport.Name = "chksupplierreport";
            this.chksupplierreport.Size = new System.Drawing.Size(99, 17);
            this.chksupplierreport.TabIndex = 72;
            this.chksupplierreport.Text = "Supplier Report";
            this.chksupplierreport.UseVisualStyleBackColor = true;
            // 
            // picClose
            // 
            this.picClose.BackColor = System.Drawing.Color.Transparent;
            this.picClose.Image = global::MoeYanPOS.Properties.Resources.close3;
            this.picClose.Location = new System.Drawing.Point(846, 2);
            this.picClose.Name = "picClose";
            this.picClose.Size = new System.Drawing.Size(31, 34);
            this.picClose.TabIndex = 1;
            this.picClose.TabStop = false;
            this.picClose.Click += new System.EventHandler(this.picClose_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.panel1.BackgroundImage = global::MoeYanPOS.Properties.Resources.titlebar;
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.picClose);
            this.panel1.Location = new System.Drawing.Point(-1, -2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(879, 34);
            this.panel1.TabIndex = 103;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(3, 5);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(132, 24);
            this.label3.TabIndex = 0;
            this.label3.Text = "Authorization";
            // 
            // btnsave
            // 
            this.btnsave.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnsave.BackgroundImage")));
            this.btnsave.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnsave.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnsave.ForeColor = System.Drawing.Color.White;
            this.btnsave.Location = new System.Drawing.Point(725, 384);
            this.btnsave.Name = "btnsave";
            this.btnsave.Size = new System.Drawing.Size(100, 32);
            this.btnsave.TabIndex = 116;
            this.btnsave.Text = "Save";
            this.btnsave.UseVisualStyleBackColor = true;
            this.btnsave.Click += new System.EventHandler(this.btnsave_Click);
            // 
            // chksetlocation
            // 
            this.chksetlocation.AutoSize = true;
            this.chksetlocation.Location = new System.Drawing.Point(725, 100);
            this.chksetlocation.Name = "chksetlocation";
            this.chksetlocation.Size = new System.Drawing.Size(86, 17);
            this.chksetlocation.TabIndex = 65;
            this.chksetlocation.Text = "Set Location";
            this.chksetlocation.UseVisualStyleBackColor = true;
            // 
            // chkexpandimp
            // 
            this.chkexpandimp.AutoSize = true;
            this.chkexpandimp.Location = new System.Drawing.Point(725, 137);
            this.chkexpandimp.Name = "chkexpandimp";
            this.chkexpandimp.Size = new System.Drawing.Size(110, 17);
            this.chkexpandimp.TabIndex = 66;
            this.chkexpandimp.Text = "Export And Import";
            this.chkexpandimp.UseVisualStyleBackColor = true;
            // 
            // lblid
            // 
            this.lblid.AutoSize = true;
            this.lblid.Font = new System.Drawing.Font("Zawgyi-One", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblid.Location = new System.Drawing.Point(514, 41);
            this.lblid.Name = "lblid";
            this.lblid.Size = new System.Drawing.Size(18, 21);
            this.lblid.TabIndex = 118;
            this.lblid.Text = "0";
            this.lblid.Visible = false;
            // 
            // btnCheckAll
            // 
            this.btnCheckAll.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnCheckAll.BackgroundImage")));
            this.btnCheckAll.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCheckAll.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnCheckAll.ForeColor = System.Drawing.Color.White;
            this.btnCheckAll.Location = new System.Drawing.Point(725, 335);
            this.btnCheckAll.Name = "btnCheckAll";
            this.btnCheckAll.Size = new System.Drawing.Size(100, 32);
            this.btnCheckAll.TabIndex = 119;
            this.btnCheckAll.Text = "Check All";
            this.btnCheckAll.UseVisualStyleBackColor = true;
            this.btnCheckAll.Click += new System.EventHandler(this.btnCheckAll_Click);
            // 
            // chkAuthorization
            // 
            this.chkAuthorization.AutoSize = true;
            this.chkAuthorization.Location = new System.Drawing.Point(725, 174);
            this.chkAuthorization.Name = "chkAuthorization";
            this.chkAuthorization.Size = new System.Drawing.Size(87, 17);
            this.chkAuthorization.TabIndex = 120;
            this.chkAuthorization.Text = "Authorization";
            this.chkAuthorization.UseVisualStyleBackColor = true;
            // 
            // chkVoucherSetting
            // 
            this.chkVoucherSetting.AutoSize = true;
            this.chkVoucherSetting.Location = new System.Drawing.Point(725, 212);
            this.chkVoucherSetting.Name = "chkVoucherSetting";
            this.chkVoucherSetting.Size = new System.Drawing.Size(102, 17);
            this.chkVoucherSetting.TabIndex = 121;
            this.chkVoucherSetting.Text = "Voucher Setting";
            this.chkVoucherSetting.UseVisualStyleBackColor = true;
            // 
            // chkStockAdjustmentType
            // 
            this.chkStockAdjustmentType.AutoSize = true;
            this.chkStockAdjustmentType.Location = new System.Drawing.Point(725, 250);
            this.chkStockAdjustmentType.Name = "chkStockAdjustmentType";
            this.chkStockAdjustmentType.Size = new System.Drawing.Size(136, 17);
            this.chkStockAdjustmentType.TabIndex = 122;
            this.chkStockAdjustmentType.Text = "Stock Adjustment Type";
            this.chkStockAdjustmentType.UseVisualStyleBackColor = true;
            // 
            // chkOutletCashHeader
            // 
            this.chkOutletCashHeader.AutoSize = true;
            this.chkOutletCashHeader.Location = new System.Drawing.Point(725, 287);
            this.chkOutletCashHeader.Name = "chkOutletCashHeader";
            this.chkOutletCashHeader.Size = new System.Drawing.Size(119, 17);
            this.chkOutletCashHeader.TabIndex = 123;
            this.chkOutletCashHeader.Text = "Outlet Cash Header";
            this.chkOutletCashHeader.UseVisualStyleBackColor = true;
            // 
            // frmAuthorization
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(876, 492);
            this.Controls.Add(this.chkOutletCashHeader);
            this.Controls.Add(this.chkStockAdjustmentType);
            this.Controls.Add(this.chkVoucherSetting);
            this.Controls.Add(this.chkAuthorization);
            this.Controls.Add(this.btnCheckAll);
            this.Controls.Add(this.lblid);
            this.Controls.Add(this.btnsave);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.chksupplierreport);
            this.Controls.Add(this.chkclass);
            this.Controls.Add(this.chkpettycashhistory);
            this.Controls.Add(this.chkstockreport);
            this.Controls.Add(this.chkmeasurement);
            this.Controls.Add(this.chkstockhistory);
            this.Controls.Add(this.chkcustomerreport);
            this.Controls.Add(this.chkbrand);
            this.Controls.Add(this.chkpayforcredithistory);
            this.Controls.Add(this.chkpurchasereport);
            this.Controls.Add(this.chksupplier);
            this.Controls.Add(this.chkopeningstockhistory);
            this.Controls.Add(this.chksalereport);
            this.Controls.Add(this.chkcustomer);
            this.Controls.Add(this.chkstocktransferhistory);
            this.Controls.Add(this.chkcashreport);
            this.Controls.Add(this.chklocation);
            this.Controls.Add(this.chkstockajustmenthistory);
            this.Controls.Add(this.chksystemsetting);
            this.Controls.Add(this.chktownship);
            this.Controls.Add(this.chkcurrency);
            this.Controls.Add(this.chksaleorderhistory);
            this.Controls.Add(this.chkdepartment);
            this.Controls.Add(this.chkexpandimp);
            this.Controls.Add(this.chkstock);
            this.Controls.Add(this.chksetlocation);
            this.Controls.Add(this.chksalereturnhistory);
            this.Controls.Add(this.chkcategory);
            this.Controls.Add(this.chkdivision);
            this.Controls.Add(this.chkuser);
            this.Controls.Add(this.chkpurchasehistory);
            this.Controls.Add(this.chksalehistory);
            this.Controls.Add(this.chkpettycash);
            this.Controls.Add(this.chkpayforcredit);
            this.Controls.Add(this.chkstockbalance);
            this.Controls.Add(this.chkopeningstock);
            this.Controls.Add(this.chkstocktransfer);
            this.Controls.Add(this.chkstockajustment);
            this.Controls.Add(this.chksaleorder);
            this.Controls.Add(this.chksalereturn);
            this.Controls.Add(this.chkpurchaseentry);
            this.Controls.Add(this.label20);
            this.Controls.Add(this.cbouser);
            this.Controls.Add(this.chksaleentry);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmAuthorization";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmAuthorization";
            this.Load += new System.EventHandler(this.frmAuthorization_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picClose)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox chksaleentry;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.ComboBox cbouser;
        private System.Windows.Forms.CheckBox chkpurchaseentry;
        private System.Windows.Forms.CheckBox chksalereturn;
        private System.Windows.Forms.CheckBox chksaleorder;
        private System.Windows.Forms.CheckBox chkstockajustment;
        private System.Windows.Forms.CheckBox chkstocktransfer;
        private System.Windows.Forms.CheckBox chkopeningstock;
        private System.Windows.Forms.CheckBox chkstockbalance;
        private System.Windows.Forms.CheckBox chkpayforcredit;
        private System.Windows.Forms.CheckBox chkpettycash;
        private System.Windows.Forms.CheckBox chksalehistory;
        private System.Windows.Forms.CheckBox chkpurchasehistory;
        private System.Windows.Forms.CheckBox chksaleorderhistory;
        private System.Windows.Forms.CheckBox chksalereturnhistory;
        private System.Windows.Forms.CheckBox chkstockhistory;
        private System.Windows.Forms.CheckBox chkopeningstockhistory;
        private System.Windows.Forms.CheckBox chkstocktransferhistory;
        private System.Windows.Forms.CheckBox chkstockajustmenthistory;
        private System.Windows.Forms.CheckBox chkpayforcredithistory;
        private System.Windows.Forms.CheckBox chkpettycashhistory;
        private System.Windows.Forms.CheckBox chkuser;
        private System.Windows.Forms.CheckBox chkdivision;
        private System.Windows.Forms.CheckBox chkdepartment;
        private System.Windows.Forms.CheckBox chktownship;
        private System.Windows.Forms.CheckBox chklocation;
        private System.Windows.Forms.CheckBox chkcustomer;
        private System.Windows.Forms.CheckBox chksupplier;
        private System.Windows.Forms.CheckBox chkbrand;
        private System.Windows.Forms.CheckBox chkmeasurement;
        private System.Windows.Forms.CheckBox chkclass;
        private System.Windows.Forms.CheckBox chkcategory;
        private System.Windows.Forms.CheckBox chkstock;
        private System.Windows.Forms.CheckBox chkcurrency;
        private System.Windows.Forms.CheckBox chksystemsetting;
        private System.Windows.Forms.CheckBox chkcashreport;
        private System.Windows.Forms.CheckBox chksalereport;
        private System.Windows.Forms.CheckBox chkpurchasereport;
        private System.Windows.Forms.CheckBox chkcustomerreport;
        private System.Windows.Forms.CheckBox chkstockreport;
        private System.Windows.Forms.CheckBox chksupplierreport;
        private System.Windows.Forms.PictureBox picClose;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnsave;
        private System.Windows.Forms.CheckBox chksetlocation;
        private System.Windows.Forms.CheckBox chkexpandimp;
        private System.Windows.Forms.Label lblid;
        private System.Windows.Forms.Button btnCheckAll;
        private System.Windows.Forms.CheckBox chkAuthorization;
        private System.Windows.Forms.CheckBox chkVoucherSetting;
        private System.Windows.Forms.CheckBox chkStockAdjustmentType;
        private System.Windows.Forms.CheckBox chkOutletCashHeader;
    }
}