using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Windows.Forms;
namespace Orders
{
    public class DB {
        public static void load()
        {
            try
            {
                customers = customer.get_customers();
                employees = employee.get_employees();
                orders = order.get_orders();
                products = product.get_products();
           
                foreach (order o in orders.Values)
                {
                    employees.TryGetValue(o.employee_id, out o.emp);

                }
                foreach (order o in orders.Values)
                {
                    customers.TryGetValue(o.customer_id, out o.cust);
                }
                
                order_detail.get_order_detail(orders);
                product.get_products();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            
        }
        public static SortedList<int, order> orders;
        public class order
        {
            static Int32 id =0;
            public int num { get; set; }
            public DateTime date { get; set; }
            public int employee_id { get; set; }
            public employee emp = null;
            public int customer_id { get; set; }
            public customer cust = null;
            public double sum;

            public order() { }
            public order(order o) {
                this.customer_id = o.customer_id;
                this.date = o.date;
                this.employee_id = o.employee_id;
                this.num = o.num;
                foreach (order_detail od in o.details) {
                    this.details.Add(new order_detail(od));
                }
                
            }
            public void OnDetailChanged() 
            {
                sum = 0;
                foreach (order_detail od in details)
                {
                    sum += od.price * od.volume;
                }
            }
            public List<order_detail> details = new List<order_detail>();
            public static SortedList<int, order> get_orders()
            {
                SortedList<int, order> result = new SortedList<int, order>();
                SqlCommand cmd = new SqlCommand("select Num, Date, EmployeeID, CustomerID from Orders", DB.cnn());
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read()) {
                    order o = new order();
                    o.num = (int)rdr.GetValue(0);
                    o.date = (DateTime)rdr.GetValue(1);
                    o.employee_id = (int)rdr.GetValue(2);
                    o.customer_id = (int)rdr.GetValue(3);
                    result[o.num] = o;
                }
                rdr.Close();
                return result;
            }
            public static void update_order(order o) {
                SqlCommand cmd = new SqlCommand("update Orders set Date=@Date, EmployeeID=@EmpID, CustomerID=@CustID where Num=@num", DB.cnn());
                SqlParameter p = new SqlParameter("@Date", o.date); p.Direction = ParameterDirection.Input; cmd.Parameters.Add(p);
                p = new SqlParameter("@EmpID", o.employee_id); p.Direction = ParameterDirection.Input; cmd.Parameters.Add(p);
                p = new SqlParameter("@CustID", o.customer_id); p.Direction = ParameterDirection.Input; cmd.Parameters.Add(p);
                p = new SqlParameter("@Num", o.num); p.Direction = ParameterDirection.Input; cmd.Parameters.Add(p);
                //cmd.Prepare();
                cmd.ExecuteNonQuery();
            }
            public static void add_order(order o)
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("insert into Orders values (@Num,@Date,@EmpID,@CustID)", DB.cnn());
                    SqlParameter p = new SqlParameter("@Num", o.num); p.Direction = ParameterDirection.Input; cmd.Parameters.Add(p);
                    p = new SqlParameter("@Date", o.date); p.Direction = ParameterDirection.Input; cmd.Parameters.Add(p);
                    p = new SqlParameter("@EmpID", o.employee_id); p.Direction = ParameterDirection.Input; cmd.Parameters.Add(p);
                    p = new SqlParameter("@CustID", o.customer_id); p.Direction = ParameterDirection.Input; cmd.Parameters.Add(p);
                    //cmd.Prepare();
                    cmd.ExecuteNonQuery();
                    orders.Add(o.num, o);
                }
                catch (Exception e) 
                {
                    MessageBox.Show(e.Message);
                }
            }
            public static void delete_order(order o) {
                SqlCommand cmd = new SqlCommand("delete from Orders where Num=@Num", DB.cnn());
                SqlParameter p = new SqlParameter("@Num", o.num); p.Direction = ParameterDirection.Input; cmd.Parameters.Add(p);
                //cmd.Prepare();
                cmd.ExecuteNonQuery();
                
            }
            public static int get_new_order_num() {
                SqlCommand cmd = new SqlCommand("select NEXT VALUE FOR sequence", DB.cnn());
                cmd.ExecuteScalar();
                int result = Convert.ToInt32(cmd.ExecuteScalar());
                //здесь сам напиши код который лезет в БД и получает номер для новой заявки из sequence
                //sequence должен быть предварительно создан в БД
                return result;
            
            }

