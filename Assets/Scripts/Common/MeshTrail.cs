using Core;
using UnityEngine;

namespace Common
{
    public class MeshTrail : IUpdate
    {
        public MeshTrail(Mesh mesh)
        {
            _mesh = mesh;
            _mesh.MarkDynamic();
        }

        public void Update(UpdateArgs e)
        {
            
        }

        Mesh _mesh;
    }
}