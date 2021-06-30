using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using PhantomAbyssServer.Database.Models;

namespace PhantomAbyssServer.JsonConverters
{
    public class UserCurrencyConverter : JsonConverter<UserCurrency>
    {
        class JsonUserCurrency
        {
            public uint Essence { get; set; }
            public List<uint> DungeonKeys { get; set; }
        }
        
        public override void WriteJson(JsonWriter writer, UserCurrency value, JsonSerializer serializer)
        {
            serializer.Serialize(writer, new
            {
                dungeonKeys = value.DungeonKeys.OrderBy(d => d.Stage).Select(d => d.NumKeys),
                value.Essence,
            });
        }

        public override UserCurrency ReadJson(JsonReader reader, Type objectType, UserCurrency existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            var data = serializer.Deserialize<JsonUserCurrency>(reader);

            if (data == null)
                return null;

            return new()
            {
                Essence = data.Essence,
                DungeonKeys = data.DungeonKeys.Select((keys, index) => new DungeonKeyCurrency
                {
                    Stage = (uint) index,
                    NumKeys = keys
                }).ToList()
            };
        }
    }
}