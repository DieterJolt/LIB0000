using System.Globalization;
using LIB0000.Services;


namespace LIB0000
{
    public partial class RecipeService : ObservableObject
    {

        #region Commands
        #endregion

        #region Constructor

        public RecipeService(string databasePath)
        {
            DatabasePath = databasePath;

            Recipe = new Recipe(DatabasePath);
            RecipeDetail = new RecipeDetail(Recipe, DatabasePath);

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
        private string _databasePath;

        [ObservableProperty]
        private Recipe _recipe;

        [ObservableProperty]
        private RecipeDetail _recipeDetail;

        #endregion


    }

    public partial class Recipe : ObservableObject
    {

        #region Commands
        #endregion

        #region Constructor

        public Recipe(string databasePath)
        {
            DatabasePath = databasePath;
            xmlService = new XmlService();
            GetList();

            if ((List.Count() == 0) || (List == null))
            {

            }

            GetList();
        }

        #endregion

        #region Events
        #endregion

        #region Fields
        private XmlService xmlService;
        #endregion

        #region Methods

        public void GetList()
        {
            using (var context = new ServerDbContext(DatabasePath))
            {
                var result = from recipes in context.RecipeDbSet
                             orderby recipes.Name
                             select recipes;

                List = result.ToList();
            }
        }

        public void AddRow(object recipeDetails)
        {
            using (var context = new ServerDbContext(DatabasePath))
            {

                RecipeModel row = context.RecipeDbSet.FirstOrDefault(e => e.Name == Edit.Name);

                if ((row == null) && (!string.IsNullOrEmpty(Edit.Name)))
                {
                    row = new RecipeModel();
                    row.Name = Edit.Name;
                    row.Description = Edit.Description;
                    row.BarCode = Edit.BarCode;
                    row.Extra1 = Edit.Extra1;
                    row.Extra2 = Edit.Extra2;
                    row.Extra3 = Edit.Extra3;
                    row.DateCreated = DateTime.Now;
                    row.DateModified = DateTime.Now;
                    context.RecipeDbSet.Add(row);
                    context.SaveChanges();
                    row = context.RecipeDbSet.FirstOrDefault(e => e.Name == Edit.Name);
                    Edit.Id = row.Id;

                    //Detail toevoegen
                    string xmlContent = xmlService.SerializeObjectToXml(recipeDetails);
                    RecipeDetailModel recipeDetailModel = new RecipeDetailModel();
                    recipeDetailModel.RecipeId = row.Id;
                    recipeDetailModel.RecipeDetail = xmlContent;

                    context.RecipeDetailsDbSet.Add(recipeDetailModel);
                    context.SaveChanges();


                    Edit.Name = "";
                    Edit.Description = "";
                    Edit.PathPicture = "";
                    Edit.BarCode = "";
                    Edit.Extra1 = "";
                    Edit.Extra2 = "";
                    Edit.Extra3 = "";

                    GetList();
                    Selected = List.FirstOrDefault(e => e.Id == Edit.Id);
                }

            }
        }

        public void UpdateRow()
        {
            using (var context = new ServerDbContext(DatabasePath))
            {
                if (string.IsNullOrEmpty(Edit.Name)) { return; }

                RecipeModel row = context.RecipeDbSet.FirstOrDefault(e => e.Id == Selected.Id);

                if (row != null)
                {
                    row.Name = Edit.Name;
                    row.Description = Edit.Description;
                    row.BarCode = Edit.BarCode;
                    row.Extra1 = Edit.Extra1;
                    row.Extra2 = Edit.Extra2;
                    row.Extra3 = Edit.Extra3;
                    row.DateModified = DateTime.Now;
                    context.SaveChanges();
                    GetList();
                    Selected = row; //

                    Edit.Name = "";
                    Edit.Description = "";
                    Edit.PathPicture = "";
                    Edit.BarCode = "";
                    Edit.Extra1 = "";
                    Edit.Extra2 = "";
                    Edit.Extra3 = "";
                }
            }
        }

