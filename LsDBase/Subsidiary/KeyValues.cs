namespace LsDBase.Subsidiary
{
    public class KeyValues
    {
        private readonly List<KeyValue> _data;
        public int Count => _data.Count;
        public KeyValues(string kvp,params string[] data)
        {
            _data = new();
            kvp = kvp.Replace(" ", "");
            string[] arg = kvp.Split('=');
            _data.Add(new KeyValue(arg[0], arg[1]));
            for(int i = 0; i < data.Length; i++)
            {
                data[i] = data[i].Replace(" ", "");
                string[] args = data[i].Split('=');
                _data.Add(new KeyValue(args[0], args[1]));
            }
        }
        private KeyValues()
            => _data = new();
        internal KeyValue this[int index]
        {
            get => _data[index];
        }
        public static KeyValues Null => new();
        public bool IsNull => _data.Count == 0;
        public bool ContainsKey(string key)
        {
            foreach (var kv in _data)
            {
                if(kv.Key == key)
                    return true;
            }
            return false;
        }
    }
    internal struct KeyValue
    {
        internal string Key { get; }
        internal string Value { get; }
        public KeyValue(string key, string value)
        {
            Key = key;
            Value = value;
        }
    }
}
