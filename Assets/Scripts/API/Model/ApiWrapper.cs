using System;

namespace API.Model
{
    [Serializable]
    public class ApiWrapper<TItem>
    {
        public TItem[] Items;
    }
}
