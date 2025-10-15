using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using LIB0000;
using LIB0000.Services;

namespace LIB0000
{
    public partial class ProductsService : ObservableObject
    {

        #region Commands
        #endregion

        #region Constructor

        public ProductsService(string databasePath)
        {
            DatabasePath = databasePath;
            Product = new Product(DatabasePath);
            ProductStructure = new ProductStructure();
        }

        #endregion

        #region Events
        #endregion

        #region Fields        
        #endregion

        #region Methods
        #endregion

        #region Properties

        [ObservableProperty]
        private Product _product;

        [ObservableProperty]
        private ProductStructure _productStructure;

        [ObservableProperty]
        private string _databasePath;


        #endregion

    }

    public partial class Product : ObservableObject
    {

        #region Commands
        #endregion

        #region Constructor

        public Product(string databasePath)
        {
            DatabasePath = databasePath;
            GetList();
        }

        #endregion

        #region Events
        #endregion

        #region Fields

        ImageService ImageService;

        #endregion

        #region Methods

        public void AddImage()
        {
            //if (Edit.Name != "" && Edit.Name != null)
            //{
            using (System.Windows.Forms.OpenFileDialog openFileDialog = new System.Windows.Forms.OpenFileDialog())
            {
                if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    string selectedFileName = openFileDialog.FileName;

                    byte[] imageBytes = File.ReadAllBytes(selectedFileName);

                    ImageService imageService = new ImageService();

                    Edit.Image = imageService.ConvertFileLocationImageToByteArray(selectedFileName, 1000, 1000);


                    using (var context = new ServerDbContext(DatabasePath))
                    {
                        ProductModel row = context.ProductDbSet.FirstOrDefault((ProductModel e) => e.Id == Selected.Id);
                        if (row != null)
                        {
                            row.Image = Edit.Image;
                            context.SaveChanges();
                            GetList();
                            Selected = row;
                        }
                    }
                }
            }
            //}
        }
        public void AddRow(object structure)
        {
            using (var context = new ServerDbContext(DatabasePath))
            {
                ProductModel row = context.ProductDbSet.FirstOrDefault(e => e.Id == Edit.Id);

                if ((row == null) && (!string.IsNullOrEmpty(Edit.Name)))
                {
                    row = new ProductModel();
                    row.Name = Edit.Name;
                    row.Description = Edit.Description;
                    row.ProductGroupId = Edit.ProductGroupId;
                    ImageService = new ImageService();
                    row.Image = Edit.Image;
                    row.Structure = new XmlService().SerializeObjectToXml(structure);
                    context.ProductDbSet.Add(row);
                    context.SaveChanges();
                    Edit.Name = "";
                    Edit.Description = "";

                }
                else if (row != null)
                {
                    UpdateRow();
                }
            }
            GetList();
        }

        public void DeleteRow()
        {

            using (var context = new ServerDbContext(DatabasePath))
            {
                ProductModel row = context.ProductDbSet.FirstOrDefault(e => e.Name == Selected.Name);

                if (row != null)
                {

                    context.ProductDbSet.Remove(row);
                    Edit.Name = "";
                    Edit.Description = "";

                }
                context.SaveChanges();
            }
            GetList();
        }
        public void FilterList(string filter)
        {
            using (var context = new ServerDbContext(DatabasePath))
            {
                var result = from Products in context.ProductDbSet
                             where EF.Functions.Like(Products.Name, $"%{filter}%")
                             || EF.Functions.Like(Products.Description, $"%{filter}%")
                             orderby Products.Id
                             select Products;

                List = result.ToList();
            }
        }
        public void FilterListOnProductGroup(int filter)
        {
            using (var context = new ServerDbContext(DatabasePath))
            {
                var result = from Products in context.ProductDbSet
                             where EF.Functions.Like(Products.ProductGroupId.ToString(), filter.ToString())
                             orderby Products.Id
                             select Products;

                List = result.ToList();
            }
        }
        public void GetList()
        {
            int selectedId = 0;
            using (var context = new ServerDbContext(DatabasePath))
            {
                if (Selected != null)
                {
                    selectedId = Selected.Id;
                }

                var result = from Products in context.ProductDbSet
                             orderby Products.Id
                             select Products;

                List = result.ToList();

                if (selectedId > 0)
                {
                    Selected = List.Where(x => x.Id == selectedId).FirstOrDefault();
                }
            }
        }
        public void LoadProduct(int? productId)
        {
            using (var context = new ServerDbContext(DatabasePath))
            {
                ProductModel product = context.ProductDbSet.FirstOrDefault(p => p.Id == productId);

                if (product is ProductModel)
                {
                    Loaded = product;
                }
            }
        }
        public ProductModel GetProduct(int id)
        {
            return List.FirstOrDefault(e => e.Id == id);
        }
        public void UpdateRow()
        {
            using (var context = new ServerDbContext(DatabasePath))
            {
                if (string.IsNullOrEmpty(Edit.Name)) { return; }

                ProductModel row = context.ProductDbSet.FirstOrDefault(e => e.Id == Edit.Id);

                if (row != null)
                {
                    row.Name = Edit.Name;
                    row.Description = Edit.Description;
                    row.ProductGroupId = Edit.ProductGroupId;
                    row.Structure = Edit.Structure;
                    row.Image = Edit.Image;
                    context.ProductDbSet.Update(row);
                    context.SaveChanges();
                    Edit.Name = "";
                    Edit.Description = "";
                    GetList();
                }
            }
        }
        public void UpdateSelected()
        {
            if (Selected is ProductModel)
            {
                Edit.Image = Selected.Image;
                Edit.Name = Selected.Name;
                Edit.Description = Selected.Description;
                Edit.Id = Selected.Id;
                Edit.Structure = Selected.Structure;
            }
        }


        #endregion

        #region Properties

        [ObservableProperty]
        private string _databasePath;

        [ObservableProperty]
        private List<ProductModel> _list = new List<ProductModel>();

        [ObservableProperty]
        private ProductModel _edit = new ProductModel();

        [ObservableProperty]
        private ProductModel _selected = new ProductModel();

        [ObservableProperty]
        private ProductModel _loaded = new ProductModel();

        #endregion


    }

    public partial class ProductStructure : ObservableObject
    {

        #region Commands
        #endregion

        #region Constructor
        #endregion

        #region Events
        #endregion

        #region Fields
        #endregion

        #region Methods
        #endregion

        #region Properties

        [ObservableProperty]
        private ProductTyp _edit = new ProductTyp();

        #endregion

    }
}
