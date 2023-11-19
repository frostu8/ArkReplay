using System.Collections.Generic;
using System.Reflection.Emit;
using ArkReplay.Replay;
using HarmonyLib;

namespace ArkReplay.Patches
{
    [HarmonyPatch(typeof(CharStatV4), nameof(CharStatV4.DrawUpgrade))]
    static class CharStatV4_DrawUpgrade_Patch
    {
        static void Prefix(CharStatV4 __instance)
        {
            if (!RunRecorder.Recording) return;
            RunRecorder recorder = RunRecorder.Instance;

            if (!__instance.AddDrawSkillBtn.interactable) return;

            var action = new ActionDrawUpgrade();

            recorder.Record(action);
        }
    }
}