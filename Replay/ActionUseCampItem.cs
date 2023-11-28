using ArkReplay.Json;
using Newtonsoft.Json;
using UnityEngine;

namespace ArkReplay.Replay
{
    [JsonTag(typeof(Action))]
    [JsonObject(MemberSerialization.OptIn)]
    public class ActionUseCampItem : IAction
    {
        [JsonProperty("index")]
        public int inventoryIndex;

        private UI_Camp dummy;

        public ActionUseCampItem(int inventoryIndex)
        {
            this.inventoryIndex = inventoryIndex;
        }

        public void Replay()
        {
            InventoryManager inv = UIManager.inst.PartyInven;

            // get slot
            ItemSlot slot = inv.Align.transform.GetChild(inventoryIndex).GetComponent<ItemSlot>();

            // get itemobject
            ItemObject item = slot.GetItem();

            ReversePatch.UI_Camp_ItemUse(dummy, item);
        }

        public bool Ready()
        {
            if (Action.CampUIReady())
            {
                if (dummy == null)
                {
                    // setup dummy
                    dummy = new GameObject("UI_Camp_dummy").AddComponent<UI_Camp>();
                    dummy.enabled = false;
                    dummy.Main = CampSingleton.ActiveCamp.UIObject;
                }

                return true;
            }

            return false;
        }

        public override string ToString()
        {
            return $"Use camp item @ index {inventoryIndex}";
        }
    }
}