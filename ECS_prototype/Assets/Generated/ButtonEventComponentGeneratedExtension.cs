using System.Collections.Generic;

namespace Entitas {
    public partial class Entity {
        public ButtonEventComponent buttonEvent { get { return (ButtonEventComponent)GetComponent(ComponentIds.ButtonEvent); } }

        public bool hasButtonEvent { get { return HasComponent(ComponentIds.ButtonEvent); } }

        static readonly Stack<ButtonEventComponent> _buttonEventComponentPool = new Stack<ButtonEventComponent>();

        public static void ClearButtonEventComponentPool() {
            _buttonEventComponentPool.Clear();
        }

        public Entity AddButtonEvent(MTON._enum.Button newBMode, MTON._enum.Type newBType) {
            var component = _buttonEventComponentPool.Count > 0 ? _buttonEventComponentPool.Pop() : new ButtonEventComponent();
            component.bMode = newBMode;
            component.bType = newBType;
            return AddComponent(ComponentIds.ButtonEvent, component);
        }

        public Entity ReplaceButtonEvent(MTON._enum.Button newBMode, MTON._enum.Type newBType) {
            var previousComponent = hasButtonEvent ? buttonEvent : null;
            var component = _buttonEventComponentPool.Count > 0 ? _buttonEventComponentPool.Pop() : new ButtonEventComponent();
            component.bMode = newBMode;
            component.bType = newBType;
            ReplaceComponent(ComponentIds.ButtonEvent, component);
            if (previousComponent != null) {
                _buttonEventComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity RemoveButtonEvent() {
            var component = buttonEvent;
            RemoveComponent(ComponentIds.ButtonEvent);
            _buttonEventComponentPool.Push(component);
            return this;
        }
    }

    public partial class Matcher {
        static IMatcher _matcherButtonEvent;

        public static IMatcher ButtonEvent {
            get {
                if (_matcherButtonEvent == null) {
                    var matcher = (Matcher)Matcher.AllOf(ComponentIds.ButtonEvent);
                    matcher.componentNames = ComponentIds.componentNames;
                    _matcherButtonEvent = matcher;
                }

                return _matcherButtonEvent;
            }
        }
    }
}
