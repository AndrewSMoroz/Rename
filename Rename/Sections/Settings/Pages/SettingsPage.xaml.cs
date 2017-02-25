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
using Rename.Sections.Settings.ViewModels;
using Rename.Utility;

namespace Rename.Sections.Settings.Pages
{

    public partial class SettingsPage : Page
    {

        private readonly SettingsViewModel _SettingsViewModel;

        public SettingsPage()
        {

            InitializeComponent();

            IInteractionManager interactionManager = MainWindow.StaticInstance.InteractionManager;
            INavigationManager navigationManager = MainWindow.StaticInstance.NavigationManager;
            IUserSettingsManager userSettingsManager = MainWindow.StaticInstance.UserSettingsManager;
            _SettingsViewModel = new SettingsViewModel(interactionManager, navigationManager, userSettingsManager);

            this.DataContext = _SettingsViewModel;

        }

    }

}
