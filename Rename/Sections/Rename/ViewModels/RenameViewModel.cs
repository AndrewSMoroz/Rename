using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Rename.Utility;
using System.Collections.ObjectModel;
using Rename.ExtensionMethods;

namespace Rename.Sections.Rename.ViewModels
{

    public class RenameViewModel : ViewModelBase
    {

#region Members

        private enum RenameMode
        {
            Show,
            Hide
        }

        private readonly IInteractionManager _InteractionManager;
        private readonly INavigationManager _NavigationManager;
        private readonly IUserSettingsManager _UserSettingsManager;

        private UserSettings _UserSettings = new UserSettings();
        private List<ExtensionMapItem> _ExtensionMap = new List<ExtensionMapItem>();
        private List<DirectoryMapItem> _DirectoryMap = new List<DirectoryMapItem>();
        private List<Message> _WorkingMessages = new List<Message>();

        // For properties
        private string _CurrentDirectory;
        private bool _ProcessSubdirectories = true;
        private bool _RenameDirectories = true;
        private bool _RenameFiles = true;
        private List<Message> _Messages;

#endregion Members

#region Constructors

        //------------------------------------------------------------------------------------------------------------------------
        public RenameViewModel(IInteractionManager interactionManager, INavigationManager navigationManager, IUserSettingsManager userSettingsManager)
        {

            _InteractionManager = interactionManager;
            _NavigationManager = navigationManager;
            _UserSettingsManager = userSettingsManager;

            _UserSettings = _UserSettingsManager.GetUserSettings();
            _ExtensionMap = _UserSettings.ExtensionMap;
            _DirectoryMap = _UserSettings.DirectoryMap;

        }

#endregion Constructors

#region Properties

        //------------------------------------------------------------------------------------------------------------------------
        public string CurrentDirectory
        {
            get { return _CurrentDirectory; }
            set { base.SetProperty(ref _CurrentDirectory, value, () => this.CurrentDirectory); }
        }

        //------------------------------------------------------------------------------------------------------------------------
        public bool ProcessSubdirectories
        {
            get { return _ProcessSubdirectories; }
            set { base.SetProperty(ref _ProcessSubdirectories, value, () => this.ProcessSubdirectories); }
        }

        //------------------------------------------------------------------------------------------------------------------------
        public bool RenameDirectories
        {
            get { return _RenameDirectories; }
            set { base.SetProperty(ref _RenameDirectories, value, () => this.RenameDirectories); }
        }

        //------------------------------------------------------------------------------------------------------------------------
        public bool RenameFiles
        {
            get { return _RenameFiles; }
            set { base.SetProperty(ref _RenameFiles, value, () => this.RenameFiles); }
        }

        //------------------------------------------------------------------------------------------------------------------------
        public List<Message> Messages
        {
            get { return _Messages; }
            set { base.SetProperty(ref _Messages, value, () => this.Messages); }
        }

#endregion Properties

#region Commands

    #region Select Directory

        //------------------------------------------------------------------------------------------------------------------------
        public ICommand SelectDirectoryCommand
        {
            get { return new RelayCommandEx(this.SelectDirectoryCommandExecute); }
        }

        //------------------------------------------------------------------------------------------------------------------------
        private void SelectDirectoryCommandExecute()
        {
            _InteractionManager.SetIsBusy("Please select directory...");

            string selectedDirectory = _InteractionManager.SelectDirectoryDialog(this.CurrentDirectory);
            if (!string.IsNullOrEmpty(selectedDirectory))
            {
                this.CurrentDirectory = selectedDirectory;
                this.Messages = new List<Message>();
            }
            _InteractionManager.ClearIsBusy();
        }

    #endregion Select Directory

    #region Show

        //------------------------------------------------------------------------------------------------------------------------
        public ICommand ShowCommand
        {
            get { return new RelayCommandEx(this.ShowCommandExecute, this.CanShowCommandExecute); }
        }

        //------------------------------------------------------------------------------------------------------------------------
        private bool CanShowCommandExecute()
        {
            if (string.IsNullOrEmpty(this.CurrentDirectory)) { return false; }
            if (this.RenameDirectories == false && this.RenameFiles == false) { return false; }
            return true;
        }

