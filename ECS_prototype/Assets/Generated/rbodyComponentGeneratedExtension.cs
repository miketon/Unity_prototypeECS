using System.Collections.Generic;

namespace Entitas {
    public partial class Entity {
        public rbodyComponent rbody { get { return (rbodyComponent)GetComponent(ComponentIds.rbody); } }

        public bool hasrbody { get { return HasComponent(ComponentIds.rbody); } }

        static readonly Stack<rbodyComponent> _rbodyComponentPool = new Stack<rbodyComponent>();

        public static void ClearrbodyComponentPool() {
            _rbodyComponentPool.Clear();
        }

        public Entity Addrbody(float newRadius, float newHeight) {
            var component = _rbodyComponentPool.Count > 0 ? _rbodyComponentPool.Pop() : new rbodyComponent();
            component.Radius = newRadius;
            component.Height = newHeight;
            return AddComponent(ComponentIds.rbody, component);
        }

        public Entity Replacerbody(float newRadius, float newHeight) {
            var previousComponent = hasrbody ? rbody : null;
            var component = _rbodyComponentPool.Count > 0 ? _rbodyComponentPool.Pop() : new rbodyComponent();
            component.Radius = newRadius;
            component.Height = newHeight;
            ReplaceComponent(ComponentIds.rbody, component);
            if (previousComponent != null) {
                _rbodyComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity Removerbody() {
            var component = rbody;
            RemoveComponent(ComponentIds.rbody);
            _rbodyComponentPool.Push(component);
            return this;
        }
    }

    public partial class Matcher {
        static IMatcher _matcherrbody;

        public static IMatcher rbody {
            get {
                if (_matcherrbody == null) {
                    var matcher = (Matcher)Matcher.AllOf(ComponentIds.rbody);
                    matcher.componentNames = ComponentIds.componentNames;
                    _matcherrbody = matcher;
                }

                return _matcherrbody;
            }
        }
    }
}
