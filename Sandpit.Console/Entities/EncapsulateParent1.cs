namespace Sandpit.Console.Entities
{

    public class EncapsulateParent1
    {

        #region - - - - - - Constructors - - - - - -
        public EncapsulateParent1()
        {


        }

        public EncapsulateParent1(Parent encapsulatedParent)
            => this.EncapsulatedParent = encapsulatedParent;

        #endregion Constructors

        #region - - - - - - Properties - - - - - -

        public int ID { get; set; }

        public Parent EncapsulatedParent { get; set; }

        #endregion Properties

    }

}
