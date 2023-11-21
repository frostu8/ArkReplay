using ArkReplay.Json;

namespace ArkReplay.Replay
{
    [JsonTag(typeof(Action))]
    public class ActionApUpgrade : IAction
    {
        public void Replay()
        {
            var charStats = UIManager.inst.CharstatUI.GetComponent<CharStatV4>();
            charStats.MPUpgrade();
        }

        public bool Ready()
        {
            return Action.FieldReady();
        }

        public override string ToString()
        {
            return $"AP upgrade";
        }
    }
}