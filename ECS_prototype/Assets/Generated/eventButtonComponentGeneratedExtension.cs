using System.Collections.Generic;

namespace Entitas {
    public partial class Entity {
        public eventButtonComponent eventButton { get { return (eventButtonComponent)GetComponent(ComponentIds.eventButton); } }

        public bool haseventButton { get { return HasComponent(ComponentIds.eventButton); } }

        static readonly Stack<eventButtonComponent> _eventButtonComponentPool = new Stack<eventButtonComponent>();

        public static void CleareventButtonComponentPool() {
            _eventButtonComponentPool.Clear();
        }

        public Entity AddeventButton(int newID, MTON._enum.Button newBMode, MTON._enum.Type newBType) {
            var component = _eventButtonComponentPool.Count > 0 ? _eventButtonComponentPool.Pop() : new eventButtonComponent();
            component.ID = newID;
            component.bMode = newBMode;
            component.bType = newBType;
            return AddComponent(ComponentIds.eventButton, component);
        }

        public Entity ReplaceeventButton(int newID, MTON._enum.Button newBMode, MTON._enum.Type newBType) {
            var previousComponent = haseventButton ? eventButton : null;
            var component = _eventButtonComponentPool.Count > 0 ? _eventButtonComponentPool.Pop() : new eventButtonComponent();
            component.ID = newID;
            component.bMode = newBMode;
            component.bType = newBType;
            ReplaceComponent(ComponentIds.eventButton, component);
            if (previousComponent != null) {
                _eventButtonComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity RemoveeventButton() {
            var component = eventButton;
            RemoveComponent(ComponentIds.eventButton);
            _eventButtonComponentPool.Push(component);
            return this;
        }
    }

    public partial class Matcher {
        static IMatcher _matchereventButton;

        public static IMatcher eventButton {
            get {
                if (_matchereventButton == null) {
                    var matcher = (Matcher)Matcher.AllOf(ComponentIds.eventButton);
                    matcher.componentNames = ComponentIds.componentNames;
                    _matchereventButton = matcher;
                }

                return _matchereventButton;
            }
        }
    }
}
