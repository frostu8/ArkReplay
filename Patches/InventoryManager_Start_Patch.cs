using HarmonyLib;

namespace ArkReplay.Replay
{
    [HarmonyPatch(typeof(InventoryManager), "Start")]
    public static class InventoryManager_Start_Patch
    {
        static void Prefix(InventoryManager __instance)
        {
            // add inventory hash
            if (__instance.GetComponent<InventoryHash>() == null)
                __instance.gameObject.AddComponent<InventoryHash>();
        }
    }
}