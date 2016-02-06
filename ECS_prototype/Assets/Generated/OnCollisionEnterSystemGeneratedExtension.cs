namespace Entitas {
    public partial class Pool {
        public ISystem CreateOnCollisionEnterSystem() {
            return this.CreateSystem<OnCollisionSystem>();
        }
    }
}