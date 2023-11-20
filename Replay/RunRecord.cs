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
        public RunInfo info;

        [JsonProperty]
        public List<Action> actions;

        public RunRecord(RunInfo info, List<Action> actions)
        {
            this.info = info;
            this.actions = actions;
        }
    }
}