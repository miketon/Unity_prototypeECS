using System.Collections.Generic;

namespace Entitas {
    public partial class Entity {
        public VstateEventComponent vstateEvent { get { return (VstateEventComponent)GetComponent(ComponentIds.VstateEvent); } }

        public bool hasVstateEvent { get { return HasComponent(ComponentIds.VstateEvent); } }

        static readonly Stack<VstateEventComponent> _vstateEventComponentPool = new Stack<VstateEventComponent>();

        public static void ClearVstateEventComponentPool() {
            _vstateEventComponentPool.Clear();
        }

        public Entity AddVstateEvent(MTON._enum.VState newVstate) {
            var component = _vstateEventComponentPool.Count > 0 ? _vstateEventComponentPool.Pop() : new VstateEventComponent();
            component.vstate = newVstate;
            return AddComponent(ComponentIds.VstateEvent, component);
        }

        public Entity ReplaceVstateEvent(MTON._enum.VState newVstate) {
            var previousComponent = hasVstateEvent ? vstateEvent : null;
            var component = _vstateEventComponentPool.Count > 0 ? _vstateEventComponentPool.Pop() : new VstateEventComponent();
            component.vstate = newVstate;
            ReplaceComponent(ComponentIds.VstateEvent, component);
            if (previousComponent != null) {
                _vstateEventComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity RemoveVstateEvent() {
            var component = vstateEvent;
            RemoveComponent(ComponentIds.VstateEvent);
            _vstateEventComponentPool.Push(component);
            return this;
        }
    }

    public partial class Matcher {
        static IMatcher _matcherVstateEvent;

        public static IMatcher VstateEvent {
            get {
                if (_matcherVstateEvent == null) {
                    var matcher = (Matcher)Matcher.AllOf(ComponentIds.VstateEvent);
                    matcher.componentNames = ComponentIds.componentNames;
                    _matcherVstateEvent = matcher;
                }

                return _matcherVstateEvent;
            }
        }
    }
}
