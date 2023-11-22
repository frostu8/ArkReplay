namespace ArkReplay
{
    /// <summary>
    /// Singleton for <see cref="Camp"/>.
    /// </summary>
    public class CampSingleton
    {
        internal static Camp instance;

        public static Camp ActiveCamp { get => instance; }    
    }
}