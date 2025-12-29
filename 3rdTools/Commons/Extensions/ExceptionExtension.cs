using System;
using System.Collections;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Extensions;

public static class ExceptionExtension
{
    public static string ToJson(this Exception e)
    {
        var options = new JsonSerializerOptions { WriteIndented = true, };
        options.Converters.Add(new CustomExceptionConverter());
        var json = JsonSerializer.Serialize(e, options);
        return json;
    }
}

public class CustomExceptionConverter : JsonConverter<Exception>
{
    public override Exception Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return new Exception();
    }

    public override void Write(Utf8JsonWriter writer, Exception value, JsonSerializerOptions options)
    {
        writer.WriteStartObject();

        writer.WriteString("Type", value.GetType().ToString());
        writer.WriteString("Message", value.Message);
        writer.WriteString("StackTrace", value.StackTrace);

        foreach (DictionaryEntry data in value.Data)
            writer.WriteString(data.Key.ToString(), data.Value.ToString());

        if (value.InnerException is not null)
        {
            writer.WritePropertyName("InnerException");
            Write(writer, value.InnerException, options);
        }
        writer.WriteEndObject();
    }
}
