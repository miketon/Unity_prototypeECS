namespace Entitas {
    public partial class Pool {
        public ISystem CreateEvent_BUTTONReactSystem() {
            return this.CreateSystem<Event_BUTTONReactSystem>();
        }
    }
}