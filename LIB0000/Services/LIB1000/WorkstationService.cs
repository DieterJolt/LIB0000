using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Resources;
using Microsoft.EntityFrameworkCore;

namespace LIB0000
{
    public partial class WorkstationService : ObservableObject
    {

        #region Commands
        #endregion

        #region Constructor

        public WorkstationService(string databasePath)
        {
            DatabasePath = databasePath;
            Workstations = new Workstations(DatabasePath);
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
        private Workstations _workstations;

        [ObservableProperty]
        private string _databasePath;

        #endregion


    }

    public partial class Workstations : ObservableObject
    {


        #region Commands
        #endregion

        #region Constructor

        public Workstations(string databasePath)
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
                var result = from Workstations in context.WorkstationDbSet
                             orderby Workstations.Id
                             select Workstations;

                List = new ObservableCollection<WorkstationModel>(result.ToList());
            }
        }

        public WorkstationModel GetWorkstation(string workstationName)
        {
            return List.FirstOrDefault(e => e.Name == workstationName);
        }

        public void AddRow()
        {
            using (var context = new ServerDbContext(DatabasePath))
            {

                WorkstationModel row = context.WorkstationDbSet.FirstOrDefault(e => e.Id == Edit.Id);

                if ((row == null) && (!string.IsNullOrEmpty(Edit.Name)))
                {
                    row = new WorkstationModel();
                    row.Name = Edit.Name;
                    row.Description = Edit.Description;
                    row.InstructionListOn = Edit.InstructionListOn;
                    row.InstructionListIdBefore = Edit.InstructionListIdBefore;
                    row.InstructionListPeriodicIdBefore = Edit.InstructionListPeriodicIdBefore;
                    row.InstructionListIdAfter = Edit.InstructionListIdAfter;
                    row.InstructionListPeriodicFrequency = Edit.InstructionListPeriodicFrequency;
                    ImageService = new ImageService();
                    row.Image = Edit.Image;
                    context.WorkstationDbSet.Add(row);
                    context.SaveChanges();
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
                WorkstationModel row = context.WorkstationDbSet.FirstOrDefault(e => e.Name == Selected.Name);

                if (row != null)
                {

                    context.WorkstationDbSet.Remove(row);
                    Edit.Name = "";
                    Edit.Description = "";

                }
                context.SaveChanges();
            }
            GetList();
        }

        public void UpdateRow()
        {
            using (var context = new ServerDbContext(DatabasePath))
            {
                if (string.IsNullOrEmpty(Edit.Name)) { return; }

                WorkstationModel row = context.WorkstationDbSet.FirstOrDefault(e => e.Id == Edit.Id);

                if (row != null)
                {
                    row.Name = Edit.Name;
                    row.Description = Edit.Description;
                    row.InstructionListOn = Edit.InstructionListOn;
                    row.InstructionListIdBefore = Edit.InstructionListIdBefore;
                    row.InstructionListPeriodicIdBefore = Edit.InstructionListPeriodicIdBefore;
                    row.InstructionListIdAfter = Edit.InstructionListIdAfter;
                    row.InstructionListPeriodicFrequency = Edit.InstructionListPeriodicFrequency;
                    context.WorkstationDbSet.Update(row);
                    context.SaveChanges();
                    Edit.Name = "";
                    Edit.Description = "";
                    GetList();
                }
            }
        }

        public void UpdateSelected()
        {
            if (Selected is WorkstationModel)
            {
                Edit.Name = Selected.Name;
                Edit.Description = Selected.Description;
                Edit.Id = Selected.Id;
                Edit.Image = Selected.Image;
                Edit.InstructionListOn = Selected.InstructionListOn;
                Edit.InstructionListIdBefore = Selected.InstructionListIdBefore;
                Edit.InstructionListPeriodicIdBefore = Selected.InstructionListPeriodicIdBefore;
                Edit.InstructionListIdAfter = Selected.InstructionListIdAfter;
                Edit.InstructionListPeriodicFrequency = Selected.InstructionListPeriodicFrequency;
            }
        }

        public void FilterList(string filter)
        {
            using (var context = new ServerDbContext(DatabasePath))
            {
                var result = from Workstations in context.WorkstationDbSet
                             where EF.Functions.Like(Workstations.Name, $"%{filter}%")
                             || EF.Functions.Like(Workstations.Description, $"%{filter}%")
                             orderby Workstations.Id
                             select Workstations;

                List = new ObservableCollection<WorkstationModel>(result.ToList());
            }
        }

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
                        WorkstationModel row = context.WorkstationDbSet.FirstOrDefault((WorkstationModel e) => e.Id == Selected.Id);
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

        public void GetJoinList(int workstionId)
        {
            //using (var context = new ServerDbContext(DatabasePath))
            //{
            //    JoinList = context.WorkstationDbSet  // Assuming this is the correct table
            //        .Where(workstation => workstation.Id == workstionId)
            //        .Join(
            //            context.InstructionListDbSet,
            //            workstation => workstation.Id, // Field in WorkstationModel
            //            instructionList => instructionList.Id, // Field in InstructionListModel
            //            (workstation, instructionList) => new WorkstationJoinModel
            //            {
            //                WorkstationName = workstation.Name,
            //                WorkstationDescription = workstation.Description,
            //                WorkstationImage = workstation.Image,  // Assuming workstation has an image field
            //                WorkstationInstructionListOn = workstation.InstructionListOn,
            //                WorkstationInstructionListIdBefore = workstation.InstructionListIdBefore,
            //                WorkstationInstructionListPeriodicIdBefore = workstation.InstructionListPeriodicIdBefore,
            //                WorkstationInstructionListIdAfter = workstation.InstructionListIdAfter,
            //                WorkstationInstructionListPeriodicFrequency = workstation.InstructionListPeriodicFrequency,
            //                InstructionListName = instructionList.Name  // Joining the InstructionList's Name field
            //            })
            //        .OrderBy(workstation => workstation.WorkstationName)  // Assuming you want to order by Name
            //        .AsNoTracking()  // Improves performance by preventing EF from tracking changes
            //        .ToList();
            //}
        }

        #endregion

        #region Properties

        [ObservableProperty]
        private string _databasePath;

        [ObservableProperty]
        private ObservableCollection<WorkstationModel> _list = new ObservableCollection<WorkstationModel>();

        [ObservableProperty]
        private ObservableCollection<WorkstationJoinModel> _joinList = new ObservableCollection<WorkstationJoinModel>();

        [ObservableProperty]
        private WorkstationModel _edit = new WorkstationModel();

        [ObservableProperty]
        private WorkstationModel _selected = new WorkstationModel();

        #endregion

    }

}