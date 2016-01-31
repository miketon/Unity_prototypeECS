using System.Collections.Generic;

namespace Entitas {
    public partial class Entity {
        public IO_OnFirstReleaseComponent iO_OnFirstRelease { get { return (IO_OnFirstReleaseComponent)GetComponent(ComponentIds.IO_OnFirstRelease); } }

        public bool hasIO_OnFirstRelease { get { return HasComponent(ComponentIds.IO_OnFirstRelease); } }

        static readonly Stack<IO_OnFirstReleaseComponent> _iO_OnFirstReleaseComponentPool = new Stack<IO_OnFirstReleaseComponent>();

        public static void ClearIO_OnFirstReleaseComponentPool() {
            _iO_OnFirstReleaseComponentPool.Clear();
        }

        public Entity AddIO_OnFirstRelease(int newID) {
            var component = _iO_OnFirstReleaseComponentPool.Count > 0 ? _iO_OnFirstReleaseComponentPool.Pop() : new IO_OnFirstReleaseComponent();
            component.ID = newID;
            return AddComponent(ComponentIds.IO_OnFirstRelease, component);
        }

        public Entity ReplaceIO_OnFirstRelease(int newID) {
            var previousComponent = hasIO_OnFirstRelease ? iO_OnFirstRelease : null;
            var component = _iO_OnFirstReleaseComponentPool.Count > 0 ? _iO_OnFirstReleaseComponentPool.Pop() : new IO_OnFirstReleaseComponent();
            component.ID = newID;
            ReplaceComponent(ComponentIds.IO_OnFirstRelease, component);
            if (previousComponent != null) {
                _iO_OnFirstReleaseComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity RemoveIO_OnFirstRelease() {
            var component = iO_OnFirstRelease;
            RemoveComponent(ComponentIds.IO_OnFirstRelease);
            _iO_OnFirstReleaseComponentPool.Push(component);
            return this;
        }
    }

    public partial class Matcher {
        static IMatcher _matcherIO_OnFirstRelease;

        public static IMatcher IO_OnFirstRelease {
            get {
                if (_matcherIO_OnFirstRelease == null) {
                    var matcher = (Matcher)Matcher.AllOf(ComponentIds.IO_OnFirstRelease);
                    matcher.componentNames = ComponentIds.componentNames;
                    _matcherIO_OnFirstRelease = matcher;
                }

                return _matcherIO_OnFirstRelease;
            }
        }
    }
}
