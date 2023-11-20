using ArkReplay.Replay;
using HarmonyLib;

namespace ArkReplay.Patches
{
    [HarmonyPatch(typeof(FieldSystem), nameof(FieldSystem._ReturnArkbutton))]
    static class FieldSystem__ReturnArkbutton_Patch
    {
        static void Prefix(bool clear)
        {
            if (!RunRecorder.Recording) return;

            if (FieldSystem.instance.IsClear() || clear)
            {
                // finish record
                RunRecorder.Instance.Save();
            }
            else
            {
                // move next camp
                RunRecorder.Instance.Record(new ActionNextCamp());
            }
        }
    }
}