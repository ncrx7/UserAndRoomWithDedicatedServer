using System;

namespace Network.ServerClient.Model
{
    [Serializable]
    public class Player
    {
        public string id;
        public string name;
        public bool isReady;
    }
}