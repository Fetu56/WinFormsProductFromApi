using ModelLibrary;
using Newtonsoft.Json;

namespace WinFormsProductFromApi
{
    public partial class Form1 : Form
    {
        System.Timers.Timer updateTimer;
        HttpClient client;
        List<Product> products;
        List<Category> categories;
        public Form1()
        {
            InitializeComponent();
        }

        private void Cat_Click(object sender, EventArgs e)
        {
            AddCategoryForm categoryForm = new AddCategoryForm();
            var result = categoryForm.ShowDialog();
            if(result == DialogResult.OK)
            {
                SendAdd(categoryForm.Category);
                RefreshData();
            }
        }

        private void CatDel_Click(object sender, EventArgs e)
        {
            if(listBox1.SelectedItems.Count > 0)
            {
                for(int i = 0; i < categories.Count; i++)
                {
                    if(categories[i].ToString() == (string)listBox1.SelectedItem)
                    {
                        SendDelete(categories[i]);
                    }
                }
            }
                        RefreshData();
        }

        private void Prod_Click(object sender, EventArgs e)
        {
            AddProductForm categoryForm = new AddProductForm(categories);
            var result = categoryForm.ShowDialog();
            if (result == DialogResult.OK)
            {
                SendAdd(categoryForm.Product);
                RefreshData();
            }
        }

        private void ProdDel_Click(object sender, EventArgs e)
        {
            if (listBox2.SelectedItems.Count > 0)
            {
                for (int i = 0; i < products.Count; i++)
                {
                    if (products[i].ToString() == (string)listBox2.SelectedItem)
                    {
                        SendDelete(products[i]);
                    }
                }
               
            }
                        RefreshData();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            client = new HttpClient();
            updateTimer = new System.Timers.Timer(60000);
            updateTimer.Elapsed += UpdateTimer_Elapsed;
            updateTimer.Start();
            RefreshData();
        }

        private void UpdateTimer_Elapsed(object? sender, System.Timers.ElapsedEventArgs e)
        {
            RefreshData();
        }

        private void RefreshData()
        {
            try
            {
                listBox1.Invoke(new Action(() => listBox1.Items.Clear()));
                listBox2.Invoke(new Action(() => listBox2.Items.Clear()));

                categories = JsonConvert.DeserializeObject<IEnumerable<Category>>(GetJson("https://localhost:5001/CategoryProduct/GetCat")).ToList();
                products = JsonConvert.DeserializeObject<IEnumerable<Product>>(GetJson("https://localhost:5001/CategoryProduct/GetProd")).ToList();
                categories.ForEach(x => listBox1.Invoke(new Action(() => listBox1.Items.Add(x.ToString()))));
                products.ForEach(x => listBox2.Invoke(new Action(() => listBox2.Items.Add(x.ToString()))));
            }
            catch { Task.Run(() => { Thread.Sleep(300); RefreshData(); }); }
        }

        private string GetJson(string url)
        {
            var resCat = client.GetAsync(url).Result;
            return resCat.Content.ReadAsStringAsync().Result;
        }

        private void SendDelete(Category category)
        {

            client.DeleteAsync("https://localhost:5001/CategoryProduct/DelCat?id=" + category.Id);
        }
        private void SendDelete(Product product)
        {
            client.DeleteAsync("https://localhost:5001/CategoryProduct/DelProd?id=" + product.Id);
        }


        private void SendAdd(Category category)
        {
            client.PostAsync("https://localhost:5001/CategoryProduct/AddCat?name=" + category.Name, null);
        }
        private void SendAdd(Product product)
        {
            client.PostAsync($"https://localhost:5001/CategoryProduct/AddProd?name={product.Name}&catId={product.CategoryId}&description={product.Description}", null);
        }
    }
}