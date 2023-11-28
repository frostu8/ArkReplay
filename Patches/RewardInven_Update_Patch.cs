using System.Collections.Generic;
using System.Reflection.Emit;
using ArkReplay.Replay;
using HarmonyLib;

namespace ArkReplay.Patches
{
    [HarmonyPatch(typeof(RewardInven), "Update")]
    static class RewardInven_Update_Patch
    {
        static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            var interestMethod = AccessTools.Method(
                typeof(InventoryManager),
                nameof(InventoryManager.ToInventory),
                new System.Type[] {}
            );

            foreach (CodeInstruction instruction in instructions)
            {
                if (instruction.Calls(interestMethod))
                {
                    yield return new CodeInstruction(
                        OpCodes.Call,
                        AccessTools.Method(typeof(PatchHelper), nameof(PatchHelper.RecordedToInventory))
                    );
                }
                else
                {
                    yield return instruction;
                }
            }
        }
    }
}