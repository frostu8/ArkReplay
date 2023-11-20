using UnityEngine;

namespace ArkReplay.Replay
{
    /// <summary>
    /// Seeds runs.
    /// </summary>
    public static class RunSeeder
    {
        public static int Seed => PlayData.TSavedata.randomSave.seed;

        /// <summary>
        /// The seed of the next run. null to generate one normally.
        /// </summary>
        public static int? NextSeed { get; set; }

        public static RandomSave NewRandomSave()
        {
            var randomSave = new RandomSave();

            if (NextSeed is int nextSeed)
            {
                Debug.Log($"Seeding run with seed={nextSeed}");
                NextSeed = null;
                randomSave.SetRandom(nextSeed);
            }
            else
            {
                randomSave.SetRandom();
                Debug.Log($"Seeding new run with random seed={randomSave.seed}");
            }

            return randomSave;
        }
    }
}