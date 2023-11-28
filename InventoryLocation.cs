using Newtonsoft.Json;

namespace ArkReplay
{
    [JsonObject(MemberSerialization.OptIn)]
    public readonly struct InventoryLocation
    {
        [JsonProperty]
        public readonly int index;
        [JsonProperty]
        public readonly InventoryRef inventory;

        public InventoryLocation(int index, InventoryRef inventory)
        {
            this.index = index;
            this.inventory = inventory;
        }

        public InventoryLocation(ItemSlot slot)
        {
            index = slot.Number;
            inventory = new InventoryRef(slot.MyManager);
        }

        public InventoryLocation(ItemBase item)
        {
            inventory = new InventoryRef(item.MyManager);
            index = item.MyManager.InventoryItems.IndexOf(item);
        }

        public static implicit operator InventoryDestination(InventoryLocation loc)
            => new InventoryDestination(loc.inventory, loc.index);

        public ItemSlot GetSlot()
        {
            var inventory = this.inventory.GetInventory();
            
            if (inventory == null)
                return null;
            
            return inventory.Align.transform.GetChild(index).GetComponent<ItemSlot>();
        }

        public override string ToString()
        {
            return $"InventoryLocation(index={index}, inventory={inventory})";
        }
    }
}