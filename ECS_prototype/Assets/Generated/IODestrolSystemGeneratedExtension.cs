namespace Entitas {
    public partial class Pool {
        public ISystem CreateIODestrolSystem() {
            return this.CreateSystem<IODestrolSystem>();
        }
    }
}