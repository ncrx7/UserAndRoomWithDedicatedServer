using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using API.Controller;
using API.Model;
using BaseModelData.User;
using Cysharp.Threading.Tasks;
using EnhancedUI.EnhancedScroller;
using GameEvents;
using ScrollSystem.Model;
using ScrollSystem.View;
using UnityEngine;
using Utils.BaseClasses;

namespace ScrollSystem.Controller
{
    public class AllUserPanelScrollerController : BaseScrollController<UserScrollerData>
    {
        [Header("Variable")]
        [SerializeField] private string _url;
          
        private void OnEnable()
        {
            GameEventSystem.OnDisplayAllUserPanel += PopulateScroller;
            GameEventSystem.OnClickRefreshUserPanelButton += PopulateScroller;
        }

        private void OnDisable()
        {
            GameEventSystem.OnDisplayAllUserPanel -= PopulateScroller;
            GameEventSystem.OnClickRefreshUserPanelButton -= PopulateScroller;
        }

        protected async override void PopulateScroller()
        {
            GameEventSystem.OnAllUserDataLoadingStart?.Invoke();

            base.PopulateScroller();

            await UniTask.Delay(700); //I am waited in purpose, because I want to display user loading panel longer

            ApiResult<List<UserScrollerData>> responseResult = await ApiHandler.GetApiResponse<List<UserScrollerData>>(_url, ParseUserScrollerList);

            if (!responseResult.CheckAnyResponseError())
            {
                //TODO: SUCCES UI
            }
            else
            {
                Debug.LogError(responseResult.ResponseErrorMessage);
                //TODO: Error UI Panel
            }


            _userData = responseResult.ResponseData;

            myScroller.Delegate = this;
            myScroller.ReloadData();

            GameEventSystem.OnAllUserDataLoadingEnd?.Invoke();
        }

        /// <summary>
        /// My Custom Users Parse callback
        /// It is for the both list and single class parse
        /// </summary>
        /// <param name="rawJson"></param>
        /// <returns></returns>
        private List<UserScrollerData> ParseUserScrollerList(string rawJson)
        {
            string wrappedJson = "{ \"Users\": " + rawJson + " }";
            var wrapper = JsonUtility.FromJson<UserRawModelWrapper>(wrappedJson);
            var userScrollerList = new List<UserScrollerData>();

            foreach (var user in wrapper.Users)
            {
                userScrollerList.Add(new UserScrollerData
                {
                    OrderId = user.id,
                    Name = user.name,
                    CityLive = user.address?.city ?? "Unknown"
                });
            }

            return userScrollerList;
        }
    }
}
