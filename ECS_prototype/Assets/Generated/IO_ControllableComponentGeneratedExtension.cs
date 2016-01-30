using System.Collections.Generic;

namespace Entitas {
    public partial class Entity {
        public IO_ControllableComponent iO_Controllable { get { return (IO_ControllableComponent)GetComponent(ComponentIds.IO_Controllable); } }

        public bool hasIO_Controllable { get { return HasComponent(ComponentIds.IO_Controllable); } }

        static readonly Stack<IO_ControllableComponent> _iO_ControllableComponentPool = new Stack<IO_ControllableComponent>();

        public static void ClearIO_ControllableComponentPool() {
            _iO_ControllableComponentPool.Clear();
        }

        public Entity AddIO_Controllable(int newID) {
            var component = _iO_ControllableComponentPool.Count > 0 ? _iO_ControllableComponentPool.Pop() : new IO_ControllableComponent();
            component.ID = newID;
            return AddComponent(ComponentIds.IO_Controllable, component);
        }

        public Entity ReplaceIO_Controllable(int newID) {
            var previousComponent = hasIO_Controllable ? iO_Controllable : null;
            var component = _iO_ControllableComponentPool.Count > 0 ? _iO_ControllableComponentPool.Pop() : new IO_ControllableComponent();
            component.ID = newID;
            ReplaceComponent(ComponentIds.IO_Controllable, component);
            if (previousComponent != null) {
                _iO_ControllableComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity RemoveIO_Controllable() {
            var component = iO_Controllable;
            RemoveComponent(ComponentIds.IO_Controllable);
            _iO_ControllableComponentPool.Push(component);
            return this;
        }
    }

    public partial class Matcher {
        static IMatcher _matcherIO_Controllable;

        public static IMatcher IO_Controllable {
            get {
                if (_matcherIO_Controllable == null) {
                    var matcher = (Matcher)Matcher.AllOf(ComponentIds.IO_Controllable);
                    matcher.componentNames = ComponentIds.componentNames;
                    _matcherIO_Controllable = matcher;
                }

                return _matcherIO_Controllable;
            }
        }
    }
}
