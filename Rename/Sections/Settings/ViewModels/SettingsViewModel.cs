using Rename.Utility;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;

namespace Rename.Sections.Settings.ViewModels
{

    public class SettingsViewModel : ViewModelBase
    {

#region Members

        private readonly IInteractionManager _InteractionManager;
        private readonly INavigationManager _NavigationManager;
        private readonly IUserSettingsManager _UserSettingsManager;

        private UserSettings _OriginalUserSettings;
        private ObservableCollection<ExtensionMapItem> _ExtensionMap;
        private ObservableCollection<DirectoryMapItem> _DirectoryMap;
        private bool _InAddExtensionMode = false;
        private bool _InAddDirectoryMode = false;
        private string _NewDirectory;
        private string _NewHiddenDirectory;
        private string _NewExtension;
        private string _NewHiddenExtension;

#endregion Members

#region Constructors

        //------------------------------------------------------------------------------------------------------------------------
        public SettingsViewModel(IInteractionManager interactionManager, INavigationManager navigationManager, IUserSettingsManager userSettingsManager)
        {
            
            // Set local references to injected managers
            _InteractionManager = interactionManager;
            _NavigationManager = navigationManager;
            _UserSettingsManager = userSettingsManager;

            // Get user settings and set related properties
            _OriginalUserSettings = _UserSettingsManager.GetUserSettings();
            this.ExtensionMap = new ObservableCollection<ExtensionMapItem>(_OriginalUserSettings.ExtensionMap);
            this.DirectoryMap = new ObservableCollection<DirectoryMapItem>(_OriginalUserSettings.DirectoryMap);

        }

#endregion Constructors

#region Properties

        //------------------------------------------------------------------------------------------------------------------------
        public ObservableCollection<ExtensionMapItem> ExtensionMap
        {
            get { return _ExtensionMap; }
            set { base.SetProperty(ref _ExtensionMap, value, () => this.ExtensionMap); }
        }

        //------------------------------------------------------------------------------------------------------------------------
        public ObservableCollection<DirectoryMapItem> DirectoryMap
        {
            get { return _DirectoryMap; }
            set { base.SetProperty(ref _DirectoryMap, value, () => this.DirectoryMap); }
        }

        //------------------------------------------------------------------------------------------------------------------------
        public bool InAddExtensionMode
        {
            get { return _InAddExtensionMode; }
            set { base.SetProperty(ref _InAddExtensionMode, value, () => this.InAddExtensionMode); }
        }

        //------------------------------------------------------------------------------------------------------------------------
        public bool InAddDirectoryMode
        {
            get { return _InAddDirectoryMode; }
            set { base.SetProperty(ref _InAddDirectoryMode, value, () => this.InAddDirectoryMode); }
        }

        //------------------------------------------------------------------------------------------------------------------------
        public string NewExtension
        {
            get { return _NewExtension; }
            set { base.SetProperty(ref _NewExtension, value, () => this.NewExtension); }
        }

        //------------------------------------------------------------------------------------------------------------------------
        public string NewHiddenExtension
        {
            get { return _NewHiddenExtension; }
            set { base.SetProperty(ref _NewHiddenExtension, value, () => this.NewHiddenExtension); }
        }

        //------------------------------------------------------------------------------------------------------------------------
        public string NewDirectory
        {
            get { return _NewDirectory; }
            set { base.SetProperty(ref _NewDirectory, value, () => this.NewDirectory); }
        }

        //------------------------------------------------------------------------------------------------------------------------
        public string NewHiddenDirectory
        {
            get { return _NewHiddenDirectory; }
            set { base.SetProperty(ref _NewHiddenDirectory, value, () => this.NewHiddenDirectory); }
        }

        #endregion Properties

#region Commands

    #region Enter Add Extension Mode

        //------------------------------------------------------------------------------------------------------------------------
        public ICommand EnterAddExtensionModeCommand
        {
            get { return new RelayCommandEx(this.EnterAddExtensionModeCommandExecute); }
        }

        //------------------------------------------------------------------------------------------------------------------------
        private void EnterAddExtensionModeCommandExecute()
        {
            this.InAddExtensionMode = true;
        }

    #endregion Enter Add Extension Mode

    #region Exit Add Extension Mode

        //------------------------------------------------------------------------------------------------------------------------
        public ICommand ExitAddExtensionModeCommand
        {
            get { return new RelayCommandEx(this.ExitAddExtensionModeCommandExecute); }
        }

        //------------------------------------------------------------------------------------------------------------------------
        private void ExitAddExtensionModeCommandExecute()
        {
            this.InAddExtensionMode = false;
            this.NewExtension = null;
            this.NewHiddenExtension = null;
        }

