using System.Collections.Generic;

namespace Entitas {
    public partial class Entity {
        public stateFacingComponent stateFacing { get { return (stateFacingComponent)GetComponent(ComponentIds.stateFacing); } }

        public bool hasstateFacing { get { return HasComponent(ComponentIds.stateFacing); } }

        static readonly Stack<stateFacingComponent> _stateFacingComponentPool = new Stack<stateFacingComponent>();

        public static void ClearstateFacingComponentPool() {
            _stateFacingComponentPool.Clear();
        }

        public Entity AddstateFacing(MTON._enum.FState newFstate) {
            var component = _stateFacingComponentPool.Count > 0 ? _stateFacingComponentPool.Pop() : new stateFacingComponent();
            component.fstate = newFstate;
            return AddComponent(ComponentIds.stateFacing, component);
        }

        public Entity ReplacestateFacing(MTON._enum.FState newFstate) {
            var previousComponent = hasstateFacing ? stateFacing : null;
            var component = _stateFacingComponentPool.Count > 0 ? _stateFacingComponentPool.Pop() : new stateFacingComponent();
            component.fstate = newFstate;
            ReplaceComponent(ComponentIds.stateFacing, component);
            if (previousComponent != null) {
                _stateFacingComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity RemovestateFacing() {
            var component = stateFacing;
            RemoveComponent(ComponentIds.stateFacing);
            _stateFacingComponentPool.Push(component);
            return this;
        }
    }

    public partial class Matcher {
        static IMatcher _matcherstateFacing;

        public static IMatcher stateFacing {
            get {
                if (_matcherstateFacing == null) {
                    var matcher = (Matcher)Matcher.AllOf(ComponentIds.stateFacing);
                    matcher.componentNames = ComponentIds.componentNames;
                    _matcherstateFacing = matcher;
                }

                return _matcherstateFacing;
            }
        }
    }
}
