using System;

namespace BaseModelData.User
{
    [Serializable]
    public class Address
    {
        public string street;
        public string suite;
        public string city;
        public string zipcode;
        public Geo geo;
    }
}
