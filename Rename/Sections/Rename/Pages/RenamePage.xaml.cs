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
using Rename.Sections.Rename.ViewModels;
using Rename.Utility;

namespace Rename.Sections.Rename.Pages
{

    public partial class RenamePage : Page
    {

        private readonly RenameViewModel _RenameViewModel;

        public RenamePage()
        {
            
            InitializeComponent();
            
            IInteractionManager interactionManager = MainWindow.StaticInstance.InteractionManager;
            INavigationManager navigationManager = MainWindow.StaticInstance.NavigationManager;
            IUserSettingsManager userSettingsManager = MainWindow.StaticInstance.UserSettingsManager;
            _RenameViewModel = new RenameViewModel(interactionManager, navigationManager, userSettingsManager);

            this.DataContext = _RenameViewModel;

        }

    }

}
