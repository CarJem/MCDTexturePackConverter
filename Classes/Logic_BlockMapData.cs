using System.Collections.Generic;
using System.Linq;

namespace MCDTexturePackConverter.Classes
{
    public class Logic_BlockMapData
    {
        public static List<Logic_BlockMapData> Convert(Logic_IBlockMapValue value)
        {
            List<Logic_BlockMapData> list = new List<Logic_BlockMapData>();
            if (value is Logic_BlockMapSimpleValue)
            {
                Logic_BlockMapData result = new Logic_BlockMapData();
                var data = (value as Logic_BlockMapSimpleValue);
                if (data.Count > 0) result.DungeonsID = data[0];
                if (data.Count > 1) result.DungeonsData = data[1];
                if (data.Count > 2) result.DungeonsDataMask = data[2];
                list.Add(result);
            }
            else if (value is Logic_BlockMapAdvancedValue)
            {
                var dataHolder = (value as Logic_BlockMapAdvancedValue);
                foreach (var data in dataHolder) 
                {
                    Logic_BlockMapData result = new Logic_BlockMapData();
                    if (data.dungeons.Count > 0) result.DungeonsID = data.dungeons[0];
                    if (data.dungeons.Count > 1) result.DungeonsData = data.dungeons[1];
                    if (data.dungeons.Count > 2) result.DungeonsDataMask = data.dungeons[2];
                    if (data.props != null) result.Properties = data.props;
                    list.Add(result);
                }

            }

            return list;
        }

        public Dictionary<string, string> Properties { get; private set; } = new Dictionary<string, string>();
        public int DungeonsID { get; private set; } = -1;
        public int DungeonsData { get; private set; } = -1;
        public int DungeonsDataMask { get; private set; } = -1;
        public string PropertiesString
        {
            get
            {
                string s = string.Join(",", Properties.Select(x => x.Key + "=" + x.Value).ToArray());
                return s;
            }
        }
    }
}
