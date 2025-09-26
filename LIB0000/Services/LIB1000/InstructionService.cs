
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Forms;
using LIB0000.Lists;
using Microsoft.EntityFrameworkCore;

namespace LIB0000
{
    public partial class InstructionService : ObservableObject
    {

        #region Commands
        #endregion

        #region Constructor

        public InstructionService(string databasePath)
        {
            DatabasePath = databasePath;
            Instructions = new Instructions(DatabasePath);
            InstructionLists = new InstructionLists(DatabasePath);
            InstructionTypes = new InstructionTypes(DatabasePath);
            InstructionHistory = new InstructionsHistory(DatabasePath);
            //NavigationService = navigationService;
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
        private Instructions _instructions;

        [ObservableProperty]
        private InstructionLists _instructionLists;

        [ObservableProperty]
        private InstructionTypes _instructionTypes;

        [ObservableProperty]
        private InstructionsHistory _instructionHistory;

        [ObservableProperty]
        private string _databasePath;

        #endregion


    }

    public partial class Instructions : ObservableObject
    {

        #region Commands



        #endregion

        #region Constructor

        public Instructions(string databasePath)
        {

            DatabasePath = databasePath;
        }

        #endregion

        #region Events
        #endregion

        #region Fields

        ImageService ImageService;

        #endregion

        #region Methods        
        public void AddRow(int instructionListIVersionId, InstructionTypesModel InstructionType)
        {
            using (var context = new ServerDbContext(DatabasePath))
            {
                InstructionModel row = context.InstructionDbSet.FirstOrDefault(e => e.Id == Edit.Id);

                ImageService imageService = new ImageService();
                byte[]? emptyImage = imageService.ConvertImageToByteArray("pack://application:,,,/Assets/Images/VoorbeeldAfbeelding.png"); ;

                if (row == null)
                {
                    row = new InstructionModel();
                    row.InstructionListVersionId = instructionListIVersionId;
                    row.InstructionType = InstructionType.InstructionType;
                    row.InstructionTypeName = InstructionType.Name;
                    row.Sequence = CountModels(instructionListIVersionId);
                    ImageService = new ImageService();
                    row.Image1 = emptyImage;
                    row.Image2 = emptyImage;
                    row.Image3 = emptyImage;
                    row.Image4 = emptyImage;
                    row.Image5 = emptyImage;
                    row.Image6 = emptyImage;
                    row.Image7 = emptyImage;
                    row.Image8 = emptyImage;
                    row.Image9 = emptyImage;
                    row.Image10 = emptyImage;
                    row.Text1 = "";
                    row.Text2 = "";
                    row.Text3 = "";
                    row.Text4 = "";
                    row.Text5 = "";
                    row.Text6 = "";
                    row.Text7 = "";
                    row.Text8 = "";
                    row.Text9 = "";
                    row.Text10 = "";
                    row.HotspotId = ""; // Eerst leeg bewaren, om straks Id op te halen
                    context.InstructionDbSet.Add(row);
                    context.SaveChanges(); // Eerst bewaren zodat we het id kunnen gebruiken om hotspotId te genereeren
                    row.HotspotId = row.Id.ToString("D6");
                    context.SaveChanges();
                }
                else if (row != null)
                {
                    UpdateRow();
                }

            }
            GetJoinList(instructionListIVersionId);
        }

