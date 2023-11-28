using System.Collections.Generic;
using ArkReplay.Replay;
using ArkReplay.Replay.Battle;
using UnityEngine;

namespace ArkReplay.Patches
{
    /// <summary>
    /// Methods to help patches do stuff.
    /// <desc>
    /// These shouldn't be used.
    /// </desc>
    /// </summary>
    public static class PatchHelper
    {
        public static void RecordedToInventory(InventoryManager mgr)
        {
            if (RunRecorder.Recording)
            {
                var action = new ActionTakeAllItems(new InventoryRef(mgr));

                RunRecorder.Instance.Record(action);
            }

            mgr.ToInventory();
        }

        public static void RecordWait()
        {
            if (!RunRecorder.Recording) return;
            RunRecorder recorder = RunRecorder.Instance;

            var action = new ActionStandby();

            recorder.Record(action);
        }

        public static void RecordItemSelect(ItemObject item)
        {
            if (!RunRecorder.Recording) return;
            RunRecorder recorder = RunRecorder.Instance;

            int index = item.MySlot.Number;

            recorder.Record(new ActionSelectItem(index));
        }

        public static void AddIndexesTo(List<GameObject> objects)
        {
            for (int i = 0; i < objects.Count; i++)
            {
                var go = objects[i];
                var goButton = go.GetComponent<SkillButtonMain>().SkillbuttonBig;
                var indexer = goButton.gameObject.AddComponent<SelectSkillListIndex>();

                indexer.index = i;
            }
        }
    }
}