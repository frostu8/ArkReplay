using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ArkReplay.Json
{
    public class TagDictionary
    {
        private Dictionary<(Type, string), Type> enumTypes;

        public TagDictionary()
        {
            enumTypes = new Dictionary<(Type, string), Type>();
        }

        public void RegisterAll(Assembly asm)
        {
            foreach (Type type in asm.GetTypes())
            {
                var attrib = type.GetCustomAttribute<JsonTagAttribute>();

                if (attrib != null)
                    Register(type, attrib);
            }
        }

        public void Register(Type type, JsonTagAttribute tag)
        {
            string tagName = tag.Name ?? StripActionPrefix(type.Name);

            if (!enumTypes.ContainsKey((tag.Parent, tagName)))
            {
                UnityEngine.Debug.Log("JsonTag registering "
                    + $"type {type.FullName} "
                    + $"(parent {tag.Parent.FullName}) "
                    + $"as tag \"{tagName}\", "
                    + $"asm {type.Assembly.FullName}");
                enumTypes.Add((tag.Parent, tagName), type);
            }
        }

        private static string StripActionPrefix(string name)
            => name.StartsWith("Action") && name.Length > 6 ? name.Substring(6) : name;

        public bool GetNameOf(Type parentType, Type type, out string name)
        {
            name = enumTypes
                .Where(p => p.Value == type)
                .Select(p => p.Key.Item2)
                .FirstOrDefault();

            return name != null;
        }

        public bool GetTypeOf(Type parentType, string en, out Type type)
        {
            return enumTypes.TryGetValue((parentType, en), out type);
        }
    }
}