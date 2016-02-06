namespace Entitas {
    public partial class Pool {
        public ISystem CreateCharacterControllerInitSystem() {
            return this.CreateSystem<CharacterControllerInitSystem>();
        }
    }
}