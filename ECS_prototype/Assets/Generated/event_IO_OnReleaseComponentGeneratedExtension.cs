using System.Collections.Generic;

namespace Entitas {
    public partial class Entity {
        public event_IO_OnReleaseComponent event_IO_OnRelease { get { return (event_IO_OnReleaseComponent)GetComponent(ComponentIds.event_IO_OnRelease); } }

        public bool hasevent_IO_OnRelease { get { return HasComponent(ComponentIds.event_IO_OnRelease); } }

        static readonly Stack<event_IO_OnReleaseComponent> _event_IO_OnReleaseComponentPool = new Stack<event_IO_OnReleaseComponent>();

        public static void Clearevent_IO_OnReleaseComponentPool() {
            _event_IO_OnReleaseComponentPool.Clear();
        }

        public Entity Addevent_IO_OnRelease(MTON._enum.GPAD newGPAD) {
            var component = _event_IO_OnReleaseComponentPool.Count > 0 ? _event_IO_OnReleaseComponentPool.Pop() : new event_IO_OnReleaseComponent();
            component.GPAD = newGPAD;
            return AddComponent(ComponentIds.event_IO_OnRelease, component);
        }

        public Entity Replaceevent_IO_OnRelease(MTON._enum.GPAD newGPAD) {
            var previousComponent = hasevent_IO_OnRelease ? event_IO_OnRelease : null;
            var component = _event_IO_OnReleaseComponentPool.Count > 0 ? _event_IO_OnReleaseComponentPool.Pop() : new event_IO_OnReleaseComponent();
            component.GPAD = newGPAD;
            ReplaceComponent(ComponentIds.event_IO_OnRelease, component);
            if (previousComponent != null) {
                _event_IO_OnReleaseComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity Removeevent_IO_OnRelease() {
            var component = event_IO_OnRelease;
            RemoveComponent(ComponentIds.event_IO_OnRelease);
            _event_IO_OnReleaseComponentPool.Push(component);
            return this;
        }
    }

    public partial class Matcher {
        static IMatcher _matcherevent_IO_OnRelease;

        public static IMatcher event_IO_OnRelease {
            get {
                if (_matcherevent_IO_OnRelease == null) {
                    var matcher = (Matcher)Matcher.AllOf(ComponentIds.event_IO_OnRelease);
                    matcher.componentNames = ComponentIds.componentNames;
                    _matcherevent_IO_OnRelease = matcher;
                }

                return _matcherevent_IO_OnRelease;
            }
        }
    }
}
