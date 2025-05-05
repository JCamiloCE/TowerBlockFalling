namespace JCC.Utils.GameplayEventSystem
{
    public interface IEventListener<in TEvent>: IEventListenerBase where TEvent : EventBase
    {
        void OnEvent(TEvent event_data);
    }
}