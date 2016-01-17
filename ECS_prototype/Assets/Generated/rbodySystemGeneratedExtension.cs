namespace Entitas {
    public partial class Pool {
        public ISystem CreaterbodySystem() {
            return this.CreateSystem<RigidBodyUpdateSystem>();
        }
    }
}