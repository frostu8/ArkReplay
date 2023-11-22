using System;
using ArkReplay.Json;
using Newtonsoft.Json;

namespace ArkReplay.Replay
{
    [JsonTag(typeof(Action))]
    [JsonObject(MemberSerialization.OptIn)]
    public class ActionEnableCamp : IAction
    {
        public ActionEnableCamp()
        { }

        public void Replay()
        {
            CampSingleton.ActiveCamp.CampEnable();
        }

        public bool Ready()
        {
            return Action.FieldReady();
        }

        public override string ToString()
        {
            return "Camp enabled";
        }
    }
}