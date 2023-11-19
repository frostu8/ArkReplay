using System.Collections.Generic;
using System.Reflection.Emit;
using HarmonyLib;

namespace ArkReplay.Patches
{
    [HarmonyPatch(typeof(SelectSkillList), nameof(SelectSkillList.NewSelectSkillList))]
    static class SelectSkillList_NewSelectSkillList_Patch
    {
        static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            var addIndexMethod = typeof(PatchHelper)
                .GetMethod(nameof(PatchHelper.AddIndexesTo));

            foreach (var instruction in instructions)
            {
                yield return instruction;
                // place after, placing before isn't necessarily wrong but it is
                // bad practice
                if (instruction.opcode == OpCodes.Endfinally)
                {
                    yield return new CodeInstruction(OpCodes.Ldloc_3);
                    yield return new CodeInstruction(OpCodes.Call, addIndexMethod);
                }
            }
        }
    }
}