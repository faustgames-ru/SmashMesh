using UnityEngine;

namespace TouchInput
{
    public struct TouchInfo
    {
        public int id;
        public float time;
        public TouchPhase phase;
        public Vector2 position;

        public TouchInfo(Touch touch, float touchTime)
        {
            id = touch.fingerId;
            phase = touch.phase;
            position = touch.position;
            time = touchTime;
        }

        public TouchInfo(int touchId, TouchPhase touchPhase, Vector2 touchPosition, float touchTime)
        {
            id = touchId;
            phase = touchPhase;
            position = touchPosition;
            time = touchTime;
        }
    }
}