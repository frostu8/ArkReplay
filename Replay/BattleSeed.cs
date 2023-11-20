namespace ArkReplay.Replay
{
    /// <summary>
    /// Seeds battles.
    /// </summary>
    public static class BattleSeed
    {
        /// <summary>
        /// The seed of the next battle. <code>null</code> to generate one
        /// normally.
        /// </summary>
        public static int? NextSeed { get; set; }

        public static RandomClass GetBattleRandom()
        {
            if (NextSeed is int nextSeed)
            {
                NextSeed = null;
                return new RandomClass
                {
                    seed = nextSeed,
                };
            }
            else
            {
                // generate normally
                return RandomSave.Instance.getRandomClass(RandomClassKey.InBattle);
            }
        }
    }
}