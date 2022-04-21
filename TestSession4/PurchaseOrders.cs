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
    public partial class PurchaseOrders : Form
    {
        TestSession4Entities db = new TestSession4Entities();
        DataTable dtParts = new DataTable();

        OrderItem objOI;
        
        public PurchaseOrders(OrderItem objOrderitem)
        {
            InitializeComponent();

            objOI = objOrderitem;

            if (objOrderitem != null)
            {
                //cbSupply.SelectedValue = objOrderitem.Order.SupplierID;
                //cbSupply.SelectedIndex = 1;

                dateDate.Value = objOrderitem.Order.Date.Date;
                cbSupply.SelectedItem = objOrderitem.Order.SupplierID;
                cbWarehouse.SelectedItem = objOrderitem.Order.DestinationWarehouseID;
            }

            

            

        }

        private void PurchaseOrders_Load(object sender, EventArgs e)
        {
            loadDataComboBox();
            //loadData();
            //initialDataLoad();
            var deleteButton = new DataGridViewButtonColumn();
            deleteButton.HeaderText = "Action";
            deleteButton.Text = "Remove";
            deleteButton.Name = "Remove";
            deleteButton.UseColumnTextForButtonValue = true;


            dgvData.Columns.Add(deleteButton);

        }

        private void initialDataLoad()
        {
            var data = db.OrderItems.Select(x => new
                                        {
                                            PartId = x.Part.Name,
                                            BatchNumber = x.BatchNumber,
                                            Amount = x.Amount
                                        }).ToList();

            dgvData.DataSource = data;
        }

        private void loadData()
        {
            Supplier cbSupplierSelected = (Supplier)cbSupply.SelectedItem;
            Warehouse cbWarehouseSelected = (Warehouse)cbWarehouse.SelectedItem;

            long supID = (cbSupplierSelected != null) ? cbSupplierSelected.ID : 0;
            long wbId = (cbWarehouseSelected != null) ? cbWarehouseSelected.ID : 0;

            // Console.WriteLine(cbSup + " " + (cbWar+1));


            var data = db.OrderItems.Where(x => (supID == 0 || x.Order.SupplierID == supID)
                                        && (wbId == 0 || x.Order.DestinationWarehouseID == wbId)
                                        && (x.Order.Date == dateDate.Value.Date)).Select(x => new
                                        {
                                            id = x.ID,
                                            PartName = x.Part.Name,
                                            BatchNumber = x.BatchNumber,
                                            Amount = x.Amount
                                        }).ToList();

                dgvData.DataSource = data;
            

            

            



        }

        //Load Data To Spinner
        private void loadDataComboBox()
        {
            var supplyData = db.Suppliers.ToList();
            supplyData.Insert(0, new Supplier
            {
                ID = 0,
                Name = "Select"
            });
            var warehouseData = db.Warehouses.ToList();

            warehouseData.Insert(0, new Warehouse
            {
                ID = 0,
                Name = "Select"
            });
            var partsData = db.Parts.ToList();

            cbSupply.DataSource = supplyData;
            cbSupply.DisplayMember = "Name";
            cbSupply.ValueMember = "ID";

            cbWarehouse.DataSource = warehouseData;
            cbWarehouse.DisplayMember = "Name";
            cbWarehouse.ValueMember = "ID";

            cbparts.DataSource = partsData;
            cbparts.DisplayMember = "Name";
            cbparts.ValueMember = "ID";
        }
        List<OrderItem> listOrderItem = new List<OrderItem>();
        private void btnAddtoList_Click(object sender, EventArgs e)
        {
            try
            {
                Supplier cbSupplierSelected = (Supplier)cbSupply.SelectedItem;
                Warehouse cbWarehouseSelected = (Warehouse)cbWarehouse.SelectedItem;
                Part cbPartSelected = (Part)cbparts.SelectedItem;

                long supID = (cbSupplierSelected != null) ? cbSupplierSelected.ID : 0;
                long wbId = (cbWarehouseSelected != null) ? cbWarehouseSelected.ID : 0;

                var order = db.Orders.FirstOrDefault(x => x.SupplierID == supID && x.DestinationWarehouseID == wbId && x.Date == dateDate.Value.Date);
                

                if (order != null)
                {
                    Console.WriteLine(order.ID + " " + cbPartSelected.ID + " " + txtAmount.Value + " ");
                    OrderItem oi = new OrderItem();
                    oi.OrderID = order.ID;
                    oi.PartID = cbPartSelected.ID;
                    if(db.Parts.Where(x=> x.BatchNumberHasRequired == true).FirstOrDefault() !=null)
                    {
                        if (txtbatchNumber.Text.Length > 0)
                        {
                            oi.BatchNumber = txtbatchNumber.Text.ToString();
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
                    if(supID!=0 && wbId != 0)
                    {
                        
                    }
                    else
                    {
                        MessageBox.Show("Select Supply and Destination Warehouse");
                    }
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex);
            }





        }

        private void exap()
        {
            Part cbPartsselected = (Part)cbparts.SelectedItem;

            /* try
             {*/

            var deleteButton = new DataGridViewButtonColumn();
            deleteButton.Name = "dataGridViewDeleteButton";
            deleteButton.HeaderText = "Action";
            deleteButton.Text = "Remove";
            deleteButton.UseColumnTextForButtonValue = true;

            Order orderid = db.Orders.OrderByDescending(x => x.ID).FirstOrDefault();

            if (cbPartsselected.BatchNumberHasRequired == true)
            {
                if (txtbatchNumber.TextLength > 0)
                {

                    if (txtAmount.Value > 0)
                    {
                        dtParts.Rows.Add(cbPartsselected.Name + "", txtbatchNumber.Text + "", txtAmount.Value + "", deleteButton);
                        var d = new OrderItem();
                        d.OrderID = (Convert.ToInt32(orderid.ID) + 1);
                        d.PartID = cbPartsselected.ID;
                        d.BatchNumber = txtbatchNumber.Text;
                        d.Amount = txtAmount.Value;
                        listOrderItem.Add(d);
                    }
                    else
                    {
                        MessageBox.Show("Amount Should Not Be Null");
                    }




                }
                else
                {
                    MessageBox.Show("Batch Number Required");
                }

            }
            else
            {
                if (txtAmount.Value > 0)
                {
                    dtParts.Rows.Add(cbPartsselected.Name + "", txtbatchNumber.Text + "", txtAmount.Value + "", deleteButton);

                    var d = new OrderItem();
                    d.OrderID = (Convert.ToInt32(orderid.ID) + 1);
                    d.PartID = cbPartsselected.ID;
                    d.BatchNumber = "";
                    d.Amount = txtAmount.Value;

                    listOrderItem.Add(d);
                }
                else
                {
                    MessageBox.Show("Amount Should Not Be Null");
                }

            }


            dgvData.DataSource = dtParts;
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (dgvData.RowCount > 0)
            {
                MessageBox.Show("Saved");
            }
            else
            {
                MessageBox.Show("One item Atleast");
            }


        }

        private void cbSupply_SelectedValueChanged(object sender, EventArgs e)
        {
                      
            loadData();
            
              //  loadData();
            
        }

        private void cbWarehouse_SelectedIndexChanged(object sender, EventArgs e)
        {
              
        }

        private void cbSupply_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void dgvData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var dtGrid = sender as DataGridView;
            long data = Convert.ToInt64(dtGrid.Rows[e.RowIndex].Cells[1].Value.ToString());
            if (dtGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex >= 0)
            {
                var delRow = db.OrderItems.FirstOrDefault(x => x.ID == data);
                db.OrderItems.Remove(delRow);
                db.SaveChanges();
                dgvData.ClearSelection();
                loadData();
            }
        }

        private void dateDate_ValueChanged(object sender, EventArgs e)
        {
           
                loadData();
            
        }

        private void cbWarehouse_SelectedValueChanged(object sender, EventArgs e)
        {
            loadData();
        }
    }
}
