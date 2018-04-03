using UnityEngine;

namespace Core
{
    public class UpdateArgs
    {
        public float time;
        public float dt;
        public static UpdateArgs GetDefault()
        {
            _default.time = Time.time;
            _default.dt = Time.deltaTime;
            return _default;
        }
        static UpdateArgs _default = new UpdateArgs();
    }
}