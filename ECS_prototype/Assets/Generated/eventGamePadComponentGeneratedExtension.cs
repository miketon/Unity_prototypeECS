using System.Collections.Generic;

namespace Entitas {
    public partial class Entity {
        public eventGamePadComponent eventGamePad { get { return (eventGamePadComponent)GetComponent(ComponentIds.eventGamePad); } }

        public bool haseventGamePad { get { return HasComponent(ComponentIds.eventGamePad); } }

        static readonly Stack<eventGamePadComponent> _eventGamePadComponentPool = new Stack<eventGamePadComponent>();

        public static void CleareventGamePadComponentPool() {
            _eventGamePadComponentPool.Clear();
        }

        public Entity AddeventGamePad(int newID, MTON._enum.GPAD newGpad) {
            var component = _eventGamePadComponentPool.Count > 0 ? _eventGamePadComponentPool.Pop() : new eventGamePadComponent();
            component.ID = newID;
            component.gpad = newGpad;
            return AddComponent(ComponentIds.eventGamePad, component);
        }

        public Entity ReplaceeventGamePad(int newID, MTON._enum.GPAD newGpad) {
            var previousComponent = haseventGamePad ? eventGamePad : null;
            var component = _eventGamePadComponentPool.Count > 0 ? _eventGamePadComponentPool.Pop() : new eventGamePadComponent();
            component.ID = newID;
            component.gpad = newGpad;
            ReplaceComponent(ComponentIds.eventGamePad, component);
            if (previousComponent != null) {
                _eventGamePadComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity RemoveeventGamePad() {
            var component = eventGamePad;
            RemoveComponent(ComponentIds.eventGamePad);
            _eventGamePadComponentPool.Push(component);
            return this;
        }
    }

    public partial class Matcher {
        static IMatcher _matchereventGamePad;

        public static IMatcher eventGamePad {
            get {
                if (_matchereventGamePad == null) {
                    var matcher = (Matcher)Matcher.AllOf(ComponentIds.eventGamePad);
                    matcher.componentNames = ComponentIds.componentNames;
                    _matchereventGamePad = matcher;
                }

                return _matchereventGamePad;
            }
        }
    }
}
