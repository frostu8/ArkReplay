using HarmonyLib;

namespace ArkReplay.Patches
{
    [HarmonyPatch(typeof(FieldSystem), nameof(FieldSystem._ReturnArkbutton))]
    static class FieldSystem__ReturnArkbutton_Patch
    {
        static void Prefix()
        {

        }
    }
}