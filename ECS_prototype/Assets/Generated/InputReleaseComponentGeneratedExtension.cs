using System.Collections.Generic;

namespace Entitas {
    public partial class Entity {
        public InputReleaseComponent inputRelease { get { return (InputReleaseComponent)GetComponent(ComponentIds.InputRelease); } }

        public bool hasInputRelease { get { return HasComponent(ComponentIds.InputRelease); } }

        static readonly Stack<InputReleaseComponent> _inputReleaseComponentPool = new Stack<InputReleaseComponent>();

        public static void ClearInputReleaseComponentPool() {
            _inputReleaseComponentPool.Clear();
        }

        public Entity AddInputRelease(bool newBRelease) {
            var component = _inputReleaseComponentPool.Count > 0 ? _inputReleaseComponentPool.Pop() : new InputReleaseComponent();
            component.bRelease = newBRelease;
            return AddComponent(ComponentIds.InputRelease, component);
        }

        public Entity ReplaceInputRelease(bool newBRelease) {
            var previousComponent = hasInputRelease ? inputRelease : null;
            var component = _inputReleaseComponentPool.Count > 0 ? _inputReleaseComponentPool.Pop() : new InputReleaseComponent();
            component.bRelease = newBRelease;
            ReplaceComponent(ComponentIds.InputRelease, component);
            if (previousComponent != null) {
                _inputReleaseComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity RemoveInputRelease() {
            var component = inputRelease;
            RemoveComponent(ComponentIds.InputRelease);
            _inputReleaseComponentPool.Push(component);
            return this;
        }
    }

    public partial class Matcher {
        static IMatcher _matcherInputRelease;

        public static IMatcher InputRelease {
            get {
                if (_matcherInputRelease == null) {
                    var matcher = (Matcher)Matcher.AllOf(ComponentIds.InputRelease);
                    matcher.componentNames = ComponentIds.componentNames;
                    _matcherInputRelease = matcher;
                }

                return _matcherInputRelease;
            }
        }
    }
}
