namespace Entitas {
    public partial class Pool {
        public ISystem CreateEvent_OnDestroyIOSystem() {
            return this.CreateSystem<Event_OnDestroyIOSystem>();
        }
    }
}