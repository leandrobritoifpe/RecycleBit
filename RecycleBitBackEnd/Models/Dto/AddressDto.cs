namespace RecycleBitBackEnd.Models.Dto {

    public class AddressDto {
        public string ZipCode { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public string Street { get; set; }
        public int Number { get; set; }
        public string Neighborhood { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string StateAbbr { get; set; }

        public USER User { get; set; }
        public COMPANY Company { get; set; }
    }
}