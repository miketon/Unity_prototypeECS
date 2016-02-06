using System.Collections.Generic;

namespace Entitas {
    public partial class Entity {
        public eventVMotionComponent eventVMotion { get { return (eventVMotionComponent)GetComponent(ComponentIds.eventVMotion); } }

        public bool haseventVMotion { get { return HasComponent(ComponentIds.eventVMotion); } }

        static readonly Stack<eventVMotionComponent> _eventVMotionComponentPool = new Stack<eventVMotionComponent>();

        public static void CleareventVMotionComponentPool() {
            _eventVMotionComponentPool.Clear();
        }

        public Entity AddeventVMotion(int newID, MTON._enum.VState newVstate) {
            var component = _eventVMotionComponentPool.Count > 0 ? _eventVMotionComponentPool.Pop() : new eventVMotionComponent();
            component.ID = newID;
            component.vstate = newVstate;
            return AddComponent(ComponentIds.eventVMotion, component);
        }

        public Entity ReplaceeventVMotion(int newID, MTON._enum.VState newVstate) {
            var previousComponent = haseventVMotion ? eventVMotion : null;
            var component = _eventVMotionComponentPool.Count > 0 ? _eventVMotionComponentPool.Pop() : new eventVMotionComponent();
            component.ID = newID;
            component.vstate = newVstate;
            ReplaceComponent(ComponentIds.eventVMotion, component);
            if (previousComponent != null) {
                _eventVMotionComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity RemoveeventVMotion() {
            var component = eventVMotion;
            RemoveComponent(ComponentIds.eventVMotion);
            _eventVMotionComponentPool.Push(component);
            return this;
        }
    }

    public partial class Matcher {
        static IMatcher _matchereventVMotion;

        public static IMatcher eventVMotion {
            get {
                if (_matchereventVMotion == null) {
                    var matcher = (Matcher)Matcher.AllOf(ComponentIds.eventVMotion);
                    matcher.componentNames = ComponentIds.componentNames;
                    _matchereventVMotion = matcher;
                }

                return _matchereventVMotion;
            }
        }
    }
}
