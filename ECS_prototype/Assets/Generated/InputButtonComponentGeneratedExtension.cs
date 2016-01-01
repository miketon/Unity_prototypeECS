using System.Collections.Generic;

namespace Entitas {
    public partial class Entity {
        public InputButtonComponent inputButton { get { return (InputButtonComponent)GetComponent(ComponentIds.InputButton); } }

        public bool hasInputButton { get { return HasComponent(ComponentIds.InputButton); } }

        static readonly Stack<InputButtonComponent> _inputButtonComponentPool = new Stack<InputButtonComponent>();

        public static void ClearInputButtonComponentPool() {
            _inputButtonComponentPool.Clear();
        }

        public Entity AddInputButton(float newHAxis, float newVAxis, bool newBFire, bool newBJump, bool newBPress) {
            var component = _inputButtonComponentPool.Count > 0 ? _inputButtonComponentPool.Pop() : new InputButtonComponent();
            component.hAxis = newHAxis;
            component.vAxis = newVAxis;
            component.bFire = newBFire;
            component.bJump = newBJump;
            component.bPress = newBPress;
            return AddComponent(ComponentIds.InputButton, component);
        }

        public Entity ReplaceInputButton(float newHAxis, float newVAxis, bool newBFire, bool newBJump, bool newBPress) {
            var previousComponent = hasInputButton ? inputButton : null;
            var component = _inputButtonComponentPool.Count > 0 ? _inputButtonComponentPool.Pop() : new InputButtonComponent();
            component.hAxis = newHAxis;
            component.vAxis = newVAxis;
            component.bFire = newBFire;
            component.bJump = newBJump;
            component.bPress = newBPress;
            ReplaceComponent(ComponentIds.InputButton, component);
            if (previousComponent != null) {
                _inputButtonComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity RemoveInputButton() {
            var component = inputButton;
            RemoveComponent(ComponentIds.InputButton);
            _inputButtonComponentPool.Push(component);
            return this;
        }
    }

    public partial class Matcher {
        static IMatcher _matcherInputButton;

        public static IMatcher InputButton {
            get {
                if (_matcherInputButton == null) {
                    var matcher = (Matcher)Matcher.AllOf(ComponentIds.InputButton);
                    matcher.componentNames = ComponentIds.componentNames;
                    _matcherInputButton = matcher;
                }

                return _matcherInputButton;
            }
        }
    }
}
