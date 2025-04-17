using System;

namespace BaseModelData.User
{
    [Serializable]
    public class UserRawModel
    {
        public int id;
        public string name;
        public string username;
        public string email;
        public Address address;
        public string phone;
        public string website;
        public Company company;
    }
}
