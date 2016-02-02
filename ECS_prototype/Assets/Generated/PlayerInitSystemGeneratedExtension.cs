namespace Entitas {
    public partial class Pool {
        public ISystem CreatePlayerInitSystem() {
            return this.CreateSystem<PlayerInitSystem>();
        }
    }
}