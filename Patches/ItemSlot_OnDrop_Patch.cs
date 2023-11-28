using ArkReplay.Replay;
using HarmonyLib;

namespace ArkReplay
{
    [HarmonyPatch(typeof(ItemSlot), nameof(ItemSlot.OnDrop))]
    static class ItemSlot_OnDrop_Patch
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