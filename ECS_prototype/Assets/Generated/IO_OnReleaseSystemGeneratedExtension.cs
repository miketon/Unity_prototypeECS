namespace Entitas {
    public partial class Pool {
        public ISystem CreateIO_OnReleaseSystem() {
            return this.CreateSystem<IO_OnReleaseSystem>();
        }
    }
}