        //------------------------------------------------------------------------------------------------------------------------
        private async void ShowCommandExecute()
        {
            try
            {
                _InteractionManager.SetIsBusy("Showing directories...");
                this.Messages = new List<Message>();
                _WorkingMessages = new List<Message>();
                _WorkingMessages.AddMessage(Message.MessageCategory.Information, GetRenameMessage("Showing"));
                // Call this a maximum of twice: once for file renames (if necessary), and then a second time for directory renames (if necessary)
                // The reason is that if we're renaming directories in a tree, on a slow drive, the renamed directories get discovered
                // and processed a second time.  For renaming files, there will be no effect the second time, but there will be a bunch
                // of unnecessary calls to DirectoryInfo that will return empty lists the second time
                if (this.RenameFiles)
                {
                    await Task.Run(() => ProcessDirectoryAsync(_CurrentDirectory, RenameMode.Show, false, true, this.ProcessSubdirectories, 0));
                }
                if (this.RenameDirectories)
                {
                    await Task.Run(() => ProcessDirectoryAsync(_CurrentDirectory, RenameMode.Show, true, false, this.ProcessSubdirectories, 0));
                }
            }
            catch (Exception ex)
            {
                _InteractionManager.DisplayError(ex);
                _WorkingMessages.AddMessage(Message.MessageCategory.Error, ex.Message);
            }
            finally
            {
                _InteractionManager.ClearIsBusy();
                _WorkingMessages.AddMessage(Message.MessageCategory.Information, "Finished.");
                this.Messages = _WorkingMessages;
            }
        }

    #endregion Show

    #region Hide

        //------------------------------------------------------------------------------------------------------------------------
        public ICommand HideCommand
        {
            get { return new RelayCommandEx(this.HideCommandExecute, this.CanHideCommandExecute); }
        }

        //------------------------------------------------------------------------------------------------------------------------
        private bool CanHideCommandExecute()
        {
            if (string.IsNullOrEmpty(this.CurrentDirectory)) { return false; }
            if (this.RenameDirectories == false && this.RenameFiles == false) { return false; }
            return true;
        }

        //------------------------------------------------------------------------------------------------------------------------
        private async void HideCommandExecute()
        {
            try
            {
                _InteractionManager.SetIsBusy("Hiding directories...");
                this.Messages = new List<Message>();
                _WorkingMessages = new List<Message>();
                _WorkingMessages.AddMessage(Message.MessageCategory.Information, (GetRenameMessage("Hiding")));
                // Call this a maximum of twice: once for file renames (if necessary), and then a second time for directory renames (if necessary)
                // The reason is that if we're renaming directories in a tree, on a slow drive, the renamed directories get discovered
                // and processed a second time.  For renaming files, the will be no effect the second time, but there will be a bunch
                // of unnecessary calls to DirectoryInfo that will return empty lists the second time
                if (this.RenameFiles)
                {
                    await Task.Run(() => ProcessDirectoryAsync(_CurrentDirectory, RenameMode.Hide, false, true, this.ProcessSubdirectories, 0));
                }
                if (this.RenameDirectories)
                {
                    await Task.Run(() => ProcessDirectoryAsync(_CurrentDirectory, RenameMode.Hide, true, false, this.ProcessSubdirectories, 0));
                }
            }
            catch (Exception ex)
            {
                _InteractionManager.DisplayError(ex);
                _WorkingMessages.AddMessage(Message.MessageCategory.Error,ex.Message);
            }
            finally
            {
                _InteractionManager.ClearIsBusy();
                _WorkingMessages.AddMessage(Message.MessageCategory.Information, "Finished.");
                this.Messages = _WorkingMessages;
            }
        }

    #endregion Hide

#endregion Commands

#region Methods

        //------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Process specified directory, and subdirectories if specified.  May rename files and the directory itself.
        /// </summary>
        /// <param name="directory">Directory to process</param>
        /// <param name="mode">Show or Hide</param>
        /// <param name="renameDirectories">Will rename directory if true</param>
        /// <param name="renameFiles">Will rename files within directory if true</param>
        /// <param name="processSubdirectories">Process contained subdirectories recursively if true</param>
        /// <param name="level">Call initially with zero to indicate root level</param>
        private void ProcessDirectoryAsync(string directory, RenameMode mode, bool renameDirectories, bool renameFiles, bool processSubdirectories, int level)
        {

            if (processSubdirectories)
            {
                IEnumerable<string> subdirectories = Directory.EnumerateDirectories(directory);
                level++;
                foreach (string subdirectory in subdirectories)
                {
                    ProcessDirectoryAsync(subdirectory, mode, renameDirectories, renameFiles, processSubdirectories, level);
                }
                level--;
            }

            if (renameFiles)
            {
                RenameFilesInSingleDirectory(directory, mode);
            }

            if (renameDirectories)
            {
                RenameSingleDirectory(directory, mode, level);
            }

        }

