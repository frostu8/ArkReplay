namespace ArkReplay.Replay
{
    public class ActionApUpgrade : IAction
    {
        public void Replay()
        {
            var charStats = UIManager.inst.CharstatUI.GetComponent<CharStatV4>();
            charStats.MPUpgrade();
        }

        public bool Ready()
        {
            return Action.FieldReady();
        }

        public override string ToString()
        {
            return $"AP upgrade";
        }
    }
}