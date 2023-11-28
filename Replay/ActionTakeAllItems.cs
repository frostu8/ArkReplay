using ArkReplay.Json;
using Newtonsoft.Json;

namespace ArkReplay.Replay
{
    [JsonTag(typeof(Action))]
    [JsonObject(MemberSerialization.OptIn)]
    public class ActionTakeAllItems : IAction
    {
        public InventoryRef inventory;

        public ActionTakeAllItems(InventoryRef inventory)
        {
            this.inventory = inventory;
        }

        public void Replay()
        {
            inventory.GetInventory().ToInventory();
        }

        public bool Ready()
        {
            return inventory.GetInventory() != null;
        }

        public override string ToString()
        {
            return "Take all items";
        }
    }
}