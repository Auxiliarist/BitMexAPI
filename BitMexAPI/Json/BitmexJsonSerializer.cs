using ServiceStack.Text;
using System;
using System.Collections.Generic;
using System.Text;

namespace BitMexAPI.Json
{
    public static class BitmexJsonSerializer
    {
        static BitmexJsonSerializer()
        {
            JsConfig.EmitCamelCaseNames = true;
        }

        public static T Deserialize<T>(string inputText) => JsonSerializer.DeserializeFromString<T>(inputText);
        
        public static string Serialize(object toSerialise) => JsonSerializer.SerializeToString(toSerialise);

        public static T Handle<T>(JsonObject response)
        {
            var json = Serialize(response);
            return Deserialize<T>(json);
        }
    }
}
