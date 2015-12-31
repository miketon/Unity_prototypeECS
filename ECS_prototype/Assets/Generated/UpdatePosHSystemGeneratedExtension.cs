namespace Entitas {
    public partial class Pool {
        public ISystem CreateUpdatePosHSystem() {
            return this.CreateSystem<UpdatePosHSystem>();
        }
    }
}