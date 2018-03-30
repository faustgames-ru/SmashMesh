using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Core;

namespace TouchInput
{
    public class TouchContainer: IPoolOnObtain
    {
        public int Id;
        public TouchInfo Touch;
        public Queue<TouchInfo> History;

        public TouchContainer(){}

        public void Start()
        {
            History.Clear();
        }

        public void Update()
        {
            
        }

        public void End()
        {

        }

        public void PoolOnObtain()
        {
        }
    }
}