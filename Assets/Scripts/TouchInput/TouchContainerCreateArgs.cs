using UnityEngine;

namespace TouchInput
{
    public class TouchContainerCreateArgs
    {
        public int TouchesReserve;
        public int TouchHistorySizeLimit;
        public float TouchHistoryTimeLimit;
        public Camera Camera;
    }
}