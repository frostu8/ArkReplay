using System.Collections.Generic;
using ArkReplay.Replay.Battle;
using Newtonsoft.Json;

namespace ArkReplay.Replay
{
    /// <summary>
    /// A serializable run record.
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class RunRecord
    {
        [JsonProperty]
        public int seed;

        [JsonProperty]
        public List<Action> actions;

        public RunRecord(int seed)
        {
            this.seed = seed;
            actions = new List<Action>();
        }
    }
}