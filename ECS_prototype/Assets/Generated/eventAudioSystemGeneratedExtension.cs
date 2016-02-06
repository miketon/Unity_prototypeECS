namespace Entitas {
    public partial class Pool {
        public ISystem CreateeventAudioSystem() {
            return this.CreateSystem<eventAudioSystem>();
        }
    }
}