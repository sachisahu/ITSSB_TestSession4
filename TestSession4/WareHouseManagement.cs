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
    public partial class WareHouseManagement : Form
    {
        TestSession4Entities db = new TestSession4Entities();
        public WareHouseManagement(OrderItem objOrderitem)
        {
            InitializeComponent();
            if(objOrderitem != null)
            {
                datePickerDate.Value = objOrderitem.Order.Date.Date;
            }
        }

        private void WareHouseManagement_Load(object sender, EventArgs e)
        {
            loadDataComboBox();


            loadData();
            DataGridViewButtonColumn delBtn = new DataGridViewButtonColumn();
            delBtn.Text = "Remove";
            delBtn.HeaderText = "Action";
            delBtn.Name = "Remove";
            delBtn.UseColumnTextForButtonValue = true;

            dgvData.Columns.Add(delBtn);

        }

        private void loadData()
        {
            Warehouse cbWareHouseStartSelected = (Warehouse)cbSourceWarehouse.SelectedItem;
            Warehouse cbWarehouseDestSelected = (Warehouse)cbDestinationWarehouse.SelectedItem;

            long wbSID = (cbWareHouseStartSelected != null) ? cbWareHouseStartSelected.ID : 0;
            long wbDId = (cbWarehouseDestSelected != null) ? cbWarehouseDestSelected.ID : 0;

            // Console.WriteLine(cbSup + " " + (cbWar+1));

            Console.WriteLine(datePickerDate.Value.Date.Date);


            var data = db.OrderItems.Where(x => (wbSID == 0 || x.Order.SourceWarehouseID == wbSID)
                                        && (wbDId == 0 || x.Order.DestinationWarehouseID == wbDId)
                                        && (x.Order.Date == datePickerDate.Value.Date.Date)).Select(x => new
                                        {
                                            id = x.ID,
                                            PartName = x.Part.Name,
                                            BatchNumber = x.BatchNumber,
                                            Amount = x.Amount
                                        }).ToList();

            dgvData.DataSource = data;
            dgvData.Columns["id"].Visible = false;

        }

        private void loadDataComboBox()
        {
            var warehouseData = db.Warehouses.ToList();
            var warehouseData2 = db.Warehouses.ToList();
            var partsData = db.Parts.ToList();


            cbSourceWarehouse.DataSource = warehouseData;
            cbSourceWarehouse.DisplayMember = "Name";
            cbSourceWarehouse.ValueMember = "ID";

            cbDestinationWarehouse.DataSource = warehouseData2;
            cbDestinationWarehouse.DisplayMember = "Name";
            cbDestinationWarehouse.ValueMember = "ID";

            cbparts.DataSource = partsData;
            cbparts.DisplayMember = "Name";
            cbparts.ValueMember = "ID";


        }

        private void cbparts_SelectedValueChanged(object sender, EventArgs e)
        {
            //loaddataBatchNo();
        }
        
        //Load data To Batch Spinner
        private void loaddataBatchNo()
        {

            Part part = (Part)cbparts.SelectedItem;
            Console.WriteLine(part.ID + " Sach");

            var batchData = db.OrderItems.Where(x => x.Part.BatchNumberHasRequired == true && x.PartID == part.ID ).Select(x => new
            {
                ID = x.Part.ID,
                BatchNo = x.BatchNumber
            }).ToList();

            cbBatchNo.DataSource = batchData;
            cbBatchNo.DisplayMember = "BatchNo";
            cbBatchNo.ValueMember = "ID";


        }

        private void cbparts_SelectedIndexChanged(object sender, EventArgs e)
        {
            loaddataBatchNo();
        }

        private void btnAddtoList_Click(object sender, EventArgs e)
        {

            Warehouse cbWareHouseStartSelected = (Warehouse)cbSourceWarehouse.SelectedItem;
            Warehouse cbWarehouseDestSelected = (Warehouse)cbDestinationWarehouse.SelectedItem;
            Part cbPartSelected = (Part)cbparts.SelectedItem;

            long wbSID = (cbWareHouseStartSelected != null) ? cbWareHouseStartSelected.ID : 0;
            long wbDId = (cbWarehouseDestSelected != null) ? cbWarehouseDestSelected.ID : 0;

            var order = db.Orders.FirstOrDefault(x => x.SourceWarehouseID == wbSID && x.DestinationWarehouseID == wbDId && x.Date == datePickerDate.Value.Date);


            if (order != null)
            {
                //Console.WriteLine(order.ID + " " + cbPartSelected.ID + " " + txtAmount.Value + " ");
                OrderItem oi = new OrderItem();
                oi.OrderID = order.ID;
                oi.PartID = cbPartSelected.ID;
                Part part = db.Parts.Where(x => x.ID == oi.PartID).FirstOrDefault();
                Console.WriteLine("inside");
                if (part.BatchNumberHasRequired == true)
                {
                    if (cbBatchNo.Text.Length > 0)
                    {
                        oi.BatchNumber = cbBatchNo.Text;
                        oi.Amount = txtAmount.Value;
                        db.OrderItems.Add(oi);
                        db.SaveChanges();
                        dgvData.ClearSelection();
                        loadData();
                    }
                    else
                    {
                        MessageBox.Show("Enter Batch Number");
                    }

                }
                else
                {
                    oi.Amount = txtAmount.Value;
                    db.OrderItems.Add(oi);
                    db.SaveChanges();
                    dgvData.ClearSelection();
                    loadData();
                }
            }
            else
            {
                if (wbSID != 0 && wbDId != 0)
                {
                    Warehouse wS = (Warehouse)cbSourceWarehouse.SelectedItem;
                    Warehouse wD = (Warehouse)cbDestinationWarehouse.SelectedItem;
                    Order order1 = new Order();
                    order1.TransactionTypeID = 2;
                    order1.SourceWarehouseID = wS.ID;
                    order1.DestinationWarehouseID = wD.ID;
                    order1.Date = datePickerDate.Value.Date;
                    db.Orders.Add(order1);
                    db.SaveChanges();


                    

                    var orderV = db.Orders.FirstOrDefault(x => x.SourceWarehouseID == wbSID && x.DestinationWarehouseID == wbDId && x.Date == datePickerDate.Value.Date);

                    OrderItem oi = new OrderItem();
                    oi.OrderID = orderV.ID;
                    oi.PartID = cbPartSelected.ID;

                    Part part = db.Parts.Where(x => x.ID == oi.PartID).FirstOrDefault();
                    Console.WriteLine("inside");
                    if (part.BatchNumberHasRequired == true)
                    {
                        
                        if (cbBatchNo.Text.Length < 0)
                        {
                            oi.BatchNumber = cbBatchNo.Text;
                            oi.Amount = txtAmount.Value;
                            db.OrderItems.Add(oi);
                            db.SaveChanges();
                            dgvData.ClearSelection();
                            loadData();
                        }
                        else
                        {
                            MessageBox.Show("Enter Batch Number");
                        }

                    }
                    else
                    {
                        oi.Amount = txtAmount.Value;
                        db.OrderItems.Add(oi);
                        db.SaveChanges();
                        dgvData.ClearSelection();
                        loadData();
                    }


                }
                else
                {
                    MessageBox.Show("Select Supply and Destination Warehouse");
                }
            }
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if(cbSourceWarehouse.SelectedItem != cbDestinationWarehouse.SelectedItem)
            {
                errorProvider.Clear();
                loadData();
            }
            else
            {
                errorProvider.SetError(cbSourceWarehouse, "Source And Destination cannot Be Same");
                errorProvider.SetError(cbDestinationWarehouse, "Source And Destination cannot Be Same");
                return;
            }

        }

        private void dateDate_ValueChanged(object sender, EventArgs e)
        {
            loadData();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            loadData();
        }

        private void dgvData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var dtgGrid = sender as DataGridView;
            long data = Convert.ToInt64(dtgGrid.Rows[e.RowIndex].Cells[1].Value.ToString());
            if (dtgGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex >= 0)
            {
                var delRow = db.OrderItems.FirstOrDefault(x => x.ID == data);
                db.OrderItems.Remove(delRow);
                db.SaveChanges();
                dgvData.ClearSelection();
                loadData();
            }

        }

        private void cbDestinationWarehouse_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadData();
        }

        private void cbSourceWarehouse_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadData();
        }
    }
}
