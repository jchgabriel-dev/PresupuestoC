using PresupuestoC.Models.Archive;
using PresupuestoC.Models.Main;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresupuestoC.Stores.Project
{
    public class ProjectSelectedStore
    {
        private ProjectModel _currentProject;

        public ProjectModel CurrentProject
        {
            get => _currentProject;
            set
            {
                _currentProject = value;
                CurrentProjectChanged?.Invoke();
            }
        }

        public bool IsSelected => CurrentProject != null;

        public event Action CurrentProjectChanged;

        public void Deselected()
        {
            CurrentProject = null;
        }
    }
}
