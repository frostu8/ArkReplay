using ArkReplay.Replay;
using GameDataEditor;
using HarmonyLib;

namespace ArkReplay.Patches
{
    [HarmonyPatch(typeof(RedWall), nameof(RedWall.Enter))]
    static class RedWall_Enter_Patch
    {
        static void Prefix()
        {
            if (!RunRecorder.Recording) return;
            RunRecorder recorder = RunRecorder.Instance;

            recorder.Record(new ActionMoveStage(GDEItemKeys.Stage_Stage_Crimson));
        }
    }
}