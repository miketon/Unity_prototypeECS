using System.Collections.Generic;

namespace Entitas {
    public partial class Entity {
        public OnCollisionEntterComponent onCollisionEntter { get { return (OnCollisionEntterComponent)GetComponent(ComponentIds.OnCollisionEntter); } }

        public bool hasOnCollisionEntter { get { return HasComponent(ComponentIds.OnCollisionEntter); } }

        static readonly Stack<OnCollisionEntterComponent> _onCollisionEntterComponentPool = new Stack<OnCollisionEntterComponent>();

        public static void ClearOnCollisionEntterComponentPool() {
            _onCollisionEntterComponentPool.Clear();
        }

        public Entity AddOnCollisionEntter(UnityEngine.Collision newCollision) {
            var component = _onCollisionEntterComponentPool.Count > 0 ? _onCollisionEntterComponentPool.Pop() : new OnCollisionEntterComponent();
            component.collision = newCollision;
            return AddComponent(ComponentIds.OnCollisionEntter, component);
        }

        public Entity ReplaceOnCollisionEntter(UnityEngine.Collision newCollision) {
            var previousComponent = hasOnCollisionEntter ? onCollisionEntter : null;
            var component = _onCollisionEntterComponentPool.Count > 0 ? _onCollisionEntterComponentPool.Pop() : new OnCollisionEntterComponent();
            component.collision = newCollision;
            ReplaceComponent(ComponentIds.OnCollisionEntter, component);
            if (previousComponent != null) {
                _onCollisionEntterComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity RemoveOnCollisionEntter() {
            var component = onCollisionEntter;
            RemoveComponent(ComponentIds.OnCollisionEntter);
            _onCollisionEntterComponentPool.Push(component);
            return this;
        }
    }

    public partial class Matcher {
        static IMatcher _matcherOnCollisionEntter;

        public static IMatcher OnCollisionEntter {
            get {
                if (_matcherOnCollisionEntter == null) {
                    var matcher = (Matcher)Matcher.AllOf(ComponentIds.OnCollisionEntter);
                    matcher.componentNames = ComponentIds.componentNames;
                    _matcherOnCollisionEntter = matcher;
                }

                return _matcherOnCollisionEntter;
            }
        }
    }
}
