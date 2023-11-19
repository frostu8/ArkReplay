using System.Linq;
using GameDataEditor;

namespace ArkReplay
{
    public static class Extensions
    {
        public static bool CanAct(this BattleSystem system)
        {
            return !BattleSystem.instance.DelayWait
                && BattleSystem.instance.ActWindow.On;
        }

        public static bool IsTargetTypeSkill(this Skill skill)
        {
            return skill.TargetTypeKey == GDEItemKeys.s_targettype_skill
                || skill.TargetTypeKey == GDEItemKeys.s_targettype_allskill;
        }

        public static bool HasDelegate(this SkillButton skill)
        {
            return skill.ClickDelegate != null
                && skill.ClickDelegate.GetInvocationList().GetLength(0) != 0
                && skill.Myskill.AllExtendeds.All(se => se.ButtonSelectTerms());
        }

        // see SkillButton.Click
        // battle system check is omitted because if BattleSystem.instance is
        // null the game will raise a NullPointerException anyway
        public static bool CanSelect(this SkillButton skill)
        {
            return !skill.Myskill.Master.Dummy
                && !skill.IsNowCasting
                && !(skill.isBattleMainSkills && !BattleSystem.instance.CanAct())
                && !UIManager.inst.CharstatUI.activeInHierarchy;
        }
    }
}