using System.Collections.Generic;

namespace Entitas {
    public partial class Entity {
        public stateHMotionComponent stateHMotion { get { return (stateHMotionComponent)GetComponent(ComponentIds.stateHMotion); } }

        public bool hasstateHMotion { get { return HasComponent(ComponentIds.stateHMotion); } }

        static readonly Stack<stateHMotionComponent> _stateHMotionComponentPool = new Stack<stateHMotionComponent>();

        public static void ClearstateHMotionComponentPool() {
            _stateHMotionComponentPool.Clear();
        }

        public Entity AddstateHMotion(MTON._enum.HState newHstate) {
            var component = _stateHMotionComponentPool.Count > 0 ? _stateHMotionComponentPool.Pop() : new stateHMotionComponent();
            component.hstate = newHstate;
            return AddComponent(ComponentIds.stateHMotion, component);
        }

        public Entity ReplacestateHMotion(MTON._enum.HState newHstate) {
            var previousComponent = hasstateHMotion ? stateHMotion : null;
            var component = _stateHMotionComponentPool.Count > 0 ? _stateHMotionComponentPool.Pop() : new stateHMotionComponent();
            component.hstate = newHstate;
            ReplaceComponent(ComponentIds.stateHMotion, component);
            if (previousComponent != null) {
                _stateHMotionComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity RemovestateHMotion() {
            var component = stateHMotion;
            RemoveComponent(ComponentIds.stateHMotion);
            _stateHMotionComponentPool.Push(component);
            return this;
        }
    }

    public partial class Matcher {
        static IMatcher _matcherstateHMotion;

        public static IMatcher stateHMotion {
            get {
                if (_matcherstateHMotion == null) {
                    var matcher = (Matcher)Matcher.AllOf(ComponentIds.stateHMotion);
                    matcher.componentNames = ComponentIds.componentNames;
                    _matcherstateHMotion = matcher;
                }

                return _matcherstateHMotion;
            }
        }
    }
}
