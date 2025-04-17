using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using API.Controller;
using API.Model;
using BaseModelData.User;
using EnhancedUI.EnhancedScroller;
using ScrollSystem.Model;
using ScrollSystem.View;
using UnityEngine;

namespace ScrollSystem.Controller
{
    public class AllUserPanelScrollerController : MonoBehaviour, IEnhancedScrollerDelegate
    {
        [Header("References")]
        public EnhancedScroller myScroller;
        public UserCellView userCellViewPrefab;

        [Header("Variable")]
        [SerializeField] private string _url;
        [SerializeField] private float _cellViewSize;

        private List<UserScrollerData> _userData;

       
        private async void PopulateAllUserPanel()
        {
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
        }


        public EnhancedScrollerCellView GetCellView(EnhancedScroller scroller, int dataIndex, int cellIndex)
        {
            UserCellView cellView = scroller.GetCellView(userCellViewPrefab) as UserCellView;
            cellView.SetData(_userData[dataIndex]);
            return cellView;
        }

        public float GetCellViewSize(EnhancedScroller scroller, int dataIndex)
        {
            return _cellViewSize;
        }

        public int GetNumberOfCells(EnhancedScroller scroller)
        {
            return _userData.Count;
        }

        /// <summary>
        /// My Custom Users Parse callback
        /// It is for the 
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
