using System.Collections.Generic;
using GameDataEditor;
using Newtonsoft.Json;

namespace ArkReplay.Replay
{
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

        public override string ToString()
        {
            return $"Add party member \"{characterKey}\" at level {level}";
        }
    }
}