using System.Collections.Generic;

namespace Entitas {
    public partial class Entity {
        public eventOnCollisionComponent eventOnCollision { get { return (eventOnCollisionComponent)GetComponent(ComponentIds.eventOnCollision); } }

        public bool haseventOnCollision { get { return HasComponent(ComponentIds.eventOnCollision); } }

        static readonly Stack<eventOnCollisionComponent> _eventOnCollisionComponentPool = new Stack<eventOnCollisionComponent>();

        public static void CleareventOnCollisionComponentPool() {
            _eventOnCollisionComponentPool.Clear();
        }

        public Entity AddeventOnCollision(int newID, UnityEngine.Collision newCollision) {
            var component = _eventOnCollisionComponentPool.Count > 0 ? _eventOnCollisionComponentPool.Pop() : new eventOnCollisionComponent();
            component.ID = newID;
            component.collision = newCollision;
            return AddComponent(ComponentIds.eventOnCollision, component);
        }

        public Entity ReplaceeventOnCollision(int newID, UnityEngine.Collision newCollision) {
            var previousComponent = haseventOnCollision ? eventOnCollision : null;
            var component = _eventOnCollisionComponentPool.Count > 0 ? _eventOnCollisionComponentPool.Pop() : new eventOnCollisionComponent();
            component.ID = newID;
            component.collision = newCollision;
            ReplaceComponent(ComponentIds.eventOnCollision, component);
            if (previousComponent != null) {
                _eventOnCollisionComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity RemoveeventOnCollision() {
            var component = eventOnCollision;
            RemoveComponent(ComponentIds.eventOnCollision);
            _eventOnCollisionComponentPool.Push(component);
            return this;
        }
    }

    public partial class Matcher {
        static IMatcher _matchereventOnCollision;

        public static IMatcher eventOnCollision {
            get {
                if (_matchereventOnCollision == null) {
                    var matcher = (Matcher)Matcher.AllOf(ComponentIds.eventOnCollision);
                    matcher.componentNames = ComponentIds.componentNames;
                    _matchereventOnCollision = matcher;
                }

                return _matchereventOnCollision;
            }
        }
    }
}
