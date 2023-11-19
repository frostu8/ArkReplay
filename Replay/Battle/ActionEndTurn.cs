namespace ArkReplay.Replay.Battle
{
    public class ActionEndTurn : IAction
    {
        public ActionEndTurn()
        { }

        public void Replay()
        {
            BattleSystem.instance.TurnEnd();
        }

        public override string ToString()
        {
            return "Turn end";
        }
    }
}