using Rename.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Rename.Sections.Home.ViewModels
{

    public class HomeViewModel : ViewModelBase
    {

#region Members

        private readonly IInteractionManager _InteractionManager;
        private readonly INavigationManager _NavigationManager;

#endregion Members

#region Constructors

        //------------------------------------------------------------------------------------------------------------------------
        public HomeViewModel(IInteractionManager interactionManager, INavigationManager navigationManager)
        {
            _InteractionManager = interactionManager;
            _NavigationManager = navigationManager;
        }

#endregion Constructors

#region Properties

#endregion Properties

#region Commands

#endregion Commands

#region Methods

#endregion Methods

    }

}
