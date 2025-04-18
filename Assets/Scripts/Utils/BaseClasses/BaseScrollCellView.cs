using System.Collections;
using System.Collections.Generic;
using EnhancedUI.EnhancedScroller;
using UnityEngine;

namespace Utils.BaseClasses
{
    /// <summary>
    /// I have created a base scroll view for creating a base scroll controller. Thanks to this, I Utilized the system.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BaseScrollCellView<T> : EnhancedScrollerCellView
    {
        public virtual void SetData(T Data) 
        {}
    }
}
