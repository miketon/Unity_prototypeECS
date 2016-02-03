namespace Entitas {
    public partial class Pool {
        public ISystem CreateCharacterUpdateSystem() {
            return this.CreateSystem<CharacterControllerInitSystem>();
        }
    }
}