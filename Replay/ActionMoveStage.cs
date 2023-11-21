using System.Collections;
using GameDataEditor;
using DarkTonic.MasterAudio;
using Newtonsoft.Json;
using UnityEngine;
using ArkReplay.Json;

namespace ArkReplay.Replay
{
    [JsonTag(typeof(Action))]
    [JsonObject(MemberSerialization.OptIn)]
    public class ActionMoveStage : IAction
    {
        [JsonProperty("key")]
        public string stageKey;

        public ActionMoveStage(string stageKey)
        {
            this.stageKey = stageKey;
        }

        public void Replay()
        {
            // disable player interaction so steps do not run early
            FieldSystem.instance.Playercontrol.enabled = false;
            RunReplayer.Instance.StartCoroutine(FadeNextStage(stageKey));
        }

        public bool Ready()
        {
            return Action.FieldReady();
        }

        public override string ToString()
        {
            return $"Move to next stage \"{stageKey}\"";
        }

        // this should be done with a reverse patch but IEnumerator functions
        // are peculiar in IL
        private static IEnumerator FadeNextStage(string stageKey)
        {
            yield return UIManager.inst.StartCoroutine(UIManager.inst.FadeSquare_Out());
            MasterAudio.FadeOutAllOfSound("Campfire", 1f);
            MasterAudio.FadeOutAllOfSound("CampAmbi", 1f);
            yield return new WaitForSecondsRealtime(1f);

            FieldSystem.instance.StageStart(stageKey);
        }

        public static string NextStageKey
        {
            get => new GDEStageListData(GDEItemKeys.StageList_NomalStage)
                .Stages[PlayData.TSavedata.StageNum]
                .Key;
        }
    }
}