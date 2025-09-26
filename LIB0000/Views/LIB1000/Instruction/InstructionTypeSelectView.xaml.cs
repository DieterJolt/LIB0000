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
    public partial class InstructionTypeSelectView
    {

        #region Commands

        [RelayCommand]
        private void cmdSelectInstructionOK()
        {
            try
            {
                BasicService.InstructionsService.Instructions.AddRow(BasicService.InstructionsService.InstructionLists.SelectedVersion.Id, BasicService.InstructionsService.InstructionTypes.Selected);
                BasicService.InstructionsService.Instructions.SelectedJoin = BasicService.InstructionsService.Instructions.JoinList.Last();
                NavigationService.GoBack();
            }
            catch { }
        }

        [RelayCommand]
        private void cmdBackToInstructionEdit()
        {
            //BasicService.InstructionsService.Instructions.Edit.InstructionTypeName = "";
            NavigationService.GoBack();
        }



        #endregion

        #region Constructor

        public InstructionTypeSelectView(BasicService basicService, INavigationService navigationService)
        {
            BasicService = basicService;
            NavigationService = navigationService;
            DataContext = this;
            InitializeComponent();

        }

        #endregion

        #region Events

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            BasicService.InstructionsService.InstructionTypes.GetList();
            BasicService.InstructionsService.InstructionTypes.Selected = new InstructionTypesModel();
            BasicService.InstructionsService.InstructionTypes.Selected.Id = BasicService.InstructionsService.Instructions.Edit.Id;
            BasicService.InstructionsService.InstructionTypes.Selected.Name = BasicService.InstructionsService.Instructions.Edit.InstructionTypeName;
        }

        private void AutoSuggestBox_TextChanged(Wpf.Ui.Controls.AutoSuggestBox sender, Wpf.Ui.Controls.AutoSuggestBoxTextChangedEventArgs args)
        {
            BasicService.InstructionsService.InstructionTypes.FilterList(sender.Text);
        }

        private void Page_Unloaded(object sender, RoutedEventArgs e)
        {
            AutoSuggestBox.Text = "";
        }

        #endregion

        #region Fields

        public BasicService BasicService { get; set; }

        public INavigationService NavigationService { get; set; }



        #endregion

        #region Methods
        #endregion

        #region Properties
        #endregion


    }
}
