namespace Entitas {
    public partial class Pool {
        public ISystem CreateEvent_VSTATEReactSystem() {
            return this.CreateSystem<Event_VSTATEReactSystem>();
        }
    }
}