namespace Entitas {
    public partial class Pool {
        public ISystem Create_OnViewSpawnSystem() {
            return this.CreateSystem<OnViewSpawnSystem>();
        }
    }
}