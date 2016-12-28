namespace Orders
{
    partial class FormMain
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.MenuItemSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.olvMain = new BrightIdeasSoftware.ObjectListView();
            this.olvColOrderNum = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColOrderDate = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColEmpID = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColCustID = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColSum = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.Change = new System.Windows.Forms.Button();
            this.Delete = new System.Windows.Forms.Button();
            this.Add = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.olvMain)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuItemSettings});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(854, 24);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // MenuItemSettings
            // 
            this.MenuItemSettings.Name = "MenuItemSettings";
            this.MenuItemSettings.Size = new System.Drawing.Size(79, 20);
            this.MenuItemSettings.Text = "Настройки";
            this.MenuItemSettings.Click += new System.EventHandler(this.MenuItemSettings_Click);
            // 
            // olvMain
            // 
            this.olvMain.AllColumns.Add(this.olvColOrderNum);
            this.olvMain.AllColumns.Add(this.olvColOrderDate);
            this.olvMain.AllColumns.Add(this.olvColEmpID);
            this.olvMain.AllColumns.Add(this.olvColCustID);
            this.olvMain.AllColumns.Add(this.olvColSum);
            this.olvMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.olvMain.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColOrderNum,
            this.olvColOrderDate,
            this.olvColEmpID,
            this.olvColCustID,
            this.olvColSum});
            this.olvMain.FullRowSelect = true;
            this.olvMain.GridLines = true;
            this.olvMain.Location = new System.Drawing.Point(0, 51);
            this.olvMain.Name = "olvMain";
            this.olvMain.Size = new System.Drawing.Size(803, 198);
            this.olvMain.TabIndex = 3;
            this.olvMain.UseCompatibleStateImageBehavior = false;
            this.olvMain.View = System.Windows.Forms.View.Details;
            this.olvMain.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.olvMain_MouseDoubleClick);
            // 
            // olvColOrderNum
            // 
            this.olvColOrderNum.AspectName = "num";
            this.olvColOrderNum.Groupable = false;
            this.olvColOrderNum.Text = "Номер заявки";
            this.olvColOrderNum.Width = 95;
            // 
            // olvColOrderDate
            // 
            this.olvColOrderDate.AspectName = "date";
            this.olvColOrderDate.Groupable = false;
            this.olvColOrderDate.Text = "Дата заявки";
            this.olvColOrderDate.Width = 104;
            // 
            // olvColEmpID
            // 
            this.olvColEmpID.AspectName = "emp";
            this.olvColEmpID.Groupable = false;
            this.olvColEmpID.Text = "Сотрудник";
            this.olvColEmpID.Width = 134;
            // 
            // olvColCustID
            // 
            this.olvColCustID.AspectName = "cust";
            this.olvColCustID.Groupable = false;
            this.olvColCustID.Text = "Клиент";
            this.olvColCustID.Width = 157;
            // 
            // olvColSum
            // 
            this.olvColSum.AspectName = "sum";
            this.olvColSum.Groupable = false;
            this.olvColSum.Text = "Суммарная стоимость";
            this.olvColSum.Width = 156;
            // 
            // Change
            // 
            this.Change.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.Change.Location = new System.Drawing.Point(50, 287);
            this.Change.Name = "Change";
            this.Change.Size = new System.Drawing.Size(75, 23);
            this.Change.TabIndex = 4;
            this.Change.Text = "Изменить";
            this.Change.UseVisualStyleBackColor = true;
            this.Change.Click += new System.EventHandler(this.Change_Click);
            // 
            // Delete
            // 
            this.Delete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.Delete.Location = new System.Drawing.Point(230, 287);
            this.Delete.Name = "Delete";
            this.Delete.Size = new System.Drawing.Size(75, 23);
            this.Delete.TabIndex = 5;
            this.Delete.Text = "Удалить";
            this.Delete.UseVisualStyleBackColor = true;
            this.Delete.Click += new System.EventHandler(this.Delete_Click);
            // 
            // Add
            // 
            this.Add.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.Add.Location = new System.Drawing.Point(411, 287);
            this.Add.Name = "Add";
            this.Add.Size = new System.Drawing.Size(75, 23);
            this.Add.TabIndex = 6;
            this.Add.Text = "Добавить";
            this.Add.UseVisualStyleBackColor = true;
            this.Add.Click += new System.EventHandler(this.Add_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(854, 366);
            this.Controls.Add(this.Add);
            this.Controls.Add(this.Delete);
            this.Controls.Add(this.Change);
            this.Controls.Add(this.olvMain);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FormMain";
            this.Text = "Заявки";
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.olvMain)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem MenuItemSettings;
        private BrightIdeasSoftware.OLVColumn olvColOrderNum;
        private BrightIdeasSoftware.OLVColumn olvColOrderDate;
        private BrightIdeasSoftware.OLVColumn olvColEmpID;
        private BrightIdeasSoftware.OLVColumn olvColCustID;
        private BrightIdeasSoftware.OLVColumn olvColSum;
        private System.Windows.Forms.Button Change;
        private System.Windows.Forms.Button Delete;
        private System.Windows.Forms.Button Add;
        public BrightIdeasSoftware.ObjectListView olvMain;
    }
}

