using System;
using HarmonyLib;

namespace ArkReplay.Patches
{
    [HarmonyPatch(typeof(FieldSystem))]
    internal static class FieldSystem_ReversePatches
    {
        [HarmonyReversePatch]
        [HarmonyPatch("IsClear", MethodType.Getter)]
        public static bool IsClear(FieldSystem _instance)
        {
            throw new NotImplementedException("stub");
        }
    }
}