using System.Collections;
using System.Collections.Generic;
using EnhancedUI.EnhancedScroller;
using Network.ServerClient.Model;
using TMPro;
using UnityEngine;
using Utils.BaseClasses;

namespace ScrollSystem.View.GameRoomPanel
{
    public class RoomPlayerCellView : BaseScrollCellView<Player>
    {
        public TextMeshProUGUI userNameText;
        public TextMeshProUGUI isReadyText;

        public override void SetData(Player data)
        {
            base.SetData(data);
            
            userNameText.text = data.name;
            isReadyText.text = data.isReady.ToString();
        }
    }
}
