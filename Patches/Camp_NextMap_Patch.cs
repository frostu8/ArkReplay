using ArkReplay.Replay;
using HarmonyLib;

namespace ArkReplay.Patches
{
    [HarmonyPatch(typeof(Camp), nameof(Camp.NextMap))]
    static class Camp_NextMap_Patch
    {
        static void Prefix()
        {
            if (!RunRecorder.Recording) return;
            RunRecorder recorder = RunRecorder.Instance;

            var nextStage = ActionMoveStage.NextStageKey;

            recorder.Record(new ActionMoveStage(nextStage));
        }
    }
}