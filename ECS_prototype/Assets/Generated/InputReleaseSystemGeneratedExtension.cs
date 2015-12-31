namespace Entitas {
    public partial class Pool {
        public ISystem CreateInputReleaseSystem() {
            return this.CreateSystem<InputReleaseSystem>();
        }
    }
}