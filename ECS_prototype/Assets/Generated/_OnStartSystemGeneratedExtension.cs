namespace Entitas {
    public partial class Pool {
        public ISystem Create_OnStartSystem() {
            return this.CreateSystem<_OnStartSystem>();
        }
    }
}