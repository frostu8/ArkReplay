using System.Collections.Generic;
using System.Reflection.Emit;
using ArkReplay.Replay;
using HarmonyLib;

namespace ArkReplay.Patches
{
    [HarmonyPatch(typeof(PlayData), nameof(PlayData.init))]
    static class PlayData_init_Patch
    {
        static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            var constructor = AccessTools.Constructor(
                typeof(RandomSave),
                // empty constructor
                new System.Type[] {}
            );
            var newRandomMethod = AccessTools.Method(
                typeof(RunSeeder),
                nameof(RunSeeder.NewRandomSave)
            );

            foreach (var instruction in instructions)
            {
                // ldsfld class TempSaveData PlayData::TSavedata
                // -newobj instance void RandomSave::.ctor()
                // +call class RandomSave RunSeeder::NewRandomSave()
                // stfld class RandomSave TempSaveData::randomSave
                if (instruction.Is(OpCodes.Newobj, constructor))
                    yield return new CodeInstruction(OpCodes.Call, newRandomMethod);
                else
                    yield return instruction;
            }
        }
    }
}