using ArkReplay.Json;
using Newtonsoft.Json;

namespace ArkReplay.Replay
{
    /// <summary>
    /// Use soulstones to upgrade a party member.
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    [JsonTag(typeof(Action))]
    public class ActionUpgradePartyMember : IAction
    {
        [JsonProperty("index")]
        public int memberIndex;

        public ActionUpgradePartyMember(int memberIndex)
        {
            this.memberIndex = memberIndex;
        }

        public void Replay()
        {
            var charStats = UIManager.inst.CharstatUI.GetComponent<CharStatV4>();

            charStats.Init();

            charStats.CharInfos[memberIndex].Upgrade(false);
        }

        public bool Ready()
        {
            return Action.FieldReady();
        }

        public override string ToString()
        {
            return $"Level up party member at #{memberIndex}";
        }
    }
}