using Rename.Sections.View.ViewModels;
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

namespace Rename.Sections.View.Pages
{

    public partial class ViewPage : Page
    {

        private readonly ViewViewModel _ViewViewModel;

        public ViewPage()
        {

            InitializeComponent();

            IInteractionManager interactionManager = MainWindow.StaticInstance.InteractionManager;
            INavigationManager navigationManager = MainWindow.StaticInstance.NavigationManager;

            _ViewViewModel = new ViewViewModel(interactionManager, navigationManager);
            this.DataContext = _ViewViewModel;

            interactionManager.SetIsBusy("Please select directory...");
            string selectedDirectory = interactionManager.SelectDirectoryDialog(null);
            _ViewViewModel.BuildImageList(selectedDirectory);
            interactionManager.ClearIsBusy();

        }

    }

}
