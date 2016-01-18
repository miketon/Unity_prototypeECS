namespace Entitas {
    public partial class Pool {
        public ISystem CreateOnCharacterInitSystem() {
            return this.CreateSystem<OnCharacterInitSystem>();
        }
    }
}