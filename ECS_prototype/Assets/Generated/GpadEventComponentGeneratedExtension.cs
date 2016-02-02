using System.Collections.Generic;

namespace Entitas {
    public partial class Entity {
        public GpadEventComponent gpadEvent { get { return (GpadEventComponent)GetComponent(ComponentIds.GpadEvent); } }

        public bool hasGpadEvent { get { return HasComponent(ComponentIds.GpadEvent); } }

        static readonly Stack<GpadEventComponent> _gpadEventComponentPool = new Stack<GpadEventComponent>();

        public static void ClearGpadEventComponentPool() {
            _gpadEventComponentPool.Clear();
        }

        public Entity AddGpadEvent(int newID, MTON._enum.GPAD newGpad) {
            var component = _gpadEventComponentPool.Count > 0 ? _gpadEventComponentPool.Pop() : new GpadEventComponent();
            component.ID = newID;
            component.gpad = newGpad;
            return AddComponent(ComponentIds.GpadEvent, component);
        }

        public Entity ReplaceGpadEvent(int newID, MTON._enum.GPAD newGpad) {
            var previousComponent = hasGpadEvent ? gpadEvent : null;
            var component = _gpadEventComponentPool.Count > 0 ? _gpadEventComponentPool.Pop() : new GpadEventComponent();
            component.ID = newID;
            component.gpad = newGpad;
            ReplaceComponent(ComponentIds.GpadEvent, component);
            if (previousComponent != null) {
                _gpadEventComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity RemoveGpadEvent() {
            var component = gpadEvent;
            RemoveComponent(ComponentIds.GpadEvent);
            _gpadEventComponentPool.Push(component);
            return this;
        }
    }

    public partial class Matcher {
        static IMatcher _matcherGpadEvent;

        public static IMatcher GpadEvent {
            get {
                if (_matcherGpadEvent == null) {
                    var matcher = (Matcher)Matcher.AllOf(ComponentIds.GpadEvent);
                    matcher.componentNames = ComponentIds.componentNames;
                    _matcherGpadEvent = matcher;
                }

                return _matcherGpadEvent;
            }
        }
    }
}
