using System.Collections.Generic;

namespace Entitas {
    public partial class Entity {
        public RbodyEventComponent rbodyEvent { get { return (RbodyEventComponent)GetComponent(ComponentIds.RbodyEvent); } }

        public bool hasRbodyEvent { get { return HasComponent(ComponentIds.RbodyEvent); } }

        static readonly Stack<RbodyEventComponent> _rbodyEventComponentPool = new Stack<RbodyEventComponent>();

        public static void ClearRbodyEventComponentPool() {
            _rbodyEventComponentPool.Clear();
        }

        public Entity AddRbodyEvent(UnityEngine.CharacterController newCc, MTON._enum.VState newVState) {
            var component = _rbodyEventComponentPool.Count > 0 ? _rbodyEventComponentPool.Pop() : new RbodyEventComponent();
            component.cc = newCc;
            component.vState = newVState;
            return AddComponent(ComponentIds.RbodyEvent, component);
        }

        public Entity ReplaceRbodyEvent(UnityEngine.CharacterController newCc, MTON._enum.VState newVState) {
            var previousComponent = hasRbodyEvent ? rbodyEvent : null;
            var component = _rbodyEventComponentPool.Count > 0 ? _rbodyEventComponentPool.Pop() : new RbodyEventComponent();
            component.cc = newCc;
            component.vState = newVState;
            ReplaceComponent(ComponentIds.RbodyEvent, component);
            if (previousComponent != null) {
                _rbodyEventComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity RemoveRbodyEvent() {
            var component = rbodyEvent;
            RemoveComponent(ComponentIds.RbodyEvent);
            _rbodyEventComponentPool.Push(component);
            return this;
        }
    }

    public partial class Matcher {
        static IMatcher _matcherRbodyEvent;

        public static IMatcher RbodyEvent {
            get {
                if (_matcherRbodyEvent == null) {
                    var matcher = (Matcher)Matcher.AllOf(ComponentIds.RbodyEvent);
                    matcher.componentNames = ComponentIds.componentNames;
                    _matcherRbodyEvent = matcher;
                }

                return _matcherRbodyEvent;
            }
        }
    }
}
