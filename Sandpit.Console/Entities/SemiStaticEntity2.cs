using System;

namespace Sandpit.Console.Entities
{

    public class SemiStaticEntity2
    {

        public string ID { get; set; } = string.Empty;

        public Guid ID2 { get; set; } = Guid.NewGuid();

        public string Name { get; set; } = string.Empty;

    }

}
