using System;
using System.Diagnostics;
using ArkReplay.Replay;
using HarmonyLib;

namespace ArkReplay.Patches
{
    [HarmonyPatch(typeof(BattleRandom), nameof(BattleRandom.init))]
    static class BattleRandom_init_Patch
    {
        static void Postfix()
        {
            if (!RunRecorder.Recording) return;
            RunRecorder recorder = RunRecorder.Instance;

            // create new battle
            var action = new ActionStartBattle(BattleRandom.BaseRandomClass.seed, PlayData.BattleQueue, PlayData.BattleCurse);
            recorder.Record(action);
        }
    }
}