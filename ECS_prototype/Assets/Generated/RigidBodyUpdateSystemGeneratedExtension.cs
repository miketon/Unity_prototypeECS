namespace Entitas {
    public partial class Pool {
        public ISystem CreateRigidBodyUpdateSystem() {
            return this.CreateSystem<RigidBodyUpdateSystem>();
        }
    }
}