using System.Collections.Generic;
using Common;
using Core;
using UnityEngine;

public class TrailsManagerBehavior : MonoBehaviour 
{
    void Start () 
    {
        _meshTrails = new List<MeshTrail>();

        foreach(Transform child in gameObject.transform)
        {
            var childObject = child.gameObject;
            var meshFilter = childObject.GetComponent<MeshFilter>();
            if (meshFilter == null) continue;
            var trail = new MeshTrail(meshFilter.mesh);
            _meshTrails.Add(trail);
        }
	}

    void Update()
    {
        var args = UpdateArgs.GetDefault();

        // todo: update input

        foreach (var trail in _meshTrails)
        {
            trail.Update(args);
        }
    }
     
    List<MeshTrail> _meshTrails;
}
