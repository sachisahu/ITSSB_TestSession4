
namespace TestSession4
{
    partial class InventoryReport
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
            this.label2 = new System.Windows.Forms.Label();
            this.cbWarehouse = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbOutOfStocks = new System.Windows.Forms.RadioButton();
            this.rbRecevedStocks = new System.Windows.Forms.RadioButton();
            this.rbCurrentStocks = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.dgvData = new System.Windows.Forms.DataGridView();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).BeginInit();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(25, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(109, 20);
            this.label2.TabIndex = 22;
            this.label2.Text = "Warehouse:";
            // 
            // cbWarehouse
            // 
            this.cbWarehouse.FormattingEnabled = true;
            this.cbWarehouse.Location = new System.Drawing.Point(25, 68);
            this.cbWarehouse.Name = "cbWarehouse";
            this.cbWarehouse.Size = new System.Drawing.Size(227, 24);
            this.cbWarehouse.TabIndex = 21;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbOutOfStocks);
            this.groupBox1.Controls.Add(this.rbRecevedStocks);
            this.groupBox1.Controls.Add(this.rbCurrentStocks);
            this.groupBox1.Location = new System.Drawing.Point(300, 51);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(443, 64);
            this.groupBox1.TabIndex = 23;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Inventory Type";
            // 
            // rbOutOfStocks
            // 
            this.rbOutOfStocks.AutoSize = true;
            this.rbOutOfStocks.Location = new System.Drawing.Point(300, 22);
            this.rbOutOfStocks.Name = "rbOutOfStocks";
            this.rbOutOfStocks.Size = new System.Drawing.Size(117, 21);
            this.rbOutOfStocks.TabIndex = 2;
            this.rbOutOfStocks.TabStop = true;
            this.rbOutOfStocks.Text = "Out Of Stocks";
            this.rbOutOfStocks.UseVisualStyleBackColor = true;
            this.rbOutOfStocks.CheckedChanged += new System.EventHandler(this.rbOutOfStocks_CheckedChanged);
            // 
            // rbRecevedStocks
            // 
            this.rbRecevedStocks.AutoSize = true;
            this.rbRecevedStocks.Location = new System.Drawing.Point(160, 22);
            this.rbRecevedStocks.Name = "rbRecevedStocks";
            this.rbRecevedStocks.Size = new System.Drawing.Size(134, 21);
            this.rbRecevedStocks.TabIndex = 1;
            this.rbRecevedStocks.TabStop = true;
            this.rbRecevedStocks.Text = "Received Stocks";
            this.rbRecevedStocks.UseVisualStyleBackColor = true;
            this.rbRecevedStocks.CheckedChanged += new System.EventHandler(this.rbRecevedStocks_CheckedChanged);
            // 
            // rbCurrentStocks
            // 
            this.rbCurrentStocks.AutoSize = true;
            this.rbCurrentStocks.Location = new System.Drawing.Point(7, 22);
            this.rbCurrentStocks.Name = "rbCurrentStocks";
            this.rbCurrentStocks.Size = new System.Drawing.Size(122, 21);
            this.rbCurrentStocks.TabIndex = 0;
            this.rbCurrentStocks.TabStop = true;
            this.rbCurrentStocks.Text = "Current Stocks";
            this.rbCurrentStocks.UseVisualStyleBackColor = true;
            this.rbCurrentStocks.CheckedChanged += new System.EventHandler(this.rbCurrentStocks_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(29, 140);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 17);
            this.label1.TabIndex = 24;
            this.label1.Text = "Results:";
            // 
            // dgvData
            // 
            this.dgvData.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvData.Location = new System.Drawing.Point(32, 177);
            this.dgvData.Name = "dgvData";
            this.dgvData.RowHeadersWidth = 51;
            this.dgvData.RowTemplate.Height = 24;
            this.dgvData.Size = new System.Drawing.Size(711, 217);
            this.dgvData.TabIndex = 25;
            // 
            // InventoryReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 429);
            this.Controls.Add(this.dgvData);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cbWarehouse);
            this.Name = "InventoryReport";
            this.Text = "InventoryReport";
            this.Load += new System.EventHandler(this.InventoryReport_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbWarehouse;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbOutOfStocks;
        private System.Windows.Forms.RadioButton rbRecevedStocks;
        private System.Windows.Forms.RadioButton rbCurrentStocks;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgvData;
    }
}