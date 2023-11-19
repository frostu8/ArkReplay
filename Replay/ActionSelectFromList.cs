using ArkReplay.Patches;
using Newtonsoft.Json;
using UnityEngine;

namespace ArkReplay.Replay
{
    /// <summary>
    /// Selects a skill from a select skill list.
    /// </summary>
    public class ActionSelectFromList : IAction
    {
        [JsonProperty("index")]
        private int buttonIndex;

        public ActionSelectFromList(int buttonIndex)
        {
            this.buttonIndex = buttonIndex;
        }

        public void Replay()
        {
            if (SelectSkillList.ButtonList.Count <= 0)
                throw new UnexpectedStateException("Skill selection is empty or unopened.");

            Skill skill = SelectSkillList.ButtonList[buttonIndex];

            // find skill button
            var list = UIManager.NowActiveUI.gameObject.GetComponent<SelectSkillList>();

            SelectSkillListIndex[] buttons = list
                .Align
                .GetComponentsInChildren<SelectSkillListIndex>();

            foreach (var button in buttons)
            {
                if (button.index == buttonIndex)
                {
                    var skillButton = button.gameObject.GetComponent<SkillButtonMain>().SkillbuttonBig;

                    if (skillButton.Myskill == skill)
                        skillButton.Click();

                    return; // finished successfully
                }
            }

            throw new UnexpectedStateException(
                "Skill selection is desynced; trying to select skill at "
                + $"index {buttonIndex} but it is out of bounds or does not "
                + "exist."
            );
        }

        public override string ToString()
        {
            return $"Select skill from skill list #{buttonIndex}";
        }
    }
}