using UnityEngine;
using System.Collections;
using Core;
using System.Collections.Generic;
using System;

namespace TouchInput
{
    public class TouchesHandler: IUpdate
    {
        public TouchContainerCreateArgs CreateArgs { get; }
        public Action<TouchContainer> OnTouchStart;

        public TouchesHandler(TouchContainerCreateArgs e)
        {
            CreateArgs = e;
            _containersPool = new Pool<TouchContainer>();
            _containersPool.Reserve(e.TouchesReserve, CreateTouchContainer);
            _containers = new Dictionary<int, TouchContainer>(e.TouchesReserve);
            _touches = new Dictionary<int, TouchInfo>(e.TouchesReserve);
            _startList = new List<TouchContainer>(e.TouchesReserve);
            _updateList = new List<TouchContainer>(e.TouchesReserve);
            _endList = new List<TouchContainer>(e.TouchesReserve);
        }

        public void Update(UpdateArgs e)
        {
            _touches.Clear();
            for (var i = 0; i < Input.touchCount; i++)
            {
                var touch = new TouchInfo(Input.GetTouch(i), e.time);
                if (!_touches.ContainsKey(touch.id))
                {
                    _touches.Add(touch.id, touch);
                }
                else
                {
                    _touches[touch.id] =  touch;
                }
                UpdateTouch(touch);
            }
            foreach (var contaier in _containers)
            {
                _endList.Add(contaier.Value);
            }

            UpdateMouse(e);

            ExecuteStartList(e);
            ExecuteUpdateList(e);
            ExecuteEndList(e);
        }

        void UpdateTouch(TouchInfo touch)
        {
            TouchContainer container = null;
            var id = touch.id;
            if (_containers.ContainsKey(id))
            {
                container = _containers[id];
            }
            else
            {
                container = _containersPool.Obtain(CreateTouchContainer);
                container.Id = id;
                _containers.Add(id, container);
            }
            container.Touch = touch;
            UpdateContainer(container, touch.phase);
        }

        void UpdateContainer(TouchContainer container, TouchPhase phase)
        {
            switch (phase)
            {
                case TouchPhase.Began:
                    _startList.Add(container);
                    break;
                case TouchPhase.Moved:
                case TouchPhase.Stationary:
                    _updateList.Add(container);
                    break;
                case TouchPhase.Ended:
                case TouchPhase.Canceled:
                    _endList.Add(container);
                    break;
            }
        }

        void ExecuteStartList(UpdateArgs e)
        {
            foreach (var container in _startList)
            {
                OnTouchStart(container);
                container.Start(e);
            }
            _startList.Clear();
        }

        void ExecuteUpdateList(UpdateArgs e)
        {
            foreach (var container in _updateList)
            {
                container.Update(e);
            }
            _updateList.Clear();
        }

        void ExecuteEndList(UpdateArgs e)
        {
            foreach (var container in _endList)
            {
                container.End(e);
                _containers.Remove(container.Id);
                _containersPool.Return(container);
            }
            _endList.Clear();
        }

        void UpdateMouse(UpdateArgs e)
        {
            var lbState = Input.GetMouseButton(0);
            var id = MouseTouchId;
            var touch = new TouchInfo(MouseTouchId, TouchPhase.Began, Input.mousePosition, e.time);
            TouchContainer container = null;
            if (lbState)
            {
                touch.id = id;
                if (_containers.ContainsKey(id))
                {
                    container = _containers[id];
                    _updateList.Add(container);
                    touch.phase = TouchPhase.Moved;
                }
                else
                {
                    container = _containersPool.Obtain(CreateTouchContainer);
                    container.Id = id;
                    _containers.Add(id, container);
                    _startList.Add(container);
                    touch.phase = TouchPhase.Began;
                }
                container.Touch = touch;
            }
            else
            {
                if (_containers.ContainsKey(id))
                {
                    container = _containers[id];
                    _endList.Add(container);
                    touch.phase = TouchPhase.Ended;
                    container.Touch = touch;
                }
            }
        }

        TouchContainer CreateTouchContainer()
        {
            return new TouchContainer(CreateArgs);
        }

        const int MouseTouchId = int.MinValue;
        Dictionary<int, TouchContainer> _containers;
        Dictionary<int, TouchInfo> _touches;
        List<TouchContainer> _startList;
        List<TouchContainer> _updateList;
        List<TouchContainer> _endList;
        Pool<TouchContainer> _containersPool;
    }
}