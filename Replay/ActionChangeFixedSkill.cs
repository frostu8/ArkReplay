using Newtonsoft.Json;
using ArkReplay.Json;

namespace ArkReplay.Replay
{
    [JsonTag(typeof(Action))]
    public class ActionChangeFixedSkill : IAction
    {
        [JsonProperty("party_index")]
        public int partyIndex;
        [JsonProperty("skill_index")]
        public int skillIndex;

        public ActionChangeFixedSkill(int partyIndex, int skillIndex)
        {
            this.partyIndex = partyIndex;
            this.skillIndex = skillIndex;
        }

        public void Replay()
        {
            var ally = PlayData.Battleallys[partyIndex];
            var partyMember = ally.Info;

            var skill = partyMember.SkillDatas[skillIndex];
            partyMember.SkillDatas.RemoveAt(skillIndex);

            if (partyMember.BasicSkill != null)
            {
                partyMember.SkillDatas.Add(new CharInfoSkillData(partyMember.BasicSkill));
            }

            partyMember.BasicSkill = skill.Skill;

            ally.BasicSkill = Skill.TempSkill(
                partyMember.BasicSkill.KeyID,
                ally,
                PlayData.TempBattleTeam);

            // update charstats menu
            var charStats = UIManager.inst.CharstatUI.GetComponent<CharStatV4>();

            charStats.CharInfos[partyIndex].Init();
            charStats.ParamSkillNum.SetParameterValue("SkillNum", ally.Skills.Count.ToString());

        }

        public bool Ready()
        {
            return Action.FieldReady();
        }

        public override string ToString()
        {
            return $"Change fixed skill to skill #{skillIndex}";
        }
    }
}