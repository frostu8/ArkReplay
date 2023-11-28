using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace ArkReplay
{
    [JsonObject(MemberSerialization.OptIn)]
    public readonly struct InventoryRef
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public enum Kind
        {
            Party,
            Equip,
            General
        }

        [JsonProperty("kind")]
        public readonly Kind kind;
        [JsonProperty("hash")]
        public readonly int hash;

        public InventoryRef(InventoryManager inven)
        {
            if (inven == null)
                throw new ArgumentNullException("inventory cannot be null");

            if (Inventories.PartyInventory == inven)
            {
                kind = Kind.Party;
                hash = 0;
            }
            else if (inven is CharEquipInven equipInv)
            {
                kind = Kind.Equip;
                hash = PlayData.TSavedata.Party.IndexOf(equipInv.Info);
            }
            else
            {
                kind = Kind.General;
                hash = inven.GetComponent<InventoryHash>().Hash;
            }
        }

        public InventoryManager GetInventory()
        {
            switch (kind)
            {
                case Kind.Party:
                    return PartyInventory.InvenM;
                case Kind.Equip:
                    return PlayData.CharEquips[hash];
                case Kind.General:
                    return Inventories.Get(hash);
                default:
                    throw new Exception("unreachable");
            }
        }

        public override string ToString()
        {
            switch (kind)
            {
                case Kind.Party:
                    return "Inventory(party)";
                case Kind.Equip:
                    return $"Inventory(equip, index={hash})";
                case Kind.General:
                    return $"Inventory(hash={hash})";
                default:
                    throw new Exception("unreachable");
            }
        }

        public static implicit operator InventoryDestination(InventoryRef re)
            => new InventoryDestination(re);
    }
}