using System;
using Enums;
using UnityEngine;

namespace GameEvents
{
    public static class GameEventSystem
    {
        public static Action OnGameDataLoadingStart;
        public static Action OnGameDataLoadingEnd;

        public static Action OnAllUserDataLoadingStart;
        public static Action OnAllUserDataLoadingEnd;

        public static Action OnDisplayAllUserPanel;

        #region Button Events
        // MAIN MENU PANEL BUTTON EVENTS
        public static Action OnClickAllUserButton;

        // USER PANEL BUTTON EVENTS
        public static Action<PanelType> OnClickReturnMainMenuButton;
        public static Action OnClickRefreshUserPanelButton;

        // GAME PANEL BUTTON EVENTS
        public static Action<string> OnClickJoinServerButton;
        #endregion
    }

}
