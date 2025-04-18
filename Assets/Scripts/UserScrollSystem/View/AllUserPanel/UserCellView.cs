using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using EnhancedUI.EnhancedScroller;
using TMPro;
using ScrollSystem.Model;
using Utils.BaseClasses;

namespace ScrollSystem.View
{
    public class UserCellView : BaseScrollCellView<UserScrollerData>
    {
        public TextMeshProUGUI userNameText;
        public TextMeshProUGUI cityLivedText;
        public TextMeshProUGUI orderIdText;

        public override void SetData(UserScrollerData data)
        {
            base.SetData(data);
            
            userNameText.text = data.Name;
            cityLivedText.text = data.CityLive;
            orderIdText.text = data.OrderId.ToString();
        }
    }
}
