using ArkReplay.Replay.Battle;
using HarmonyLib;
using UnityEngine;

namespace ArkReplay.Patches
{
    [HarmonyPatch(typeof(BattleSystem), "Update")]
    class BattleSystem_Update_Patch
    {
        static void Prefix(BattleSystem __instance)
        {
            if (!RunRecorder.Recording) return;
            RunRecorder recorder = RunRecorder.Instance;

            // weird edge case, pressing escape to cancel a skilll does a
            // slightly different routine 
            if (__instance.TargetSelecting
                && !__instance.ItemSelect
                && __instance.ActWindow.On
                && !__instance.DelayWait
                && (Input.GetMouseButtonDown(1) || Input.GetKeyDown(KeyCode.Escape)))
            {
                recorder.Record(new ActionCancelSkill());
            }
        }
    }
}