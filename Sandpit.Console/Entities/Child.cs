namespace Sandpit.Console.Entities
{

    public class Child
    {

        #region - - - - - - Constructors - - - - - -
        private Child()
        {

        }

        public Child(string name, Parent parent)
        {
            this.Name = name;
            this.Parent = parent;
        }

        #endregion Constructors

        #region - - - - - - Properties - - - - - -

        public int ID { get; set; }

        public DateTime Date { get; set; }

        public string Name { get; set; }

        public Parent Parent { get; set; }

        #endregion Properties

    }

}
