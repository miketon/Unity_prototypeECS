namespace Entitas {
    public partial class Pool {
        public ISystem CreateRbodyEventSystem() {
            return this.CreateSystem<RbodyEventSystem>();
        }
    }
}