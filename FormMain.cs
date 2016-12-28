using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Orders
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
            try
            {
                    //
            } 
            catch (Exception e)
            {
                Console.WriteLine(e.Message);    
            }
        }
    

            private void MenuItemSettings_Click(object sender, EventArgs e)
            {
                FormSettings frm = new FormSettings();
                frm.ShowDialog();
                frm.Dispose();
            }

            public void FormMain_Load(object sender, EventArgs e)
            {
                DB.load();
                
                olvMain.SetObjects(DB.orders.Values);
            }

            private void olvMain_MouseDoubleClick(object sender, MouseEventArgs e)
            {
                FormOrder frm = new FormOrder();
                if ((DB.order)olvMain.SelectedObject != null)
                {
                    frm.Execute((DB.order)olvMain.SelectedObject);
                    frm.Dispose();

                    olvMain.SetObjects(DB.orders.Values);
                    olvMain.Refresh();
                }
                
            }

            private void Add_Click(object sender, EventArgs e)
            {
                FormOrder frm = new FormOrder();
                
                    frm.Execute(null);
                
                    olvMain.Refresh();
                

            }

            private void Change_Click(object sender, EventArgs e)
            {
                FormOrder frm = new FormOrder();
                if ((DB.order)olvMain.SelectedObject != null)
                {
                    frm.Execute((DB.order)olvMain.SelectedObject);
                    frm.Dispose();

                    olvMain.SetObjects(DB.orders.Values);
                    olvMain.Refresh();
                }
            }

            private void Delete_Click(object sender, EventArgs e)
            {
                if (MessageBox.Show("Удалить данную заявку", "Внимание!", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    //DB.order_detail od = (DB.order_detail)olvMain.SelectedObject;
                    DB.order o = (DB.order)olvMain.SelectedObject;
                    foreach (var od in o.details)
                    {
                        od.order_num = o.num;
                        od.order_date = o.date;

                        DB.order_detail.delete_order_details(od);

                        o.details.Remove(od);
                    }

                    DB.order.delete_order(o);
                    //DB.order_detail.delete_order_details(od);
                   
                    
                    olvMain.RefreshObjects(o.details);
                    olvMain.RemoveObject(o);

                   // DB.load();
                  //  olvMain.SetObjects(DB.orders.Values);
                    olvMain.Refresh();

                }
                
            }

       

         
            }
    }