    #endregion Exit Add Extension Mode

    #region Add New Extension

        //------------------------------------------------------------------------------------------------------------------------
        public ICommand AddNewExtensionCommand
        {
            get { return new RelayCommandEx(this.AddNewExtensionCommandExecute, this.CanAddNewExtensionCommandExecute); }
        }

        //------------------------------------------------------------------------------------------------------------------------
        private bool CanAddNewExtensionCommandExecute()
        {
            if (string.IsNullOrEmpty(this.NewExtension)) { return false; }
            if (string.IsNullOrEmpty(this.NewHiddenExtension)) { return false; }
            return true;
        }

        //------------------------------------------------------------------------------------------------------------------------
        private void AddNewExtensionCommandExecute()
        {

            try
            {
                ExtensionMapItem newItem = new ExtensionMapItem(){ Extension = this.NewExtension, HiddenExtension = this.NewHiddenExtension };
                this.ExtensionMap.Add(newItem);
                this.InAddExtensionMode = false;
                this.NewExtension = null;
                this.NewHiddenExtension = null;
            }
            catch (Exception ex)
            {
                _InteractionManager.DisplayError(ex);
            }

        }

    #endregion Add New Extension

    #region Enter Add Directory Mode

        //------------------------------------------------------------------------------------------------------------------------
        public ICommand EnterAddDirectoryModeCommand
        {
            get { return new RelayCommandEx(this.EnterAddDirectoryModeCommandExecute); }
        }

        //------------------------------------------------------------------------------------------------------------------------
        private void EnterAddDirectoryModeCommandExecute()
        {
            this.InAddDirectoryMode = true;
        }

    #endregion Enter Add Directory Mode

    #region Exit Add Directory Mode

        //------------------------------------------------------------------------------------------------------------------------
        public ICommand ExitAddDirectoryModeCommand
        {
            get { return new RelayCommandEx(this.ExitAddDirectoryModeCommandExecute); }
        }

        //------------------------------------------------------------------------------------------------------------------------
        private void ExitAddDirectoryModeCommandExecute()
        {
            this.InAddDirectoryMode = false;
            this.NewDirectory = null;
            this.NewHiddenDirectory = null;
        }

    #endregion Exit Add Directory Mode

    #region Add New Directory

        //------------------------------------------------------------------------------------------------------------------------
        public ICommand AddNewDirectoryCommand
        {
            get { return new RelayCommandEx(this.AddNewDirectoryCommandExecute, this.CanAddNewDirectoryCommandExecute); }
        }

        //------------------------------------------------------------------------------------------------------------------------
        private bool CanAddNewDirectoryCommandExecute()
        {
            if (string.IsNullOrEmpty(this.NewDirectory)) { return false; }
            if (string.IsNullOrEmpty(this.NewHiddenDirectory)) { return false; }
            return true;
        }

        //------------------------------------------------------------------------------------------------------------------------
        private void AddNewDirectoryCommandExecute()
        {

            try
            {
                DirectoryMapItem newItem = new DirectoryMapItem() { Name = this.NewDirectory, HiddenName = this.NewHiddenDirectory };
                this.DirectoryMap.Add(newItem);
                this.InAddDirectoryMode = false;
                this.NewDirectory = null;
                this.NewHiddenDirectory = null;
            }
            catch (Exception ex)
            {
                _InteractionManager.DisplayError(ex);
            }

        }

    #endregion Add New Directory

    #region Delete Extension

        //------------------------------------------------------------------------------------------------------------------------
        public ICommand DeleteExtensionCommand
        {
            get { return new RelayCommandEx<ExtensionMapItem>(this.DeleteExtensionExecute); }
        }

        //------------------------------------------------------------------------------------------------------------------------
        private void DeleteExtensionExecute(ExtensionMapItem item)
        {

            try
            {
                if (_InteractionManager.DisplayYesNoDialog("Are you sure you want to delete this item:" + Environment.NewLine + Environment.NewLine + item.Extension + Environment.NewLine + item.HiddenExtension, "Confirm Delete"))
                {
                    this.ExtensionMap.Remove(item);
                }
            }
            catch (Exception ex)
            {
                _InteractionManager.DisplayError(ex);
            }

        }

    #endregion Delete Extension

    #region Delete Directory

        //------------------------------------------------------------------------------------------------------------------------
        public ICommand DeleteDirectoryCommand
        {
            get { return new RelayCommandEx<DirectoryMapItem>(this.DeleteDirectoryExecute); }
        }

