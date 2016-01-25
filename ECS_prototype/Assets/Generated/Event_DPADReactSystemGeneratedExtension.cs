namespace Entitas {
    public partial class Pool {
        public ISystem CreateEvent_DPADReactSystem() {
            return this.CreateSystem<Event_DPADReactSystem>();
        }
    }
}