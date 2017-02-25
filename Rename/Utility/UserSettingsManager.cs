using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rename.Utility
{

    public class UserSettingsManager : IUserSettingsManager
    {

        //TODO: Is it possible to encrypt/decrypt the json file so it's not human-readable?

        string _UserSettingsPath = "";
        string _UserSettingsFileName = "UserSettings.json";
        string _UserSettingsBackupFileName = "UserSettings_Backup.json";

        //------------------------------------------------------------------------------------------------------------------------
        public UserSettingsManager()
        {
            
            // Calculate path and file name of user settings file
            _UserSettingsPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), @"AndrewSMoroz", @"Rename");
            //_UserSettingsFileName = Path.Combine(_UserSettingsPath, _UserSettingsFileName);

            // Create path if necessary
            if (!Directory.Exists(_UserSettingsPath))
            {
                Directory.CreateDirectory(_UserSettingsPath);
            }

        }

        //------------------------------------------------------------------------------------------------------------------------
        public UserSettings GetUserSettings()
        {

            UserSettings userSettings = new UserSettings();

            // Read file into a string and deserialize JSON to a type
            userSettings = JsonConvert.DeserializeObject<UserSettings>(File.ReadAllText(Path.Combine(_UserSettingsPath, _UserSettingsFileName)));
            
            //// deserialize JSON directly from a file
            //using(StreamReader file = File.OpenText(_UserSettingsFileName))
            //{
            //    JsonSerializer serializer = new JsonSerializer();
            //    userSettings = (UserSettings)serializer.Deserialize(file, typeof(UserSettings));
            //}

            return userSettings;

        }

        //------------------------------------------------------------------------------------------------------------------------
        public void SaveUserSettings(UserSettings userSettings)
        {

            // Backup existing file
            File.Copy(Path.Combine(_UserSettingsPath, _UserSettingsFileName), Path.Combine(_UserSettingsPath, _UserSettingsBackupFileName), true);

            // Serialize JSON to a string and then write string to a file
            File.WriteAllText(Path.Combine(_UserSettingsPath, _UserSettingsFileName), JsonConvert.SerializeObject(userSettings));
            
            //// serialize JSON directly to a file
            //using(StreamWriter file = File.CreateText(_UserSettingsFileName))
            //{
            //    JsonSerializer serializer = new JsonSerializer();
            //    serializer.Serialize(file, userSettings);
            //}

        }

    }

}
