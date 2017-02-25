using Rename.Sections.Home.ViewModels;
using Rename.Utility;
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

namespace Rename.Sections.Home.Pages
{

    public partial class HomePage : Page
    {

        private readonly HomeViewModel _HomeViewModel;

        public HomePage()
        {

            InitializeComponent();

            IInteractionManager interactionManager = MainWindow.StaticInstance.InteractionManager;
            INavigationManager navigationManager = MainWindow.StaticInstance.NavigationManager;
            _HomeViewModel = new HomeViewModel(interactionManager, navigationManager);

            this.DataContext = _HomeViewModel;

        }

    }

}
