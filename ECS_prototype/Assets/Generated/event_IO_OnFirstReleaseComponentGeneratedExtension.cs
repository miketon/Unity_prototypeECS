using System.Collections.Generic;

namespace Entitas {
    public partial class Entity {
        public event_IO_OnFirstReleaseComponent event_IO_OnFirstRelease { get { return (event_IO_OnFirstReleaseComponent)GetComponent(ComponentIds.event_IO_OnFirstRelease); } }

        public bool hasevent_IO_OnFirstRelease { get { return HasComponent(ComponentIds.event_IO_OnFirstRelease); } }

        static readonly Stack<event_IO_OnFirstReleaseComponent> _event_IO_OnFirstReleaseComponentPool = new Stack<event_IO_OnFirstReleaseComponent>();

        public static void Clearevent_IO_OnFirstReleaseComponentPool() {
            _event_IO_OnFirstReleaseComponentPool.Clear();
        }

        public Entity Addevent_IO_OnFirstRelease(int newID) {
            var component = _event_IO_OnFirstReleaseComponentPool.Count > 0 ? _event_IO_OnFirstReleaseComponentPool.Pop() : new event_IO_OnFirstReleaseComponent();
            component.ID = newID;
            return AddComponent(ComponentIds.event_IO_OnFirstRelease, component);
        }

        public Entity Replaceevent_IO_OnFirstRelease(int newID) {
            var previousComponent = hasevent_IO_OnFirstRelease ? event_IO_OnFirstRelease : null;
            var component = _event_IO_OnFirstReleaseComponentPool.Count > 0 ? _event_IO_OnFirstReleaseComponentPool.Pop() : new event_IO_OnFirstReleaseComponent();
            component.ID = newID;
            ReplaceComponent(ComponentIds.event_IO_OnFirstRelease, component);
            if (previousComponent != null) {
                _event_IO_OnFirstReleaseComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity Removeevent_IO_OnFirstRelease() {
            var component = event_IO_OnFirstRelease;
            RemoveComponent(ComponentIds.event_IO_OnFirstRelease);
            _event_IO_OnFirstReleaseComponentPool.Push(component);
            return this;
        }
    }

    public partial class Matcher {
        static IMatcher _matcherevent_IO_OnFirstRelease;

        public static IMatcher event_IO_OnFirstRelease {
            get {
                if (_matcherevent_IO_OnFirstRelease == null) {
                    var matcher = (Matcher)Matcher.AllOf(ComponentIds.event_IO_OnFirstRelease);
                    matcher.componentNames = ComponentIds.componentNames;
                    _matcherevent_IO_OnFirstRelease = matcher;
                }

                return _matcherevent_IO_OnFirstRelease;
            }
        }
    }
}
