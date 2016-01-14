using System.Collections.Generic;

namespace Entitas {
    public partial class Entity {
        public rbodyEventComponent rbodyEvent { get { return (rbodyEventComponent)GetComponent(ComponentIds.rbodyEvent); } }

        public bool hasrbodyEvent { get { return HasComponent(ComponentIds.rbodyEvent); } }

        static readonly Stack<rbodyEventComponent> _rbodyEventComponentPool = new Stack<rbodyEventComponent>();

        public static void ClearrbodyEventComponentPool() {
            _rbodyEventComponentPool.Clear();
        }

        public Entity AddrbodyEvent(MTON._enum.Rbody newRbState) {
            var component = _rbodyEventComponentPool.Count > 0 ? _rbodyEventComponentPool.Pop() : new rbodyEventComponent();
            component.rbState = newRbState;
            return AddComponent(ComponentIds.rbodyEvent, component);
        }

        public Entity ReplacerbodyEvent(MTON._enum.Rbody newRbState) {
            var previousComponent = hasrbodyEvent ? rbodyEvent : null;
            var component = _rbodyEventComponentPool.Count > 0 ? _rbodyEventComponentPool.Pop() : new rbodyEventComponent();
            component.rbState = newRbState;
            ReplaceComponent(ComponentIds.rbodyEvent, component);
            if (previousComponent != null) {
                _rbodyEventComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity RemoverbodyEvent() {
            var component = rbodyEvent;
            RemoveComponent(ComponentIds.rbodyEvent);
            _rbodyEventComponentPool.Push(component);
            return this;
        }
    }

    public partial class Matcher {
        static IMatcher _matcherrbodyEvent;

        public static IMatcher rbodyEvent {
            get {
                if (_matcherrbodyEvent == null) {
                    var matcher = (Matcher)Matcher.AllOf(ComponentIds.rbodyEvent);
                    matcher.componentNames = ComponentIds.componentNames;
                    _matcherrbodyEvent = matcher;
                }

                return _matcherrbodyEvent;
            }
        }
    }
}
