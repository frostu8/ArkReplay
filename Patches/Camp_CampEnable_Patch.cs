using ArkReplay.Replay;
using HarmonyLib;

namespace ArkReplay.Patches
{
    [HarmonyPatch(typeof(Camp), nameof(Camp.CampEnable))]
    static class Camp_CampEnable_Patch
    {
        static void Prefix(Camp __instance)
        {
            if (!RunRecorder.Recording) return;
            RunRecorder recorder = RunRecorder.Instance;

            if (!__instance.CampEnd)
                recorder.Record(new ActionEnableCamp());
        }
    }
}