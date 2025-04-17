using System.Collections;
using System.Collections.Generic;
using Enums;
using GameEvents;
using UnityEngine;
using UnityEngine.UI;
using UnityUtils.BaseClasses;

namespace UI
{
    public class UIManager : BaseUIManager
    {
        [Header("Panels")]
        [SerializeField] private GameObject _loadingPanel;
        [SerializeField] private GameObject _mainMenuPanel;
        [SerializeField] private GameObject _allUserPanel;

        [Header("Main Menu Buttons")]
        [SerializeField] private Button _startGameButton;
        [SerializeField] private Button _displayAllUserButton;
        [SerializeField] private Button _quitButton;

        [Header("All User Panel Buttons")]
        [SerializeField] private Button _returnFromUserPanelButton;
        [SerializeField] private Button _refreshUserPanelButton;

        protected override void Awake()
        {
            base.Awake();
        }

        private void OnEnable()
        {
            BindGameEvents();
        }

        private void OnDisable()
        {
            RemoveGameEvents();
        }

        private void Start()
        {
            BindButtonEvents();
        }

        private void BindGameEvents()
        {
            GameEventSystem.OnClickAllUserButton += () =>
            {
                OpenPanel(PanelType.AllUser);
                ClosePanel(PanelType.MainMenu);

                GameEventSystem.OnDisplayAllUserPanel?.Invoke();
            };

            GameEventSystem.OnClickReturnMainMenuButton += (currentPanelType) =>
            {
                ClosePanel(currentPanelType);
                OpenPanel(PanelType.MainMenu);
            };

            GameEventSystem.OnGameDataLoadingStart += () =>
            {
                OpenPanel(PanelType.Loading);
            };

            GameEventSystem.OnGameDataLoadingEnd += () =>
            {
                ClosePanel(PanelType.Loading);
            };
        }

        private void RemoveGameEvents()
        {
            GameEventSystem.OnClickAllUserButton -= () =>
            {
                OpenPanel(PanelType.AllUser);
                ClosePanel(PanelType.MainMenu);

                GameEventSystem.OnDisplayAllUserPanel?.Invoke();
            };

            GameEventSystem.OnClickReturnMainMenuButton -= (currentPanelType) =>
            {
                ClosePanel(currentPanelType);
                OpenPanel(PanelType.MainMenu);
            };

            GameEventSystem.OnGameDataLoadingStart -= () =>
            {
                OpenPanel(PanelType.Loading);
            };

            GameEventSystem.OnGameDataLoadingEnd -= () =>
            {
                ClosePanel(PanelType.Loading);
            };
        }

        private void BindButtonEvents()
        {
            _displayAllUserButton.onClick.AddListener(() => GameEventSystem.OnClickAllUserButton?.Invoke());
            _returnFromUserPanelButton.onClick.AddListener(() => GameEventSystem.OnClickReturnMainMenuButton?.Invoke(PanelType.AllUser));
            _refreshUserPanelButton.onClick.AddListener(() => GameEventSystem.OnClickRefreshUserPanelButton?.Invoke());
            _quitButton.onClick.AddListener(() => Application.Quit());
        }

        private void OpenPanel(PanelType type)
        {
            switch (type)
            {
                case PanelType.MainMenu:
                    ExecuteUIAction<bool, GameObject>(UIActionType.SetPanelDisplay, true, _mainMenuPanel);
                    break;
                case PanelType.Game:

                    break;
                case PanelType.AllUser:
                    ExecuteUIAction<bool, GameObject>(UIActionType.SetPanelDisplay, true, _allUserPanel);
                    break;
                case PanelType.Loading:
                    ExecuteUIAction<bool, GameObject>(UIActionType.SetPanelDisplay, true, _loadingPanel);
                    break;
                default:
                    Debug.LogError("Undefined panel type!");
                    break;
            }
        }

        private void ClosePanel(PanelType type)
        {
            switch (type)
            {
                case PanelType.MainMenu:
                    ExecuteUIAction<bool, GameObject>(UIActionType.SetPanelDisplay, false, _mainMenuPanel);
                    break;
                case PanelType.Game:

                    break;
                case PanelType.AllUser:
                    ExecuteUIAction<bool, GameObject>(UIActionType.SetPanelDisplay, false, _allUserPanel);
                    break;
                case PanelType.Loading:
                    ExecuteUIAction<bool, GameObject>(UIActionType.SetPanelDisplay, false, _loadingPanel);
                    break;
                default:
                    Debug.LogError("Undefined panel type!");
                    break;
            }
        }
    }
}


