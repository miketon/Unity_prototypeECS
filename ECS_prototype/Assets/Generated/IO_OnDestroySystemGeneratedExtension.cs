namespace Entitas {
    public partial class Pool {
        public ISystem CreateIO_OnDestroySystem() {
            return this.CreateSystem<Event_OnDestroyIOSystem>();
        }
    }
}