using System.Collections.Generic;

namespace Entitas {
    public partial class Entity {
        public eventCrouchComponent eventCrouch { get { return (eventCrouchComponent)GetComponent(ComponentIds.eventCrouch); } }

        public bool haseventCrouch { get { return HasComponent(ComponentIds.eventCrouch); } }

        static readonly Stack<eventCrouchComponent> _eventCrouchComponentPool = new Stack<eventCrouchComponent>();

        public static void CleareventCrouchComponentPool() {
            _eventCrouchComponentPool.Clear();
        }

        public Entity AddeventCrouch(int newID, bool newBCrouch) {
            var component = _eventCrouchComponentPool.Count > 0 ? _eventCrouchComponentPool.Pop() : new eventCrouchComponent();
            component.ID = newID;
            component.bCrouch = newBCrouch;
            return AddComponent(ComponentIds.eventCrouch, component);
        }

        public Entity ReplaceeventCrouch(int newID, bool newBCrouch) {
            var previousComponent = haseventCrouch ? eventCrouch : null;
            var component = _eventCrouchComponentPool.Count > 0 ? _eventCrouchComponentPool.Pop() : new eventCrouchComponent();
            component.ID = newID;
            component.bCrouch = newBCrouch;
            ReplaceComponent(ComponentIds.eventCrouch, component);
            if (previousComponent != null) {
                _eventCrouchComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity RemoveeventCrouch() {
            var component = eventCrouch;
            RemoveComponent(ComponentIds.eventCrouch);
            _eventCrouchComponentPool.Push(component);
            return this;
        }
    }

    public partial class Matcher {
        static IMatcher _matchereventCrouch;

        public static IMatcher eventCrouch {
            get {
                if (_matchereventCrouch == null) {
                    var matcher = (Matcher)Matcher.AllOf(ComponentIds.eventCrouch);
                    matcher.componentNames = ComponentIds.componentNames;
                    _matchereventCrouch = matcher;
                }

                return _matchereventCrouch;
            }
        }
    }
}
