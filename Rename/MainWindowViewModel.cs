using System;
using Rename.Utility;
using System.Windows.Input;

namespace Rename
{

    public class MainWindowViewModel : Rename.Utility.ViewModelBase, IDisposable
	{

#region Members

		private readonly IInteractionManager _InteractionManager;
        private readonly INavigationManager _NavigationManager;

        private bool _IsBusy;
        private string _BusyText;
        private bool _IsProgressBarVisible;
        private int _ProgressBarMaximum;
        private int _ProgressBarValue;

#endregion Members

#region Constructors

        //------------------------------------------------------------------------------------------------------------------------
        public MainWindowViewModel(IInteractionManager interactionManager, INavigationManager navigationManager)
		{
			_InteractionManager = interactionManager;
            _InteractionManager.IsBusyChanged += _InteractionManager_IsBusyChanged;
            _NavigationManager = navigationManager;
            _NavigationManager.GoToHomePage();
		}

        //------------------------------------------------------------------------------------------------------------------------
        ~MainWindowViewModel()
        {
            try
            {
                _InteractionManager.IsBusyChanged += _InteractionManager_IsBusyChanged;
            }
            catch { }
        }

#endregion Constructors

#region Properties

        //------------------------------------------------------------------------------------------------------------------------
        public IInteractionManager InteractionManager
        {
            get { return _InteractionManager; }
        }

        //------------------------------------------------------------------------------------------------------------------------
        public bool IsBusy
        {
            get { return _IsBusy; }
            set { base.SetProperty(ref _IsBusy, value, () => this.IsBusy); }
        }

        //------------------------------------------------------------------------------------------------------------------------
        public string BusyText
        {
            get { return _BusyText; }
            set { base.SetProperty(ref _BusyText, value, () => this.BusyText); }
        }

        //------------------------------------------------------------------------------------------------------------------------
        public bool IsProgressBarVisible
        {
            get { return _IsProgressBarVisible; }
            set { base.SetProperty(ref _IsProgressBarVisible, value, () => this.IsProgressBarVisible); }
        }

        //------------------------------------------------------------------------------------------------------------------------
        public int ProgressBarMaximum
        {
            get { return _ProgressBarMaximum; }
            set { base.SetProperty(ref _ProgressBarMaximum, value, () => this.ProgressBarMaximum); }
        }

        //------------------------------------------------------------------------------------------------------------------------
        public int ProgressBarValue
        {
            get { return _ProgressBarValue; }
            set { base.SetProperty(ref _ProgressBarValue, value, () => this.ProgressBarValue); }
        }

#endregion Properties

#region Commands

    #region Navigate Rename

        //------------------------------------------------------------------------------------------------------------------------
        public ICommand NavigateRenameCommand
        {
            get { return new RelayCommandEx(this.NavigateRenameCommandExecute); }
        }

        //------------------------------------------------------------------------------------------------------------------------
        private void NavigateRenameCommandExecute()
        {
            try
            {
                _InteractionManager.SetIsBusy("Going to Rename page...");
                _NavigationManager.GoToRenamePage();
            }
            catch (Exception ex)
            {
                _InteractionManager.DisplayError(ex);
            }
            finally
            {
                _InteractionManager.ClearIsBusy();
            }
        }

#endregion Navigate Rename

#region Navigate View

        //------------------------------------------------------------------------------------------------------------------------
        public ICommand NavigateViewCommand
        {
            get { return new RelayCommandEx(this.NavigateViewCommandExecute); }
        }

        //------------------------------------------------------------------------------------------------------------------------
        private void NavigateViewCommandExecute()
        {
            try
            {
                _InteractionManager.SetIsBusy("Going to View page...");
                _NavigationManager.GoToViewPage();
            }
            catch (Exception ex)
            {
                _InteractionManager.DisplayError(ex);
            }
            finally
            {
                _InteractionManager.ClearIsBusy();
            }
        }

#endregion Navigate View

#region Navigate Settings

        //------------------------------------------------------------------------------------------------------------------------
        public ICommand NavigateSettingsCommand
        {
            get { return new RelayCommandEx(this.NavigateSettingsCommandExecute); }
        }

        //------------------------------------------------------------------------------------------------------------------------
        private void NavigateSettingsCommandExecute()
        {
            try
            {
                _InteractionManager.SetIsBusy("Going to Settings page...");
                _NavigationManager.GoToSettingsPage();
            }
            catch (Exception ex)
            {
                _InteractionManager.DisplayError(ex);
            }
            finally
            {
                _InteractionManager.ClearIsBusy();
            }
        }

    #endregion Navigate Settings

#endregion Commands

#region Methods

        //------------------------------------------------------------------------------------------------------------------------
        void _InteractionManager_IsBusyChanged(bool value, string text, int progressBarMaximum, int progressBarValue)
        {
            this.IsBusy = value;
            this.BusyText = (text ?? "Working, please wait...");
            this.IsProgressBarVisible = !(progressBarMaximum == 0 && ProgressBarValue == 0);
            this.ProgressBarMaximum = progressBarMaximum;
            this.ProgressBarValue = progressBarValue;
        }

        //------------------------------------------------------------------------------------------------------------------------
        public void Dispose()
        {
            //_InteractionManager.OnBusyDialogStatusChanged -= new System.Action<string>(InteractionManager_OnBusyDialogStatusChanged);
        }

#endregion Methods

	}

}
