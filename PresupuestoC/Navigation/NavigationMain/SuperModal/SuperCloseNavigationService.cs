using PresupuestoC.Navigation.Modal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresupuestoC.Navigation.SuperModal
{
 
    public class SuperCloseNavigationService
    {
        private readonly SuperModalNavigationStore _navigationStore;

        public SuperCloseNavigationService(SuperModalNavigationStore navigationStore)
        {
            _navigationStore = navigationStore;
        }

        public void Navigate()
        {
            _navigationStore.Close();
        }
    }
}
