using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestSession4
{
    public partial class Form1 : Form
    {
        TestSession4Entities db = new TestSession4Entities();
        OrderItem objOrderitem;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            loadData();
            var removeCol = new DataGridViewButtonColumn();
            removeCol.HeaderText = "Action";
            removeCol.Text = "Remove";
            removeCol.Name = "Remove";
            removeCol.UseColumnTextForButtonValue = true;

            dgvData.Columns.Add(removeCol);

            var editCol = new DataGridViewButtonColumn();
            editCol.HeaderText = "Action";
            editCol.Text = "Edit";
            editCol.Name = "Edit";
            editCol.UseColumnTextForButtonValue = true;
            dgvData.Columns.Add(editCol);
        }

        //Get Data From database
        private void loadData()
        {
            
            var data = db.OrderItems.Select(x => new
            {
                ID = x.ID,
                PartName = x.Part.Name,
                TranscationType = x.Order.TransactionType.Name,
                Date = x.Order.Date,
                Amount = x.Amount,
                Source = x.Order.Warehouse.Name,
                Destination = x.Order.Warehouse1.Name,
                obj =x
            }).OrderByDescending(x=>x.Date).ThenBy(x=> x.TranscationType).ToList();

            dgvData.DataSource = data;

            

            

            for(int x = 0; x < dgvData.Rows.Count; x++)
            {
                var Ttype = dgvData.Rows[x].Cells["TranscationType"].Value.ToString();
                if(Ttype == "Purchase Order")
                {
                    dgvData.Rows[x].Cells["Amount"].Style.BackColor = Color.Green;
                }

            }


            

            //dgvData.Columns["ID"].Visible = false;
        }

        private void btnPurchaseOrder_Click(object sender, EventArgs e)
        {
            try
            {
                PurchaseOrders po = new PurchaseOrders(objOrderitem);
                po.Show();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void btnWarehouse_Click(object sender, EventArgs e)
        {
            try
            {
                WareHouseManagement wm = new WareHouseManagement(objOrderitem);
                wm.Show();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void btnInventory_Click(object sender, EventArgs e)
        {
            try
            {
                InventoryReport ir = new InventoryReport();
                ir.Show();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void dgvData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {

                var dtGrid = sender as DataGridView;
                if (dtGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex > 0)
                {
                    string data = dtGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                    if (data == "Remove")
                    {
                        Console.WriteLine("Row Index = ," + e.RowIndex);
                        //Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
                        int dtID = Convert.ToInt32(dtGrid.Rows[e.RowIndex].Cells[0].Value.ToString());

                        OrderItem oi = db.OrderItems.FirstOrDefault(x => x.ID == dtID);

                        if (oi != null)
                        {
                            var delRow = db.OrderItems.FirstOrDefault(y => y.ID == dtID);
                            db.OrderItems.Remove(oi);
                            db.SaveChanges();
                            dgvData.ClearSelection();
                            loadData();
                        }

                        Console.WriteLine("Remove clicked" + oi.ID);
                    }
                    if (data == "Edit")
                    {
                        objOrderitem = (OrderItem)dgvData.Rows[e.RowIndex].Cells["obj"].Value;
                        if(objOrderitem.Order.TransactionTypeID == 1)
                        {
                            PurchaseOrders po = new PurchaseOrders(objOrderitem);
                            po.Show();
                        }
                        if (objOrderitem.Order.TransactionTypeID == 2)
                        {
                            WareHouseManagement wm = new WareHouseManagement(objOrderitem);
                            wm.Show();
                        }
                    }
                }
            } catch(Exception ex)
                {
                Console.WriteLine(ex.Message);
                }
        }
    }
}
