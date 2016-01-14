using System.Collections.Generic;

namespace Entitas {
    public partial class Entity {
        public OnCollisionEnterComponent onCollisionEnter { get { return (OnCollisionEnterComponent)GetComponent(ComponentIds.OnCollisionEnter); } }

        public bool hasOnCollisionEnter { get { return HasComponent(ComponentIds.OnCollisionEnter); } }

        static readonly Stack<OnCollisionEnterComponent> _onCollisionEnterComponentPool = new Stack<OnCollisionEnterComponent>();

        public static void ClearOnCollisionEnterComponentPool() {
            _onCollisionEnterComponentPool.Clear();
        }

        public Entity AddOnCollisionEnter(UnityEngine.Collision newCollision) {
            var component = _onCollisionEnterComponentPool.Count > 0 ? _onCollisionEnterComponentPool.Pop() : new OnCollisionEnterComponent();
            component.collision = newCollision;
            return AddComponent(ComponentIds.OnCollisionEnter, component);
        }

        public Entity ReplaceOnCollisionEnter(UnityEngine.Collision newCollision) {
            var previousComponent = hasOnCollisionEnter ? onCollisionEnter : null;
            var component = _onCollisionEnterComponentPool.Count > 0 ? _onCollisionEnterComponentPool.Pop() : new OnCollisionEnterComponent();
            component.collision = newCollision;
            ReplaceComponent(ComponentIds.OnCollisionEnter, component);
            if (previousComponent != null) {
                _onCollisionEnterComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity RemoveOnCollisionEnter() {
            var component = onCollisionEnter;
            RemoveComponent(ComponentIds.OnCollisionEnter);
            _onCollisionEnterComponentPool.Push(component);
            return this;
        }
    }

    public partial class Matcher {
        static IMatcher _matcherOnCollisionEnter;

        public static IMatcher OnCollisionEnter {
            get {
                if (_matcherOnCollisionEnter == null) {
                    var matcher = (Matcher)Matcher.AllOf(ComponentIds.OnCollisionEnter);
                    matcher.componentNames = ComponentIds.componentNames;
                    _matcherOnCollisionEnter = matcher;
                }

                return _matcherOnCollisionEnter;
            }
        }
    }
}
