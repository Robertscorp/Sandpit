namespace Sandpit.Console.Entities
{

    public class EncapsulateParent2
    {

        #region - - - - - - Constructors - - - - - -

        public EncapsulateParent2()
        {

        }

        public EncapsulateParent2(Parent encapsulatedParent)
            => this.EncapsulatedParent = encapsulatedParent;

        #endregion Constructors

        #region - - - - - - Properties - - - - - -

        public int ID { get; set; }

        public Parent EncapsulatedParent { get; set; }

        #endregion Properties

    }

}
