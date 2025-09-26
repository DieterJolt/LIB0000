using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LIB0000
{
    public partial class InstructionListsEditView
    {

        #region Commands

        [RelayCommand]
        private void cmdOk()
        {
            BasicService.InstructionsService.InstructionLists.AddRow();
            INavigationService.GoBack();
        }

        [RelayCommand]
        private void cmdCancel()
        {
            BasicService.InstructionsService.InstructionLists.Edit.Name = "";
            BasicService.InstructionsService.InstructionLists.Edit.Description = "";
            INavigationService.GoBack();
        }

        #endregion

        #region Constructor

        public InstructionListsEditView(BasicService basicService, INavigationService iNavigationService)
        {
            BasicService = basicService;
            INavigationService = iNavigationService;
            DataContext = this;
            InitializeComponent();

        }



        #endregion

        #region Events

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BasicService.InstructionsService.Instructions.UpdateSelected();
        }

        #endregion

        #region Fields

        public BasicService BasicService { get; set; }

        public INavigationService INavigationService { get; set; }


        #endregion

        #region Methods
        #endregion

        #region Properties
        #endregion

    }
}
