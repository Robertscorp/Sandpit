namespace Sandpit.Console.Entities
{

    public class SemiStaticEntityOwner
    {

        public SemiStaticEntityOwner()
        {

        }

        public SemiStaticEntityOwner(SemiStaticEntity semiStaticEntity)
            => this.SemiStaticEntity = semiStaticEntity;

        public int ID { get; set; }

        public string Name
        {
            get;
            set;
        } = string.Empty;

        public SemiStaticEntity SemiStaticEntity
        {
            get;
            set;
        }

    }

}
