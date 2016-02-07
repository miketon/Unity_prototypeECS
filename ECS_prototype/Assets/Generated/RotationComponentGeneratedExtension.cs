using System.Collections.Generic;

namespace Entitas {
    public partial class Entity {
        public RotationComponent rotation { get { return (RotationComponent)GetComponent(ComponentIds.Rotation); } }

        public bool hasRotation { get { return HasComponent(ComponentIds.Rotation); } }

        static readonly Stack<RotationComponent> _rotationComponentPool = new Stack<RotationComponent>();

        public static void ClearRotationComponentPool() {
            _rotationComponentPool.Clear();
        }

        public Entity AddRotation(UnityEngine.Quaternion newRot) {
            var component = _rotationComponentPool.Count > 0 ? _rotationComponentPool.Pop() : new RotationComponent();
            component.rot = newRot;
            return AddComponent(ComponentIds.Rotation, component);
        }

        public Entity ReplaceRotation(UnityEngine.Quaternion newRot) {
            var previousComponent = hasRotation ? rotation : null;
            var component = _rotationComponentPool.Count > 0 ? _rotationComponentPool.Pop() : new RotationComponent();
            component.rot = newRot;
            ReplaceComponent(ComponentIds.Rotation, component);
            if (previousComponent != null) {
                _rotationComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity RemoveRotation() {
            var component = rotation;
            RemoveComponent(ComponentIds.Rotation);
            _rotationComponentPool.Push(component);
            return this;
        }
    }

    public partial class Matcher {
        static IMatcher _matcherRotation;

        public static IMatcher Rotation {
            get {
                if (_matcherRotation == null) {
                    var matcher = (Matcher)Matcher.AllOf(ComponentIds.Rotation);
                    matcher.componentNames = ComponentIds.componentNames;
                    _matcherRotation = matcher;
                }

                return _matcherRotation;
            }
        }
    }
}
