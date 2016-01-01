namespace Entitas {
    public partial class Pool {
        public ISystem CreateIO_ForceSystem() {
            return this.CreateSystem<IO_ForceSystem>();
        }
    }
}