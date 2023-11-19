namespace ArkReplay.Replay
{
    public class ActionApUpgrade : IAction
    {
        public void Replay()
        {
            var charStats = UIManager.inst.CharstatUI.GetComponent<CharStatV4>();
            charStats.MPUpgrade();
        }

        public override string ToString()
        {
            return $"AP upgrade";
        }
    }
}