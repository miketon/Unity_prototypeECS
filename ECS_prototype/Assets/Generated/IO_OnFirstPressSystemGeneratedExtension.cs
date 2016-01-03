namespace Entitas {
    public partial class Pool {
        public ISystem CreateIO_OnFirstPressSystem() {
            return this.CreateSystem<IO_OnFirstPressSystem>();
        }
    }
}