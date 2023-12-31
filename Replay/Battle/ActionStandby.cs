using ArkReplay.Json;

namespace ArkReplay.Replay.Battle
{
    [JsonTag(typeof(Action))]
    public class ActionStandby : IAction
    {
        public ActionStandby()
        { }

        public void Replay()
        {
            var wait = BattleSystem.instance.ActWindow.WaitButton.GetComponent<WaitButton>();
            wait.WaitAct();
        }

        public bool Ready()
        {
            return Action.BattleReady();
        }

        public override string ToString()
        {
            return "Standby";
        }
    }
}