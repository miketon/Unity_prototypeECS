using System.Collections.Generic;

namespace Entitas {
    public partial class Entity {
        public stateCBodyComponent stateCBody { get { return (stateCBodyComponent)GetComponent(ComponentIds.stateCBody); } }

        public bool hasstateCBody { get { return HasComponent(ComponentIds.stateCBody); } }

        static readonly Stack<stateCBodyComponent> _stateCBodyComponentPool = new Stack<stateCBodyComponent>();

        public static void ClearstateCBodyComponentPool() {
            _stateCBodyComponentPool.Clear();
        }

        public Entity AddstateCBody(UnityEngine.CharacterController newCc) {
            var component = _stateCBodyComponentPool.Count > 0 ? _stateCBodyComponentPool.Pop() : new stateCBodyComponent();
            component.cc = newCc;
            return AddComponent(ComponentIds.stateCBody, component);
        }

        public Entity ReplacestateCBody(UnityEngine.CharacterController newCc) {
            var previousComponent = hasstateCBody ? stateCBody : null;
            var component = _stateCBodyComponentPool.Count > 0 ? _stateCBodyComponentPool.Pop() : new stateCBodyComponent();
            component.cc = newCc;
            ReplaceComponent(ComponentIds.stateCBody, component);
            if (previousComponent != null) {
                _stateCBodyComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity RemovestateCBody() {
            var component = stateCBody;
            RemoveComponent(ComponentIds.stateCBody);
            _stateCBodyComponentPool.Push(component);
            return this;
        }
    }

    public partial class Matcher {
        static IMatcher _matcherstateCBody;

        public static IMatcher stateCBody {
            get {
                if (_matcherstateCBody == null) {
                    var matcher = (Matcher)Matcher.AllOf(ComponentIds.stateCBody);
                    matcher.componentNames = ComponentIds.componentNames;
                    _matcherstateCBody = matcher;
                }

                return _matcherstateCBody;
            }
        }
    }
}
