using ArkReplay.Replay.Battle;
using HarmonyLib;

namespace ArkReplay.Patches
{
    [HarmonyPatch(typeof(BattleSystem), nameof(BattleSystem.LoseBattle))]
    class BattleSystem_LoseBattle_Patch
    {
        static void Prefix(BattleSystem __instance)
        {
            if (RunRecorder.Recording)
            {
                // finish record
                RunRecorder.Instance.Save();
            }
            else if (RunReplayer.Replaying)
            {
                // finish replaying
                RunReplayer.Instance.FinishReplay();
            }
        }
    }
}