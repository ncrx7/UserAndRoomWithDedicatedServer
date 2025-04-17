using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SocketIOClient;
using TMPro;
using System;
using SocketIOClient.Newtonsoft.Json;
using Network.ServerClient.Model;
using GameEvents;
using Newtonsoft.Json;
using Cysharp.Threading.Tasks;

namespace Network.ServerClient.Controller
{
    public class NetworkClientManager : MonoBehaviour
    {
        public string serverUrl = "http://localhost:3000";
        public TextMeshProUGUI playerListText;

        private SocketIOUnity socket;
        private bool isConnected = false;

        private void OnEnable()
        {
            GameEventSystem.OnClickJoinServerButton += JoinServerByName;
        }

        private void OnDisable()
        {
            GameEventSystem.OnClickJoinServerButton -= JoinServerByName;
        }

        public async void JoinServerByName(string userName)
        {
            await StartSocketConnection();

            await UniTask.Delay(1000);

            if (!isConnected)
            {
                Debug.LogWarning("Bağlantı henüz yok.");
                return;
            }


            if (string.IsNullOrEmpty(userName))
            {
                Debug.LogWarning("Lütfen isim girin.");
                return;
            }

            var playerData = new
            {
                name = userName
            };

            string json = JsonConvert.SerializeObject(playerData);
            socket.Emit("join", JsonConvert.DeserializeObject(json));
            Debug.Log("🚀 İsim gönderildi: " + json);
        }

        public async UniTask StartSocketConnection()
        {
            if (isConnected)
            {
                Debug.Log("The server is already connected!");
                return;
            }

            Debug.Log("Server is starting..");
            var uri = new Uri(serverUrl);
            socket = new SocketIOUnity(uri, new SocketIOOptions
            {
                Query = new Dictionary<string, string>
                {
                    {"token", "UNITY" }
                }
                ,
                Transport = SocketIOClient.Transport.TransportProtocol.WebSocket
            });

            socket.JsonSerializer = new NewtonsoftJsonSerializer();

            socket.OnConnected += (sender, e) =>
            {
                Debug.Log("Has Connected to server");
                isConnected = true;
            };

            socket.OnDisconnected += (sender, e) =>
            {
                Debug.Log("Has Disconnected from server");
                isConnected = false;
            };

            socket.On("updatePlayers", (SocketIOResponse response) =>
            {
                Player[] players = response.GetValue<Player[]>();

                UpdatePlayerList(players);
            });

            await socket.ConnectAsync();
        }

        private void UpdatePlayerList(Player[] players)
        {
            playerListText.text = "Players:\n";
            foreach (var p in players)
            {
                playerListText.text += $"- {p.id}\n";
            }
        }
    }
}



