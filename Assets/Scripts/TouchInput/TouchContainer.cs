using System.Collections.Generic;
using Core;

namespace TouchInput
{
    public class TouchContainer: IPoolOnObtain, IPoolOnReturn
    {
        public int Id;
        public TouchInfo Touch;
        public Queue<TouchInfo> History = new Queue<TouchInfo>();

        public void Start(TouchInfo touch)
        {
            History.Clear();
            Touch = touch;
            History.Enqueue(touch);

            // todo: call listener
        }

        public void Update(TouchInfo touch)
        {
            
        }

        public void End(TouchInfo touch)
        {

        }

        public void PoolOnObtain()
        {
        }

        public void PoolOnReturn()
        {
            
        }
    }
}