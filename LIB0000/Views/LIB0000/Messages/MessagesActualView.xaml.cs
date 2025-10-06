//*********************************************************
//
// Copyright (c) Microsoft. All rights reserved.
// THIS CODE IS PROVIDED *AS IS* WITHOUT WARRANTY OF
// ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING ANY
// IMPLIED WARRANTIES OF FITNESS FOR A PARTICULAR
// PURPOSE, MERCHANTABILITY, OR NON-INFRINGEMENT.
//
//*********************************************************
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace LIB0000
{
    public partial class MessagesActualView
    {
        #region Commands

        [RelayCommand]
        private void cmdNavigate()
        {
            NavigationService.Navigate(typeof(MessageStepView));
        }

        [RelayCommand]
        public void ToggleErrorsVisible()
        {
            BasicService.MessagesService.FilterErrorsVisible = !BasicService.MessagesService.FilterErrorsVisible;
            BasicService.MessagesService.UpdateList();
            BasicService.MessagesService.UpdateListPossible();
        }

        [RelayCommand]
        private void ToggleWarningsVisible()
        {
            BasicService.MessagesService.FilterWarningsVisible = !BasicService.MessagesService.FilterWarningsVisible;
            BasicService.MessagesService.UpdateList();
            BasicService.MessagesService.UpdateListPossible();
        }

        [RelayCommand]
        private void ToggleInfosVisible()
        {
            BasicService.MessagesService.FilterInfosVisible = !BasicService.MessagesService.FilterInfosVisible;
            BasicService.MessagesService.UpdateList();
            BasicService.MessagesService.UpdateListPossible();
        }

        #endregion
        #region Constructor
        public MessagesActualView(BasicService basicService, INavigationService navigationService)
        {
            BasicService = basicService;
            NavigationService = navigationService;
            DataContext = this;
            this.InitializeComponent();
        }
        #endregion
        #region Events
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
        #region Fields
        public BasicService BasicService { get; }

        public INavigationService NavigationService { get; set; }

        #endregion

        #region Methods
        #endregion

        #region Properties
        #endregion

    }
}
