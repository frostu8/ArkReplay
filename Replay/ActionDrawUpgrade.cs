namespace ArkReplay.Replay
{
    public class ActionDrawUpgrade : IAction
    {
        public void Replay()
        {
            var charStats = UIManager.inst.CharstatUI.GetComponent<CharStatV4>();
            charStats.DrawUpgrade();
        }

        public bool Ready()
        {
            return Action.FieldReady();
        }

        public override string ToString()
        {
            return $"Draw upgrade";
        }
    }
}