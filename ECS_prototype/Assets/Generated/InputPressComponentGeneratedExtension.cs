using System.Collections.Generic;

namespace Entitas {
    public partial class Entity {
        public InputPressComponent inputPress { get { return (InputPressComponent)GetComponent(ComponentIds.InputPress); } }

        public bool hasInputPress { get { return HasComponent(ComponentIds.InputPress); } }

        static readonly Stack<InputPressComponent> _inputPressComponentPool = new Stack<InputPressComponent>();

        public static void ClearInputPressComponentPool() {
            _inputPressComponentPool.Clear();
        }

        public Entity AddInputPress(bool newBFire, bool newBJump, bool newBRelease) {
            var component = _inputPressComponentPool.Count > 0 ? _inputPressComponentPool.Pop() : new InputPressComponent();
            component.bFire = newBFire;
            component.bJump = newBJump;
            component.bRelease = newBRelease;
            return AddComponent(ComponentIds.InputPress, component);
        }

        public Entity ReplaceInputPress(bool newBFire, bool newBJump, bool newBRelease) {
            var previousComponent = hasInputPress ? inputPress : null;
            var component = _inputPressComponentPool.Count > 0 ? _inputPressComponentPool.Pop() : new InputPressComponent();
            component.bFire = newBFire;
            component.bJump = newBJump;
            component.bRelease = newBRelease;
            ReplaceComponent(ComponentIds.InputPress, component);
            if (previousComponent != null) {
                _inputPressComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity RemoveInputPress() {
            var component = inputPress;
            RemoveComponent(ComponentIds.InputPress);
            _inputPressComponentPool.Push(component);
            return this;
        }
    }

    public partial class Matcher {
        static IMatcher _matcherInputPress;

        public static IMatcher InputPress {
            get {
                if (_matcherInputPress == null) {
                    var matcher = (Matcher)Matcher.AllOf(ComponentIds.InputPress);
                    matcher.componentNames = ComponentIds.componentNames;
                    _matcherInputPress = matcher;
                }

                return _matcherInputPress;
            }
        }
    }
}
