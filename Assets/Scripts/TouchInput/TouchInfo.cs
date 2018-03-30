using UnityEngine;

namespace TouchInput
{
    public struct TouchInfo
    {
        Vector2 position;

        public TouchInfo(Touch source)
        {
            position = source.position;
        }

        public TouchInfo(Vector2 value)
        {
            position = value;
        }
    }
}