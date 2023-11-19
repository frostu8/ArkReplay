using ArkReplay.Replay.Battle;
using HarmonyLib;

namespace ArkReplay.Patches
{
    [HarmonyPatch(typeof(SelectSkillList), nameof(SelectSkillList.CancelButton))]
    static class SelectSkillList_CancelButton_Patch
    {
        static void Prefix()
        {
            if (!RunRecorder.Recording) return;
            RunRecorder recorder = RunRecorder.Instance;

            recorder.Record(new ActionCancelSkill());
        }
    }
}