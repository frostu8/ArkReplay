using ArkReplay.Replay;
using HarmonyLib;

namespace ArkReplay.Patches
{
    [HarmonyPatch(typeof(UI_Camp), nameof(UI_Camp.ItemUse))]
    static class UI_Camp_ItemUse_Patch
    {
        static void Prefix(ItemObject select)
        {
            if (!RunRecorder.Recording) return;
            RunRecorder recorder = RunRecorder.Instance;

            // get index
            int index = select.MySlot.Number;

            recorder.Record(new ActionUseCampItem(index));
        }
    }
}