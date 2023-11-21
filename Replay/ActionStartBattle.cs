using System.Collections.Generic;
using ArkReplay.Json;
using GameDataEditor;
using Newtonsoft.Json;

namespace ArkReplay.Replay
{
    /// <summary>
    /// A single serializable battle record.
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    [JsonTag(typeof(Action))]
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
            // seed battle
            BattleSeed.NextSeed = seed;

            var queueData = new GDEEnemyQueueData(queueKey);
            var mapKey = StageSystem.instance.StageData.BattleMap.Key;
            FieldSystem.instance.BattleStart(
                queueData,
                mapKey,
                NomalBattle: true,
                Curse: isCursed
            );
        }

        public bool Ready()
        {
            return Action.FieldReady();
        }

        public override string ToString()
        {
            return $"Start battle, with queue=\"{queueKey}\", seed={seed}, cursed={isCursed}";
        }
    }
}