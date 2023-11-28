using Newtonsoft.Json;

namespace ArkReplay
{
    [JsonObject(MemberSerialization.OptIn)]
    public readonly struct InventoryDestination
    {
        [JsonProperty]
        public readonly int? index;
        [JsonProperty]
        public readonly InventoryRef inventory;

        public InventoryDestination(InventoryRef inventory)
        {
            this.inventory = inventory;
            this.index = null;
        }

        public InventoryDestination(InventoryRef inventory, int index)
        {
            this.inventory = inventory;
            this.index = index;
        }

        public bool TryGetLocation(out InventoryLocation loc)
        {
            if (index is int newIndex)
            {
                loc = new InventoryLocation(newIndex, inventory);
                return true;
            }
            else
            {
                loc = default;
                return false;
            }
        }

        public override string ToString()
        {
            if (index is int newIndex)
                return $"InventoryDestination(index={newIndex}, inventory={inventory})";
            else
                return $"InventoryDestination(inventory={inventory})";
        }
    }
}