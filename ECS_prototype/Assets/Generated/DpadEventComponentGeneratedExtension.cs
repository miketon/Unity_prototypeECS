using System.Collections.Generic;

namespace Entitas {
    public partial class Entity {
        public DpadEventComponent dpadEvent { get { return (DpadEventComponent)GetComponent(ComponentIds.DpadEvent); } }

        public bool hasDpadEvent { get { return HasComponent(ComponentIds.DpadEvent); } }

        static readonly Stack<DpadEventComponent> _dpadEventComponentPool = new Stack<DpadEventComponent>();

        public static void ClearDpadEventComponentPool() {
            _dpadEventComponentPool.Clear();
        }

        public Entity AddDpadEvent(MTON._enum.Press newBStat, MTON._enum.Dirn newEDirn) {
            var component = _dpadEventComponentPool.Count > 0 ? _dpadEventComponentPool.Pop() : new DpadEventComponent();
            component.bStat = newBStat;
            component.eDirn = newEDirn;
            return AddComponent(ComponentIds.DpadEvent, component);
        }

        public Entity ReplaceDpadEvent(MTON._enum.Press newBStat, MTON._enum.Dirn newEDirn) {
            var previousComponent = hasDpadEvent ? dpadEvent : null;
            var component = _dpadEventComponentPool.Count > 0 ? _dpadEventComponentPool.Pop() : new DpadEventComponent();
            component.bStat = newBStat;
            component.eDirn = newEDirn;
            ReplaceComponent(ComponentIds.DpadEvent, component);
            if (previousComponent != null) {
                _dpadEventComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity RemoveDpadEvent() {
            var component = dpadEvent;
            RemoveComponent(ComponentIds.DpadEvent);
            _dpadEventComponentPool.Push(component);
            return this;
        }
    }

    public partial class Matcher {
        static IMatcher _matcherDpadEvent;

        public static IMatcher DpadEvent {
            get {
                if (_matcherDpadEvent == null) {
                    var matcher = (Matcher)Matcher.AllOf(ComponentIds.DpadEvent);
                    matcher.componentNames = ComponentIds.componentNames;
                    _matcherDpadEvent = matcher;
                }

                return _matcherDpadEvent;
            }
        }
    }
}
