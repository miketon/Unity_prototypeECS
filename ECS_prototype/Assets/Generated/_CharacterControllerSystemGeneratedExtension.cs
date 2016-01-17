namespace Entitas {
    public partial class Pool {
        public ISystem Create_CharacterControllerSystem() {
            return this.CreateSystem<CharacterUpdateSystem>();
        }
    }
}