        //------------------------------------------------------------------------------------------------------------------------
        private void RenameFilesInSingleDirectory(string directory, RenameMode mode)
        {

            string statusMessageBase = "Renaming files in " + directory;
            _InteractionManager.SetIsBusy(statusMessageBase);

            int countFilesInDirectory = 0;
            StringBuilder newFileName = new StringBuilder();

            // Rename the extensions of any files in the specified directory that exist in the ExtensionMap
            foreach (ExtensionMapItem emi in _ExtensionMap)
            {
                try
                {

                    string oldExtension = (mode == RenameMode.Show ? emi.HiddenExtension : emi.Extension);
                    string newExtension = (mode == RenameMode.Show ? emi.Extension : emi.HiddenExtension);
                    DirectoryInfo di = new DirectoryInfo(directory);
                    FileInfo[] files = di.GetFiles("*." + oldExtension);
                    int countTotalFilesCurrentExtension = files.Length;
                    int countProcessedFilesCurrentExtension = 0;
                    string statusMessageCurrentExtension = statusMessageBase + " : " + oldExtension + " to " + newExtension;
                    _InteractionManager.SetIsBusy(statusMessageCurrentExtension, 0, countTotalFilesCurrentExtension);

                    foreach (FileInfo file in files)
                    {
                        try
                        {

                            // Majority of files will already have hidden filenames, so most of the time, only the extension needs to change
                            newFileName.Clear();
                            newFileName.Append(file.Name.ToLower().Replace("." + oldExtension.ToLower(), "." + newExtension.ToLower()));

                            if (mode == RenameMode.Hide && !file.Name.ToLower().StartsWith(newExtension.ToLower() + "."))
                            {
                                // Assumption is that a proper hidden filename starts with the hidden extension + "."
                                // If it doesn't, we'll rename the filename as well as the extension
                                newFileName.Clear();
                                newFileName.Append(newExtension.ToLower() + "." + Guid.NewGuid().ToString() + "." + newExtension);
                            }

                            file.MoveTo(Path.Combine(directory, newFileName.ToString()));
                            countFilesInDirectory++;
                            countProcessedFilesCurrentExtension++;

                            if (countProcessedFilesCurrentExtension % 100 == 0)
                            {
                                _InteractionManager.SetIsBusy(statusMessageCurrentExtension, countTotalFilesCurrentExtension, countProcessedFilesCurrentExtension);
                            }

                        }
                        catch (Exception exRenamingFile)
                        {
                            _WorkingMessages.AddMessage(Message.MessageCategory.Error, "Error while renaming file " + file.FullName + ": " + exRenamingFile.Message);
                        }
                    }
                }
                catch (Exception exGettingFiles)
                {
                    _WorkingMessages.AddMessage(Message.MessageCategory.Error, "Error while getting files in " + directory + ": " + exGettingFiles.Message);
                }
            }

            _WorkingMessages.AddMessage(Message.MessageCategory.Information, "Files renamed in " + directory + ": " + countFilesInDirectory.ToString());

        }

