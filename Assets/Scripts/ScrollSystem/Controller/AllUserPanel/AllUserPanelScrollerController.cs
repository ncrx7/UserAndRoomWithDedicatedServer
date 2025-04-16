using System.Collections;
using System.Collections.Generic;
using EnhancedUI.EnhancedScroller;
using ScrollSystem.Model;
using ScrollSystem.View;
using UnityEngine;

namespace ScrollSystem.Controller
{
    public class ScrollerController : MonoBehaviour, IEnhancedScrollerDelegate
    {
        private List<UserScrollerData> _userData;
        public EnhancedScroller myScroller;
        public UserCellView userCellViewPrefab;

        void Start()
        {
            _userData = new List<UserScrollerData>();
            _userData.Add(new UserScrollerData() { Name = "Lion" });
            _userData.Add(new UserScrollerData() { Name = "Bear" });
            _userData.Add(new UserScrollerData() { Name = "Eagle" });
            _userData.Add(new UserScrollerData() { Name = "Dolphin" });
            _userData.Add(new UserScrollerData() { Name = "Ant" });
            _userData.Add(new UserScrollerData() { Name = "Cat" });
            _userData.Add(new UserScrollerData() { Name = "Sparrow" });
            _userData.Add(new UserScrollerData() { Name = "Dog" });
            _userData.Add(new UserScrollerData() { Name = "Spider" });
            _userData.Add(new UserScrollerData() { Name = "Elephant" });
            _userData.Add(new UserScrollerData() { Name = "Falcon" });
            _userData.Add(new UserScrollerData() { Name = "Mouse" });
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
            return 100f;
        }

        public int GetNumberOfCells(EnhancedScroller scroller)
        {
            return _userData.Count;
        }
    }
}
