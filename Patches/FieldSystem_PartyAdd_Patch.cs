using System;
using ArkReplay.Replay;
using GameDataEditor;
using HarmonyLib;

namespace ArkReplay.Patches
{
    [HarmonyPatch(
        typeof(FieldSystem),
        nameof(FieldSystem.PartyAdd),
        new Type[] {typeof(GDECharacterData), typeof(int)}
    )]
    static class FieldSystem_PartyAdd_Patch
    {
        static void Prefix(GDECharacterData CData, int Levelup)
        {
            if (!RunRecorder.Recording) return;
            RunRecorder recorder = RunRecorder.Instance;

            recorder.Record(new ActionAddPartyMember(CData.Key, Levelup));
        }
    }
}