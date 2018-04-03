using System.Collections.Generic;
using Common;
using Core;
using TouchInput;
using UnityEngine;

public class TrailsManagerBehavior : MonoBehaviour 
{
    [Header("Touches")]
    public int TouchesReserve = 8;
    [Header("Touches")]
    public int TouchHistorySizeLimit = 60;
    [Header("Touches")]
    public float TouchHistoryTimeLimit = 1.0f;

    void Start () 
    {
        var touchesCreateArgs = new TouchContainerCreateArgs
        {
            TouchesReserve = TouchesReserve,
            TouchHistorySizeLimit = TouchHistorySizeLimit,
            TouchHistoryTimeLimit = TouchHistoryTimeLimit
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
