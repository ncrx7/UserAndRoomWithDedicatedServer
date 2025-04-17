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
        [SerializeField] private GameObject _mainMenuPanel;
        [SerializeField] private GameObject _allUserPanel;

        [Header("Main Menu Buttons")]
        [SerializeField] private Button _startGameButton;
        [SerializeField] private Button _displayAllUserButton;

        [Header("All User Panel Buttons")]
        [SerializeField] private Button _returnFromUserPanelButton;
        [SerializeField] private Button _refreshPanelButton;

        protected override void Awake()
        {
            base.Awake();
        }

        private void OnEnable()
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
        }

        private void OnDisable()
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
        }

        private void Start()
        {
            BindButtonEvents();
        }

        private void BindButtonEvents()
        {
            _displayAllUserButton.onClick.AddListener(() => GameEventSystem.OnClickAllUserButton?.Invoke());
            _returnFromUserPanelButton.onClick.AddListener(() => GameEventSystem.OnClickReturnMainMenuButton?.Invoke(PanelType.AllUser));
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
                default:
                    Debug.LogError("Undefined panel type!");
                    break;
            }
        }
    }
}


