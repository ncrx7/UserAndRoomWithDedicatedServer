using System.Collections;
using System.Collections.Generic;
using Enums;
using GameEvents;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityUtils.BaseClasses;

namespace UI
{
    public class UIManager : BaseUIManager
    {
        [Header("Base Panels")]
        [SerializeField] private GameObject _loadingPanel;
        [SerializeField] private GameObject _mainMenuPanel;
        [SerializeField] private GameObject _allUserPanel;

        [Header("Main Menu References")]
        [SerializeField] private Button _startGameButton;
        [SerializeField] private Button _displayAllUserButton;
        [SerializeField] private Button _quitButton;

        [Header("All User Panel References")]
        [SerializeField] private Button _returnFromUserPanelButton;
        [SerializeField] private Button _refreshUserPanelButton;
        [SerializeField] private GameObject _userLoadingPanel;
        [SerializeField] private GameObject _usersContainerPanel;

        [Header("Game Panel References")]
        [SerializeField] private GameObject _gamePanel;
        [SerializeField] private GameObject _gameJoinServerPanel;
        [SerializeField] private Button _joinServerButton;
        [SerializeField] private TMP_InputField _userNameInputField;

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
                OpenPanel(PanelType.LoadingFullScreen);
            };

            GameEventSystem.OnGameDataLoadingEnd += () =>
            {
                ClosePanel(PanelType.LoadingFullScreen);
            };

            GameEventSystem.OnAllUserDataLoadingStart += () =>
            {
                ClosePanel(PanelType.UsersContainer);
                OpenPanel(PanelType.LoadingAllUser);
            };

            GameEventSystem.OnAllUserDataLoadingEnd += () =>
            {
                ClosePanel(PanelType.LoadingAllUser);
                OpenPanel(PanelType.UsersContainer);
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
                OpenPanel(PanelType.LoadingFullScreen);
            };

            GameEventSystem.OnGameDataLoadingEnd -= () =>
            {
                ClosePanel(PanelType.LoadingFullScreen);
            };

            GameEventSystem.OnAllUserDataLoadingStart -= () =>
            {
                ClosePanel(PanelType.UsersContainer);
                OpenPanel(PanelType.LoadingAllUser);
            };

            GameEventSystem.OnAllUserDataLoadingEnd -= () =>
            {
                ClosePanel(PanelType.LoadingAllUser);
                OpenPanel(PanelType.UsersContainer);
            };
        }

        private void BindButtonEvents()
        {
            _displayAllUserButton.onClick.AddListener(() => GameEventSystem.OnClickAllUserButton?.Invoke());
            _returnFromUserPanelButton.onClick.AddListener(() => GameEventSystem.OnClickReturnMainMenuButton?.Invoke(PanelType.AllUser));
            _refreshUserPanelButton.onClick.AddListener(() => GameEventSystem.OnClickRefreshUserPanelButton?.Invoke());
            _quitButton.onClick.AddListener(() => Application.Quit());

            _joinServerButton.onClick.AddListener(() => GameEventSystem.OnClickJoinServerButton?.Invoke(_userNameInputField.text));
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
                case PanelType.LoadingFullScreen:
                    ExecuteUIAction<bool, GameObject>(UIActionType.SetPanelDisplay, true, _loadingPanel);
                    break;
                case PanelType.LoadingAllUser:
                    ExecuteUIAction<bool, GameObject>(UIActionType.SetPanelDisplay, true, _userLoadingPanel);
                    break;
                case PanelType.UsersContainer:
                    ExecuteUIAction<bool, GameObject>(UIActionType.SetPanelDisplay, true, _usersContainerPanel);
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
                case PanelType.LoadingFullScreen:
                    ExecuteUIAction<bool, GameObject>(UIActionType.SetPanelDisplay, false, _loadingPanel);
                    break;
                case PanelType.LoadingAllUser:
                    ExecuteUIAction<bool, GameObject>(UIActionType.SetPanelDisplay, false, _userLoadingPanel);
                    break;
                case PanelType.UsersContainer:
                    ExecuteUIAction<bool, GameObject>(UIActionType.SetPanelDisplay, false, _usersContainerPanel);
                    break;
                default:
                    Debug.LogError("Undefined panel type!");
                    break;
            }
        }
    }
}


