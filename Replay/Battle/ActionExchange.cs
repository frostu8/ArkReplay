using Newtonsoft.Json;

namespace ArkReplay.Replay.Battle
{
    public class ActionExchange : IAction
    {
        [JsonProperty("index")]
        private int skillIndex;

        public ActionExchange(int skillIndex)
        {
            this.skillIndex = skillIndex;
        }

        public void Replay()
        {
            Skill skill = BattleSystem.instance.AllyTeam.Skills[skillIndex];

            skill.MyButton.ClickWaste();
        }

        public bool Ready()
        {
            return Action.BattleReady();
        }

        public override string ToString()
        {
            return $"Exchange skill #{skillIndex}";
        }
    }
}