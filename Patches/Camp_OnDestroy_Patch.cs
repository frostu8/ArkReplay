using HarmonyLib;
using UnityEngine;

namespace ArkReplay.Patches
{
    // By some ungodly miracle, the Camp has a defined OnDestroy function. We
    // don't need to do any Unity insanity to make this work.
    [HarmonyPatch(typeof(Camp), nameof(Camp.OnDestroy))]
    static class Camp_OnDestroy_Patch
    {
        static void Prefix(Camp __instance)
        {
            if (CampSingleton.instance == __instance)
                CampSingleton.instance = null;
        }
    }
}