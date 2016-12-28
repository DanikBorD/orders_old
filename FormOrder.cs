using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Orders
{
    public partial class FormOrder : Form
    {
        public FormOrder()
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
        DB.order ord;
        DB.order ordnew;

        bool is_new() { return ord == null; } //этот метод показывает, что мы в режиме ввода новой заявки

        public void Execute(DB.order o) {
            ord = o;
            if (ord == null) {
                ord = null;
                ordnew = new DB.order();
                ordnew.num = DB.order.get_new_order_num();
                ordnew.customer_id = DB.customers.Values.First().id;
                ordnew.employee_id = DB.employees.Values.First().id;
                ordnew.date = DateTime.Today.Date; //здесь самостоятельно укоротить до даты (без часов и т.п.)
            } else  {
                ordnew = new DB.order(o);
                ord = o;
                TextBoxNum.Text = o.num.ToString();
            }
            TextBoxDate.Text = ordnew.date.Date.ToString(); //для поля сделать шаблон ввода, чтобы только год/месяц/день вводился. и ToString() здесь не пройдет наверное (при шаблоне), нужен будет форматированный вывод даты
            TextBoxNum.ReadOnly.ToString(); //нет, номер пускай нельзя вводить, только подставлять готовый (TextBoxNum.ReadOnly)
            comboBoxEmp.DataSource = DB.employees.Values.ToList();
            comboBoxEmp.DisplayMember = "name";
            comboBoxEmp.ValueMember = "id";
            comboBoxEmp.SelectedValue = ordnew.employee_id;
            comboBoxCust.DataSource = DB.customers.Values.ToList();
            comboBoxCust.DisplayMember = "printed_name";
            comboBoxCust.ValueMember = "id";
            comboBoxCust.SelectedValue = ordnew.customer_id;
            comboBoxProduct.DataSource = DB.products.Values.ToList();
            comboBoxProduct.DisplayMember = "name";
            comboBoxProduct.ValueMember = "id";
            if (ordnew.details.Count() > 0)
                comboBoxProduct.SelectedValue = ordnew.details.First().product_id;
            else
                comboBoxProduct.SelectedValue = DB.products.Values.First().id;

            olvOrderDetails.SetObjects(ordnew.details);
            ShowDialog();
            //далее работаешь c ordnew. По кнопке применить или сохранить, смотришь, что изменилось в заявке (сравнить старую и новую) и обновляешь старую и вносишь изменения в БД.
            //если в режиме ввода новой заявки, то новую добавляешь в БД и затем все записи из details. Если все успешно, то добавляешь заявку в список orders.
        }

        private void olvOrder_SelectedIndexChanged(object sender, EventArgs e)
        {
            DB.order_detail od = (DB.order_detail)olvOrderDetails.SelectedObject;
            if (od != null) {
                comboBoxProduct.SelectedValue = od.product_id;
                textBoxVolume.Text = od.volume.ToString();
                textBoxPrice.Text = od.price.ToString();
            }
            else
            {
                comboBoxProduct.SelectedValue = DB.products.Values.First().id;
                textBoxVolume.Text = "0";
                textBoxPrice.Text = "0";
            }
        }
        private void comboBoxProduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            //DB.product p;
            //DB.products.TryGetValue((int)comboBoxProduct.SelectedValue, out p);
            //if (p != null)
            //    textBoxPrice.Text = p.price.ToString();
            //else
            //    textBoxPrice.Text = "0";
            
            textBoxPrice.Text = ((DB.product)comboBoxProduct.SelectedItem).price.ToString();

        }
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            DB.order_detail newdetail = new DB.order_detail();

            newdetail.price = Convert.ToInt32(textBoxPrice.Text); // найти функцию перевода из стоковой в число
            newdetail.volume = Convert.ToInt32(textBoxVolume.Text);
            
            newdetail.product_id = Convert.ToInt32(comboBoxProduct.SelectedValue);
            newdetail.order_date = Convert.ToDateTime(TextBoxDate.Text);
            ordnew.details.Add(newdetail);
            olvOrderDetails.RefreshObjects(ordnew.details);
        }
        
        private void Primenit_MouseClick(object sender, MouseEventArgs e)
        {
            if (is_new())
            {
                ordnew.num = Convert.ToInt32(TextBoxNum.Text);
                ordnew.date = Convert.ToDateTime(TextBoxDate.Text);
                ordnew.employee_id = Convert.ToInt32(comboBoxEmp.SelectedValue);
                ordnew.customer_id = Convert.ToInt32(comboBoxCust.SelectedValue);
                DB.order.add_order(ordnew);

                foreach (var od in ordnew.details)
                {
                    od.order_num = ordnew.num;
                    od.order_date = ordnew.date;

                    DB.order_detail.add_order_detail(od);
                }
                    

            } 
            else
            {
                //DB.order.update_order(ord);
                ordnew.num = Convert.ToInt32(TextBoxNum.Text);
                ordnew.date = Convert.ToDateTime(TextBoxDate.Text);
                ordnew.employee_id = Convert.ToInt32(comboBoxEmp.SelectedValue);
                ordnew.customer_id = Convert.ToInt32(comboBoxCust.SelectedValue);
                DB.order.update_order(ordnew);

                foreach (var od in ordnew.details)
                {
                    od.order_num = ordnew.num;
                    od.order_date = ordnew.date;

                    DB.order_detail.update_order_detail(od);
                }
            }
            DB.load();
            //(Owner as FormMain).olvMain.SetObjects(DB.orders.Values); 

            this.Close();
           
        }

        private void buttonDelete_Click_1(object sender, EventArgs e)
        {


            DB.order_detail od = (DB.order_detail)olvOrderDetails.SelectedObject;
            if (od != null) {
                ordnew.details.Remove(od);
                olvOrderDetails.RefreshObjects(ordnew.details);
                if (ordnew.details.Count > 0)
                {
                    olvOrderDetails.SelectObject(ordnew.details.First());
                    olvOrder_SelectedIndexChanged(sender, e);
                }
            }
        }

     

       

  

       

      
     

    
 

   
     

       
    }
}