        //------------------------------------------------------------------------------------------------------------------------
        private void RenameSingleDirectory(string directory, RenameMode mode, int level)
        {

            DirectoryMapItem dmi = null;
            string FROM = "";
            string TO = "";

            try
            {

                dmi = _DirectoryMap.Where(item => directory.EndsWith(@"\" + (mode == RenameMode.Show ? item.HiddenName : item.Name))).SingleOrDefault();

                if (dmi != null)
                {
                    FROM = (mode == RenameMode.Show ? dmi.HiddenName : dmi.Name);
                    TO = (mode == RenameMode.Show ? dmi.Name : dmi.HiddenName);
                    _InteractionManager.SetIsBusy("Renaming directory " + FROM + " to " + TO);
                    Directory.Move(directory, directory.Replace(@"\" + FROM, @"\" + TO));
                    if (level == 0) { this.CurrentDirectory = directory.Replace(@"\" + FROM, @"\" + TO); }  // Old value in CurrentDirectory doesn't exist anymore, so change it to the new value
                    _WorkingMessages.AddMessage(Message.MessageCategory.Information, "Renamed " + directory + " to " + TO);
                }
                else
                {
                    DirectoryMapItem dmiNotRenaming = _DirectoryMap.Where(item => directory.EndsWith(@"\" + (mode == RenameMode.Show ? item.Name : item.HiddenName))).SingleOrDefault();
                    if (dmiNotRenaming == null) { _WorkingMessages.AddMessage(Message.MessageCategory.Warning, "Unknown directory: " + directory); }
                }

            }
            catch (Exception ex)
            {
                string message = "Error occurred while renaming directory " +
                    (dmi == null ? "?" : FROM) + " to " + (dmi == null ? "?" : TO) +
                    " : " + ex.Message;
                _InteractionManager.SetIsBusy(message);
                _WorkingMessages.AddMessage(Message.MessageCategory.Error, message);
            }

        }

        //------------------------------------------------------------------------------------------------------------------------
        private string GetRenameMessage(string prefix)
        {
            string message = "";
            if (this.RenameDirectories && this.RenameFiles) { message = prefix + " directories and files"; }
            else if (this.RenameDirectories) { message = prefix + " directories only"; }
            else { message = prefix + " files only"; }
            return message;
        }

#endregion Methods

#region Commented Methods

        ////------------------------------------------------------------------------------------------------------------------------
        //private async void LoadData()
        //{
        //    try
        //    {
        //        _InteractionManager.SetIsBusy();
        //        //tbResults.Text = null;
        //        //await Task.Run(() => ProcessFileAsync(_SourceFile, _OutputFile));
        //    }
        //    catch (Exception ex)
        //    {
        //        _InteractionManager.DisplayError(ex);
        //    }
        //    finally
        //    {
        //        _InteractionManager.ClearIsBusy();
        //    }
        //}

        ////------------------------------------------------------------------------------------------------------------------------
        // Shows waiting for two methods, each returning a different type
        //private async void LoadData()
        //{

        //    _InteractionManager.SetIsBusy();

        //    Task[] tasks = new Task[2];
        //    tasks[0] = Task<List<ExtensionMapItem>>.Run(() => this.GetExtensionMap());
        //    tasks[1] = Task<List<DirectoryMapItem>>.Run(() => this.GetDirectoryMap());

        //    try
        //    {
        //        await Task.WhenAll(tasks);
        //        _ExtensionMap = await (Task<List<ExtensionMapItem>>)tasks[0];
        //        _DirectoryMap = await (Task<List<DirectoryMapItem>>)tasks[1];
        //    }
        //    catch (Exception ex)
        //    {
        //        _InteractionManager.DisplayError(ex);       // Figure out the best way to deal with the potential multiple Exceptions
        //    }
        //    finally
        //    {
        //        _InteractionManager.ClearIsBusy();
        //    }

        //}


        //------------------------------------------------------------------------------------------------------
        // THIS USED TO BE IN ProcessDirectoryAsync - was replaced with RenameSingleDirectory
        //if (mode == RenameMode.Show)
        //{
        //    DirectoryMapItem dmi = _DirectoryMap.Where(item => directory.EndsWith(@"\" + item.HiddenName)).SingleOrDefault();
        //    try
        //    {
        //        if (dmi != null)
        //        {
        //            _InteractionManager.SetIsBusy("Renaming directory " + dmi.HiddenName + " to " + dmi.Name);
        //            Directory.Move(directory, directory.Replace(@"\" + dmi.HiddenName, @"\" + dmi.Name));
        //            if (level == 0) { this.CurrentDirectory = directory.Replace(@"\" + dmi.HiddenName, @"\" + dmi.Name); }  // Old value in CurrentDirectory doesn't exist anymore, so change it to the new value
        //            _WorkingMessages.Add("Renamed " + directory + " to " + dmi.Name);
        //        }
        //        else
        //        {
        //            DirectoryMapItem dmiNotRenaming = _DirectoryMap.Where(item => directory.EndsWith(@"\" + item.Name)).SingleOrDefault();
        //            _WorkingMessages.Add((dmiNotRenaming == null ? "Unknown directory: " : "Already shown: ") + directory);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        string message = "Error occurred while renaming directory " +
        //            (dmi == null ? "?" : dmi.HiddenName) + " to " + (dmi == null ? "?" : dmi.Name) +
        //            " : " + ex.Message;
        //        _InteractionManager.SetIsBusy(message);
        //        _WorkingMessages.Add(message);
        //    }
        //}
        //else if (mode == RenameMode.Hide)
        //{
        //    DirectoryMapItem dmi = _DirectoryMap.Where(item => directory.EndsWith(@"\" + item.Name)).SingleOrDefault();
        //    try
        //    {
        //        if (dmi != null)
        //        {
        //            _InteractionManager.SetIsBusy("Renaming directory " + dmi.Name + " to " + dmi.HiddenName);
        //            Directory.Move(directory, directory.Replace(@"\" + dmi.Name, @"\" + dmi.HiddenName));
        //            if (level == 0) { this.CurrentDirectory = directory.Replace(@"\" + dmi.Name, @"\" + dmi.HiddenName); }  // Old value in CurrentDirectory doesn't exist anymore, so change it to the new value
        //            _WorkingMessages.Add("Renamed " + directory + " to " + dmi.HiddenName);
        //        }
        //        else
        //        {
        //            DirectoryMapItem dmiNotRenaming = _DirectoryMap.Where(item => directory.EndsWith(@"\" + item.HiddenName)).SingleOrDefault();
        //            _WorkingMessages.Add((dmiNotRenaming == null ? "Unknown directory: " : "Already hidden: ") + directory);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        string message = "Error occurred while renaming directory " +
        //            (dmi == null ? "?" : dmi.Name) + " to " + (dmi == null ? "?" : dmi.HiddenName) +
        //            " : " + ex.Message;
        //        _InteractionManager.SetIsBusy(message);
        //        _WorkingMessages.Add(message);
        //    }
        //}
        //else
        //{
        //    // Do nothing
        //}

#endregion Commented Methods

    }

}
