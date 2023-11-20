using System.Collections;
using Newtonsoft.Json;

namespace ArkReplay.Replay
{
    /// <summary>
    /// Generated when the player moves on to the camp.
    /// <desc>
    /// This can either be when the player walks through the mini boss door or
    /// selects "Move to Next Stage"
    /// </desc>
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class ActionNextCamp : IAction
    {
        public void Replay()
        {
            NextStage();
        }

        public bool Ready()
        {
            return Action.FieldReady();
        }

        public override string ToString()
        {
            return "Move to next camp";
        }

        private IEnumerable NextStage()
        {
            yield return UIManager.inst.StartCoroutine(UIManager.inst.FadeBlack_Out());
			FieldSystem.instance.NextStage();
			yield return UIManager.inst.StartCoroutine(UIManager.inst.FadeBlack_In());
        }
    }
}