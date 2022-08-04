namespace LsDBase.Subsidiary
{
    public struct Fields
    {
        private readonly List<string> _fields = new();
        public int Count => _fields.Count;
        public Fields(params string[] args)
        {
            foreach (string arg in args)
                _fields.Add(arg);
        }
        public string this[int index]
        {
            get=>_fields[index];
        }
        public static Fields All => new("*");
        public bool IsAll => _fields.Contains("*") && _fields.Count == 1;
    }
}
