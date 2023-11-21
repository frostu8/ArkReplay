using System;

namespace ArkReplay.Json
{
    public class JsonTagAttribute : Attribute
    {
        public Type Parent { get; set; }
        public string Name { get; set; }

        public JsonTagAttribute(Type parent, string name = null)
        {
            Parent = parent;
            Name = name;
        }
    }
}