using System;
using System.Windows;

namespace Rename.Utility
{

    /// <summary>
	/// Interface to the dialog service used by view models to relay messages (errors, exceptions, questions, etc...) to the users.
	/// </summary>
	public interface IInteractionManager
	{

        /// <summary>
        /// Action that will be raised when the busy indicator should be changed
        /// </summary>
        event Action<bool, string, int, int> IsBusyChanged;         // bool: whether to show the busy indicator
                                                                    // string: message to display
                                                                    // int: maximum value for progress bar
                                                                    // int: current value for progress bar

        Window Owner { get; set; }

        /// <summary>
        /// Displays an informational message
        /// </summary>
        /// <param name="message">Informational message</param>
        void DisplayInformationalMessage(string message, string caption = null, Window owner = null);

        /// <summary>
        /// Displays an error message caused by an exception.
        /// </summary>
        /// <param name="ex">Exception</param>
        /// <param name="error">Error message</param>
        void DisplayError(Exception ex, string errorMessage = null, Window owner = null);

		/// <summary>
		/// Displays a warning message.
		/// </summary>
		/// <param name="message">Warning message</param>
        void DisplayAlert(string message, string caption = null, Window owner = null);

		/// <summary>
		/// Displays a confirmation dialog with Yes/No buttons for use confirmation.
		/// </summary>
		/// <returns>True if the user clicked 'Yes', False otherwise</returns>
        bool DisplayYesNoDialog(string message, string caption, Window owner = null);

        /// <summary>
        /// Displays an open file dialog
        /// </summary>
        /// <returns>Returns selected file name</returns>
        string OpenFileDialog();

        /// <summary>
        /// Displays a directory selection dialog
        /// </summary>
        /// <returns>Returns selected directory name</returns>
        string SelectDirectoryDialog(string initialDirectory);

        /// <summary>
        /// Request the IsBusy indicator be displayed, with the progress bar hidden
        /// </summary>
        void SetIsBusy(string text);

        /// <summary>
        /// Request the IsBusy indicator be displayed, with the progress bar visible
        /// </summary>
        void SetIsBusy(string text, int progressBarMaximum, int progressBarValue);

        /// <summary>
        /// Request the IsBusy indicator be cleared
        /// </summary>
        void ClearIsBusy();

    }

}
