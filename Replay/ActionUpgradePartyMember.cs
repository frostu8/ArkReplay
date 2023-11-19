namespace ArkReplay.Replay
{
    /// <summary>
    /// Use soulstones to upgrade a party member.
    /// </summary>
    public class ActionUpgradePartyMember : IAction
    {
        private int memberIndex;

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

        public override string ToString()
        {
            return $"Level up party member at #{memberIndex}";
        }
    }
}