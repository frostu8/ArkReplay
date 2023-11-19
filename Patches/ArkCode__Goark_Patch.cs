using HarmonyLib;

namespace ArkReplay.Patches
{
    [HarmonyPatch(typeof(ArkCode), "_Goark")]
    class ArkCode__Goark_Patch
    {
        static void Postfix()
        {
            if (RunRecorder.Instance != null)
            {
                int seed = PlayData.TSavedata.randomSave.seed;
                RunRecorder.Instance.Begin(seed);
            }
        }
    }
}