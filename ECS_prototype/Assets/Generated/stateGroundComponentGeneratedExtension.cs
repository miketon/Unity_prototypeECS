using System.Collections.Generic;

namespace Entitas {
    public partial class Entity {
        public stateGroundComponent stateGround { get { return (stateGroundComponent)GetComponent(ComponentIds.stateGround); } }

        public bool hasstateGround { get { return HasComponent(ComponentIds.stateGround); } }

        static readonly Stack<stateGroundComponent> _stateGroundComponentPool = new Stack<stateGroundComponent>();

        public static void ClearstateGroundComponentPool() {
            _stateGroundComponentPool.Clear();
        }

        public Entity AddstateGround(bool newBGround) {
            var component = _stateGroundComponentPool.Count > 0 ? _stateGroundComponentPool.Pop() : new stateGroundComponent();
            component.bGround = newBGround;
            return AddComponent(ComponentIds.stateGround, component);
        }

        public Entity ReplacestateGround(bool newBGround) {
            var previousComponent = hasstateGround ? stateGround : null;
            var component = _stateGroundComponentPool.Count > 0 ? _stateGroundComponentPool.Pop() : new stateGroundComponent();
            component.bGround = newBGround;
            ReplaceComponent(ComponentIds.stateGround, component);
            if (previousComponent != null) {
                _stateGroundComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity RemovestateGround() {
            var component = stateGround;
            RemoveComponent(ComponentIds.stateGround);
            _stateGroundComponentPool.Push(component);
            return this;
        }
    }

    public partial class Matcher {
        static IMatcher _matcherstateGround;

        public static IMatcher stateGround {
            get {
                if (_matcherstateGround == null) {
                    var matcher = (Matcher)Matcher.AllOf(ComponentIds.stateGround);
                    matcher.componentNames = ComponentIds.componentNames;
                    _matcherstateGround = matcher;
                }

                return _matcherstateGround;
            }
        }
    }
}
