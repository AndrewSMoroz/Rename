using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rename.Utility
{

    public interface IUserSettingsManager
    {

        UserSettings GetUserSettings();
        void SaveUserSettings(UserSettings userSettings);

    }

}
