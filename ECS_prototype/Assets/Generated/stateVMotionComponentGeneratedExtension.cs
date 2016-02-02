using System.Collections.Generic;

namespace Entitas {
    public partial class Entity {
        public stateVMotionComponent stateVMotion { get { return (stateVMotionComponent)GetComponent(ComponentIds.stateVMotion); } }

        public bool hasstateVMotion { get { return HasComponent(ComponentIds.stateVMotion); } }

        static readonly Stack<stateVMotionComponent> _stateVMotionComponentPool = new Stack<stateVMotionComponent>();

        public static void ClearstateVMotionComponentPool() {
            _stateVMotionComponentPool.Clear();
        }

        public Entity AddstateVMotion(MTON._enum.VState newVstate) {
            var component = _stateVMotionComponentPool.Count > 0 ? _stateVMotionComponentPool.Pop() : new stateVMotionComponent();
            component.vstate = newVstate;
            return AddComponent(ComponentIds.stateVMotion, component);
        }

        public Entity ReplacestateVMotion(MTON._enum.VState newVstate) {
            var previousComponent = hasstateVMotion ? stateVMotion : null;
            var component = _stateVMotionComponentPool.Count > 0 ? _stateVMotionComponentPool.Pop() : new stateVMotionComponent();
            component.vstate = newVstate;
            ReplaceComponent(ComponentIds.stateVMotion, component);
            if (previousComponent != null) {
                _stateVMotionComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity RemovestateVMotion() {
            var component = stateVMotion;
            RemoveComponent(ComponentIds.stateVMotion);
            _stateVMotionComponentPool.Push(component);
            return this;
        }
    }

    public partial class Matcher {
        static IMatcher _matcherstateVMotion;

        public static IMatcher stateVMotion {
            get {
                if (_matcherstateVMotion == null) {
                    var matcher = (Matcher)Matcher.AllOf(ComponentIds.stateVMotion);
                    matcher.componentNames = ComponentIds.componentNames;
                    _matcherstateVMotion = matcher;
                }

                return _matcherstateVMotion;
            }
        }
    }
}
