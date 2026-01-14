using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace NetFlow.Blazor.Shared.Utils
{
    public class KeyValuePairSerializer<TKey, TValue>
    {
        public KeyValuePairSerializer(TKey key, TValue value)
        {
            Key = key;
            Value = value;
        }

        public TKey Key { get; set; }

        public TValue Value { get; set; }

        [JsonIgnore] public KeyValuePair<TKey, TValue> ToKeyValuePair => new(Key, Value);
    }
}
