using System.Collections.Generic;

namespace Entitas {
    public partial class Entity {
        public event_IO_OnFirstPressComponent event_IO_OnFirstPress { get { return (event_IO_OnFirstPressComponent)GetComponent(ComponentIds.event_IO_OnFirstPress); } }

        public bool hasevent_IO_OnFirstPress { get { return HasComponent(ComponentIds.event_IO_OnFirstPress); } }

        static readonly Stack<event_IO_OnFirstPressComponent> _event_IO_OnFirstPressComponentPool = new Stack<event_IO_OnFirstPressComponent>();

        public static void Clearevent_IO_OnFirstPressComponentPool() {
            _event_IO_OnFirstPressComponentPool.Clear();
        }

        public Entity Addevent_IO_OnFirstPress(int newID) {
            var component = _event_IO_OnFirstPressComponentPool.Count > 0 ? _event_IO_OnFirstPressComponentPool.Pop() : new event_IO_OnFirstPressComponent();
            component.ID = newID;
            return AddComponent(ComponentIds.event_IO_OnFirstPress, component);
        }

        public Entity Replaceevent_IO_OnFirstPress(int newID) {
            var previousComponent = hasevent_IO_OnFirstPress ? event_IO_OnFirstPress : null;
            var component = _event_IO_OnFirstPressComponentPool.Count > 0 ? _event_IO_OnFirstPressComponentPool.Pop() : new event_IO_OnFirstPressComponent();
            component.ID = newID;
            ReplaceComponent(ComponentIds.event_IO_OnFirstPress, component);
            if (previousComponent != null) {
                _event_IO_OnFirstPressComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity Removeevent_IO_OnFirstPress() {
            var component = event_IO_OnFirstPress;
            RemoveComponent(ComponentIds.event_IO_OnFirstPress);
            _event_IO_OnFirstPressComponentPool.Push(component);
            return this;
        }
    }

    public partial class Matcher {
        static IMatcher _matcherevent_IO_OnFirstPress;

        public static IMatcher event_IO_OnFirstPress {
            get {
                if (_matcherevent_IO_OnFirstPress == null) {
                    var matcher = (Matcher)Matcher.AllOf(ComponentIds.event_IO_OnFirstPress);
                    matcher.componentNames = ComponentIds.componentNames;
                    _matcherevent_IO_OnFirstPress = matcher;
                }

                return _matcherevent_IO_OnFirstPress;
            }
        }
    }
}
