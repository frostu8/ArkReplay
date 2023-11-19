using System.Linq;
using ArkReplay.Replay;
using HarmonyLib;

namespace ArkReplay.Patches
{
    [HarmonyPatch(typeof(CharStatV4), nameof(CharStatV4.ToBasicSkill))]
    static class CharStatV4_ToBasicSkill_Patch
    {
        static void Prefix(SkillButton Mybutton)
        {
            if (!RunRecorder.Recording) return;
            RunRecorder recorder = RunRecorder.Instance;

            int partyIndex = PlayData
                .TSavedata
                .Party
                .IndexOf(Mybutton.CharData.Info);

            int index = Mybutton
                .CharData
                .Info
                .SkillDatas
                .IndexOf(Mybutton.Myskill.CharinfoSkilldata);

            var action = new ActionChangeFixedSkill(partyIndex, index);

            recorder.Record(action);
        }
    }
}