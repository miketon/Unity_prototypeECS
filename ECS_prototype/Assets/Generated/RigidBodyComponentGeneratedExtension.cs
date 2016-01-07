using System.Collections.Generic;

namespace Entitas {
    public partial class Entity {
        public RigidBodyComponent rigidBody { get { return (RigidBodyComponent)GetComponent(ComponentIds.RigidBody); } }

        public bool hasRigidBody { get { return HasComponent(ComponentIds.RigidBody); } }

        static readonly Stack<RigidBodyComponent> _rigidBodyComponentPool = new Stack<RigidBodyComponent>();

        public static void ClearRigidBodyComponentPool() {
            _rigidBodyComponentPool.Clear();
        }

        public Entity AddRigidBody(UnityEngine.Rigidbody newRbody) {
            var component = _rigidBodyComponentPool.Count > 0 ? _rigidBodyComponentPool.Pop() : new RigidBodyComponent();
            component.rbody = newRbody;
            return AddComponent(ComponentIds.RigidBody, component);
        }

        public Entity ReplaceRigidBody(UnityEngine.Rigidbody newRbody) {
            var previousComponent = hasRigidBody ? rigidBody : null;
            var component = _rigidBodyComponentPool.Count > 0 ? _rigidBodyComponentPool.Pop() : new RigidBodyComponent();
            component.rbody = newRbody;
            ReplaceComponent(ComponentIds.RigidBody, component);
            if (previousComponent != null) {
                _rigidBodyComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity RemoveRigidBody() {
            var component = rigidBody;
            RemoveComponent(ComponentIds.RigidBody);
            _rigidBodyComponentPool.Push(component);
            return this;
        }
    }

    public partial class Matcher {
        static IMatcher _matcherRigidBody;

        public static IMatcher RigidBody {
            get {
                if (_matcherRigidBody == null) {
                    var matcher = (Matcher)Matcher.AllOf(ComponentIds.RigidBody);
                    matcher.componentNames = ComponentIds.componentNames;
                    _matcherRigidBody = matcher;
                }

                return _matcherRigidBody;
            }
        }
    }
}
