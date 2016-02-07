using System.Collections.Generic;

namespace Entitas {
    public partial class Entity {
        public eventFacingComponent eventFacing { get { return (eventFacingComponent)GetComponent(ComponentIds.eventFacing); } }

        public bool haseventFacing { get { return HasComponent(ComponentIds.eventFacing); } }

        static readonly Stack<eventFacingComponent> _eventFacingComponentPool = new Stack<eventFacingComponent>();

        public static void CleareventFacingComponentPool() {
            _eventFacingComponentPool.Clear();
        }

        public Entity AddeventFacing(int newID, MTON._enum.FState newFstate) {
            var component = _eventFacingComponentPool.Count > 0 ? _eventFacingComponentPool.Pop() : new eventFacingComponent();
            component.ID = newID;
            component.fstate = newFstate;
            return AddComponent(ComponentIds.eventFacing, component);
        }

        public Entity ReplaceeventFacing(int newID, MTON._enum.FState newFstate) {
            var previousComponent = haseventFacing ? eventFacing : null;
            var component = _eventFacingComponentPool.Count > 0 ? _eventFacingComponentPool.Pop() : new eventFacingComponent();
            component.ID = newID;
            component.fstate = newFstate;
            ReplaceComponent(ComponentIds.eventFacing, component);
            if (previousComponent != null) {
                _eventFacingComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity RemoveeventFacing() {
            var component = eventFacing;
            RemoveComponent(ComponentIds.eventFacing);
            _eventFacingComponentPool.Push(component);
            return this;
        }
    }

    public partial class Matcher {
        static IMatcher _matchereventFacing;

        public static IMatcher eventFacing {
            get {
                if (_matchereventFacing == null) {
                    var matcher = (Matcher)Matcher.AllOf(ComponentIds.eventFacing);
                    matcher.componentNames = ComponentIds.componentNames;
                    _matchereventFacing = matcher;
                }

                return _matchereventFacing;
            }
        }
    }
}
