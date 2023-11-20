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

        public bool Ready()
        {
            return Action.BattleReady();
        }

        public override string ToString()
        {
            return "Turn end";
        }
    }
}