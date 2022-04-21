using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestSession4
{
    public partial class InventoryReport : Form
    {
        TestSession4Entities db = new TestSession4Entities();
        public InventoryReport()
        {
            InitializeComponent();
        }

        private void InventoryReport_Load(object sender, EventArgs e)
        {
            cbWarehouse.DataSource = db.Warehouses.ToList();
            cbWarehouse.DisplayMember = "Name";
            cbWarehouse.ValueMember = "ID";
        }

        private void rbCurrentStocks_CheckedChanged(object sender, EventArgs e)
        {
            loaddata(1);
        }

        private void loaddata(int id)
        {
            if (id == 1)
            {
                var data = db.OrderItems.Where(x => x.Part.MinimumAmount > 0).Select(x => new
                {
                    PartsName = x.Part.Name,
                    CurrentStock = x.Amount
                }).ToList();

                dgvData.DataSource = data;
            }
            if (id == 2)
            {
                var data = db.OrderItems.Where(x => x.Part.MinimumAmount == 0).Select(x => new
                {
                    PartsName = x.Part.Name,
                    RecivedStock = x.Amount,
                    BatchNo = x.BatchNumber
                }).ToList();

                dgvData.DataSource = data;
            }
            if (id == 3)
            {
                var data = db.OrderItems.Where(x => x.Part.MinimumAmount == 0).Select(x => new
                {
                    PartsName = x.Part.Name
                }).ToList();

                dgvData.DataSource = data;
            }
        }

        private void rbOutOfStocks_CheckedChanged(object sender, EventArgs e)
        {
            loaddata(3);
        }

        private void rbRecevedStocks_CheckedChanged(object sender, EventArgs e)
        {
            loaddata(2);
        }
    }
}
