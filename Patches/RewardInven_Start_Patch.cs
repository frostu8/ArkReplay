using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using ArkReplay.Replay;
using HarmonyLib;
using UnityEngine;
using UnityEngine.UI;

namespace ArkReplay.Patches
{
    [HarmonyPatch(typeof(RewardInven), nameof(RewardInven.Start))]
    static class RewardInven_Start_Patch
    {
        static void Prefix(RewardInven __instance)
        {
            // find button
            var lootAllButton = FindLootAllButton(__instance);

            if (lootAllButton != null)
            {
                lootAllButton.onClick.AddListener(delegate
                {
                    if (!RunRecorder.Recording) return;
                    RunRecorder recorder = RunRecorder.Instance;

                    recorder.Record(new ActionTakeAllItems(new InventoryRef(__instance.MyInven)));
                });
            }
        }

        private static Button FindLootAllButton(RewardInven obj)
        {
            foreach (ButtonHighliteV2 hiButton in obj.Buttons)
            {
                var button = hiButton.MyButton;

                for (int i = 0; i < button.onClick.GetPersistentEventCount(); i++)
                {
                    // find one with toInventory
                    var methodName = button.onClick.GetPersistentMethodName(i);

                    if (methodName == "ToInventory")
                    {
                        // this is our button!
                        return button;
                    }
                }
            }

            return null;
        }
    }
}