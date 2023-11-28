using System;
using Newtonsoft.Json;

namespace ArkReplay
{
    [JsonObject(MemberSerialization.OptIn)]
    public readonly struct InventoryRef
    {
        [JsonProperty("party_inventory")]
        public readonly bool partyInventory;
        [JsonProperty("hash")]
        public readonly int hash;

        public InventoryRef(InventoryManager inven)
        {
            if (inven == null)
                throw new ArgumentNullException("inventory cannot be null");

            if (Inventories.PartyInventory == inven)
            {
                partyInventory = true;
                hash = 0;
            }
            else
            {
                partyInventory = false;
                hash = inven.GetComponent<InventoryHash>().Hash;
            }
        }

        public InventoryManager GetInventory()
        {
            if (partyInventory)
                return Inventories.PartyInventory;
            else
                return Inventories.Get(hash);
        }

        public override string ToString()
        {
            if (partyInventory)
                return "Inventory(party)";
            else
                return $"Inventory(hash={hash})";
        }

        public static implicit operator InventoryDestination(InventoryRef re)
            => new InventoryDestination(re);
    }
}