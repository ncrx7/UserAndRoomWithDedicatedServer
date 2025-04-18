using System.Collections;
using System.Collections.Generic;
using EnhancedUI.EnhancedScroller;
using UnityEngine;

namespace Utils.BaseClasses
{
    /// <summary>
    /// I have created a base scroll controller to create scroll list elements faster like game room list or etc.
    /// Thanks to this, I have provided No Code repetition and scalable SOLID PRINCIPIPLES
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BaseScrollController<T> : MonoBehaviour, IEnhancedScrollerDelegate
    {
        [Header("References")]
        public EnhancedScroller myScroller;
        public BaseScrollCellView<T> userCellViewPrefab;
        [SerializeField] private float _cellViewSize;

        [SerializeField] protected List<T> _userData;

        protected virtual void PopulateScroller()
        {

        }

        public EnhancedScrollerCellView GetCellView(EnhancedScroller scroller, int dataIndex, int cellIndex)
        {
            BaseScrollCellView<T> cellView = scroller.GetCellView(userCellViewPrefab) as BaseScrollCellView<T>;
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
    }
}
