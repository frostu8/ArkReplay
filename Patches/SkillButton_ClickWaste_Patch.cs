using ArkReplay.Replay.Battle;
using HarmonyLib;

namespace ArkReplay.Patches
{
    [HarmonyPatch(typeof(SkillButton), nameof(SkillButton.ClickWaste))]
    class SkillButton_ClickWaste_Patch
    {
        static void Prefix(SkillButton __instance)
        {
            if (!RunRecorder.Recording) return;
            RunRecorder recorder = RunRecorder.Instance;

            // get index
            int index = BattleSystem
                .instance
                .AllyTeam
                .Skills
                .FindIndex(skill => skill.MyButton == __instance);

            var action = new ActionExchange(index);

            recorder.Record(action);
        }
    }
}