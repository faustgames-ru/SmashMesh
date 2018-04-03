using System.Collections.Generic;
using Core;

namespace TouchInput
{
    public class TouchContainer: IPoolOnObtain, IPoolOnReturn
    {
        public int Id;
        public TouchInfo Touch;
        public Queue<TouchInfo> History;
        public ITouchListener TouchListener;
        public TouchContainerCreateArgs CreateArgs { get; }

        public TouchContainer(TouchContainerCreateArgs e)
        {
            CreateArgs = e;
            History = new Queue<TouchInfo>(e.TouchHistorySizeLimit);
        }

        public void Start(UpdateArgs e)
        {
            History.Enqueue(Touch);
            TouchListener?.Start(this);
        }

        public void Update(UpdateArgs e)
        {
            if (History.Count >= CreateArgs.TouchHistorySizeLimit)
            {
                History.Dequeue();
            }
            var timeLimit = e.time - CreateArgs.TouchHistoryTimeLimit;
            while (true)
            {
                if (History.Count == 0) break;
                if (History.Peek().time > timeLimit) break;
                History.Dequeue();
            }
            History.Enqueue(Touch);
            // todo: limit history with time & count
            TouchListener?.Update(this);
        }

        public void End(UpdateArgs e)
        {
            History.Enqueue(Touch);
            TouchListener?.End(this);
        }

        public void PoolOnObtain()
        {
            History.Clear();
            TouchListener = null;
        }

        public void PoolOnReturn()
        {
            History.Clear();
            TouchListener = null;
        }
    }
}