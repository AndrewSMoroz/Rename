using System;
using System.Windows.Controls;
using Rename.Sections.Home.Pages;
using Rename.Sections.Rename.Pages;
using Rename.Sections.View.Pages;
using Rename.Sections.Settings.Pages;

namespace Rename.Utility
{

    /// <summary>
    /// Implementation of INavigationManager for navigation to various pages
    /// </summary>
    public class NavigationManager: INavigationManager
    {

        private readonly Frame _navFrame;

        //------------------------------------------------------------------------------------------------------------------------
        public NavigationManager(Frame navFrame)
        {
            _navFrame = navFrame;
        }

        //------------------------------------------------------------------------------------------------------------------------
        public void ClearContent()
        {
            _navFrame.NavigationService.Content = null;
        }

        //------------------------------------------------------------------------------------------------------------------------
        public void GoToHomePage()
        {
            _navFrame.NavigationService.Navigate(new HomePage());
        }

        //------------------------------------------------------------------------------------------------------------------------
        public void GoToRenamePage()
        {
            _navFrame.NavigationService.Navigate(new RenamePage());
        }

        //------------------------------------------------------------------------------------------------------------------------
        public void GoToViewPage()
        {
            _navFrame.NavigationService.Navigate(new ViewPage());
        }

        //------------------------------------------------------------------------------------------------------------------------
        public void GoToSettingsPage()
        {
            _navFrame.NavigationService.Navigate(new SettingsPage());
        }

        //------------------------------------------------------------------------------------------------------------------------
        public void RemoveHistory()
        {
            try
            {
                if (!_navFrame.CanGoBack && !_navFrame.CanGoForward)
                {
                    return;
                }
                var entry = _navFrame.RemoveBackEntry();
                while (entry != null)
                {
                    entry = _navFrame.RemoveBackEntry();
                }
            }
            catch (Exception ex)
            {
            }
        }

    }

}
