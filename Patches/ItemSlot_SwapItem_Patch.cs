using ArkReplay.Replay;
using HarmonyLib;

namespace ArkReplay
{
    [HarmonyPatch(typeof(ItemSlot), nameof(ItemSlot.SwapItem))]
    static class ItemSlot_SwapItem_Patch
    {
        static void Prefix(ItemSlot __instance)
        {
            if (!RunRecorder.Recording) return;
            RunRecorder recorder = RunRecorder.Instance;

            var startSlot = ItemObject.itemBeingDragged.StartedSlot.GetComponent<ItemSlot>();

            var action = new ActionMoveItem(
                new InventoryLocation(startSlot),
                new InventoryLocation(__instance)
            );

            recorder.Record(action);
        }
    }
}