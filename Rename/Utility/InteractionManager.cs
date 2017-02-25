using System;
using System.Windows;
using System.Windows.Forms;
//using Microsoft.Win32;

namespace Rename.Utility
{
    /// <summary>
    /// Actual implementation of the IInteractionManager displaying messages to the users for alerts, confirmations, etc...
    /// </summary>
    public class InteractionManager : IInteractionManager
	{

        public event Action<bool, string, int, int> IsBusyChanged;

        private Window _Owner;

        //------------------------------------------------------------------------------------------------------------------------
        public InteractionManager(Window owner)
		{
			_Owner = owner;
		}

        //------------------------------------------------------------------------------------------------------------------------
        public Window Owner
		{
			get { return _Owner; }
			set { _Owner = value; }
		}

        //------------------------------------------------------------------------------------------------------------------------
        public void DisplayInformationalMessage(string message, string caption = null, Window owner = null)
        {
            if (owner == null) { owner = MainWindow.StaticInstance; }
            if (caption == null) { caption = "Info";  }
            System.Windows.MessageBox.Show(owner, message, caption, MessageBoxButton.OK, MessageBoxImage.Information);
        }

        //------------------------------------------------------------------------------------------------------------------------
        public void DisplayError(Exception ex, string errorMessage = null, Window owner = null)
		{
			if (ex != null)
			{
    			DisplayAlert(ex.Message, "ERROR", owner);
			}
		}

        //------------------------------------------------------------------------------------------------------------------------
        public void DisplayAlert(string message, string caption = "ERROR", Window owner = null)
		{
            if (owner == null) { owner = MainWindow.StaticInstance; }
            if (caption == null) { caption = "ERROR"; }
            System.Windows.MessageBox.Show(owner, message, caption, MessageBoxButton.OK, MessageBoxImage.Error);
		}

        //------------------------------------------------------------------------------------------------------------------------
        public bool DisplayYesNoDialog(string message, string caption, Window owner = null)
		{

            if (owner == null) { owner = MainWindow.StaticInstance; }
            MessageBoxResult result = System.Windows.MessageBox.Show(owner, message, caption, MessageBoxButton.YesNo, MessageBoxImage.Question);
			return result.Equals(MessageBoxResult.Yes);
		}

        //------------------------------------------------------------------------------------------------------------------------
        public string OpenFileDialog()
        {
            string rval = string.Empty;

            Microsoft.Win32.OpenFileDialog OpenFileDialog = new Microsoft.Win32.OpenFileDialog();
            bool? OpenFileResult = OpenFileDialog.ShowDialog();
            if (OpenFileResult.HasValue && OpenFileResult.Value)
            {
                rval = OpenFileDialog.FileName;
            }
            return rval; 
        }

        //------------------------------------------------------------------------------------------------------------------------
        public string SelectDirectoryDialog(string initialDirectory)
        {
            string rval = string.Empty;
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.SelectedPath = initialDirectory;
            fbd.ShowNewFolderButton = false;
            fbd.Description = System.Environment.NewLine +  "NOTE: Press right arrow key to jump to selected directory";
            //DialogResult result = fbd.ShowDialog();
            DialogResult result = FolderBrowserLauncher.ShowFolderBrowser(fbd);     // Got class FolderBrowserLauncher on stack overflow
            rval = fbd.SelectedPath;
            return rval;
        }

        //------------------------------------------------------------------------------------------------------------------------
        public void SetIsBusy(string text)
        {
            //if (this.IsBusyChanged != null) { this.IsBusyChanged(true); }
            this.IsBusyChanged?.Invoke(true, text, 0, 0);
        }

        //------------------------------------------------------------------------------------------------------------------------
        public void SetIsBusy(string text, int progressBarMaximum, int progressBarValue)
        {
            //if (this.IsBusyChanged != null) { this.IsBusyChanged(true); }
            this.IsBusyChanged?.Invoke(true, text, progressBarMaximum, progressBarValue);
        }

        //------------------------------------------------------------------------------------------------------------------------
        public void ClearIsBusy()
        {
            //if (this.IsBusyChanged != null) { this.IsBusyChanged(false); }
            this.IsBusyChanged?.Invoke(false, null, 0, 0);
        }

	}
}
