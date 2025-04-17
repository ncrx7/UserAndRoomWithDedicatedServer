using System;
using Enums;
using UnityEngine;

namespace GameEvents
{
    public static class GameEventSystem
    {
        public static Action OnGameDataLoadingStart;
        public static Action OnGameDataLoadingEnd;

        public static Action OnDisplayAllUserPanel;

        #region Button Events
        // MAIN MENU BUTTON EVENTS
        public static Action OnClickAllUserButton;

        // USER PANEL BUTTON EVENTS
        public static Action<PanelType> OnClickReturnMainMenuButton;
        public static Action OnClickRefreshUserPanelButton;
        #endregion
    }

}
