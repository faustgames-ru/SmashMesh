namespace TouchInput
{
    public interface ITouchListener
    {
        void Start(TouchContainer touch);
        void Update(TouchContainer touch);
        void End(TouchContainer touch);
    }
}