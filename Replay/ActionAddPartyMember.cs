using System.Collections.Generic;
using ArkReplay.Json;
using GameDataEditor;
using Newtonsoft.Json;

namespace ArkReplay.Replay
{
    [JsonTag(typeof(Action))]
    public class ActionAddPartyMember : IAction
    {
        [JsonProperty("key")]
        public string characterKey;

        [JsonProperty("level")]
        public int level;

        public ActionAddPartyMember(string characterKey, int level)
        {
            this.characterKey = characterKey;
            this.level = level;
        }

        public void Replay()
        {
            var character = new GDECharacterData(characterKey);

            FieldSystem.PartyAdd(character, level);
			PlayData.TSavedata.NowMaxMemberNum++;
        }

        public bool Ready()
        {
            // this is always ready because it can happen at the Ark
            return true;
        }

        public override string ToString()
        {
            return $"Add party member \"{characterKey}\" at level {level}";
        }
    }
}