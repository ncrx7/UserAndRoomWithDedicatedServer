using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using EnhancedUI.EnhancedScroller;
using GameEvents;
using Network.ServerClient.Controller;
using Network.ServerClient.Model;
using ScrollSystem.View.GameRoomPanel;
using UnityEngine;

namespace ScrollSystem.Controller.GameRoomPanel
{
    public class GameRoomPanelScrollerController : MonoBehaviour, IEnhancedScrollerDelegate
    {
        [Header("References")]
        public EnhancedScroller myScroller;
        public RoomPlayerCellView userCellViewPrefab;
        [SerializeField] private float _cellViewSize;

        [SerializeField] private List<Player> _userData;

        private void OnEnable()
        {
            GameEventSystem.OnJoinServer += PopulateGameRoomUserScroll;
        }

        private void OnDisable()
        {
            GameEventSystem.OnJoinServer -= PopulateGameRoomUserScroll;
        }

        private async void PopulateGameRoomUserScroll()
        {
            await UniTask.WaitUntil(() => NetworkClientManager.Instance.GetPlayers.Length != 0);

            _userData = NetworkClientManager.Instance.GetPlayers.ToList();

            myScroller.Delegate = this;
            myScroller.ReloadData();
        }

        public EnhancedScrollerCellView GetCellView(EnhancedScroller scroller, int dataIndex, int cellIndex)
        {
            RoomPlayerCellView cellView = scroller.GetCellView(userCellViewPrefab) as RoomPlayerCellView;
            cellView.SetData(_userData[dataIndex]);
            return cellView;
        }

        public float GetCellViewSize(EnhancedScroller scroller, int dataIndex)
        {
            return _cellViewSize;
        }

        public int GetNumberOfCells(EnhancedScroller scroller)
        {
            return _userData.Count;
        }
    }
}
