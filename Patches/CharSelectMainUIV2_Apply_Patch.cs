using ArkReplay.Replay;
using HarmonyLib;

namespace ArkReplay.Patches
{
    [HarmonyPatch(typeof(CharSelectMainUIV2), nameof(CharSelectMainUIV2.Apply))]
    class CharSelectMainUIV2_Apply_Patch
    {
        static void Prefix()
        {
            if (RunRecorder.Instance != null)
            {
                // begin recording
                RunRecorder.Instance.Begin(RunInfo.ThisRun());
            }
        }
    }
}