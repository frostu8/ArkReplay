using Newtonsoft.Json;

namespace ArkReplay.Replay
{
    /// <summary>
    /// A serializable action.
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    [JsonConverter(typeof(JsonActionConverter))]
    public class Action
    {
        public IAction action;

        public Action(IAction action)
        {
            this.action = action;
        }
    }
}
