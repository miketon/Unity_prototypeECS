namespace Entitas {
    public partial class Pool {
        public ISystem CreateEvent_StateVReactSystem() {
            return this.CreateSystem<Event_StateVReactSystem>();
        }
    }
}