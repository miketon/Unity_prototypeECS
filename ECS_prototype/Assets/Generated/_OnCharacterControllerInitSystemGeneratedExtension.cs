namespace Entitas {
    public partial class Pool {
        public ISystem Create_OnCharacterControllerInitSystem() {
            return this.CreateSystem<OnCharacterInitSystem>();
        }
    }
}