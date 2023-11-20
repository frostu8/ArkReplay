using Newtonsoft.Json;

namespace ArkReplay.Replay.Battle
{
    public class ActionTargetSingle : IAction
    {
        [JsonProperty("index")]
        private int targetIndex;
        [JsonProperty("ally_team")]
        private bool ally;

        public ActionTargetSingle(int targetIndex, bool ally)
        {
            this.targetIndex = targetIndex;
            this.ally = ally;
        }

        public void Replay()
        {
            if (ally) {
                BattleSystem.instance.AllyTeam.Chars[targetIndex].Click();
            } else {
                BattleSystem.instance.EnemyTeam.Chars[targetIndex].Click();
            }
        }

        public bool Ready()
        {
            return Action.BattleReady();
        }

        public override string ToString()
        {
            var hostile = ally ? "ally" : "enemy";

            return $"Selected {hostile} target #{targetIndex}";
        }
    }
}