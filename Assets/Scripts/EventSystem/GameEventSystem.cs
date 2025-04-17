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
        public static Action OnClickAllUserButton;
        public static Action<PanelType> OnClickReturnMainMenuButton;
        #endregion
    }

}
