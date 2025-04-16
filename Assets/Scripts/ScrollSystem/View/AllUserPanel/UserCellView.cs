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
        public void SetData(UserScrollerData data)
        {
            userNameText.text = data.Name;
        }
    }
}
