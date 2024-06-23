using PresupuestoC.MVVM;
using PresupuestoC.Navigation.Currency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresupuestoC.Navigation.Client
{
    public class ClientNavigationService<TViewModel> where TViewModel : ViewModelBase
    {
        private readonly ClientNavigationStore _clientNavigationStore;
        private readonly Func<TViewModel> _createViewModel;

        public ClientNavigationService(ClientNavigationStore navigationStore, Func<TViewModel> createViewModel)
        {
            _clientNavigationStore = navigationStore;
            _createViewModel = createViewModel;
        }

        public void Navigate()
        {
            _clientNavigationStore.CurrentViewModel = _createViewModel();
        }
    }
}
