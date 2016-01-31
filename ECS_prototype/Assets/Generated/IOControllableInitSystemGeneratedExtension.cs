namespace Entitas {
    public partial class Pool {
        public ISystem CreateIOControllableInitSystem() {
            return this.CreateSystem<IOControllableInitSystem>();
        }
    }
}