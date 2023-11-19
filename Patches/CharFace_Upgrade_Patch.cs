using System;
using ArkReplay.Replay;
using HarmonyLib;

namespace ArkReplay.Patches
{
    [HarmonyPatch(typeof(CharFace), nameof(CharFace.Upgrade), new Type[] {})]
    static class CharFace_Upgrade_Patch
    {
        static void Prefix(CharFace __instance)
        {
            if (!RunRecorder.Recording) return;
            RunRecorder recorder = RunRecorder.Instance;

            var charStats = UIManager.inst.CharstatUI.GetComponent<CharStatV4>();

            int index = charStats.CharInfos.IndexOf(__instance);

            recorder.Record(new ActionUpgradePartyMember(index));
        }
    }
}