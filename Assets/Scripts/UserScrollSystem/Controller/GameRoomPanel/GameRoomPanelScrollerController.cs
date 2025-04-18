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
using Utils.BaseClasses;

namespace ScrollSystem.Controller.GameRoomPanel
{
    public class GameRoomPanelScrollerController : BaseScrollController<Player>
    {
        private void OnEnable()
        {
            GameEventSystem.OnJoinServer += PopulateScroller;
            GameEventSystem.OnUpdateRoomPlayerList += PopulateScroller;
        }

        private void OnDisable()
        {
            GameEventSystem.OnJoinServer -= PopulateScroller;
            GameEventSystem.OnUpdateRoomPlayerList -= PopulateScroller;
        }

        protected async override void PopulateScroller()
        {
            base.PopulateScroller();

            await UniTask.WaitUntil(() => NetworkClientManager.Instance.GetPlayers.Length != 0);

            _userData = NetworkClientManager.Instance.GetPlayers.ToList();

            myScroller.Delegate = this;
            myScroller.ReloadData();

            GameEventSystem.OnPopulateGameRoomUserEnd?.Invoke();
        }

        /*         private async void PopulateGameRoomUserScroll()
                {
                    await UniTask.WaitUntil(() => NetworkClientManager.Instance.GetPlayers.Length != 0);

                    _userData = NetworkClientManager.Instance.GetPlayers.ToList();

                    myScroller.Delegate = this;
                    myScroller.ReloadData();

                    GameEventSystem.OnPopulateGameRoomUserEnd?.Invoke();
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
                } */
    }
}
