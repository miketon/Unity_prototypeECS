using System.Collections.Generic;

namespace Entitas {
    public partial class Entity {
        public stateCrouchComponent stateCrouch { get { return (stateCrouchComponent)GetComponent(ComponentIds.stateCrouch); } }

        public bool hasstateCrouch { get { return HasComponent(ComponentIds.stateCrouch); } }

        static readonly Stack<stateCrouchComponent> _stateCrouchComponentPool = new Stack<stateCrouchComponent>();

        public static void ClearstateCrouchComponentPool() {
            _stateCrouchComponentPool.Clear();
        }

        public Entity AddstateCrouch(bool newBCrouch) {
            var component = _stateCrouchComponentPool.Count > 0 ? _stateCrouchComponentPool.Pop() : new stateCrouchComponent();
            component.bCrouch = newBCrouch;
            return AddComponent(ComponentIds.stateCrouch, component);
        }

        public Entity ReplacestateCrouch(bool newBCrouch) {
            var previousComponent = hasstateCrouch ? stateCrouch : null;
            var component = _stateCrouchComponentPool.Count > 0 ? _stateCrouchComponentPool.Pop() : new stateCrouchComponent();
            component.bCrouch = newBCrouch;
            ReplaceComponent(ComponentIds.stateCrouch, component);
            if (previousComponent != null) {
                _stateCrouchComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity RemovestateCrouch() {
            var component = stateCrouch;
            RemoveComponent(ComponentIds.stateCrouch);
            _stateCrouchComponentPool.Push(component);
            return this;
        }
    }

    public partial class Matcher {
        static IMatcher _matcherstateCrouch;

        public static IMatcher stateCrouch {
            get {
                if (_matcherstateCrouch == null) {
                    var matcher = (Matcher)Matcher.AllOf(ComponentIds.stateCrouch);
                    matcher.componentNames = ComponentIds.componentNames;
                    _matcherstateCrouch = matcher;
                }

                return _matcherstateCrouch;
            }
        }
    }
}
