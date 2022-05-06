namespace Sandpit.Console.Entities
{

    public class SemiStaticEntityOwner
    {

        public SemiStaticEntityOwner()
        {

        }

        public SemiStaticEntityOwner(SemiStaticEntity semiStaticEntity)
            => this.JSemiStaticEntity = semiStaticEntity;

        public int ID { get; set; }

        public string Name { get; }

        public SemiStaticEntity JSemiStaticEntity
        {
            get;
            set;
        }

    }

}
