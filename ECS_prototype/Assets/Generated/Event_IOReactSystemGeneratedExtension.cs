namespace Entitas {
    public partial class Pool {
        public ISystem CreateEvent_IOReactSystem() {
            return this.CreateSystem<Event_IOReactSystem>();
        }
    }
}