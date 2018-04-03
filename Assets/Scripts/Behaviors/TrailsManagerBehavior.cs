using System.Collections.Generic;
using Common;
using Core;
using TouchInput;
using UnityEngine;

public class TrailsManagerBehavior : MonoBehaviour 
{
    [Header("Touches")]
    [Range(0, 16)]
    public int TouchesReserve = 8;
    [Range(0, 300)]
    public int TouchHistorySizeLimit = 150;
    [Range(0.1f, 5.0f)]
    public float TouchHistoryTimeLimit = 0.5f;

    void Start () 
    {
        var touchesCreateArgs = new TouchContainerCreateArgs
        {
            TouchesReserve = TouchesReserve,
            TouchHistorySizeLimit = TouchHistorySizeLimit,
            TouchHistoryTimeLimit = TouchHistoryTimeLimit,
            Camera = Camera.main
        };
        _touches = new TouchesHandler(touchesCreateArgs);
        _touches.OnTouchStart = OnStartTouch;

        _trailsPool = new Pool<MeshTrail>();
        foreach (Transform child in gameObject.transform)
        {
            var childObject = child.gameObject;
            var meshFilter = childObject.GetComponent<MeshFilter>();
            if (meshFilter == null) continue;
            var trail = new MeshTrail(meshFilter.mesh, _trailsPool);
            _trailsPool.Return(trail);
        }
	}

    void OnStartTouch(TouchContainer container)
    {
        var trail = _trailsPool.Obtain(null);
        if (trail == null) return;
        container.TouchListener = trail;
    }

    void Update()
    {
        var args = UpdateArgs.GetDefault();
        _touches.Update(args);
    }

    TouchesHandler _touches;
    Pool<MeshTrail> _trailsPool;
}
