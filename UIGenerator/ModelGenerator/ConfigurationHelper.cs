using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Json;

namespace UIGenerator.ModelGenerator
{
    public static class ConfigurationHelper
    {    // IEnumerable<IModelParam>
        public static void SaveConfiguration(Dictionary<string, string> configParams) {
            try {
                DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(Dictionary<string, string>));
                using (FileStream fs = new FileStream(ConfigurationFilePath(), FileMode.Create))
                {
                    jsonFormatter.WriteObject(fs, configParams);
                }
            }
            catch (Exception) {
                return;
            }
        }

        public static Dictionary<string, string> LoadConfiguration() {
            try {
                DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(Dictionary<string, string>));
                using (FileStream fs = new FileStream(ConfigurationFilePath(), FileMode.Open))
                {
                    return jsonFormatter.ReadObject(fs) as Dictionary<string, string>;
                }
            }
            catch (Exception) {
                return new Dictionary<string, string>();
            }
        }

        private static string ConfigurationFilePath() {
            return "configuration.json";
        }
    }
}
