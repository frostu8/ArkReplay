using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace ArkReplay.Replay
{
    /// <summary>
    /// The difficulty setting selected in the menu.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum Difficulty
    {
        Normal,
        Expert,
    }
}