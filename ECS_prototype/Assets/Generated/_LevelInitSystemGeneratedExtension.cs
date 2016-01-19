namespace Entitas {
    public partial class Pool {
        public ISystem Create_LevelInitSystem() {
            return this.CreateSystem<_LevelInitSystem>();
        }
    }
}