        //------------------------------------------------------------------------------------------------------------------------
        private void DeleteDirectoryExecute(DirectoryMapItem item)
        {

            try
            {
                if (_InteractionManager.DisplayYesNoDialog("Are you sure you want to delete this item:" + Environment.NewLine + Environment.NewLine + item.Name + Environment.NewLine + item.HiddenName, "Confirm Delete"))
                {
                    this.DirectoryMap.Remove(item);
                }
            }
            catch (Exception ex)
            {
                _InteractionManager.DisplayError(ex);
            }

        }

    #endregion Delete Directory

    #region Save

        //------------------------------------------------------------------------------------------------------------------------
        public ICommand SaveCommand
        {
            get { return new RelayCommandEx(this.SaveCommandExecute); }
        }

        //------------------------------------------------------------------------------------------------------------------------
        private void SaveCommandExecute()
        {
            
            try
            {

                //------------------ VALIDATIONS

                // Abort and alert user if either collection is empty
                if ((this.ExtensionMap == null || !this.ExtensionMap.Any()) ||
                    (this.DirectoryMap == null || !this.DirectoryMap.Any()))
                {
                    throw new Exception("At least one of the settings collections has no values.  There must be at least one value in each collection.");
                }

                // Abort and alert user if there are any blank values in either collection
                if (this.ExtensionMap.Where(item => string.IsNullOrEmpty(item.Extension) || string.IsNullOrEmpty(item.HiddenExtension)).Any() ||
                    this.DirectoryMap.Where(item => string.IsNullOrEmpty(item.Name) || string.IsNullOrEmpty(item.HiddenName)).Any())
                {
                    throw new Exception("There is at least one empty value in one of the settings collections.  " + 
                                        "Please provide a value for all grid cells highlighted in yellow.");
                }

                // Abort and alert user if there are any duplicated values in the collection of extensions
                List<string> duplicatedExtensions = this.ExtensionMap.GroupBy(item => item.Extension.ToLower())
                                                                     .Where(g => g.Count() > 1)
                                                                     .Select(g => g.Key)
                                                                     .ToList();
                if (duplicatedExtensions.Any())
                {
                    throw new Exception("Can't save because the following Extensions are duplictated: " + 
                                        Environment.NewLine + Environment.NewLine + 
                                        string.Join(", ", duplicatedExtensions));
                }

                // Abort and alert user if there are any duplicated values in the collection of hidden extensions
                List<string> duplicatedHiddenExtensions = this.ExtensionMap.GroupBy(item => item.HiddenExtension.ToLower())
                                                                           .Where(g => g.Count() > 1)
                                                                           .Select(g => g.Key)
                                                                           .ToList();
                if (duplicatedHiddenExtensions.Any())
                {
                    throw new Exception("Can't save because the following Hidden Extensions are duplictated: " +
                                        Environment.NewLine + Environment.NewLine +
                                        string.Join(", ", duplicatedHiddenExtensions));
                }

                // Abort and alert user if there are any duplicated values in the collection of directory names
                List<string> duplicatedDirectories = this.DirectoryMap.GroupBy(item => item.Name.ToLower())
                                                                      .Where(g => g.Count() > 1)
                                                                      .Select(g => g.Key)
                                                                      .ToList();
                if (duplicatedDirectories.Any())
                {
                    throw new Exception("Can't save because the following Directories are duplictated: " +
                                        Environment.NewLine + Environment.NewLine +
                                        string.Join(", ", duplicatedDirectories));
                }

                // Abort and alert user if there are any duplicated values in the collection of directory hidden names
                List<string> duplicatedHiddenDirectories = this.DirectoryMap.GroupBy(item => item.HiddenName.ToLower())
                                                                            .Where(g => g.Count() > 1)
                                                                            .Select(g => g.Key)
                                                                            .ToList();
                if (duplicatedHiddenDirectories.Any())
                {
                    throw new Exception("Can't save because the following Hidden Directories are duplictated: " +
                                        Environment.NewLine + Environment.NewLine +
                                        string.Join(", ", duplicatedHiddenDirectories));
                }

                //------------------ SAVE SETTINGS TO FILE

                UserSettings newUserSettings = new UserSettings();
                newUserSettings.ExtensionMap = this.ExtensionMap.ToList<ExtensionMapItem>();
                newUserSettings.DirectoryMap = this.DirectoryMap.ToList<DirectoryMapItem>();
                _UserSettingsManager.SaveUserSettings(newUserSettings);
                _InteractionManager.DisplayInformationalMessage("Settings saved successfully.");

            }
            catch (Exception ex)
            {
                _InteractionManager.DisplayError(ex);
            }

        }

    #endregion Save

#endregion Commands

#region Methods

#endregion Methods

    }

}
