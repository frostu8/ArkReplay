using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;

namespace ArkReplay.Json
{
    public class JsonTaggedConverter : JsonConverter
    {
        private static TagDictionary tagDict = new TagDictionary();

        public static void RegisterAll(Assembly asm)
            => tagDict.RegisterAll(asm);

        public override bool CanConvert(Type objectType)
        {
            return typeof(IJsonTagged).IsAssignableFrom(objectType);
        }

        public override object ReadJson(
            JsonReader reader,
            Type objectType,
            object existingValue,
            JsonSerializer serializer)
        {
            IJsonTagged tagged = CreateTagged(objectType);

            if (reader.TokenType != JsonToken.StartObject)
                throw new JsonException($"expected object, got {reader.TokenType}");

            if (!reader.Read() || reader.TokenType != JsonToken.PropertyName)
                throw new JsonException($"expected tagged field, got {reader.TokenType}");

            // get action type name
            string name = (string) reader.Value;

            if (tagDict.GetTypeOf(objectType, name, out Type enumType))
            {
                // get value (this is infallible)
                reader.Read();

                // deserialize
                tagged.SetTagged(serializer.Deserialize(reader, enumType));

                // consume endobject token
                reader.Read();

                return tagged;
            }
            else
            {
                throw new JsonException($"expected valid enum name, got {name}");
            }
        }

        public override void WriteJson(
            JsonWriter writer,
            object value,
            JsonSerializer serializer)
        {
            IJsonTagged tagged = (IJsonTagged) value;

            Type enumType = tagged.GetTagged().GetType();

            // find name
            if (tagDict.GetNameOf(tagged.GetType(), enumType, out string name))
            {
                writer.WriteStartObject();
                writer.WritePropertyName(name);

                serializer.Serialize(writer, tagged.GetTagged());

                writer.WriteEndObject();
            }
            else
            {
                throw new JsonException("serializing a type that isn't "
                    + "registered with the json converter");
            }
        }

        public static IJsonTagged CreateTagged(Type type)
        {
            return (IJsonTagged) Activator.CreateInstance(type);
        }
    }
}