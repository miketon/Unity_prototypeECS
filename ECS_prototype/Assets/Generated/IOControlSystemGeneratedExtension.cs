namespace Entitas {
    public partial class Pool {
        public ISystem CreateIOControlSystem() {
            return this.CreateSystem<IOControlSystem>();
        }
    }
}