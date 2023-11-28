
using System;
using ArkReplay.Json;
using Newtonsoft.Json;
using UnityEngine;
using Object = UnityEngine.Object;

namespace ArkReplay.Replay
{
    [JsonTag(typeof(Action))]
    [JsonObject(MemberSerialization.OptIn)]
    public class ActionMoveItem : IAction
    {
        [JsonProperty]
        public InventoryLocation from;
        [JsonProperty]
        public InventoryDestination to;

        public ActionMoveItem(InventoryLocation from, InventoryDestination to)
        {
            this.from = from;
            this.to = to;
        }

        public void Replay()
        {
            // find slots
            ItemSlot fromSlot = from.GetSlot();
            ItemBase fromItem = fromSlot.MyManager.InventoryItems[fromSlot.Number];

            // first take away item
            fromSlot.MyManager.InventoryItems[fromSlot.Number] = null;
            fromSlot.MyManager.ItemUpdateFromInven();

            bool success = AddItemTo(to, fromItem, out ItemBase replaced);

            if (success && replaced != null)
            {
                fromSlot.MyManager.InventoryItems[fromSlot.Number] = replaced;
                fromSlot.MyManager.ItemUpdateFromInven();
            }
        }

        private bool AddItemTo(InventoryDestination dst, ItemBase item, out ItemBase replaced)
        {
            if (to.TryGetLocation(out InventoryLocation loc))
            {
                // try to input item
                ItemSlot toSlot = loc.GetSlot();

                var replacedItem = toSlot.MyManager.InventoryItems[toSlot.Number];

                if (!toSlot.ItemInput(item))
                    replaced = replacedItem;
                else
                    replaced = null;

                return true;
            }
            else
            {
                replaced = null;
                return dst.inventory.GetInventory().AddNewItem(item);
            }
        }

        public bool Ready()
        {
            // we don't know if this will run in a battle or field context, so
            return from.inventory.GetInventory() != null
                && to.inventory.GetInventory() != null;
        }

        public override string ToString()
        {
            return $"Move item {from} to {to}";
        }
    }
}