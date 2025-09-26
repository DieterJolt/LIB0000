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
    public partial class InstructionListSelectView
    {

        #region Commands

        [RelayCommand]
        private void cmdOk()
        {
            BasicService.InstructionsService.InstructionLists.ObjectToWriteSelected.SetValue(BasicService.InstructionsService.InstructionLists.Selected.Id);
            NavigationService.GoBack();
        }

        [RelayCommand]
        private void cmdCancel()
        {
            NavigationService.GoBack();
        }

        #endregion

        #region Constructor

        public InstructionListSelectView(BasicService basicService, INavigationService navigationService, ImageService imageService)
        {
            BasicService = basicService;
            NavigationService = navigationService;
            ImageService = imageService;
            DataContext = this;
            InitializeComponent();
        }

        #endregion

        #region Events

        private void AutoSuggestBox_TextChanged(Wpf.Ui.Controls.AutoSuggestBox sender, Wpf.Ui.Controls.AutoSuggestBoxTextChangedEventArgs args)
        {
            BasicService.InstructionsService.InstructionLists.FilterList(sender.Text);
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BasicService.InstructionsService.InstructionLists.UpdateSelected();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            BasicService.InstructionsService.InstructionLists.GetList();
        }
        private void Page_Unloaded(object sender, RoutedEventArgs e)
        {
            AutoSuggestBox.Text = "";
        }

        #endregion

        #region Fields

        public BasicService BasicService { get; set; }

        public INavigationService NavigationService { get; set; }

        public ImageService ImageService { get; set; }


        #endregion

        #region Methods
        #endregion

        #region Properties

        private string selectedTextBox;


        #endregion


    }
}