        public void DeleteRow()
        {

            if (SelectedJoin != null)
            {
                using (var context = new ServerDbContext(DatabasePath))
                {
                    InstructionModel row = context.InstructionDbSet.FirstOrDefault(e => e.Id == SelectedJoin.InstructionId && e.InstructionListVersionId == SelectedJoin.InstructionsListVersionId);

                    if (row != null)
                    {
                        int listVersionId = row.InstructionListVersionId;
                        int sequence = row.Sequence;
                        var list = context.InstructionDbSet
                            .Where(e => e.Sequence > sequence)
                            .ToList();

                        foreach (var item in list)
                        {
                            item.Sequence -= 1;
                        }

                        context.InstructionDbSet.Remove(row);
                        context.SaveChanges();
                        GetJoinList(listVersionId);
                    }
                }
            }



        }
        public void UpdateRow()
        {
            using (var context = new ServerDbContext(DatabasePath))
            {
                InstructionModel row = context.InstructionDbSet.FirstOrDefault(e => e.Id == SelectedJoin.InstructionId && e.InstructionListVersionId == SelectedJoin.InstructionsListVersionId);

                if (row != null)
                {
                    row.InstructionType = Edit.InstructionType;
                    row.InstructionTypeName = Edit.InstructionTypeName;
                    row.Text1 = Edit.Text1;
                    row.Text2 = Edit.Text2;
                    row.Text3 = Edit.Text3;
                    row.Text4 = Edit.Text4;
                    row.Text5 = Edit.Text5;
                    row.Text6 = Edit.Text6;
                    row.Text7 = Edit.Text7;
                    row.Text8 = Edit.Text8;
                    row.Text9 = Edit.Text9;
                    row.Text10 = Edit.Text10;
                    row.Image1 = Edit.Image1;
                    row.Image2 = Edit.Image2;
                    row.Image3 = Edit.Image3;
                    row.Image4 = Edit.Image4;
                    row.Image5 = Edit.Image5;
                    row.Image6 = Edit.Image6;
                    row.Image7 = Edit.Image7;
                    row.Image8 = Edit.Image8;
                    row.Image9 = Edit.Image9;
                    row.Image10 = Edit.Image10;
                    context.InstructionDbSet.Update(row);
                    context.SaveChanges();
                    GetJoinList(SelectedJoin.InstructionsListVersionId);
                }
            }
        }

        public void MoveOneUp(int ListVersionId, int instructionId)
        {
            using (var context = new ServerDbContext(DatabasePath))
            {
                var rows = context.InstructionDbSet
                    .Where(e => e.InstructionListVersionId == ListVersionId)
                    .Where(e => e.Id == instructionId || e.Sequence == context.InstructionDbSet
                        .Where(x => x.InstructionListVersionId == ListVersionId && x.Id == instructionId)
                        .Select(x => x.Sequence - 1)
                        .FirstOrDefault())
                    .ToList();

                var currentRow = rows.FirstOrDefault(e => e.Id == instructionId);
                var aboveRow = rows.FirstOrDefault(e => e.Sequence == currentRow?.Sequence - 1);

                if (currentRow != null && aboveRow != null)
                {
                    // Swap sequences
                    (currentRow.Sequence, aboveRow.Sequence) = (aboveRow.Sequence, currentRow.Sequence);
                    context.SaveChanges();
                }
            }
            GetJoinList(ListVersionId);
        }

        public void MoveOneDown(int ListVersionId, int instructionId)
        {
            using (var context = new ServerDbContext(DatabasePath))
            {
                var rows = context.InstructionDbSet
                    .Where(e => e.InstructionListVersionId == ListVersionId)
                    .Where(e => e.Id == instructionId || e.Sequence == context.InstructionDbSet
                        .Where(x => x.InstructionListVersionId == ListVersionId && x.Id == instructionId)
                        .Select(x => x.Sequence + 1)
                        .FirstOrDefault())
                    .ToList();

                var currentRow = rows.FirstOrDefault(e => e.Id == instructionId);
                var belowRow = rows.FirstOrDefault(e => e.Sequence == currentRow?.Sequence + 1);

                if (currentRow != null && belowRow != null)
                {
                    // Swap sequences
                    (currentRow.Sequence, belowRow.Sequence) = (belowRow.Sequence, currentRow.Sequence);
                    context.SaveChanges();
                }
            }
            GetJoinList(ListVersionId);
        }
        public void UpdateSelected()
        {
            if (Selected is InstructionModel)
            {
                Edit.Id = Selected.Id;
                Edit.InstructionTypeName = Selected.InstructionTypeName;
                Edit.InstructionType = Selected.InstructionType;
                Edit.Text1 = Selected.Text1;
                Edit.Text2 = Selected.Text2;
                Edit.Text3 = Selected.Text3;
                Edit.Text4 = Selected.Text4;
                Edit.Text5 = Selected.Text5;
                Edit.Text6 = Selected.Text6;
                Edit.Text7 = Selected.Text7;
                Edit.Text8 = Selected.Text8;
                Edit.Text9 = Selected.Text9;
                Edit.Text10 = Selected.Text10;
                Edit.Image1 = Selected.Image1;
                Edit.Image2 = Selected.Image2;
                Edit.Image3 = Selected.Image3;
                Edit.Image4 = Selected.Image4;
                Edit.Image5 = Selected.Image5;
                Edit.Image6 = Selected.Image6;
                Edit.Image7 = Selected.Image7;
                Edit.Image8 = Selected.Image8;
                Edit.Image9 = Selected.Image9;
                Edit.Image10 = Selected.Image10;
            }
        }

