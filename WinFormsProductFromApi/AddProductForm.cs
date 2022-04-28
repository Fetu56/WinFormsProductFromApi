using ModelLibrary;

namespace WinFormsProductFromApi
{
    public partial class AddProductForm : Form
    {
        public Product Product { get; set; }
        private List<Category> _cats;
        public AddProductForm(List<Category> categories)
        {
            InitializeComponent();
            _cats = categories;
            comboBox1.Items.AddRange(_cats.ToArray());
        }

        private void Add_Click(object sender, EventArgs e)
        {
            if(textBox1.Text != null && textBox2.Text != null && comboBox1.SelectedItem != null)
            {
                this.DialogResult = DialogResult.OK;
                Product = new Product() { Name = textBox1.Text, Description = textBox2.Text, CategoryId = (comboBox1.SelectedItem as Category).Id };
            }
        }

        private void Exit_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
