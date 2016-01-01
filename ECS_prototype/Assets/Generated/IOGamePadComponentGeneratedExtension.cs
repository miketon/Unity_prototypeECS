using System.Collections.Generic;

namespace Entitas {
    public partial class Entity {
        public IOGamePadComponent iOGamePad { get { return (IOGamePadComponent)GetComponent(ComponentIds.IOGamePad); } }

        public bool hasIOGamePad { get { return HasComponent(ComponentIds.IOGamePad); } }

        static readonly Stack<IOGamePadComponent> _iOGamePadComponentPool = new Stack<IOGamePadComponent>();

        public static void ClearIOGamePadComponentPool() {
            _iOGamePadComponentPool.Clear();
        }

        public Entity AddIOGamePad(float newHAxis, float newVAxis, bool newBFire, bool newBJump, bool newBNeutral) {
            var component = _iOGamePadComponentPool.Count > 0 ? _iOGamePadComponentPool.Pop() : new IOGamePadComponent();
            component.hAxis = newHAxis;
            component.vAxis = newVAxis;
            component.bFire = newBFire;
            component.bJump = newBJump;
            component.bNeutral = newBNeutral;
            return AddComponent(ComponentIds.IOGamePad, component);
        }

        public Entity ReplaceIOGamePad(float newHAxis, float newVAxis, bool newBFire, bool newBJump, bool newBNeutral) {
            var previousComponent = hasIOGamePad ? iOGamePad : null;
            var component = _iOGamePadComponentPool.Count > 0 ? _iOGamePadComponentPool.Pop() : new IOGamePadComponent();
            component.hAxis = newHAxis;
            component.vAxis = newVAxis;
            component.bFire = newBFire;
            component.bJump = newBJump;
            component.bNeutral = newBNeutral;
            ReplaceComponent(ComponentIds.IOGamePad, component);
            if (previousComponent != null) {
                _iOGamePadComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity RemoveIOGamePad() {
            var component = iOGamePad;
            RemoveComponent(ComponentIds.IOGamePad);
            _iOGamePadComponentPool.Push(component);
            return this;
        }
    }

    public partial class Matcher {
        static IMatcher _matcherIOGamePad;

        public static IMatcher IOGamePad {
            get {
                if (_matcherIOGamePad == null) {
                    var matcher = (Matcher)Matcher.AllOf(ComponentIds.IOGamePad);
                    matcher.componentNames = ComponentIds.componentNames;
                    _matcherIOGamePad = matcher;
                }

                return _matcherIOGamePad;
            }
        }
    }
}
