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

        internal static bool FieldReady()
        {
            return FieldSystem.instance.Playercontrol.enabled;
        }

        internal static bool BattleReady()
        {
            // battle actions can only happen in battle
            if (BattleSystem.instance == null)
                return false;

            return !BattleSystem.instance.DelayWait
                && BattleSystem.instance.ActWindow.On;
        }
    }
}
