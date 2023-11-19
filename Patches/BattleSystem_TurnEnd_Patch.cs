using ArkReplay.Replay.Battle;
using HarmonyLib;

namespace ArkReplay.Patches
{
    [HarmonyPatch(typeof(BattleSystem), nameof(BattleSystem.TurnEnd))]
    class BattleSystem_TurnEnd_Patch
    {
        static void Prefix()
        {
            if (!RunRecorder.Recording) return;
            RunRecorder recorder = RunRecorder.Instance;

            if (!BattleSystem.instance.CantTurnEnd && BattleSystem.instance.CanAct())
                recorder.Record(new ActionEndTurn());
        }
    }
}