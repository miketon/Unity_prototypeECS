namespace Entitas {
    public partial class Pool {
        public ISystem CreateIO_OnPressSystem() {
            return this.CreateSystem<IO_OnPressSystem>();
        }
    }
}