            internal static void add_order()
            {
                throw new NotImplementedException();
            }

            internal static void update_order()
            {
                throw new NotImplementedException();
            }
        }
     
        public class order_detail
        {
            public int order_num { get; set; }
            public DateTime order_date { get; set; }
            public int product_id { get; set; }
            public int volume { get; set; }
            public double price { get; set; }
            public order_detail() { }
            public order_detail(order_detail od) {
                this.order_num = od.order_num;
                this.order_date = od.order_date;
                this.product_id = od.product_id;
                this.price = od.price;
                this.volume = od.volume;
            }
            public static void get_order_detail(SortedList<int, order> orders)
            {
                order o = null;
                SqlCommand cmd = new SqlCommand("select OrderNum, OrderDate, ProductID, Volume, Price from OrderDetails order by OrderNum, ProductId", DB.cnn());
                SqlDataReader rdr = cmd.ExecuteReader();
                int order_num = -1;
                while (rdr.Read())
                {
                    order_detail od = new order_detail();
                    od.order_num = (int)rdr.GetValue(0);
                    od.order_date = (DateTime)rdr.GetValue(1);
                    od.product_id = (int)rdr.GetValue(2);
                    od.volume = (int)rdr.GetValue(3);
                    od.price = (Double)rdr.GetValue(4);
                    if(od.order_num!=order_num)
                    {
                        orders.TryGetValue(od.order_num, out o);
                        order_num = od.order_num;

                    }
                    if (o != null)
                    {
                        o.details.Add(od);
                        o.OnDetailChanged();
                    }
                    
                   
                }
                rdr.Close();
                
            }
            public static void update_order_detail(order_detail od)
            {
                SqlCommand cmd = new SqlCommand("update OrderDetails set OrderDate=@OrderDate, ProductID=@ProductID, Volume=@Volume, Price=@Price where OrderNum=@OrderNum", DB.cnn());
                SqlParameter p = new SqlParameter("@OrderDate", od.order_date); p.Direction = ParameterDirection.Input; cmd.Parameters.Add(p);
                p = new SqlParameter("@ProductID", od.product_id); p.Direction = ParameterDirection.Input; cmd.Parameters.Add(p);
                p = new SqlParameter("@Volume", od.volume); p.Direction = ParameterDirection.Input; cmd.Parameters.Add(p);
                p = new SqlParameter("@Price", od.price); p.Direction = ParameterDirection.Input; cmd.Parameters.Add(p);
                p = new SqlParameter("@OrderNum", od.order_num); p.Direction = ParameterDirection.Input; cmd.Parameters.Add(p);
                //cmd.Prepare();
                cmd.ExecuteNonQuery();
            }

                 public static void add_order_detail(order_detail od)
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("insert into OrderDetails values (@OrderNum,@OrderDate,@ProductID,@Volume,@Price)", DB.cnn());
                    SqlParameter p = new SqlParameter("@OrderNum", od.order_num); p.Direction = ParameterDirection.Input; cmd.Parameters.Add(p);
                    p = new SqlParameter("@OrderDate", od.order_date); p.Direction = ParameterDirection.Input; cmd.Parameters.Add(p);
                    p = new SqlParameter("@ProductID", od.product_id); p.Direction = ParameterDirection.Input; cmd.Parameters.Add(p);
                    p = new SqlParameter("@Volume", od.volume); p.Direction = ParameterDirection.Input; cmd.Parameters.Add(p);
                    p = new SqlParameter("@Price", od.price); p.Direction = ParameterDirection.Input; cmd.Parameters.Add(p);
                    //cmd.Prepare();
                    cmd.ExecuteNonQuery();
                }
                catch (Exception e) {
                    Console.WriteLine(e.Message);
                    MessageBox.Show(e.Message);
                }
            }
                 public static void delete_order_details(order_detail od)
                 {
                     SqlCommand cmd = new SqlCommand("delete from OrderDetails where OrderNum=@OrderNum", DB.cnn());
                     SqlParameter p = new SqlParameter("@OrderNum", od.order_num); p.Direction = ParameterDirection.Input; cmd.Parameters.Add(p);
                     //cmd.Prepare();
                     cmd.ExecuteNonQuery();

                 }

                 internal static void update_order_detail()
                 {
                     throw new NotImplementedException();
                 }

                 internal static void add_order_detail()
                 {
                     throw new NotImplementedException();
                 }
                 internal static void delete_order_details()
                 {
                     throw new NotImplementedException();
                 }
            
        }
        public static SortedList<int, employee> employees;
        public class employee 
        {
            public int id { get; set; }
            public string name { get; set; }
            public string office { get; set; }
            public static SortedList<int, employee> get_employees()
            {
                order o = null;
                SortedList<int, employee> result = new SortedList<int, employee>();
                SqlCommand cmd = new SqlCommand("select ID, Name, Office from Employees", DB.cnn());
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    employee e = new employee();
                    e.id = (int)rdr.GetValue(0);
                    e.name = (string)rdr.GetValue(1);
                    e.office = (string)rdr.GetValue(2);  
                    result[e.id] = e;
                }
                rdr.Close();
                return result;
            }
            public override string ToString()  {
                return name + "; " + office;
            }
        }
        public static SortedList<int, customer> customers;
        public class customer 
        {
            public int id { get; set; }
            public string name { get; set; }
            public int inn { get; set; }
            public string address { get; set; }
            public int phone { get; set; }
            public string printed_name { get { return ToString(); } }

            public static SortedList<int, customer> get_customers()
            {
                SortedList<int, customer> result = new SortedList<int, customer>();
                SqlCommand cmd = new SqlCommand("select ID, Name, INN, Address, Phone from Customers", DB.cnn());
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    customer c = new customer();
                    c.id = (int)rdr.GetValue(0);
                    c.name = (string)rdr.GetValue(1);
                    c.inn = (int)rdr.GetValue(2);
                    c.address = (string)rdr.GetValue(3);
                    c.phone = (int)rdr.GetValue(4);
                    result[c.id] = c;

                }
                rdr.Close();
                return result;
            }
            public override string ToString()
            {
                return name + "; " + inn;
            }
        }
        public static SortedList<int, product> products;
        public class product 
        {
            public int id { get; set; }
            public string name { get; set; }
            public string unit { get; set; }
            public double price { get; set; }
            public static SortedList<int, product> get_products()
            {
                SortedList<int, product> result = new SortedList<int, product>();
                SqlCommand cmd = new SqlCommand("select ID, Name, Unit, Price from Products", DB.cnn());
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    product p = new product();
                    p.id = (int)rdr.GetValue(0);
                    p.name = (string)rdr.GetValue(1);
                    p.unit = (string)rdr.GetValue(2);
                    p.price = (double)rdr.GetValue(3);
                    result[p.id] = p;
                }
                rdr.Close();
                return result;
            }
         
        }
        public static void refresh() {
            m_cnn.Close();
        }


        static SqlConnection m_cnn = new SqlConnection(); 
        public static SqlConnection cnn() {
            string cnn_str = "Data Source=" + Properties.Settings.Default.server + ";Initial Catalog=" +
                Properties.Settings.Default.db;
            if(Properties.Settings.Default.trusted_connection)
            {
                cnn_str=cnn_str+";Integrated Security=yes";
            }
            else
            {
                cnn_str = cnn_str + ";user="+ Properties.Settings.Default.user + ";password=" + Properties.Settings.Default.password;
            }
            
            if (m_cnn.State == ConnectionState.Open)
            {
                return m_cnn;
            }
            else {
                m_cnn.ConnectionString = cnn_str;
                m_cnn.Open();
                return m_cnn;
            }
           
         }

    }
}
