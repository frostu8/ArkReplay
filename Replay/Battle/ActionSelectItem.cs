using System;
using Newtonsoft.Json;

namespace ArkReplay.Replay.Battle
{
    /// <summary>
    /// Selects an item from the inventory.
    /// </summary>
    public class ActionSelectItem : IAction
    {
        [JsonProperty("index")]
        private int inventoryIndex;

        public ActionSelectItem(int inventoryIndex)
        {
            this.inventoryIndex = inventoryIndex;
        }

        public void Replay()
        {
            throw new NotImplementedException();
        }

        public bool Ready()
        {
            return Action.BattleReady();
        }

        public override string ToString()
        {
            return $"Select item @ index {inventoryIndex}";
        }
    }
}