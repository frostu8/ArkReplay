using System.Collections.Generic;
using HarmonyLib;

namespace ArkReplay.Patches
{
    [HarmonyPatch(typeof(PlayData), nameof(PlayData.CharEquips), MethodType.Getter)]
    static class PlayData_get_CharEquips_Patch
    {
        static void Postfix(List<CharEquipInven> __result)
        {
            // this is lazy but should be fine
            // run inventory initializers
            foreach (CharEquipInven inven in __result)
            {
                if (inven.GetComponent<InventoryHash>() == null)
                {
                    var hash = inven.gameObject.AddComponent<InventoryHash>();
                    hash.Initialize();
                }
            }
        }
    }
}