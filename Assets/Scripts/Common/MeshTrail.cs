using Core;
using TouchInput;
using UnityEngine;

namespace Common
{
    public class MeshTrail : ITouchListener
    {
        public MeshTrail(Mesh mesh, Pool<MeshTrail> originPool)
        {
            _mesh = mesh;
            _mesh.MarkDynamic();
            _originPool = originPool;
        }

        public void Start(TouchContainer touch)
        {       
            var p = touch.Touch.scenePos;
            Debug.Log($"touch start: {p.x:0.00}x{p.y:0.00}x{p.z:0.00}");
        }

        public void Update(TouchContainer touch)
        {
            var p = touch.Touch.scenePos;
            Debug.Log($"touch move: {p.x:0.00}x{p.y:0.00}x{p.z:0.00}");
        }

        public void End(TouchContainer touch)
        {
            var p = touch.Touch.scenePos;
            Debug.Log($"touch end: {p.x:0.00}x{p.y:0.00}x{p.z:0.00}");
            _originPool.Return(this);
        }

        Mesh _mesh;
        Pool<MeshTrail> _originPool;
    }
}