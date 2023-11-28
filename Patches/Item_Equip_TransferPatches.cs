using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using ArkReplay.Replay;
using HarmonyLib;
using UnityEngine;
using UnityEngine.Events;

namespace ArkReplay.Patches
{
    [HarmonyPatch]
    static class Item_Equip_TransferPatches
    {
        static IEnumerable<MethodBase> TargetMethods()
        {
            yield return AccessTools.Method(typeof(Item_Equip), nameof(Item_Equip.RightClick));
            yield return AccessTools.Method(typeof(Item_Equip), nameof(Item_Equip.PadClick));
        }

        static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            var addNewItemMethod = AccessTools.Method(
                typeof(InventoryManager),
                nameof(InventoryManager.AddNewItem),
                new System.Type[] { typeof(ItemBase) }
            );
            var initButtonsMethod = AccessTools.Method(
                typeof(SelectButtons),
                nameof(SelectButtons.Init)
            );

            foreach (CodeInstruction instruction in instructions)
            {
                if (instruction.Calls(addNewItemMethod))
                {
                    yield return AddNewItem();
                }
                else if (instruction.Calls(initButtonsMethod))
                {
                    yield return new CodeInstruction(OpCodes.Ldarg_0);
                    yield return ModifyButtonsList();
                }
                else
                {
                    yield return instruction;
                }
            }
        }

        private static CodeInstruction ModifyButtonsList()
        {
            return Transpilers.EmitDelegate(
                (SelectButtons buttons, List<ButtonData> datas, ItemBase item) => 
                {
                    for (int i = 0; i < PlayData.CharEquips.Count; i++)
                    {
                        CharEquipInven equipInven = PlayData.CharEquips[i];
                        UnityAction oldAction = datas[i].call;
                        InventoryManager manager = item.MyManager;

                        // sadly, there is no ergonomic way of doing this
                        // becaus delegate names are not guranateed by the
                        // compiler
                        datas[i].call = delegate
                        {
                            if (!RunRecorder.Recording)
                            {
                                // delegate to old delegate
                                oldAction.Invoke();
                                return;
                            }

                            // create transfer
                            var action = new ActionMoveItem(
                                new InventoryLocation(item),
                                new InventoryRef(equipInven)
                            );

                            if (equipInven.AddNewItem(item))
                            {
                                manager.DelItem(item);
                                RunRecorder.Instance.Record(action);
                            }
                            else
                            {
                                // update action with index 0
                                action.to = new InventoryLocation(
                                    0,
                                    new InventoryRef(equipInven)
                                );

                                if ((equipInven.InventoryItems[0] as Item_Equip).IsCurse)
                                {
                                    return;
                                }
                                ItemBase item = equipInven.InventoryItems[0];
                                equipInven.DelItem(0);
                                equipInven.AddNewItem(item);
                                manager.DelItem(item);
                                manager.AddNewItem(item);
                            }
                            
                            VeryBadShamelessILCopyToFixInventoryNonsense();
                        };
                    }

                    // finally do button stuff!
                    buttons.Init(datas);
                }
            );
        }

        /// <summary>
        /// This code is in a delegate, so fishing it out with a reverse patch
        /// is excessively difficult and is not guaranteed by the compiler.
        /// </summary>
        private static void VeryBadShamelessILCopyToFixInventoryNonsense()
        {
            GameObject[] array2 = GameObject.FindGameObjectsWithTag("MainCharEquipInven");
            foreach (GameObject gameObject3 in array2)
            {
                if (gameObject3.GetComponent<CharEquipInven>().Info != null)
                {
                    gameObject3.GetComponent<CharEquipInven>().CreateInven(gameObject3.GetComponent<CharEquipInven>().Info.Equip);
                }
            }
            if (FieldSystem.instance != null)
            {
                foreach (AllyWindow item3 in FieldSystem.instance.PartyWindow)
                {
                    item3.EquipUpdate();
                }
            }
        }

        private static CodeInstruction AddNewItem()
        {
            return Transpilers.EmitDelegate((InventoryManager inv, ItemBase item) => {
                // create transfer
                var action = new ActionMoveItem(
                    new InventoryLocation(item),
                    new InventoryRef(inv)
                );

                // try add item
                bool res = inv.AddNewItem(item);

                if (res && RunRecorder.Recording)
                {
                    RunRecorder.Instance.Record(action);
                }

                return res;
            });
        }
    }
}