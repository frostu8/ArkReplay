using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using HarmonyLib;

namespace ArkReplay
{
    /// <summary>
    /// A collection of reverse patches.
    /// </summary>
    [HarmonyPatch]
    public static class ReversePatch
    {
        [HarmonyReversePatch]
        [HarmonyPatch(typeof(UI_Camp), nameof(UI_Camp.ItemUse))]
        public static void UI_Camp_ItemUse(UI_Camp __instance, ItemObject select)
        {
            IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
            {
                return Transpilers.Manipulator(
                    instructions,
                    code => code.opcode == OpCodes.Ldloc_0,
                    code => code.opcode = OpCodes.Ldc_I4_0
                );
            }

            _ = Transpiler(null);
        }
    }
}