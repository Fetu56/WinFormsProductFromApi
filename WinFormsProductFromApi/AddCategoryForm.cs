using ModelLibrary;

namespace WinFormsProductFromApi
{
    public partial class AddCategoryForm : Form
    {
        public Category Category { get; set; }
        public AddCategoryForm()
        {
            InitializeComponent();
        }

        private void Exit_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void Add_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != null)
            {
                this.DialogResult = DialogResult.OK;
                Category = new Category() { Name = textBox1.Text };
            }
        }
    }
}
