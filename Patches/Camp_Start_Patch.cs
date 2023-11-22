using HarmonyLib;
using UnityEngine;

namespace ArkReplay.Patches
{
    [HarmonyPatch(typeof(Camp), "Start")]
    static class Camp_Start_Patch
    {
        static void Prefix(Camp __instance)
        {
            if (CampSingleton.instance == null)
            {
                CampSingleton.instance = __instance;
            }
            else
            {
                Debug.LogWarning("Camp initialized twice.");
            }
        }
    }
}