        public byte[] ConvertImageFileLocationToByteArray()
        {
            byte[] result = null;

            using (System.Windows.Forms.OpenFileDialog openFileDialog = new System.Windows.Forms.OpenFileDialog())
            {
                openFileDialog.Title = "Kies bestand";
                openFileDialog.Filter = "Afbeeldingen|*.jpg;*.jpeg;*.png;*.gif;*.bmp|Alle bestanden (*.*)|*.*";

                if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    string selectedFileName = openFileDialog.FileName;
                    result = File.ReadAllBytes(selectedFileName);

                }
            }
            return result;
        }

        public void AddImage(string parameter)
        {
            using (System.Windows.Forms.OpenFileDialog openFileDialog = new System.Windows.Forms.OpenFileDialog())
            {
                openFileDialog.Title = "Kies bestand";
                openFileDialog.Filter = "Afbeeldingen|*.jpg;*.jpeg;*.png;*.gif;*.bmp|Alle bestanden (*.*)|*.*";

                if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    string selectedFileName = openFileDialog.FileName;
                    byte[] imageBytes = File.ReadAllBytes(selectedFileName);

                    using (var context = new ServerDbContext(DatabasePath))
                    {
                        InstructionModel row = context.InstructionDbSet.FirstOrDefault(e => e.Id == Selected.Id);
                        if (row != null)
                        {

                            var property = typeof(InstructionModel).GetProperty(parameter);
                            if (property != null && property.CanWrite)
                            {
                                property.SetValue(row, imageBytes);
                                context.SaveChanges();
                                UpdateSelected();
                            }
                        }
                    }
                }
            }
        }

        public int CountModels(int ListVersionId)
        {
            using (var context = new ServerDbContext(DatabasePath))
            {
                int count = context.InstructionDbSet.Where(e => e.InstructionListVersionId == ListVersionId).Count();
                return count + 1;
            }
        }

        public void GetJoinList(int listVersionId)
        {
            using (var context = new ServerDbContext(DatabasePath))
            {
                JoinList = context.InstructionDbSet
                    .Where(instruction => instruction.InstructionListVersionId == listVersionId)
                    .Join(
                        context.InstructionTypesDbSet,
                        instruction => instruction.InstructionType,
                        instructionType => instructionType.InstructionType,
                        (instruction, instructionType) => new InstructionJoinModel
                        {
                            InstructionsListDetailModelId = instruction.Id,
                            InstructionsListVersionId = instruction.InstructionListVersionId,
                            Sequence = instruction.Sequence,
                            InstructionHotspotId = instruction.HotspotId,
                            InstructionTypeName = instruction.InstructionTypeName,
                            InstructionId = instruction.Id,
                            InstructionIcon = instructionType.Icon
                        })
                    .OrderBy(instruction => instruction.Sequence)
                    .AsNoTracking()  // Improves performance by preventing EF from tracking changes
                    .ToList();
            }
        }

        public InstructionModel GetInstructionFromSequence(int? listId, int sequence)
        {
            using (var context = new ServerDbContext(DatabasePath))
            {
                var query = from instructionList in context.InstructionListDbSet
                            join instructionListVersion in context.InstructionListVersionDbSet
                            on instructionList.Id equals instructionListVersion.InstructionListId
                            join instruction in context.InstructionDbSet
                            on instructionListVersion.Id equals instruction.InstructionListVersionId
                            where instructionList.Id == listId
                            && instructionList.CurrentVersion == instructionListVersion.Version
                            && instruction.Sequence == sequence
                            select instruction;

                return query.FirstOrDefault();
            }
        }

        public InstructionModel GetInstruction(int? id)
        {
            using (var context = new ServerDbContext(DatabasePath))
            {
                var query = from instruction in context.InstructionDbSet
                            where instruction.Id == id
                            select instruction;

                return query.FirstOrDefault();
            }
        }

        public int GetInstructionCount(int? listId)
        {
            using (var context = new ServerDbContext(DatabasePath))
            {
                return (from instructionList in context.InstructionListDbSet
                        join instructionListVersion in context.InstructionListVersionDbSet
                        on instructionList.Id equals instructionListVersion.InstructionListId
                        join instruction in context.InstructionDbSet
                        on instructionListVersion.Id equals instruction.InstructionListVersionId
                        where instructionList.Id == listId
                        && instructionList.CurrentVersion == instructionListVersion.Version
                        select instruction).Count();

            }
        }

