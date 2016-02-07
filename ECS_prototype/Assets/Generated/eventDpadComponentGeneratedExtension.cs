using System.Collections.Generic;

namespace Entitas {
    public partial class Entity {
        public eventDpadComponent eventDpad { get { return (eventDpadComponent)GetComponent(ComponentIds.eventDpad); } }

        public bool haseventDpad { get { return HasComponent(ComponentIds.eventDpad); } }

        static readonly Stack<eventDpadComponent> _eventDpadComponentPool = new Stack<eventDpadComponent>();

        public static void CleareventDpadComponentPool() {
            _eventDpadComponentPool.Clear();
        }

        public Entity AddeventDpad(int newID, MTON._enum.Dirn newDpad, float newMdir) {
            var component = _eventDpadComponentPool.Count > 0 ? _eventDpadComponentPool.Pop() : new eventDpadComponent();
            component.ID = newID;
            component.dpad = newDpad;
            component.mdir = newMdir;
            return AddComponent(ComponentIds.eventDpad, component);
        }

        public Entity ReplaceeventDpad(int newID, MTON._enum.Dirn newDpad, float newMdir) {
            var previousComponent = haseventDpad ? eventDpad : null;
            var component = _eventDpadComponentPool.Count > 0 ? _eventDpadComponentPool.Pop() : new eventDpadComponent();
            component.ID = newID;
            component.dpad = newDpad;
            component.mdir = newMdir;
            ReplaceComponent(ComponentIds.eventDpad, component);
            if (previousComponent != null) {
                _eventDpadComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity RemoveeventDpad() {
            var component = eventDpad;
            RemoveComponent(ComponentIds.eventDpad);
            _eventDpadComponentPool.Push(component);
            return this;
        }
    }

    public partial class Matcher {
        static IMatcher _matchereventDpad;

        public static IMatcher eventDpad {
            get {
                if (_matchereventDpad == null) {
                    var matcher = (Matcher)Matcher.AllOf(ComponentIds.eventDpad);
                    matcher.componentNames = ComponentIds.componentNames;
                    _matchereventDpad = matcher;
                }

                return _matchereventDpad;
            }
        }
    }
}
