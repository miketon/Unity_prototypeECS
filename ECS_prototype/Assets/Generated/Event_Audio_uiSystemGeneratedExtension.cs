namespace Entitas {
    public partial class Pool {
        public ISystem CreateEvent_Audio_uiSystem() {
            return this.CreateSystem<Event_Audio_uiSystem>();
        }
    }
}