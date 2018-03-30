using System.Collections.Generic;
using Common;
using Core;
using UnityEngine;

public class TrailsManagerBehavior : MonoBehaviour 
{
    static string Tag = "TrailsManagerBehavior";

    void Start () 
    {
        Debug.Log($"Start:{Tag}");

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
        Debug.Log($"Update:{Tag}");

        var args = UpdateArgs.GetDefault();

        foreach (var trail in _meshTrails)
        {
            trail.Update(args);
        }
    }
     
    List<MeshTrail> _meshTrails;
}
