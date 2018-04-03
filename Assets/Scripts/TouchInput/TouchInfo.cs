using UnityEngine;

namespace TouchInput
{
    public struct TouchInfo
    {
        public int id;
        public float time;
        public TouchPhase phase;
        public Vector2 position;
        public Vector3 scenePos;
        public Ray ray;

        public TouchInfo(Touch touch, float touchTime)
        {
            id = touch.fingerId;
            phase = touch.phase;
            position = touch.position;
            time = touchTime;
            ray = new Ray(Vector3.zero, Vector3.forward);
            scenePos = Vector3.zero;
        }

        public TouchInfo(int touchId, TouchPhase touchPhase, Vector2 touchPosition, float touchTime)
        {
            id = touchId;
            phase = touchPhase;
            position = touchPosition;
            time = touchTime;
            ray = new Ray(Vector3.zero, Vector3.forward);
            scenePos = Vector3.zero;
        }

        public void CalRay(Camera camera, Plane plane)
        {
            if (camera == null) return;
            ray = camera.ScreenPointToRay(position);
            float distance = 0;
            if (plane.Raycast(ray, out distance))
            {
                scenePos = ray.GetPoint(distance);
            }

        }
    }
}