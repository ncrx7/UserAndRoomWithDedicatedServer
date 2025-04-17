using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using EnhancedUI.EnhancedScroller;
using TMPro;
using ScrollSystem.Model;

namespace ScrollSystem.View
{
    public class UserCellView : EnhancedScrollerCellView
    {
        public TextMeshProUGUI userNameText;
        public TextMeshProUGUI cityLivedText;
        public TextMeshProUGUI orderIdText;

        public void SetData(UserScrollerData data)
        {
            userNameText.text = data.Name;
            cityLivedText.text = data.CityLive;
            orderIdText.text = data.OrderId.ToString();
        }
    }
}
