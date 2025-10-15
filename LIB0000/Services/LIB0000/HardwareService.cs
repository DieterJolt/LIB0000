using LIB0000.Lists;
using Microsoft.EntityFrameworkCore;

namespace LIB0000
{
    public partial class HardwareService : ObservableObject
    {

        #region Commands
        #endregion

        #region Constructor

        public HardwareService()
        {
            Hardware = new Hardware(DatabasePath);
            HardwareTypes = new HardwareTypes();
            Hardware.GetList();
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
        private Hardware _hardware;

        [ObservableProperty]
        private HardwareTypes _hardwareTypes;

        [ObservableProperty]
        private string _databasePath;

        [ObservableProperty]
        private PropertyReferenceModel _objectToWriteSelectedProduct;

        #endregion
    }

    public partial class Hardware : ObservableObject
    {

        #region Commands
        #endregion

        #region Constructor

        public Hardware(string databasePath)
        {
            DatabasePath = databasePath;
        }

        #endregion

        #region Events
        #endregion

        #region Fields
        #endregion

        #region Methods

        public void GetList()
        {
            using (var context = new LocalDbContext())
            {
                var result = from Hardware in context.HardwareDbSet
                             orderby Hardware.Id
                             select Hardware;

                List = result.ToList();
            }
        }
        public void AddRow()
        {
            using (var context = new LocalDbContext())
            {

                HardwareModel row = context.HardwareDbSet.FirstOrDefault(e => e.Id == Edit.Id);

                if ((row == null) && (!string.IsNullOrEmpty(Edit.Name)))
                {
                    row = new HardwareModel();
                    row.Name = Edit.Name;
                    row.HardwareTypeId = Edit.HardwareTypeId;
                    row.HardwareType = Edit.HardwareType;
                    context.HardwareDbSet.Add(row);
                    context.SaveChanges();
                    Edit.Name = "";
                }
                else if (row != null)
                {
                    UpdateRow();
                }

            }
            GetList();
            GetListJoinHardwareType();

        }
        public void DeleteRow()
        {

            using (var context = new LocalDbContext())
            {
                HardwareModel row = context.HardwareDbSet.FirstOrDefault(e => e.Name == SelectedJoin.HardwareName);

                if (row != null)
                {

                    context.HardwareDbSet.Remove(row);
                    Edit.Name = "";
                    Edit.HardwareType = HardwareType.None;
                }
                context.SaveChanges();
            }
            GetList();
            GetListJoinHardwareType();
        }

        public void UpdateRow()
        {
            using (var context = new LocalDbContext())
            {
                if (string.IsNullOrEmpty(Edit.Name)) { return; }

                HardwareModel row = context.HardwareDbSet.FirstOrDefault(e => e.Id == Edit.Id);

                if (row != null)
                {
                    row.Name = Edit.Name;
                    row.HardwareTypeId = Edit.HardwareTypeId;
                    row.HardwareType = Edit.HardwareType;
                    context.HardwareDbSet.Update(row);
                    context.SaveChanges();
                    Edit.Name = "";
                    GetList();
                }
            }
            GetListJoinHardwareType();
        }

        public void FilterList(string filter)
        {
            using (var context = new LocalDbContext())
            {
                var result = from Hardware in context.HardwareDbSet
                             where EF.Functions.Like(Hardware.Name, $"%{filter}%")
                             orderby Hardware.Id
                             select Hardware;

                List = result.ToList();
            }
        }

        public void UpdateSelected()
        {
            if (SelectedJoin is HardwareTypeJoinHardwareModel)
            {
                Edit.Name = SelectedJoin.HardwareName;
                Edit.HardwareTypeId = SelectedJoin.HardwareTypeId;
                Edit.HardwareType = SelectedJoin.HardwareType;
                Edit.Id = SelectedJoin.Id;
            }
        }

        public void GetListJoinHardwareType()
        {
            using (var context = new LocalDbContext())
            {
                var result = (from hardware in context.HardwareDbSet
                              join hardwareTypes in context.HardwareTypesDbSet
                              on hardware.HardwareType equals hardwareTypes.HardwareType
                              select new HardwareTypeJoinHardwareModel
                              {
                                  Id = hardware.Id,
                                  HardwareName = hardware.Name,
                                  HardwareDescription = hardwareTypes.Description,
                                  HardwareTypeId = hardwareTypes.Id,
                                  HardwareType = hardwareTypes.HardwareType,
                                  HardwareTypeName = hardwareTypes.Name,
                                  HardwareTypeDescription = hardwareTypes.Description,
                                  HardwareTypeImage = hardwareTypes.Image
                              }).ToList();
                JoinList = result.ToList();
            }

        }



        #endregion

        #region Properties


        [ObservableProperty]
        private List<HardwareModel> _list = new List<HardwareModel>();

        [ObservableProperty]
        private List<HardwareTypeJoinHardwareModel> _joinList = new List<HardwareTypeJoinHardwareModel>();

        [ObservableProperty]
        private HardwareModel _edit = new HardwareModel();

        [ObservableProperty]
        private HardwareModel _selected = new HardwareModel();

        [ObservableProperty]
        private HardwareTypeJoinHardwareModel _selectedJoin = new HardwareTypeJoinHardwareModel();

        [ObservableProperty]
        private string _databasePath;

        #endregion


    }

    public partial class HardwareTypes : ObservableObject
    {

        #region Commands 
        #endregion

        #region Constructor

        public HardwareTypes()
        {
            AddTypesToDatabase();
        }

        #endregion

        #region Events
        #endregion

        #region Fields      
        #endregion

        #region Methods

        public void GetList()
        {
            using (var context = new LocalDbContext())
            {
                var result = from HardwareTypes in context.HardwareTypesDbSet
                             orderby HardwareTypes.Id
                             select HardwareTypes;

                List = result.ToList();
            }
        }

        public void AddTypesToDatabase()
        {
            using (var context = new LocalDbContext())
            {

                List<HardwareTypesModel> lTypes = HardwareTypesList.GetTypes();

                foreach (var type in lTypes)
                {
                    bool typeExists = context.HardwareTypesDbSet
                .Any(existingType => existingType.Id == type.Id);

                    if (!typeExists)
                    {
                        context.HardwareTypesDbSet.Add(type);
                        context.SaveChanges();
                    }
                }
            }
        }

        public void filterList(string filter)
        {
            using (var context = new LocalDbContext())
            {
                var result = from HardwareTypes in context.HardwareTypesDbSet
                             where EF.Functions.Like(HardwareTypes.Name, $"%{filter}%")
                             || EF.Functions.Like(HardwareTypes.Description, $"%{filter}%")
                             orderby HardwareTypes.Id
                             select HardwareTypes;

                List = result.ToList();
            }
        }

        #endregion

        #region Properties


        [ObservableProperty]
        private List<HardwareTypesModel> _list = new List<HardwareTypesModel>();

        [ObservableProperty]
        private HardwareTypesModel _edit = new HardwareTypesModel();

        [ObservableProperty]
        private HardwareTypesModel _selected = new HardwareTypesModel();



        #endregion


    }
}
