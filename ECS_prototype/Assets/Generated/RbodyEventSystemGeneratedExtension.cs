namespace Entitas {
    public partial class Pool {
        public ISystem CreateRbodyEventSystem() {
            return this.CreateSystem<Event_RbodySystem>();
        }
    }
}