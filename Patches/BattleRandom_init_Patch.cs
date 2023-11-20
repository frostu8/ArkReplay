using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection.Emit;
using ArkReplay.Replay;
using HarmonyLib;

namespace ArkReplay.Patches
{
    [HarmonyPatch(typeof(BattleRandom), nameof(BattleRandom.init))]
    static class BattleRandom_init_Patch
    {
        static void Postfix()
        {
            if (!RunRecorder.Recording) return;
            RunRecorder recorder = RunRecorder.Instance;

            // create new battle
            var action = new ActionStartBattle(BattleRandom.BaseRandomClass.seed, PlayData.BattleQueue, PlayData.BattleCurse);
            recorder.Record(action);
        }

/*
        static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            var interestMethod = AccessTools.PropertyGetter(
                typeof(RandomSave),
                nameof(RandomSave.Instance)
            );
            var getRandomMethod = AccessTools.Method(
                typeof(BattleSeed),
                nameof(BattleSeed.GetBattleRandom)
            );

            int skipCounter = 0;

            foreach (var instruction in instructions)
            {
                // -call class RandomSave RandomSave::get_Instance()
                // -ldsfld string RandomClassKey::InBattle
                // -callvirt instance class RandomClass RandomSave::getRandomClass(string)
                // -call class RandomClass RandomClass::CreateRandomClass(class RandomClass)
                // +call class RandomClass BattleSeed::GetBattleRandom()
                //  stsfld class RandomClass BattleRandom::BaseRandomClass
                if (instruction.Calls(interestMethod))
                {
                    skipCounter = 4;
                    yield return new CodeInstruction(OpCodes.Call, getRandomMethod);
                }

                if (skipCounter > 0)
                {
                    skipCounter -= 1;
                }
                else
                {
                    yield return instruction;
                }
            }
        }
        */
    }
}