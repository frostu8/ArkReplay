using System.Linq;
using ArkReplay.Replay;
using ArkReplay.Replay.Battle;
using GameDataEditor;
using HarmonyLib;
using UnityEngine;

namespace ArkReplay.Patches
{
    [HarmonyPatch(typeof(SkillButton), nameof(SkillButton.Click))]
    class SkillButton_Click_Patch
    {
        static void Prefix(SkillButton __instance)
        {
            if (!RunRecorder.Recording) return;
            RunRecorder recorder = RunRecorder.Instance;

            if (__instance.HasDelegate())
            {
                // this skill is most likely part of a select menu
                var buttonIndex = __instance.gameObject.GetComponent<SelectSkillListIndex>();

                if (buttonIndex != null)
                {
                    var selectFromList = new ActionSelectFromList(buttonIndex.index);

                    recorder.Record(selectFromList);
                }
                return;
            }

            if (!__instance.CanSelect()) return;

            // get index
            int index = BattleSystem
                .instance
                .AllyTeam
                .Skills
                .FindIndex(skill => skill.MyButton == __instance);

            if (BattleSystem.instance.TargetSelecting)        
            {
                var selectedSkill = BattleSystem.instance.SelectedSkill;
                var isBasic = __instance.Myskill.BasicSkill;

                if (__instance.Myskill == selectedSkill)
                {
                    // cancel targeting
                    var cancelSkill = new ActionCancelSkill();
                    recorder.Record(cancelSkill);
                }
                else if (!isBasic && selectedSkill.IsTargetTypeSkill())
                {
                    // target skill
                    var selectSkill = new ActionSelectSkill(index);
                    recorder.Record(selectSkill);
                }
            }
            else
            {
                if (!__instance.interactable) return;

                // target skill
                var selectSkill = new ActionSelectSkill(index);
                recorder.Record(selectSkill);
            }
        }
    }
}