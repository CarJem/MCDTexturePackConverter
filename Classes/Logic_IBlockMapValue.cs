using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace MCDTexturePackConverter.Classes
{
    public interface Logic_IBlockMapValue
    {
        public static Logic_IBlockMapValue Convert(JToken jToken)
        {
            var settings = new JsonSerializerSettings { Error = (se, ev) => { ev.ErrorContext.Handled = true; } };

            if (jToken.Type == JTokenType.Array && jToken.HasValues && jToken.First.Type == JTokenType.Integer)
            {
                var simpleResult = JsonConvert.DeserializeObject<Logic_BlockMapSimpleValue>(jToken.ToString(), settings);
                if (simpleResult != null) return simpleResult;
            }


            var advancedResult = JsonConvert.DeserializeObject<Logic_BlockMapAdvancedValue>(jToken.ToString(), settings);
            if (advancedResult != null) return advancedResult;

            throw new Exception(string.Format("JToken \"{0}\" not convertable!", jToken));
        }
    }
}