        public void UpdateSelected()
        {
            if (Selected is RecipeModel)
            {
                Edit.Name = Selected.Name;
                Edit.Description = Selected.Description;
                Edit.PathPicture = Selected.PathPicture;
                Edit.BarCode = Selected.BarCode;
                Edit.Extra1 = Selected.Extra1;
                Edit.Extra2 = Selected.Extra2;
                Edit.Extra3 = Selected.Extra3;
            }

        }

        public void AddImage(string selectedFileName)
        {
            using (var context = new ServerDbContext(DatabasePath))
            {
                RecipeModel row = context.RecipeDbSet.FirstOrDefault(e => e.Id == Selected.Id);

                if (row != null)
                {
                    row.PathPicture = selectedFileName;
                    row.DateModified = DateTime.Now;
                    context.SaveChanges();
                    GetList();
                    Selected = row;
                }
            }
        }

        public void DeleteRow()
        {
            using (var context = new ServerDbContext(DatabasePath))
            {
                RecipeModel row = context.RecipeDbSet.FirstOrDefault(e => e.Id == Selected.Id);

                if (row != null)
                {
                    context.RecipeDbSet.Remove(row);
                    Edit.Name = "";
                    Edit.Description = "";
                    Edit.PathPicture = "";
                    Edit.BarCode = "";
                    Edit.Extra1 = "";
                    Edit.Extra2 = "";
                    Edit.Extra3 = "";
                }

                //Delete alle details van het recept
                var detailRows = context.RecipeDetailsDbSet.Where(e => e.RecipeId == Selected.Id);

                foreach (var detailRow in detailRows)
                {
                    context.RecipeDetailsDbSet.Remove(detailRow);
                }
                context.SaveChanges();
            }
            GetList();

        }

        public void LoadRecipe(int id)
        {
            using (var context = new ServerDbContext(DatabasePath))
            {
                RecipeModel row = context.RecipeDbSet.FirstOrDefault(e => e.Id == id);

                if (row != null)
                {
                    Loaded = row;
                }
            }
        }

        #endregion

        #region Properties

        [ObservableProperty]
        public string _databasePath;

        [ObservableProperty]
        public List<RecipeModel> _list = new List<RecipeModel>();

        [ObservableProperty]
        private RecipeModel _edit = new RecipeModel();

        [ObservableProperty]
        private RecipeModel _selected = new RecipeModel();

        [ObservableProperty]
        private RecipeModel _loaded = new RecipeModel();

        #endregion


    }

    public partial class RecipeDetail : ObservableObject
    {

        #region Commands
        #endregion

        #region Constructor
        public RecipeDetail(Recipe recipe, string databasePath)
        {
            DatabasePath = databasePath;
            xmlService = new XmlService();
            Recipe = recipe;
        }

        #endregion

        #region Events
        #endregion

        #region Fields        
        private XmlService xmlService;
        #endregion

        private Recipe Recipe;

        #region Methods


        public bool UpdateRow(object recipeDetails, int recipeId)
        {
            bool result = true;

            string xmlContent = xmlService.SerializeObjectToXml(recipeDetails);

            using (var context = new ServerDbContext(DatabasePath))
            {
                RecipeDetailModel row = context.RecipeDetailsDbSet.FirstOrDefault(e => e.RecipeId == recipeId);

                if (row != null)
                {
                    row.RecipeDetail = xmlContent;

                    context.RecipeDetailsDbSet.Update(row);
                    context.SaveChanges();
                }
            }

            return result;
        }

        public T Get<T>(int recipeId)
        {
            using (var context = new ServerDbContext(DatabasePath))
            {
                var get = context.RecipeDetailsDbSet.FirstOrDefault(e => e.RecipeId == recipeId);

                if (get != null)
                {
                    return xmlService.DeserializeXmlToObject<T>(get.RecipeDetail);
                }
                else
                {
                    return default(T);
                }
            }
        }

        public string ConvertToSystemDecimal(string input)
        {
            // Get the decimal separator for the current culture
            string systemDecimalSeparator = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;

            // Replace points and commas with the system's decimal separator
            string result = input.Replace(".", systemDecimalSeparator).Replace(",", systemDecimalSeparator);

            return result;
        }

        #endregion

        #region Properties   

        [ObservableProperty]
        private string _databasePath;

        #endregion


    }

}

