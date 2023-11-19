namespace ArkReplay.Replay.Battle
{
    public class ActionStandby : IAction
    {
        public ActionStandby()
        { }

        public void Replay()
        {
            BattleSystem.instance.Wait();
        }

        public override string ToString()
        {
            return "Standby";
        }
    }
}