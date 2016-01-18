namespace Entitas {
    public partial class Pool {
        public ISystem CreateOnViewSpawnSystem() {
            return this.CreateSystem<OnViewSpawnSystem>();
        }
    }
}