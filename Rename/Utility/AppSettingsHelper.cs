using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;

namespace Rename.Utility
{

    public sealed class AppSettingsHelper
    {

        //----------------------------------------------------------------------------------------------------------------------------------
        private AppSettingsHelper()
        {
        }

        //----------------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Gets the value for the specified key from the configuration file
        /// </summary>
        public static string GetStringAppSetting(string key, string defaultValue)
        {
            string value = ConfigurationManager.AppSettings[key];

            if (string.IsNullOrEmpty(value))
            {
                value = defaultValue;
            }

            return value;
        }

        //----------------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Gets the value for the specified key within the specified section of the configuration file
        /// </summary>
        private static string GetStringAppSetting(string section, string key, string defaultValue)
        {

            NameValueCollection sectionCollection = (NameValueCollection)ConfigurationManager.GetSection(section);
            string value = (sectionCollection ?? new NameValueCollection())[key];

            if (string.IsNullOrEmpty(value))
            {
                value = defaultValue;
            }

            return value;
        }

        //----------------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Type and exception safe helper to retrieve app settings from the configuration files.
        /// </summary>
        /// <typeparam name="T">Type of item to retrieve</typeparam>
        /// <param name="key">Key of the item to retrieve</param>
        /// <param name="defaultValue">Default value to use in case of missing item</param>
        /// <returns>Typed item value</returns>
        public static T GetAppSetting<T>(string key, T defaultValue) where T : struct
        {
            T result = defaultValue;

            string value = ConfigurationManager.AppSettings[key];

            if (!string.IsNullOrEmpty(value))
            {
                try
                {
                    result = (T)Convert.ChangeType(value, typeof(T));
                }
                catch (Exception ex)
                {
                    System.Windows.MessageBox.Show("AppSettingsHelper.GetAppSetting<T> without section: " + ex.Message, "ERROR", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
                }
            }

            return result;
        }

        //----------------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Type and exception safe helper to retrieve an app setting from the specified config section of the configuration files.
        /// </summary>
        /// <typeparam name="T">Type of item to retrieve</typeparam>
        /// <param name="section">Name of config section containing item to retrieve</param>
        /// <param name="key">Key of the item to retrieve</param>
        /// <param name="defaultValue">Default value to use in case of missing item</param>
        /// <returns>Typed item value</returns>
        private static T GetAppSetting<T>(string section, string key, T defaultValue) where T : struct
        {
            T result = defaultValue;

            NameValueCollection sectionCollection = (NameValueCollection)ConfigurationManager.GetSection(section);
            string value = (sectionCollection ?? new NameValueCollection())[key];

            if (!string.IsNullOrEmpty(value))
            {
                try
                {
                    result = (T)Convert.ChangeType(value, typeof(T));
                }
                catch (Exception ex)
                {
                    System.Windows.MessageBox.Show("AppSettingsHelper.GetAppSetting<T> with section: " + ex.Message, "ERROR", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
                }
            }

            return result;
        }

        //----------------------------------------------------------------------------------------------------------------------------------
        public static KeyValuePair<string, string>[] GetAppSettingGroup(string groupPrefix)
        {
            var appSettings = ConfigurationManager.AppSettings.Keys.Cast<string>();

            var groupSettings = from s in appSettings
                                where s.StartsWith(groupPrefix)
                                select new KeyValuePair<string, string>(s, ConfigurationManager.AppSettings[s]);

            KeyValuePair<string, string>[] result = groupSettings.ToArray();

            return result;
        }

    }

}
