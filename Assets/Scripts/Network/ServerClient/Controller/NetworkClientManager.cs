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
using UnityUtils.BaseClasses;

namespace Network.ServerClient.Controller
{
    public class NetworkClientManager : SingletonBehavior<NetworkClientManager>
    {
        public string serverUrl = "http://localhost:3000";

        private SocketIOUnity socket;
        private bool isConnected = false;

        [SerializeField] private Player[] _players;

        public Player[] GetPlayers => _players;

        private void OnEnable()
        {
            GameEventSystem.OnClickJoinServerButton += JoinServerByName;
            GameEventSystem.OnClickReturnMainMenuButton += (args) => DisconnectFromServer();
            GameEventSystem.OnClickReadyButton += SetPlayerReady;
        }

        private void OnDisable()
        {
            GameEventSystem.OnClickJoinServerButton -= JoinServerByName;
            GameEventSystem.OnClickReturnMainMenuButton -= (args) => DisconnectFromServer();
            GameEventSystem.OnClickReadyButton -= SetPlayerReady;
        }

        public async void JoinServerByName(string userName)
        {
            GameEventSystem.OnPopulateGameRoomUserStart?.Invoke();

            await StartSocketConnection();

            await UniTask.Delay(1000);

            if (!isConnected)
            {
                //TODO: CONNECTION ERROR UI EVENT
                return;
            }


            if (string.IsNullOrEmpty(userName))
            {
                //TODO: EMPTY NAME UI EVENT
                return;
            }

            var playerData = new
            {
                name = userName,
                isReady = false
            };

            string json = JsonConvert.SerializeObject(playerData);
            socket.Emit("join", JsonConvert.DeserializeObject(json));

            GameEventSystem.OnJoinServer?.Invoke();
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
                UpdatePlayerList(_players, response);
            });

            await socket.ConnectAsync();
        }

        private void UpdatePlayerList(Player[] players, SocketIOResponse response)
        {
            _players = response.GetValue<Player[]>();

            GameEventSystem.OnUpdateRoomPlayerList?.Invoke();
        }

        private void DisconnectFromServer()
        {
            if (!isConnected)
                return;

            socket.Disconnect();
        }

        public void SetPlayerReady()
        {
            if (!isConnected || socket == null) return;

            var data = new
            {
                id = socket.Id, 
                ready = true
            };

            string json = JsonConvert.SerializeObject(data);
            socket.Emit("playerReady", JsonConvert.DeserializeObject(json));
        }
    }
}



