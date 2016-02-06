namespace Entitas {
    public partial class Pool {
        public ISystem CreateEvent_StateHReactSystem() {
            return this.CreateSystem<Event_StateHReactSystem>();
        }
    }
}