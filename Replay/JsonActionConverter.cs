using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using ArkReplay.Replay.Battle;

namespace ArkReplay.Replay
{
    class JsonActionConverter : JsonConverter
    {
        private static Dictionary<string, Type> actionTypes = new Dictionary<string, Type>()
        {
            { "CancelSkill", typeof(ActionCancelSkill) },
            { "EndTurn", typeof(ActionEndTurn) },
            { "SelectBasicSkill", typeof(ActionSelectBasicSkill) },
            { "SelectFromList", typeof(ActionSelectFromList) },
            { "SelectSkill", typeof(ActionSelectSkill) },
            { "TargetSingle", typeof(ActionTargetSingle) },
            { "Exchange", typeof(ActionExchange) },
            { "Standby", typeof(ActionStandby) },
            { "SelectItem", typeof(ActionSelectItem) },
            { "StartBattle", typeof(ActionStartBattle) },
            { "AddPartyMember", typeof(ActionAddPartyMember) },
            { "UpgradePartyMember", typeof(ActionUpgradePartyMember) },
            { "DrawUpgrade", typeof(ActionDrawUpgrade) },
            { "ApUpgrade", typeof(ActionApUpgrade) },
            { "ChangeFixedSkill", typeof(ActionChangeFixedSkill) },
        };

        public override bool CanConvert(Type objectType)
        {
            return typeof(Action).IsAssignableFrom(objectType);
        }

        public override object ReadJson(
            JsonReader reader,
            Type objectType,
            object existingValue,
            JsonSerializer serializer)
        {
            if (!reader.Read() || reader.TokenType != JsonToken.StartObject)
                throw new JsonException("expected object");

            if (!reader.Read() || reader.TokenType != JsonToken.PropertyName)
                throw new JsonException("expected tagged field");

            // get action type name
            string name = (string) reader.Value;

            if (!actionTypes.ContainsKey(name))
                throw new JsonException("expected valid action name");

            // get value (this is infallible)
            reader.Read();

            // deserialize
            IAction action = (IAction) serializer
                .Deserialize(reader, actionTypes[name]);

            return new Action(action);
        }

        public override void WriteJson(
            JsonWriter writer,
            object value,
            JsonSerializer serializer)
        {
            Action action = (Action) value;

            Type actionType = action.action.GetType();

            // find name
            string name = actionTypes
                .Where(p => p.Value == actionType)
                .Select(p => p.Key)
                .First();

            if (name == null) {
                throw new JsonException("serializing a type that isn't "
                    + "registered with the json converter");
            }

            writer.WriteStartObject();
            writer.WritePropertyName(name);

            serializer.Serialize(writer, action.action);

            writer.WriteEndObject();
        }
    }
}