using System;

namespace Sandpit.Console.Entities
{
    public class Bar
    {
        public int ID { get; set; }
        public string Test { get; set; }
        public Guid Test2 { get; set; } = Guid.NewGuid();
    }
}
