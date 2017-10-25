namespace ConsoleAddress
{
    public class AddressItem
    {
        private string name;
        private string street;
        private string city;
        private string state;
        private string zip;
        private string country;

        public AddressItem() : this("", "", "", "", "", "") {}

        public AddressItem(string name, string street, string city, string state, string zip, string country)
        {
            this.name = name;
            this.street = street;
            this.city = city;
            this.state = state;
            this.zip = zip;
            this.country = country;
        }

        public string Name { get => name; set => name = value; }
        public string Street { get => street; set => street = value; }            
        public string City { get => city; set => city = value; }
        public string State { get => state; set => state = value; }
        public string Zip { get => zip; set => zip = value; }
        public string Country { get => country; set => country = value; }
    }
}
