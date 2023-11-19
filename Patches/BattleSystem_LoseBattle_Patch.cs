using ArkReplay.Replay.Battle;
using HarmonyLib;

namespace ArkReplay.Patches
{
    [HarmonyPatch(typeof(BattleSystem), nameof(BattleSystem.LoseBattle))]
    class BattleSystem_LoseBattle_Patch
    {
        static void Prefix(BattleSystem __instance)
        {
            if (!RunRecorder.Recording) return;
            RunRecorder recorder = RunRecorder.Instance;

            // finish record
            recorder.Save();
        }
    }
}