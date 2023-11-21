using ArkReplay.Json;

namespace ArkReplay.Replay.Battle
{
    /// <summary>
    /// Cancels a skill or targeting. Weird edge case because using ESC or right
    /// click to cancel skills uses a slightly different routine.
    /// </summary>
    [JsonTag(typeof(Action))]
    public class ActionCancelSkill : IAction
    {
        public ActionCancelSkill()
        { }

        public void Replay()
        {
            BattleSystem.instance.TargetSelectCancel();
        }

        public bool Ready()
        {
            return Action.BattleReady();
        }

        public override string ToString()
        {
            return "Cancel skill selection";
        }
    }
}