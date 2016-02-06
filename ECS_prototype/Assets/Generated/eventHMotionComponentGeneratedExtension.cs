using System.Collections.Generic;

namespace Entitas {
    public partial class Entity {
        public eventHMotionComponent eventHMotion { get { return (eventHMotionComponent)GetComponent(ComponentIds.eventHMotion); } }

        public bool haseventHMotion { get { return HasComponent(ComponentIds.eventHMotion); } }

        static readonly Stack<eventHMotionComponent> _eventHMotionComponentPool = new Stack<eventHMotionComponent>();

        public static void CleareventHMotionComponentPool() {
            _eventHMotionComponentPool.Clear();
        }

        public Entity AddeventHMotion(int newID, MTON._enum.HState newHstate) {
            var component = _eventHMotionComponentPool.Count > 0 ? _eventHMotionComponentPool.Pop() : new eventHMotionComponent();
            component.ID = newID;
            component.hstate = newHstate;
            return AddComponent(ComponentIds.eventHMotion, component);
        }

        public Entity ReplaceeventHMotion(int newID, MTON._enum.HState newHstate) {
            var previousComponent = haseventHMotion ? eventHMotion : null;
            var component = _eventHMotionComponentPool.Count > 0 ? _eventHMotionComponentPool.Pop() : new eventHMotionComponent();
            component.ID = newID;
            component.hstate = newHstate;
            ReplaceComponent(ComponentIds.eventHMotion, component);
            if (previousComponent != null) {
                _eventHMotionComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity RemoveeventHMotion() {
            var component = eventHMotion;
            RemoveComponent(ComponentIds.eventHMotion);
            _eventHMotionComponentPool.Push(component);
            return this;
        }
    }

    public partial class Matcher {
        static IMatcher _matchereventHMotion;

        public static IMatcher eventHMotion {
            get {
                if (_matchereventHMotion == null) {
                    var matcher = (Matcher)Matcher.AllOf(ComponentIds.eventHMotion);
                    matcher.componentNames = ComponentIds.componentNames;
                    _matchereventHMotion = matcher;
                }

                return _matchereventHMotion;
            }
        }
    }
}
