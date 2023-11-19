using Newtonsoft.Json;

namespace ArkReplay.Replay.Battle
{
    /// <summary>
    /// Selects a skill. 
    /// <desc>
    /// This can also be created when the user targets a skill with another
    /// skill.
    /// </desc>
    /// </summary>
    public class ActionSelectSkill : IAction
    {
        [JsonProperty("index")]
        private int skillIndex;

        public ActionSelectSkill(int skillIndex)
        {
            this.skillIndex = skillIndex;
        }

        public void Replay()
        {
            Skill skill = BattleSystem.instance.AllyTeam.Skills[skillIndex];

            skill.MyButton.Click();
        }

        public override string ToString()
        {
            return $"Select skill #{skillIndex}";
        }
    }
}