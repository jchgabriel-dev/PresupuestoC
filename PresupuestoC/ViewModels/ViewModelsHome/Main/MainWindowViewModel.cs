using PresupuestoC.Command.Archive;
using PresupuestoC.Models;
using PresupuestoC.MVVM;
using PresupuestoC.Navigation.Main;
using PresupuestoC.Navigation.Modal;
using PresupuestoC.Navigation.SuperModal;
using PresupuestoC.Services;
using PresupuestoC.Stores.Archive;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;


namespace PresupuestoC.ViewModels.Main
{
    public class MainWindowViewModel : ViewModelBase
    {
        private readonly NavigationStore _navigationStore;

        private readonly ModalNavigationStore _modalNavigationStore;

        private readonly SuperModalNavigationStore _superModalNavigationStore;
    

        public ViewModelBase CurrentViewModel => _navigationStore.CurrentViewModel;
        public ViewModelBase CurrentModalViewModel => _modalNavigationStore.CurrentViewModel;
        public ViewModelBase CurrentSuperModalViewModel => _superModalNavigationStore.CurrentViewModel;

        public bool IsOpen => _modalNavigationStore.IsOpen;
        public bool IsSuperOpen => _superModalNavigationStore.IsOpen;


        public ICommand LoadCommand { get; set; }

        public MainWindowViewModel(NavigationStore navigationStore, 
            ModalNavigationStore modalNavigationStore,
            SuperModalNavigationStore superNavigationStore)
        {
            _navigationStore = navigationStore;
            _modalNavigationStore = modalNavigationStore;
            _superModalNavigationStore = superNavigationStore;

            _navigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;
            _modalNavigationStore.CurrentViewModelChanged += OnCurrentModalViewModelChanged;
            _superModalNavigationStore.CurrentViewModelChanged += OnCurrentSuperModalViewModelChanged;
        }   

        private void OnCurrentViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentViewModel));
        }

        private void OnCurrentModalViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentModalViewModel));
            OnPropertyChanged(nameof(IsOpen));
        }

        private void OnCurrentSuperModalViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentSuperModalViewModel));
            OnPropertyChanged(nameof(IsSuperOpen));
        }
    }
}
