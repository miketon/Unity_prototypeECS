namespace Entitas {
    public partial class Pool {
        public ISystem CreateOnAddViewSystem() {
            return this.CreateSystem<OnAddViewSystem>();
        }
    }
}