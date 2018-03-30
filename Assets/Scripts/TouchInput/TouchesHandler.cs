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
            _containersPool = new Pool<TouchContainer>();
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

        void UpdateMouse()
        {
            var lbState = Input.GetMouseButton(0);
            var id = int.MinValue;
            if (lbState)
            {
                
            }
            else
            {
                
            }
        }

        Dictionary<int, TouchContainer> _touches;
        Pool<TouchContainer> _containersPool;
    }
}