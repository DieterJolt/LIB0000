using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

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
        private string _databasePath;

        [ObservableProperty]
        private PropertyReferenceModel _objectToWriteSelectedProduct;

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

        public void AddRow()
        {
            using (var context = new ServerDbContext(DatabasePath))
            {
                ProductModel row = context.ProductDbSet.FirstOrDefault(e => e.Id == Edit.Id);

                if ((row == null) && (!string.IsNullOrEmpty(Edit.Name)))
                {
                    row = new ProductModel();
                    row.Name = Edit.Name;
                    row.Description = Edit.Description;
                    ImageService = new ImageService();
                    row.Image = Edit.Image;
                    context.ProductDbSet.Add(row);
                    context.SaveChanges();
                    Edit.Name = "";
                    Edit.Description = "";

                    addProductHardwareDatabase(row.Id);
                    addDefaultSettingsToProductValueSettingsDatabase(row.Id);
                }
                else if (row != null)
                {
                    UpdateRow();
                }
            }
            GetList();
        }

        private void addProductHardwareDatabase(int productId)
        {
            using (var localContext = new LocalDbContext())
            {
                // Voor alle hardware de nodige records aanmaken in de tabel ProductHardware, en als default de eerste functie van de hardware kiezen
                foreach (HardwareModel hardware in localContext.HardwareDbSet)
                {
                    ProductHardwareModel productHardware = new ProductHardwareModel();
                    productHardware.ProductId = productId;
                    productHardware.HardwareId = hardware.Id;
                    productHardware.HardwareFunction = HardwareFunctionMapper.GetFunctionsForHardware(hardware.HardwareType).First();

                    localContext.ProductHardwareDbSet.Add(productHardware);
                    localContext.SaveChanges();
                }
            }
        }
        private void addDefaultSettingsToProductValueSettingsDatabase(int productId)
        {

            using (var context = new LocalDbContext())
            {
                foreach (ProductDetailModel productDetail in context.ProductDetailsDbSet)
                {
                    ProductDetailValueModel productDetailsValue = new ProductDetailValueModel();
                    productDetailsValue = context.ProductDetailValueDbSet.FirstOrDefault(psv => psv.ProductId == productId && psv.HardwareId == productDetail.HardwareId && psv.HardwareFunction == psv.HardwareFunction && psv.SettingNr == psv.SettingNr);

                    // setting does not exist, create it
                    if (productDetailsValue == null)
                    {
                        productDetailsValue = new ProductDetailValueModel();
                        productDetailsValue.ProductId = productId;
                        productDetailsValue.SettingNr = productDetail.Nr;
                        productDetailsValue.HardwareId = productDetail.HardwareId;
                        productDetailsValue.HardwareFunction = productDetail.HardwareFunction;
                        productDetailsValue.SettingValue = productDetail.DefaultValue;

                        context.ProductDetailValueDbSet.Add(productDetailsValue);
                    }
                }
                context.SaveChanges();
            }
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
        public void GetList()
        {
            using (var context = new ServerDbContext(DatabasePath))
            {
                var result = from Products in context.ProductDbSet
                             orderby Products.Id
                             select Products;

                List = result.ToList();
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
}
