using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

/// <summary>
/// Summary description for Root
/// </summary>
public class Root : JsonConverter
{


    public Root()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public override bool CanConvert(Type objectType)
    {
        return (objectType == typeof(decimal)
            || objectType == typeof(decimal?));
    }

    public override void WriteJson(JsonWriter writer, object value,
                                   JsonSerializer serializer)
    {
        writer.WriteValue(Math.Round(Convert.ToDecimal(value), 4));
    }

    public override bool CanRead { get { return false; } }

    public override object ReadJson(JsonReader reader, Type objectType,
                                 object existingValue, JsonSerializer serializer)
    {
        throw new NotImplementedException();
    }
}