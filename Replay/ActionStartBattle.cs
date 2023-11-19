using System.Collections.Generic;
using Newtonsoft.Json;

namespace ArkReplay.Replay
{
    /// <summary>
    /// A single serializable battle record.
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class ActionStartBattle : IAction
    {
        [JsonProperty]
        public int seed;

        [JsonProperty]
        public string queueKey;

        [JsonProperty]
        public bool isCursed;

        public ActionStartBattle(int seed, string queueKey, bool isCursed)
        {
            this.seed = seed;
            this.queueKey = queueKey;
            this.isCursed = isCursed;
        }

        public void Replay()
        {
            throw new System.NotImplementedException();
        }

        public override string ToString()
        {
            return $"Start battle, with queue=\"{queueKey}\", seed={seed}, cursed={isCursed}";
        }
    }
}