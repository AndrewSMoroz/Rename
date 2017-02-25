using System;

namespace Rename.Utility
{

	public abstract class ViewModelBase : ObservableObject
	{

#region Members

        private bool _IsAccessDenied;

#endregion Members

#region Properties

        //------------------------------------------------------------------------------------------------------------------------
        public bool IsAccessDenied
        {
            get { return _IsAccessDenied; }
            set { base.SetProperty(ref _IsAccessDenied, value, () => this.IsAccessDenied); }
        }

#endregion Properties

#region Methods

        //------------------------------------------------------------------------------------------------------------------------
        protected void Pause()
        {
            int count = 500;
            for (int i = 0; i < count; i++)
                for (int j = 0; j < count; j++)
                    for (int k = 0; k < count; k++) { }
        }

#endregion

    }

}
