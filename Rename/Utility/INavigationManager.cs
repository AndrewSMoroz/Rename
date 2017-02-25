using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rename.Utility
{
    /// <summary>
    /// Interface to the navigation manager used by view models to navigate to other pages in the application
    /// </summary>
    public interface INavigationManager
    {

        void ClearContent();
        void GoToHomePage();
        void GoToRenamePage();
        void GoToViewPage();
        void GoToSettingsPage();
        void RemoveHistory();

    }
}
