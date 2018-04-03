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
        }

        public void Update(TouchContainer touch)
        {            
        }

        public void End(TouchContainer touch)
        {
            _originPool.Return(this);
        }

        Mesh _mesh;
        Pool<MeshTrail> _originPool;
    }
}