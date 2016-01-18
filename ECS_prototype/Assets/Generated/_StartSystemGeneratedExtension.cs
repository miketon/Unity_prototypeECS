namespace Entitas {
    public partial class Pool {
        public ISystem Create_StartSystem() {
            return this.CreateSystem<_StartSystem>();
        }
    }
}