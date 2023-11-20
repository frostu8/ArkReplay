using ArkReplay.Replay;
using HarmonyLib;

namespace ArkReplay.Patches
{
    [HarmonyPatch(typeof(MiniBossObject), nameof(MiniBossObject.GoCamp))]
    static class MiniBossObject_GoCamp_Patch
    {
        static void Prefix()
        {
            if (!RunRecorder.Recording) return;
            RunRecorder recorder = RunRecorder.Instance;

            recorder.Record(new ActionNextCamp());
        }
    }
}