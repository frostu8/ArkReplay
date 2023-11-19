using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using ArkReplay.Replay.Battle;
using GameDataEditor;
using HarmonyLib;
using UnityEngine;

namespace ArkReplay.Patches
{
    [HarmonyPatch(typeof(WaitButton), nameof(WaitButton.WaitAct))]
    class WaitButton_WaitAct_Patch
    {
        static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            var interestMethod = typeof(MonoBehaviour)
                .GetMethod(nameof(MonoBehaviour.StartCoroutine),
                    new Type[] {typeof(IEnumerator)});
            var recordWaitMethod = typeof(PatchHelper)
                .GetMethod(nameof(PatchHelper.RecordWait));

            foreach (var instruction in instructions)
            {
                yield return instruction;
                // place after, placing before isn't necessarily wrong but it is
                // bad practice
                if (instruction.Calls(interestMethod))
                    yield return new CodeInstruction(OpCodes.Call, recordWaitMethod);
            }
        }
    }
}