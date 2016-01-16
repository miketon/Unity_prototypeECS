namespace Entitas {
    public partial class Pool {
        public ISystem Create_OnLevelInitSystem() {
            return this.CreateSystem<_OnLevelInitSystem>();
        }
    }
}