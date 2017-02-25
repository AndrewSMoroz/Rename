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

namespace Rename
{

    public partial class MainWindow : Window
    {

        private readonly MainWindowViewModel _MainWindowViewModel;
        private readonly IInteractionManager _InteractionManager;
        private readonly INavigationManager _NavigationManager;
        private readonly IUserSettingsManager _UserSettingsManager;

        #region Constructors

        //--------------------------------------------------------------------------------------------------------------
        public MainWindow()
        {

            InitializeComponent();

            _InteractionManager = new InteractionManager(this);
            _NavigationManager = new NavigationManager(this.MainFrame);
            _UserSettingsManager = new UserSettingsManager();

            _MainWindowViewModel = new MainWindowViewModel(_InteractionManager, _NavigationManager);
            this.DataContext = _MainWindowViewModel;

        }

        #endregion

        #region Properties

        //------------------------------------------------------------------------------------------------------------------------
        public static MainWindow StaticInstance
        {
            get
            {
                return (MainWindow)Application.Current.Windows[0];
            }
        }

        //------------------------------------------------------------------------------------------------------------------------
        public IInteractionManager InteractionManager
        {
            get
            {
                return _InteractionManager;
            }
        }

        //------------------------------------------------------------------------------------------------------------------------
        public INavigationManager NavigationManager
        {
            get
            {
                return _NavigationManager;
            }
        }

        //------------------------------------------------------------------------------------------------------------------------
        public IUserSettingsManager UserSettingsManager
        {
            get
            {
                return _UserSettingsManager;
            }
        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            if (MainFrame.Content != null && MainFrame.Content.GetType() == typeof(Rename.Sections.View.Pages.ViewPage))
            {
                try
                {
                    Rename.Sections.View.Pages.ViewPage page = (Rename.Sections.View.Pages.ViewPage)MainFrame.Content;
                    Rename.Sections.View.ViewModels.ViewViewModel vm = (Rename.Sections.View.ViewModels.ViewViewModel)page.DataContext;
                    vm.LoadRandomImageCommand.Execute(null);
                }
                catch { }
            }
        }

        #endregion

        #region Methods



        #endregion

        ////--------------------------------------------------------------------------------------------------------------
        //private void btnBrowse_Click(object sender, RoutedEventArgs e)
        //{

        //    var dialog = new System.Windows.Forms.FolderBrowserDialog();
        //    System.Windows.Forms.DialogResult result = dialog.ShowDialog();
        //    txtDirectory.Text = dialog.SelectedPath;

        //    btnShow.IsEnabled = !string.IsNullOrEmpty(txtDirectory.Text);
        //    btnHide.IsEnabled = !string.IsNullOrEmpty(txtDirectory.Text);

        //}

        ////--------------------------------------------------------------------------------------------------------------
        //private void btnShow_Click(object sender, RoutedEventArgs e)
        //{

        //    //MessageBox.Show(txtDirectory.Text);

        //    try
        //    {

        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message, "Error");
        //    }

        //    //foreach (ExtensionMapItem item in ExtensionMap) 
        //    //{
        //    //    MessageBox.Show(item.HiddenExtension + " to " + item.Extension);
        //    //}

        //    //foreach (DirectoryMapItem item in DirectoryMap)
        //    //{
        //    //    MessageBox.Show(item.HiddenName + " to " + item.Name);
        //    //}

        //}

        ////--------------------------------------------------------------------------------------------------------------
        //private void btnHide_Click(object sender, RoutedEventArgs e)
        //{

        //    //foreach (ExtensionMapItem item in ExtensionMap)
        //    //{
        //    //    MessageBox.Show(item.Extension + " to " + item.HiddenExtension);
        //    //}

        //    //foreach (DirectoryMapItem item in DirectoryMap)
        //    //{
        //    //    MessageBox.Show(item.Name + " to " + item.HiddenName);
        //    //}

        //}

    }

}
