using ArkReplay.Json;
using Newtonsoft.Json;

namespace ArkReplay.Replay.Battle
{
    [JsonTag(typeof(Action))]
    public class ActionSelectBasicSkill : IAction
    {
        [JsonProperty("index")]
        private int basicSkillIndex;

        public ActionSelectBasicSkill(int basicSkillIndex)
        {
            this.basicSkillIndex = basicSkillIndex;
        }

        public void Replay()
        {
            var ally = BattleSystem.instance.AllyList[basicSkillIndex];

            ally.MyBasicSkill.Click();
        }

        public bool Ready()
        {
            return Action.BattleReady();
        }

        public override string ToString()
        {
            return $"Select basic skill of ally #{basicSkillIndex}";
        }
    }
}