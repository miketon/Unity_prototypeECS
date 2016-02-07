using System.Collections.Generic;

namespace Entitas {
    public partial class Entity {
        public stateButtonComponent stateButton { get { return (stateButtonComponent)GetComponent(ComponentIds.stateButton); } }

        public bool hasstateButton { get { return HasComponent(ComponentIds.stateButton); } }

        static readonly Stack<stateButtonComponent> _stateButtonComponentPool = new Stack<stateButtonComponent>();

        public static void ClearstateButtonComponentPool() {
            _stateButtonComponentPool.Clear();
        }

        public Entity AddstateButton(MTON._enum.Button newBMode, MTON._enum.Type newBType) {
            var component = _stateButtonComponentPool.Count > 0 ? _stateButtonComponentPool.Pop() : new stateButtonComponent();
            component.bMode = newBMode;
            component.bType = newBType;
            return AddComponent(ComponentIds.stateButton, component);
        }

        public Entity ReplacestateButton(MTON._enum.Button newBMode, MTON._enum.Type newBType) {
            var previousComponent = hasstateButton ? stateButton : null;
            var component = _stateButtonComponentPool.Count > 0 ? _stateButtonComponentPool.Pop() : new stateButtonComponent();
            component.bMode = newBMode;
            component.bType = newBType;
            ReplaceComponent(ComponentIds.stateButton, component);
            if (previousComponent != null) {
                _stateButtonComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity RemovestateButton() {
            var component = stateButton;
            RemoveComponent(ComponentIds.stateButton);
            _stateButtonComponentPool.Push(component);
            return this;
        }
    }

    public partial class Matcher {
        static IMatcher _matcherstateButton;

        public static IMatcher stateButton {
            get {
                if (_matcherstateButton == null) {
                    var matcher = (Matcher)Matcher.AllOf(ComponentIds.stateButton);
                    matcher.componentNames = ComponentIds.componentNames;
                    _matcherstateButton = matcher;
                }

                return _matcherstateButton;
            }
        }
    }
}
