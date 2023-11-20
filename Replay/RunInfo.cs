using Newtonsoft.Json;
using UnityEngine;

namespace ArkReplay.Replay
{
    /// <summary>
    /// Global information about the start of the run.
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public struct RunInfo
    {
        /// <summary>
        /// The game version of the run.
        /// </summary>
        [JsonProperty]
        public string version;

        /// <summary>
        /// The difficulty of the run.
        /// </summary>
        [JsonProperty]
        public Difficulty difficulty;

        /// <summary>
        /// The loop number of the run. Mostly for flair.
        /// </summary>
        [JsonProperty]
        public int loopNum;

        /// <summary>
        /// The seed of the run. Use <see cref="RandomSave.seed"/> to set the
        /// seed.
        /// </summary>
        [JsonProperty]
        public int seed;

        public static RunInfo ThisRun()
        {
            return new RunInfo
            {
                version = Application.version,
                seed = RunSeeder.Seed,
                loopNum = SaveManager.NowData.LoopNum,
                difficulty = GetDifficulty(),
            };
        }

        private static Difficulty GetDifficulty()
        {
            if (SaveManager.NowData.GameOptions.Difficulty == 2)
            {
                return Difficulty.Expert;
            }
            else
            {
                return Difficulty.Normal;
            }
        }
    }
}