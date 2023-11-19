using ArkReplay.Replay.Battle;
using HarmonyLib;

namespace ArkReplay.Patches
{
    [HarmonyPatch(typeof(BattleChar), nameof(BattleChar.Click))]
    class BattleChar_Click_Patch
    {
        static void Prefix(BattleChar __instance)
        {
            if (!RunRecorder.Recording) return;
            RunRecorder recorder = RunRecorder.Instance;

            if (!BattleSystem.instance.TargetSelecting) return;

            if (__instance is BattleEnemy) {
                // get index
                int index = BattleSystem
                    .instance
                    .EnemyTeam
                    .Chars
                    .IndexOf(__instance);

                var action = new ActionTargetSingle(index, false);

                recorder.Record(action);
            } else if (__instance is BattleAlly) {
                // get index
                int index = BattleSystem
                    .instance
                    .AllyTeam
                    .Chars
                    .FindIndex(ally => ally == __instance);

                var action = new ActionTargetSingle(index, true);

                recorder.Record(action);
            }
        }
    }
}