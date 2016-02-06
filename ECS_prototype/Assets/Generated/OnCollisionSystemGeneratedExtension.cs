namespace Entitas {
    public partial class Pool {
        public ISystem CreateOnCollisionSystem() {
            return this.CreateSystem<OnCollisionSystem>();
        }
    }
}