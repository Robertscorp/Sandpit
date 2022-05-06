namespace Sandpit.Console.Entities
{
    public class SemiStaticEntityOwner2
    {

        public SemiStaticEntityOwner2()
        {

        }

        public SemiStaticEntityOwner2(SemiStaticEntity2 semiStaticEntity)
            => this.SemiStaticEntity2 = semiStaticEntity;

        public int ID { get; set; }

        public string Name { get; }

        public SemiStaticEntity SemiStaticEntity
        {
            get;
            set;
        }

        public SemiStaticEntity2 SemiStaticEntity2
        {
            get;
            set;
        }

    }
}
