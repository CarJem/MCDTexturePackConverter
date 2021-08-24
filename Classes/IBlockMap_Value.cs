using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MCDTexturePackConverter.Classes
{
    public class BlockMap_SimpleValue : List<int>, IBlockMap_Value { }
    public class BlockMap_AdvancedValue : List<BlockMap_AdvancedValueData>, IBlockMap_Value { }
    public class BlockMap_AdvancedValueData
    {
        public Dictionary<string, string> props { get; set; }
        public List<int> dungeons { get; set; }
    }
    public interface IBlockMap_Value
    {
        public static IBlockMap_Value Convert(JToken jToken)
        {
            var settings = new JsonSerializerSettings { Error = (se, ev) => { ev.ErrorContext.Handled = true; } };

            if (jToken.Type == JTokenType.Array && jToken.HasValues && jToken.First.Type == JTokenType.Integer)
            {
                var simpleResult = JsonConvert.DeserializeObject<BlockMap_SimpleValue>(jToken.ToString(), settings);
                if (simpleResult != null) return simpleResult;
            }


            var advancedResult = JsonConvert.DeserializeObject<BlockMap_AdvancedValue>(jToken.ToString(), settings);
            if (advancedResult != null) return advancedResult;

            throw new Exception(string.Format("JToken \"{0}\" not convertable!", jToken));
        }
    }
    public class BlockMapData
    {
        public static List<BlockMapData> Convert(IBlockMap_Value value)
        {
            List<BlockMapData> list = new List<BlockMapData>();
            if (value is BlockMap_SimpleValue)
            {
                BlockMapData result = new BlockMapData();
                var data = (value as BlockMap_SimpleValue);
                if (data.Count > 0) result.DungeonsID = data[0];
                if (data.Count > 1) result.DungeonsData = data[1];
                if (data.Count > 2) result.DungeonsDataMask = data[2];
                list.Add(result);
            }
            else if (value is BlockMap_AdvancedValue)
            {
                var dataHolder = (value as BlockMap_AdvancedValue);
                foreach (var data in dataHolder) 
                {
                    BlockMapData result = new BlockMapData();
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
