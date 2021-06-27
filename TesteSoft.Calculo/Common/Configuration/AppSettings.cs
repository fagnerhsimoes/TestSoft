using System.Collections.Generic;
using System.Linq;

namespace TesteSoft.Calculo.Common.Configuration
{
    public class AppSettings
    {

        public List<Setting> Settings { get; set; }


        public string Get(string key)
        {
            var setting = Settings.FirstOrDefault(x => x.Key == key);
            return setting != null ? setting.Value : "";
        }

    }
}
