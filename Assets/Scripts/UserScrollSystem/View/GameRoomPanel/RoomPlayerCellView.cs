using System.Collections;
using System.Collections.Generic;
using EnhancedUI.EnhancedScroller;
using Network.ServerClient.Model;
using TMPro;
using UnityEngine;

namespace ScrollSystem.View.GameRoomPanel
{
    public class RoomPlayerCellView : EnhancedScrollerCellView
    {
        public TextMeshProUGUI userNameText;
        public TextMeshProUGUI isReadyText;

        public void SetData(Player data)
        {
            userNameText.text = data.name;
            isReadyText.text = data.isReady.ToString();
        }
    }
}
