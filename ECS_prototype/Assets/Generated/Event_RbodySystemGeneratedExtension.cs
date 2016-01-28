namespace Entitas {
    public partial class Pool {
        public ISystem CreateEvent_RbodySystem() {
            return this.CreateSystem<Event_RbodySystem>();
        }
    }
}