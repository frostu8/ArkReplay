using ArkReplay.Json;
using ArkReplay.Patches;
using Newtonsoft.Json;
using UnityEngine;

namespace ArkReplay.Replay
{
    /// <summary>
    /// Selects a skill from a select skill list.
    /// </summary>
    [JsonTag(typeof(Action))]
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

            // find skill button
            var list = UIManager.NowActiveUI.gameObject.GetComponent<SelectSkillList>();

            SelectSkillListIndex[] buttons = list
                .Align
                .GetComponentsInChildren<SelectSkillListIndex>();

            foreach (var button in buttons)
            {
                if (button.index == buttonIndex)
                {
                    button.GetComponent<SkillButton>().Click();
                    return; // finished successfully
                }
            }

            throw new UnexpectedStateException(
                "Skill selection is desynced; trying to select skill at "
                + $"index {buttonIndex} but it is out of bounds or does not "
                + "exist."
            );
        }

        public bool Ready()
        {
            // check if skill list is active
            var activeUI = UIManager.NowActiveUI;

            if (activeUI != null)
            {
                var list = activeUI.GetComponent<SelectSkillList>();
                return list != null;
            }
            else
            {
                return false;
            }
        }

        public override string ToString()
        {
            return $"Select skill from skill list #{buttonIndex}";
        }
    }
}