namespace AddressBookClient
{
    /// <summary>
    /// Wraps the domain address class into data that can easily bind to a view.
    /// </summary>
    public class AddressViewModel
    {
        private string name;
        private string street;
        private string city;
        private string state;
        private string zip;
        private string country;
        
        #region Constructors
        /// <summary>
        /// Default constructor.
        /// </summary>
        public AddressViewModel() : this("", "", "", "", "", "") {}

        public AddressViewModel(string name, string street, string city, string state, string zip, string country)
        {
            this.name = name;
            this.street = street;
            this.city = city;
            this.state = state;
            this.zip = zip;
            this.country = country;
        }

        #endregion

        #region Accessors
        public string Name { get => name; set => name = value; }
        public string Street { get => street; set => street = value; }
        public string City { get => city; set => city = value; }
        public string State { get => state; set => state = value; }
        public string Zip { get => zip; set => zip = value; }
        public string Country { get => country; set => country = value; }
        #endregion
    }
}
