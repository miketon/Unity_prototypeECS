using System.Collections.Generic;

namespace Entitas {
    public partial class Entity {
        public IO_OnFirstPressComponent iO_OnFirstPress { get { return (IO_OnFirstPressComponent)GetComponent(ComponentIds.IO_OnFirstPress); } }

        public bool hasIO_OnFirstPress { get { return HasComponent(ComponentIds.IO_OnFirstPress); } }

        static readonly Stack<IO_OnFirstPressComponent> _iO_OnFirstPressComponentPool = new Stack<IO_OnFirstPressComponent>();

        public static void ClearIO_OnFirstPressComponentPool() {
            _iO_OnFirstPressComponentPool.Clear();
        }

        public Entity AddIO_OnFirstPress(float newFBonus) {
            var component = _iO_OnFirstPressComponentPool.Count > 0 ? _iO_OnFirstPressComponentPool.Pop() : new IO_OnFirstPressComponent();
            component.fBonus = newFBonus;
            return AddComponent(ComponentIds.IO_OnFirstPress, component);
        }

        public Entity ReplaceIO_OnFirstPress(float newFBonus) {
            var previousComponent = hasIO_OnFirstPress ? iO_OnFirstPress : null;
            var component = _iO_OnFirstPressComponentPool.Count > 0 ? _iO_OnFirstPressComponentPool.Pop() : new IO_OnFirstPressComponent();
            component.fBonus = newFBonus;
            ReplaceComponent(ComponentIds.IO_OnFirstPress, component);
            if (previousComponent != null) {
                _iO_OnFirstPressComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity RemoveIO_OnFirstPress() {
            var component = iO_OnFirstPress;
            RemoveComponent(ComponentIds.IO_OnFirstPress);
            _iO_OnFirstPressComponentPool.Push(component);
            return this;
        }
    }

    public partial class Matcher {
        static IMatcher _matcherIO_OnFirstPress;

        public static IMatcher IO_OnFirstPress {
            get {
                if (_matcherIO_OnFirstPress == null) {
                    var matcher = (Matcher)Matcher.AllOf(ComponentIds.IO_OnFirstPress);
                    matcher.componentNames = ComponentIds.componentNames;
                    _matcherIO_OnFirstPress = matcher;
                }

                return _matcherIO_OnFirstPress;
            }
        }
    }
}
