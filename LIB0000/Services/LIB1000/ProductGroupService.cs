using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using Microsoft.EntityFrameworkCore;

namespace LIB0000
{
    public partial class ProductGroupService : ObservableObject
    {

        #region Commands
        #endregion

        #region Constructor

        public ProductGroupService(string databasePath)
        {
            DatabasePath = databasePath;
            ProductGroup = new ProductGroup(DatabasePath);
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
        private ProductGroup _productGroup;

        [ObservableProperty]
        private string _databasePath;

        #endregion

    }

    public partial class ProductGroup : ObservableObject
    {

        #region Commands
        #endregion

        #region Constructor

        public ProductGroup(string databasePath)
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

        public void GetList()
        {
            using (var context = new ServerDbContext(DatabasePath))
            {
                var result = from Products in context.ProductGroupDbSet
                             orderby Products.Id
                             select Products;

                List = result.ToList();

            }
        }
        public void AddRow()
        {
            using (var context = new ServerDbContext(DatabasePath))
            {

                ProductGroupModel row = context.ProductGroupDbSet.FirstOrDefault(e => e.Id == Edit.Id);

                if ((row == null) && (!string.IsNullOrEmpty(Edit.Name)))
                {
                    row = new ProductGroupModel();
                    row.Name = Edit.Name;
                    row.Description = Edit.Description;
                    ImageService = new ImageService();
                    row.Image = Edit.Image;
                    context.ProductGroupDbSet.Add(row);
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
                ProductGroupModel row = context.ProductGroupDbSet.FirstOrDefault(e => e.Name == Selected.Name);

                if (row != null)
                {
                    context.ProductGroupDbSet.Remove(row);
                    Edit.Name = "";
                    Edit.Description = "";
                }
                context.SaveChanges();
            }
            GetList();
        }

        public void LoadProductGroup(int? productGroupId)
        {
            using (var context = new ServerDbContext(DatabasePath))
            {
                ProductGroupModel productGroup = context.ProductGroupDbSet.FirstOrDefault(p => p.Id == productGroupId);

                if (productGroup is ProductGroupModel)
                {
                    Loaded = productGroup;
                }
            }
        }

        public ProductGroupModel GetProductGroup(int id)
        {
            return List.FirstOrDefault(e => e.Id == id);
        }

        public void UpdateRow()
        {
            using (var context = new ServerDbContext(DatabasePath))
            {
                if (string.IsNullOrEmpty(Edit.Name)) { return; }

                ProductGroupModel row = context.ProductGroupDbSet.FirstOrDefault(e => e.Id == Edit.Id);

                if (row != null)
                {
                    row.Name = Edit.Name;
                    row.Description = Edit.Description;
                    context.ProductGroupDbSet.Update(row);
                    context.SaveChanges();
                    Edit.Name = "";
                    Edit.Description = "";
                    GetList();
                }
            }
        }
        public void UpdateSelected()
        {
            if (Selected is ProductGroupModel)
            {
                Edit.Name = Selected.Name;
                Edit.Description = Selected.Description;
                Edit.Image = Selected.Image;
                Edit.Id = Selected.Id;
            }
        }
        public void AddImage()
        {
            if (Edit.Name != "" && Edit.Name != null)
            {
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
                            ProductGroupModel row = context.ProductGroupDbSet.FirstOrDefault((ProductGroupModel e) => e.Id == Selected.Id);
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
            }
        }

        public void FilterList(string filter)
        {
            using (var context = new ServerDbContext(DatabasePath))
            {
                var result = from Products in context.ProductGroupDbSet
                             where EF.Functions.Like(Products.Name, $"%{filter}%")
                             || EF.Functions.Like(Products.Description, $"%{filter}%")
                             orderby Products.Id
                             select Products;

                List = result.ToList();
            }
        }

        #endregion

        #region Properties

        [ObservableProperty]
        private string _databasePath;

        [ObservableProperty]
        private List<ProductGroupModel> _list = new List<ProductGroupModel>();

        [ObservableProperty]
        private ProductGroupModel _loaded = new();

        [ObservableProperty]
        private ProductGroupModel _edit = new ProductGroupModel();

        [ObservableProperty]
        private ProductGroupModel _selected = new ProductGroupModel();

        #endregion

    }
}
