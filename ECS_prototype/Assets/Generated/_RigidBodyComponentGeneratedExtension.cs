using System.Collections.Generic;

namespace Entitas {
    public partial class Entity {
        public _RigidBodyComponent _RigidBody { get { return (_RigidBodyComponent)GetComponent(ComponentIds._RigidBody); } }

        public bool has_RigidBody { get { return HasComponent(ComponentIds._RigidBody); } }

        static readonly Stack<_RigidBodyComponent> __RigidBodyComponentPool = new Stack<_RigidBodyComponent>();

        public static void Clear_RigidBodyComponentPool() {
            __RigidBodyComponentPool.Clear();
        }

        public Entity Add_RigidBody(UnityEngine.Rigidbody newBody) {
            var component = __RigidBodyComponentPool.Count > 0 ? __RigidBodyComponentPool.Pop() : new _RigidBodyComponent();
            component.body = newBody;
            return AddComponent(ComponentIds._RigidBody, component);
        }

        public Entity Replace_RigidBody(UnityEngine.Rigidbody newBody) {
            var previousComponent = has_RigidBody ? _RigidBody : null;
            var component = __RigidBodyComponentPool.Count > 0 ? __RigidBodyComponentPool.Pop() : new _RigidBodyComponent();
            component.body = newBody;
            ReplaceComponent(ComponentIds._RigidBody, component);
            if (previousComponent != null) {
                __RigidBodyComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity Remove_RigidBody() {
            var component = _RigidBody;
            RemoveComponent(ComponentIds._RigidBody);
            __RigidBodyComponentPool.Push(component);
            return this;
        }
    }

    public partial class Matcher {
        static IMatcher _matcher_RigidBody;

        public static IMatcher _RigidBody {
            get {
                if (_matcher_RigidBody == null) {
                    var matcher = (Matcher)Matcher.AllOf(ComponentIds._RigidBody);
                    matcher.componentNames = ComponentIds.componentNames;
                    _matcher_RigidBody = matcher;
                }

                return _matcher_RigidBody;
            }
        }
    }
}
