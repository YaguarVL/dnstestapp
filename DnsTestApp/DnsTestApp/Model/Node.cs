namespace DnsTestApp.Model
{
    public class Node
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public int Parent { get; set; }

        public bool Folder { get; set; }

        public int Sort { get; set; }
    }
}