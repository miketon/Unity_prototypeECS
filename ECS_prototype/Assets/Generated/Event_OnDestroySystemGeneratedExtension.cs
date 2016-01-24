namespace Entitas {
    public partial class Pool {
        public ISystem CreateEvent_OnDestroySystem() {
            return this.CreateSystem<Event_OnDestroySystem>();
        }
    }
}