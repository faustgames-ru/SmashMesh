using UnityEngine;
using System.Collections;
using Core;
using System.Collections.Generic;

namespace TouchInput
{
    public class TouchesHandler: IUpdate
    {
        public TouchesHandler()
        {
            _containersPool = new TouchContainerPool();
            _containersPool.Reserve(8);
            _touches = new Dictionary<int, TouchContainer>();
        }

        public void Update(UpdateArgs e)
        {
            for (var i = 0; i < Input.touchCount; i++)
            {
                UpdateTouch(Input.GetTouch(i));
            }
        }

        void UpdateTouch(Touch touch)
        {
            var id = touch.fingerId;
        }

        Dictionary<int, TouchContainer> _touches;
        TouchContainerPool _containersPool;
    }
}