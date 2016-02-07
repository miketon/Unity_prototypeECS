using System.Collections.Generic;

namespace Entitas {
    public partial class Entity {
        public stateDpadComponent stateDpad { get { return (stateDpadComponent)GetComponent(ComponentIds.stateDpad); } }

        public bool hasstateDpad { get { return HasComponent(ComponentIds.stateDpad); } }

        static readonly Stack<stateDpadComponent> _stateDpadComponentPool = new Stack<stateDpadComponent>();

        public static void ClearstateDpadComponentPool() {
            _stateDpadComponentPool.Clear();
        }

        public Entity AddstateDpad(MTON._enum.Dirn newDpad) {
            var component = _stateDpadComponentPool.Count > 0 ? _stateDpadComponentPool.Pop() : new stateDpadComponent();
            component.dpad = newDpad;
            return AddComponent(ComponentIds.stateDpad, component);
        }

        public Entity ReplacestateDpad(MTON._enum.Dirn newDpad) {
            var previousComponent = hasstateDpad ? stateDpad : null;
            var component = _stateDpadComponentPool.Count > 0 ? _stateDpadComponentPool.Pop() : new stateDpadComponent();
            component.dpad = newDpad;
            ReplaceComponent(ComponentIds.stateDpad, component);
            if (previousComponent != null) {
                _stateDpadComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity RemovestateDpad() {
            var component = stateDpad;
            RemoveComponent(ComponentIds.stateDpad);
            _stateDpadComponentPool.Push(component);
            return this;
        }
    }

    public partial class Matcher {
        static IMatcher _matcherstateDpad;

        public static IMatcher stateDpad {
            get {
                if (_matcherstateDpad == null) {
                    var matcher = (Matcher)Matcher.AllOf(ComponentIds.stateDpad);
                    matcher.componentNames = ComponentIds.componentNames;
                    _matcherstateDpad = matcher;
                }

                return _matcherstateDpad;
            }
        }
    }
}
