using System.Collections.Generic;

namespace Entitas {
    public partial class Entity {
        public _OnStartComponent _OnStart { get { return (_OnStartComponent)GetComponent(ComponentIds._OnStart); } }

        public bool has_OnStart { get { return HasComponent(ComponentIds._OnStart); } }

        static readonly Stack<_OnStartComponent> __OnStartComponentPool = new Stack<_OnStartComponent>();

        public static void Clear_OnStartComponentPool() {
            __OnStartComponentPool.Clear();
        }

        public Entity Add_OnStart(bool newBInit) {
            var component = __OnStartComponentPool.Count > 0 ? __OnStartComponentPool.Pop() : new _OnStartComponent();
            component.bInit = newBInit;
            return AddComponent(ComponentIds._OnStart, component);
        }

        public Entity Replace_OnStart(bool newBInit) {
            var previousComponent = has_OnStart ? _OnStart : null;
            var component = __OnStartComponentPool.Count > 0 ? __OnStartComponentPool.Pop() : new _OnStartComponent();
            component.bInit = newBInit;
            ReplaceComponent(ComponentIds._OnStart, component);
            if (previousComponent != null) {
                __OnStartComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity Remove_OnStart() {
            var component = _OnStart;
            RemoveComponent(ComponentIds._OnStart);
            __OnStartComponentPool.Push(component);
            return this;
        }
    }

    public partial class Matcher {
        static IMatcher _matcher_OnStart;

        public static IMatcher _OnStart {
            get {
                if (_matcher_OnStart == null) {
                    var matcher = (Matcher)Matcher.AllOf(ComponentIds._OnStart);
                    matcher.componentNames = ComponentIds.componentNames;
                    _matcher_OnStart = matcher;
                }

                return _matcher_OnStart;
            }
        }
    }
}
