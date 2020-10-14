using Northwind.Business.Concrete;
using Northwind.Business.Abstract;
using Northwind.DataAccess.Concrete.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Northwind.Entites.Concrete;
using Northwind.Business.DependencyResolvers.Ninject;

namespace Northwind.WebFormsUI
{
    public partial class Form1 : Form
    {
        private IProductService _productService;
        private ICategoryService _categoryService;
       
        public Form1()
        {
            InitializeComponent();
            _productService = InstanceFactory.GetInstance<IProductService>();
            _categoryService = InstanceFactory.GetInstance<ICategoryService>();

        }

        private void DgwProduct_CellContentClick(object sender, System.Windows.Forms.DataGridViewCellEventArgs e)
        {
            MessageBox.Show("elif");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Loadproducts();
            LoadCategories();

        }

        private void LoadCategories()
        {
            cbxCategory.DataSource = _categoryService.GetAll();
            cbxCategory.DisplayMember = "CategoryName";
            cbxCategory.ValueMember = "CategoryId";

            cbxCategoryId.DataSource = _categoryService.GetAll();
            cbxCategoryId.DisplayMember = "CategoryName";
            cbxCategoryId.ValueMember = "CategoryId";

            cbxCategoryIdUpdate.DataSource = _categoryService.GetAll();
            cbxCategoryIdUpdate.DisplayMember = "CategoryName";
            cbxCategoryIdUpdate.ValueMember = "CategoryId";
        }

        private void Loadproducts()
        {
            dgwProduct.DataSource = _productService.GetAll();
        }

        private void dgwProduct_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void cbxCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                dgwProduct.DataSource = _productService.GetProductsByCategory(Convert.ToInt32(cbxCategory.SelectedValue));

            }

            catch (Exception exception)
            {
               
            }
        }

        private void tbxProductName_TextChanged(object sender, EventArgs e)
        {

         


            if (!String.IsNullOrEmpty(tbxProductName.Text))//textboxda değer varsa
            {
                dgwProduct.DataSource = _productService.GetProductsByProductName(tbxProductName.Text);
            }
            else
            {
                Loadproducts();
            }
            
        }

        private void groupBox1_Enter_1(object sender, EventArgs e)
        {

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                _productService.Add(new Product
                {
                    CategoryId = Convert.ToInt32(cbxCategoryId.SelectedValue),
                    ProductName = tbxProduct2.Text,
                    QuantityPerUnit = tbxQuantityPerUnit.Text,
                    UnitPrice = Convert.ToDecimal(tbxUnitPrice.Text),
                    UnitsInStock = Convert.ToInt16(tbxStock.Text),

                });
                MessageBox.Show("ürün eklendi!");
                Loadproducts();
            }
            catch(Exception exception)
            {
                MessageBox.Show(exception.Message);
                
            }
        

            
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                _productService.Update(new Product
                {
                    ProductId = Convert.ToInt32(dgwProduct.CurrentRow.Cells[0].Value),//productid'yi gridten alıyoruz
                    ProductName = tbxUpdateProductName.Text,
                    CategoryId = Convert.ToInt32(cbxCategoryIdUpdate.SelectedValue),
                    UnitsInStock = Convert.ToInt16(tbxUnitPriceUpdate.Text),
                    UnitPrice = Convert.ToDecimal(tbxUnitPriceUpdate.Text)
                });
                MessageBox.Show("Ürün Güncellendi.!");
                Loadproducts();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);

            }



        }

        private void dgwProduct_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var row = dgwProduct.CurrentRow;
            tbxUpdateProductName.Text = row.Cells[1].Value.ToString();
            cbxCategoryIdUpdate.SelectedValue = row.Cells[2].Value;
            tbxUnitPriceUpdate.Text = row.Cells[3].ToString();
            tbxQuantityPerUnitUpdate.Text = row.Cells[4].Value.ToString();
            tbxUnitInStockUpdate.Text = row.Cells[5].Value.ToString();

            tbxStock.Text=row.Cells[5].Value.ToString();
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if(dgwProduct.CurrentRow != null)
            {
                try
                {
                    var product = new Product();
                    var id = Convert.ToInt32(dgwProduct.CurrentRow.Cells[0].Value);
                    product.ProductId = id;
                    _productService.Delete(product);

                    //_productService.Delete(new Product
                    //{
                    //    ProductId = Convert.ToInt32(dgwProduct.CurrentRow.Cells[0].Value)
                    //});
                    MessageBox.Show("Ürün silindi!!");
                    Loadproducts();
                }
                catch(Exception exception)
                {
                    MessageBox.Show(exception.Message);
                }
              
            }

            

        }



    }
}
