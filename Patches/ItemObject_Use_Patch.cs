using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using ArkReplay.Replay.Battle;
using HarmonyLib;

namespace ArkReplay.Patches
{
    [HarmonyPatch(typeof(ItemObject), nameof(ItemObject.Use))]
    static class ItemObject_Use_Patch
    {
        // In order to avoid gigantic, ugly conditionals, a transpiler is used.
        static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            var interestMethod = typeof(BattleSystem)
                .GetMethod(nameof(BattleSystem.TargetSelect),
                    new Type[] {typeof(Skill), typeof(BattleChar)});
            var recordMethod = typeof(PatchHelper)
                .GetMethod(nameof(PatchHelper.RecordItemSelect));

            foreach (var instruction in instructions)
            {
                yield return instruction;
                // place after, placing before isn't necessarily wrong but it is
                // bad practice
                if (instruction.Calls(interestMethod)) {
                    // ldarg.0
                    yield return new CodeInstruction(OpCodes.Ldarg_0);
                    // call ItemObject_Use_Patch.RecordItemSelect(this)
                    yield return new CodeInstruction(OpCodes.Call, recordMethod);
                }
            }
        }
    }
}