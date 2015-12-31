namespace Entitas {
    public partial class Pool {
        public ISystem CreateLevelSystem() {
            return this.CreateSystem<LevelSystem>();
        }
    }
}