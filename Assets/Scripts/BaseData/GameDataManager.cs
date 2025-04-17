using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using GameEvents;
using UnityEngine;

namespace BaseModelData
{
    public class GameDataManager : MonoBehaviour
    {
        private void Start()
        {
            Setup();
        }

        private async void Setup()
        {
            GameEventSystem.OnGameDataLoadingStart?.Invoke();

            await LoadGameDataAsync();

            GameEventSystem.OnGameDataLoadingEnd?.Invoke();
        }

        private async UniTask LoadGameDataAsync()
        {
            await UniTask.Delay(1500);
        }
    }
}
