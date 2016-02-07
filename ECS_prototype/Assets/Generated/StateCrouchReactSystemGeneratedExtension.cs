namespace Entitas {
    public partial class Pool {
        public ISystem CreateStateCrouchReactSystem() {
            return this.CreateSystem<StateCrouchReactSystem>();
        }
    }
}