using System.Collections.Generic;

namespace Entitas {
    public partial class Entity {
        public IOReleaseComponent iORelease { get { return (IOReleaseComponent)GetComponent(ComponentIds.IORelease); } }

        public bool hasIORelease { get { return HasComponent(ComponentIds.IORelease); } }

        static readonly Stack<IOReleaseComponent> _iOReleaseComponentPool = new Stack<IOReleaseComponent>();

        public static void ClearIOReleaseComponentPool() {
            _iOReleaseComponentPool.Clear();
        }

        public Entity AddIORelease(bool newBALLPAD, bool newBDIRPAD, bool newBBUTTON) {
            var component = _iOReleaseComponentPool.Count > 0 ? _iOReleaseComponentPool.Pop() : new IOReleaseComponent();
            component.bALLPAD = newBALLPAD;
            component.bDIRPAD = newBDIRPAD;
            component.bBUTTON = newBBUTTON;
            return AddComponent(ComponentIds.IORelease, component);
        }

        public Entity ReplaceIORelease(bool newBALLPAD, bool newBDIRPAD, bool newBBUTTON) {
            var previousComponent = hasIORelease ? iORelease : null;
            var component = _iOReleaseComponentPool.Count > 0 ? _iOReleaseComponentPool.Pop() : new IOReleaseComponent();
            component.bALLPAD = newBALLPAD;
            component.bDIRPAD = newBDIRPAD;
            component.bBUTTON = newBBUTTON;
            ReplaceComponent(ComponentIds.IORelease, component);
            if (previousComponent != null) {
                _iOReleaseComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity RemoveIORelease() {
            var component = iORelease;
            RemoveComponent(ComponentIds.IORelease);
            _iOReleaseComponentPool.Push(component);
            return this;
        }
    }

    public partial class Matcher {
        static IMatcher _matcherIORelease;

        public static IMatcher IORelease {
            get {
                if (_matcherIORelease == null) {
                    var matcher = (Matcher)Matcher.AllOf(ComponentIds.IORelease);
                    matcher.componentNames = ComponentIds.componentNames;
                    _matcherIORelease = matcher;
                }

                return _matcherIORelease;
            }
        }
    }
}
