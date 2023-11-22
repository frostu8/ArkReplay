using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using ArkReplay.Json;
using ArkReplay.Replay.Battle;
using Newtonsoft.Json;
using ReflectionExtensions;

namespace ArkReplay.Replay
{
    /// <summary>
    /// A serializable action.
    /// </summary>
    [JsonConverter(typeof(JsonTaggedConverter))]
    public class Action : IJsonTagged
    {
        public IAction action;

        public Action()
        { }

        public Action(IAction action)
        {
            this.action = action;
        }

        public void Replay() => this.action.Replay();

        public bool Ready() => this.action.Ready();

        public override string ToString()
        {
            return this.action?.ToString() ?? "action=null";
        }

        public void SetTagged(object value)
        {
            action = (IAction) value;
        }

        public object GetTagged()
        {
            return action;
        }

        internal static bool CampUIReady()
        {
            return CampSingleton.ActiveCamp.UIObject != null;
        }

        internal static bool FieldReady()
        {
            return FieldSystem.instance.Playercontrol.enabled;
        }

        internal static bool BattleReady()
        {
            // battle actions can only happen in battle
            if (BattleSystem.instance == null)
                return false;

            return !BattleSystem.instance.DelayWait
                && BattleSystem.instance.ActWindow.On;
        }
    }
}
