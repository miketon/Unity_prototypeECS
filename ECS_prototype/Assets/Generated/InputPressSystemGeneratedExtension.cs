namespace Entitas {
    public partial class Pool {
        public ISystem CreateInputPressSystem() {
            return this.CreateSystem<InputPressSystem>();
        }
    }
}