        public int CountInstructionsInVersionList(int listId, int version)
        {
            using (var context = new ServerDbContext(DatabasePath))
            {
                return (from instructionList in context.InstructionListDbSet
                        join instructionListVersion in context.InstructionListVersionDbSet
                        on instructionList.Id equals instructionListVersion.InstructionListId
                        join instruction in context.InstructionDbSet
                        on instructionListVersion.Id equals instruction.InstructionListVersionId
                        where instructionList.Id == listId
                        && instructionListVersion.Version == version
                        select instruction).Count();
            }
        }

        #endregion

        #region Properties

        [ObservableProperty]
        private string _databasePath;



        [ObservableProperty]
        private InstructionModel _edit = new InstructionModel();

        [ObservableProperty]
        private InstructionModel _selected = new InstructionModel();

        [ObservableProperty]
        private List<InstructionJoinModel> _joinList = new List<InstructionJoinModel>();

        [ObservableProperty]
        private InstructionJoinModel _selectedJoin = new InstructionJoinModel();

        #endregion


    }

    public partial class InstructionsHistory : ObservableObject
    {

        #region Commands
        #endregion

        #region Constructor

        public InstructionsHistory(string databasePath)
        {

            DatabasePath = databasePath;
        }

        #endregion

        #region Events
        #endregion

        #region Fields
        #endregion

        #region Methods

        public void GetJoinModel()
        {
            if (OrderHistorySelected != null)
            {
                using (var context = new ServerDbContext(DatabasePath))
                {
                    HistorySelected = context.InstructionHistoryDbSet
                         .Where(instructionHistory => instructionHistory.Id == OrderHistorySelected.RelatedId)
                         .Join(
                             context.InstructionDbSet,
                             instructionHistory => instructionHistory.InstructionId,
                             instruction => instruction.Id,
                             (history, instruction) => new InstructionModel
                             {
                                 Id = instruction.Id,
                                 InstructionType = instruction.InstructionType,
                                 InputText1 = history.InputText1,
                                 InputText2 = history.InputText2,
                                 InputText3 = history.InputText3,
                                 InputText4 = history.InputText4,
                                 InputText5 = history.InputText5,
                                 InputText6 = history.InputText6,
                                 InputText7 = history.InputText7,
                                 InputText8 = history.InputText8,
                                 InputText9 = history.InputText9,
                                 InputText10 = history.InputText10,
                                 InputBool1 = (bool)history.InputBool1,
                                 InputBool2 = (bool)history.InputBool2,
                                 InputBool3 = (bool)history.InputBool3,
                                 InputBool4 = (bool)history.InputBool4,
                                 InputBool5 = (bool)history.InputBool5,
                                 InputBool6 = (bool)history.InputBool6,
                                 InputBool7 = (bool)history.InputBool7,
                                 InputBool8 = (bool)history.InputBool8,
                                 InputBool9 = (bool)history.InputBool9,
                                 InputBool10 = (bool)history.InputBool10,
                                 Text1 = instruction.Text1,
                                 Text2 = instruction.Text2,
                                 Text3 = instruction.Text3,
                                 Text4 = instruction.Text4,
                                 Text5 = instruction.Text5,
                                 Text6 = instruction.Text6,
                                 Text7 = instruction.Text7,
                                 Text8 = instruction.Text8,
                                 Text9 = instruction.Text9,
                                 Text10 = instruction.Text10,
                                 Image1 = instruction.Image1,
                                 Image2 = instruction.Image2,
                                 Image3 = instruction.Image3,
                                 Image4 = instruction.Image4,
                                 Image5 = instruction.Image5,
                                 Image6 = instruction.Image6,
                                 Image7 = instruction.Image7,
                                 Image8 = instruction.Image8,
                                 Image9 = instruction.Image9,
                                 Image10 = instruction.Image10,
                                 InputImage1 = history.InputImage1,
                             })
                         .AsNoTracking()
                         .FirstOrDefault();
                }
            }
        }

        #endregion

        #region Properties

        [ObservableProperty]
        private string _databasePath;

        [ObservableProperty]
        private InstructionModel _historySelected = new InstructionModel();

        [ObservableProperty]
        private OrderHistoryModel _orderHistorySelected = new OrderHistoryModel();

        #endregion


    }
    public partial class InstructionLists : ObservableObject
    {

        #region Commands

        [RelayCommand]
        private void cmdAddRow()
        {
            AddRow();
        }

        [RelayCommand]
        private void cmdDeleteRow()
        {
            DeleteRow();
            UpdateRow();
        }

        [RelayCommand]
        private void cmdUpdateRow()
        {
            UpdateRow();
        }

        #endregion

        #region Constructor

        public InstructionLists(string databasePath)
        {

            DatabasePath = databasePath;
            GetList();
        }

        #endregion

        #region Events
        #endregion

        #region Fields
        #endregion

        #region Methods

        public void Approve()
        {
            using (var context = new ServerDbContext(DatabasePath))
            {
                InstructionListVersionModel row = context.InstructionListVersionDbSet.FirstOrDefault(i => i.Id == SelectedVersion.Id);

                if (row != null)
                {
                    row.Approved = true;
                    row.DateTimeApproved = DateTime.Now;
                    context.InstructionListVersionDbSet.Update(row);
                    context.SaveChanges();
                    SelectedVersion = row;
                }
            }
        }

        public bool CheckIfVersionListisApproved(int id)
        {
            using (var context = new ServerDbContext(DatabasePath))
            {
                InstructionListVersionModel row = context.InstructionListVersionDbSet.FirstOrDefault(i => i.Id == id);

                if ((row != null) && (row.Approved == true))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public void GetList()
        {
            using (var context = new ServerDbContext(DatabasePath))
            {
                var result = from InstructionLists in context.InstructionListDbSet
                             orderby InstructionLists.Id
                             select InstructionLists;

                List = result.ToList();
            }
        }

        public void GetListVersion()
        {
            using (var context = new ServerDbContext(DatabasePath))
            {
                var result = from InstructionListVersion in context.InstructionListVersionDbSet
                             where InstructionListVersion.InstructionListId == Selected.Id
                             orderby InstructionListVersion.Version descending
                             select InstructionListVersion;

                ListVersions = result.ToList();
            }
        }

        public void AddRow()
        {
            using (var context = new ServerDbContext(DatabasePath))
            {

                InstructionListModel row = context.InstructionListDbSet.FirstOrDefault(e => e.Id == Edit.Id);

                if ((row == null) && (!string.IsNullOrEmpty(Edit.Name)))
                {
                    row = new InstructionListModel();
                    row.Name = Edit.Name;
                    row.Description = Edit.Description;
                    row.CurrentVersion = 1;
                    context.InstructionListDbSet.Add(row);
                    context.SaveChanges();

                    InstructionListVersionModel instructionListVersionModel = new();
                    instructionListVersionModel.InstructionListId = row.Id;
                    instructionListVersionModel.Version = 1;
                    instructionListVersionModel.DateTime = DateTime.Now;
                    context.InstructionListVersionDbSet.Add(instructionListVersionModel);

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
            if (Selected == null) return;

            using var context = new ServerDbContext(DatabasePath);

            var row = context.InstructionListDbSet.FirstOrDefault(e => e.Name == Selected.Name);

            if (row == null) return;

            // Controleren of de lijst nog in gebruik is
            int checkUsage(int id)
            {
                return context.WorkstationDbSet.Count(e => e.InstructionListIdBefore == id || e.InstructionListPeriodicIdBefore == id || e.InstructionListIdAfter == id)
                     + context.ProductDbSet.Count(e => e.InstructionListIdBefore == id || e.InstructionListPeriodicIdBefore == id || e.InstructionListIdAfter == id)
                     + context.ProductGroupDbSet.Count(e => e.InstructionListIdBefore == id || e.InstructionListPeriodicIdBefore == id || e.InstructionListIdAfter == id);
            }

            int usageCount = checkUsage(row.Id);

            if (usageCount != 0)
            {
                CustomMessageBox.Show("Deze lijst is nog ergens in gebruik!", "Warning", MessageBoxImage.Warning);
                return;
            }

            // Zoek alle versies van deze lijst
            var versions = context.InstructionListVersionDbSet.Where(v => v.InstructionListId == row.Id).ToList();

            // Zoek alle instructies die aan deze versies hangen en verwijder ze
            var versionIds = versions.Select(v => v.Id).ToList();
            var instructionsToDelete = context.InstructionDbSet.Where(i => versionIds.Contains(i.InstructionListVersionId)).ToList();

            context.InstructionDbSet.RemoveRange(instructionsToDelete); // Verwijder instructies
            context.InstructionListVersionDbSet.RemoveRange(versions);  // Verwijder versies
            context.InstructionListDbSet.Remove(row);                   // Verwijder de lijst

            context.SaveChanges();
            Edit.Name = "";
            Edit.Description = "";
            GetList();
        }


        public void MakeCurrent()
        {
            using (var context = new ServerDbContext(DatabasePath))
            {
                InstructionListModel row = context.InstructionListDbSet.FirstOrDefault(i => i.Id == SelectedVersion.InstructionListId);

                if (row != null)
                {
                    row.CurrentVersion = SelectedVersion.Version;
                    context.InstructionListDbSet.Update(row);
                    context.SaveChanges();
                    SelectedVersion = context.InstructionListVersionDbSet.FirstOrDefault(i => i.Id == SelectedVersion.Id);
                }
            }
        }

        public void NewVersion()
        {
            using (var context = new ServerDbContext(DatabasePath))
            {
                var highestVersion = context.InstructionListVersionDbSet
                    .Where(v => v.InstructionListId == SelectedVersion.InstructionListId)
                    .OrderByDescending(v => v.Version)
                    .FirstOrDefault();

                int newVersionNumber = (highestVersion != null) ? highestVersion.Version + 1 : 1;

                var newVersion = new InstructionListVersionModel
                {
                    InstructionListId = SelectedVersion.InstructionListId,
                    Version = newVersionNumber,
                    DateTime = DateTime.Now,
                    Approved = false
                };

                context.InstructionListVersionDbSet.Add(newVersion);
                context.SaveChanges();


                if (highestVersion != null)
                {
                    var oldInstructions = context.InstructionDbSet
                        .Where(i => i.InstructionListVersionId == SelectedVersion.Id)
                        .ToList();

                    foreach (var oldInstruction in oldInstructions)
                    {
                        var newInstruction = new InstructionModel
                        {
                            InstructionListVersionId = newVersion.Id,
                            Sequence = oldInstruction.Sequence,
                            InstructionType = oldInstruction.InstructionType,
                            InstructionTypeName = oldInstruction.InstructionTypeName,
                            InputText1 = oldInstruction.InputText1,
                            InputText2 = oldInstruction.InputText2,
                            InputText3 = oldInstruction.InputText3,
                            InputText4 = oldInstruction.InputText4,
                            InputText5 = oldInstruction.InputText5,
                            InputText6 = oldInstruction.InputText6,
                            InputText7 = oldInstruction.InputText7,
                            InputText8 = oldInstruction.InputText8,
                            InputText9 = oldInstruction.InputText9,
                            InputText10 = oldInstruction.InputText10,
                            InputBool1 = oldInstruction.InputBool1,
                            InputBool2 = oldInstruction.InputBool2,
                            InputBool3 = oldInstruction.InputBool3,
                            InputBool4 = oldInstruction.InputBool4,
                            InputBool5 = oldInstruction.InputBool5,
                            InputBool6 = oldInstruction.InputBool6,
                            InputBool7 = oldInstruction.InputBool7,
                            InputBool8 = oldInstruction.InputBool8,
                            InputBool9 = oldInstruction.InputBool9,
                            InputBool10 = oldInstruction.InputBool10,
                            InputImage1 = oldInstruction.InputImage1,
                            Text1 = oldInstruction.Text1,
                            Text2 = oldInstruction.Text2,
                            Text3 = oldInstruction.Text3,
                            Text4 = oldInstruction.Text4,
                            Text5 = oldInstruction.Text5,
                            Text6 = oldInstruction.Text6,
                            Text7 = oldInstruction.Text7,
                            Text8 = oldInstruction.Text8,
                            Text9 = oldInstruction.Text9,
                            Text10 = oldInstruction.Text10,
                            Image1 = oldInstruction.Image1,
                            Image2 = oldInstruction.Image2,
                            Image3 = oldInstruction.Image3,
                            Image4 = oldInstruction.Image4,
                            Image5 = oldInstruction.Image5,
                            Image6 = oldInstruction.Image6,
                            Image7 = oldInstruction.Image7,
                            Image8 = oldInstruction.Image8,
                            Image9 = oldInstruction.Image9,
                            Image10 = oldInstruction.Image10,
                            HotspotId = oldInstruction.HotspotId
                        };

                        context.InstructionDbSet.Add(newInstruction);
                    }
                }
                context.SaveChanges();
                SelectedVersion = newVersion;
            }
        }

        public void UpdateRow()
        {
            using (var context = new ServerDbContext(DatabasePath))
            {
                if (string.IsNullOrEmpty(Edit.Name)) { return; }

                InstructionListModel row = context.InstructionListDbSet.FirstOrDefault(e => e.Id == Edit.Id);

                if (row != null)
                {
                    row.Name = Edit.Name;
                    row.Description = Edit.Description;
                    context.InstructionListDbSet.Update(row);
                    context.SaveChanges();
                    Edit.Name = "";
                    Edit.Description = "";
                    GetList();
                }
            }
        }

        public void UpdateSelected()
        {
            if (Selected is InstructionListModel)
            {
                Edit.Id = Selected.Id;
                Edit.Name = Selected.Name;
                Edit.Description = Selected.Description;
                Edit.CurrentVersion = Selected.CurrentVersion;

                using (var context = new ServerDbContext(DatabasePath))
                {

                    SelectedVersion = context.InstructionListVersionDbSet.FirstOrDefault(i => i.InstructionListId == Selected.Id && i.Version == Selected.CurrentVersion);
                }
            }
            else
            {
                SelectedVersion = null;
            }
        }

        public void FilterList(string filter)
        {
            using (var context = new ServerDbContext(DatabasePath))
            {
                var result = from InstructionsList in context.InstructionListDbSet
                             where EF.Functions.Like(InstructionsList.Name, $"%{filter}%")
                             || EF.Functions.Like(InstructionsList.Description, $"%{filter}%")
                             orderby InstructionsList.Id
                             select InstructionsList;

                List = result.ToList();
            }
        }

        public void CopyList(int id, int currentVersion)
        {
            using var context = new ServerDbContext(DatabasePath);

            // Haal de huidige versie op
            int instructionListVersionListId = context.InstructionListVersionDbSet
                .FirstOrDefault(e => e.InstructionListId == id && e.Version == currentVersion)?.Id ?? 0;

            if (instructionListVersionListId == 0)
                return;

            // Haal de gekoppelde instructies op
            var usedInstructions = context.InstructionDbSet
                .Where(i => i.InstructionListVersionId == instructionListVersionListId)
                .OrderBy(i => i.Id)
                .ToList();

            // Bepaal een unieke naam voor de gekopieerde lijst (direct starten met (1))
            string baseName = Edit.Name + " - copy";
            int copyNumber = 1;
            string newName = $"{baseName}({copyNumber})";

            while (context.InstructionListDbSet.Any(l => l.Name == newName))
            {
                copyNumber++;
                newName = $"{baseName}({copyNumber})";
            }

            // Maak een nieuwe instructielijst aan
            var newList = new InstructionListModel
            {
                Name = newName,
                Description = Edit.Description,
                CurrentVersion = 1
            };
            context.InstructionListDbSet.Add(newList);
            context.SaveChanges();  // Zorg ervoor dat newList.Id beschikbaar is

            // Maak een nieuwe versie aan
            var newVersion = new InstructionListVersionModel
            {
                InstructionListId = newList.Id,
                Version = 1,
                DateTime = DateTime.Now
            };
            context.InstructionListVersionDbSet.Add(newVersion);
            context.SaveChanges();  // Zorg ervoor dat newVersion.Id beschikbaar is

            // Kopieer alle instructies
            var newInstructions = usedInstructions.Select(instruction => new InstructionModel
            {
                InstructionListVersionId = newVersion.Id,
                Sequence = instruction.Sequence,
                InstructionType = instruction.InstructionType,
                InstructionTypeName = instruction.InstructionTypeName,
                InputText1 = instruction.InputText1,
                InputText2 = instruction.InputText2,
                InputText3 = instruction.InputText3,
                InputText4 = instruction.InputText4,
                InputText5 = instruction.InputText5,
                InputText6 = instruction.InputText6,
                InputText7 = instruction.InputText7,
                InputText8 = instruction.InputText8,
                InputText9 = instruction.InputText9,
                InputText10 = instruction.InputText10,
                InputBool1 = instruction.InputBool1,
                InputBool2 = instruction.InputBool2,
                InputBool3 = instruction.InputBool3,
                InputBool4 = instruction.InputBool4,
                InputBool5 = instruction.InputBool5,
                InputBool6 = instruction.InputBool6,
                InputBool7 = instruction.InputBool7,
                InputBool8 = instruction.InputBool8,
                InputBool9 = instruction.InputBool9,
                InputBool10 = instruction.InputBool10,
                InputImage1 = instruction.InputImage1,
                Text1 = instruction.Text1,
                Text2 = instruction.Text2,
                Text3 = instruction.Text3,
                Text4 = instruction.Text4,
                Text5 = instruction.Text5,
                Text6 = instruction.Text6,
                Text7 = instruction.Text7,
                Text8 = instruction.Text8,
                Text9 = instruction.Text9,
                Text10 = instruction.Text10,
                Image1 = instruction.Image1,
                Image2 = instruction.Image2,
                Image3 = instruction.Image3,
                Image4 = instruction.Image4,
                Image5 = instruction.Image5,
                Image6 = instruction.Image6,
                Image7 = instruction.Image7,
                Image8 = instruction.Image8,
                Image9 = instruction.Image9,
                Image10 = instruction.Image10,
                HotspotId = instruction.HotspotId
            }).ToList();

            foreach (InstructionModel instruction in newInstructions)
            {
                context.InstructionDbSet.Add(instruction);
                context.SaveChanges();
                instruction.HotspotId = instruction.Id.ToString("D6");
                context.SaveChanges();
            }
            GetList();
        }

        #endregion

        #region Properties

        [ObservableProperty]
        private string _databasePath;

        [ObservableProperty]
        private List<InstructionListModel> _list = new List<InstructionListModel>();

        [ObservableProperty]
        private List<InstructionListVersionModel> _listVersions = new List<InstructionListVersionModel>();

        [ObservableProperty]
        private InstructionListModel _edit = new InstructionListModel();

        [ObservableProperty]
        private InstructionListModel _selected = new InstructionListModel();

        [ObservableProperty]
        private InstructionListVersionModel _selectedVersion = new InstructionListVersionModel();

        [ObservableProperty]
        private PropertyReferenceModel _objectToWriteSelected;

        #endregion
    }

    public partial class InstructionTypes : ObservableObject
    {

        #region Commands
        #endregion

        #region Constructor

        public InstructionTypes(string databasePath)
        {
            DatabasePath = databasePath;
            AddTypesToDatabase();
        }

        #endregion

        #region Events
        #endregion

        #region Fields
        #endregion

        #region Methods

        public void AddTypesToDatabase()
        {
            using (var context = new ServerDbContext(DatabasePath))
            {

                List<InstructionTypesModel> lTypes = InstructionTypesList.GetTypes();

                foreach (var type in lTypes)
                {
                    bool typeExists = context.InstructionTypesDbSet
                .Any(existingType => existingType.Id == type.Id);
                    if (!typeExists)
                    {
                        var newType = new InstructionTypesModel
                        {
                            InstructionType = type.InstructionType,
                            Icon = type.Icon,
                            Name = type.Name
                        };

                        context.InstructionTypesDbSet.Add(newType);
                        context.SaveChanges();
                    }
                }
            }
        }
        public void GetList()
        {
            using (var context = new ServerDbContext(DatabasePath))
            {
                var result = from InstructionTypes in context.InstructionTypesDbSet
                             orderby InstructionTypes.Id
                             select InstructionTypes;

                List = result.ToList();
            }
        }
        public void FilterList(string filter)
        {
            using (var context = new ServerDbContext(DatabasePath))
            {
                var result = from InstructionTypes in context.InstructionTypesDbSet
                             where EF.Functions.Like(InstructionTypes.Name, $"%{filter}%")
                             orderby InstructionTypes.Id
                             select InstructionTypes;

                List = result.ToList();
            }
        }

        #endregion

        #region Properties

        [ObservableProperty]
        private string _databasePath;

        [ObservableProperty]
        private List<InstructionTypesModel> _list = new List<InstructionTypesModel>();

        [ObservableProperty]
        private InstructionTypesModel _edit = new InstructionTypesModel();

        [ObservableProperty]
        private InstructionTypesModel _selected = new InstructionTypesModel();

        #endregion


    }


}
