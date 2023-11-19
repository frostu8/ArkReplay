using ArkReplay.Replay.Battle;
using HarmonyLib;

namespace ArkReplay.Patches
{
    [HarmonyPatch(typeof(BasicSkill), nameof(BasicSkill.Click))]
    class BasicSkill_Click_Patch
    {
        static void Prefix(BasicSkill __instance)
        {
            if (!RunRecorder.Recording) return;
            RunRecorder recorder = RunRecorder.Instance;

            if (!__instance.interactable) return;

            if (BattleSystem.instance.TargetSelecting)
            {
                if (__instance.buttonData == BattleSystem.instance.SelectedSkill)
                {
                    recorder.Record(new ActionCancelSkill());
                }
            }
            else
            {
                // find index
                int index = BattleSystem
                    .instance
                    .AllyList
                    .FindIndex(ally => ally.MyBasicSkill == __instance);

                var action = new ActionSelectBasicSkill(index);

                recorder.Record(action);
            }
        }
    }
}