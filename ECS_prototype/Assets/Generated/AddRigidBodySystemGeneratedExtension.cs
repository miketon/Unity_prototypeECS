namespace Entitas {
    public partial class Pool {
        public ISystem CreateAddRigidBodySystem() {
            return this.CreateSystem<AddRigidBodySystem>();
        }
    }
}