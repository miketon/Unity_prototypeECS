using System.Collections.Generic;

namespace Entitas {
    public partial class Entity {
        public ViewComponent view { get { return (ViewComponent)GetComponent(ComponentIds.View); } }

        public bool hasView { get { return HasComponent(ComponentIds.View); } }

        static readonly Stack<ViewComponent> _viewComponentPool = new Stack<ViewComponent>();

        public static void ClearViewComponentPool() {
            _viewComponentPool.Clear();
        }

        public Entity AddView(UnityEngine.GameObject newGameobject) {
            var component = _viewComponentPool.Count > 0 ? _viewComponentPool.Pop() : new ViewComponent();
            component.gameobject = newGameobject;
            return AddComponent(ComponentIds.View, component);
        }

        public Entity ReplaceView(UnityEngine.GameObject newGameobject) {
            var previousComponent = hasView ? view : null;
            var component = _viewComponentPool.Count > 0 ? _viewComponentPool.Pop() : new ViewComponent();
            component.gameobject = newGameobject;
            ReplaceComponent(ComponentIds.View, component);
            if (previousComponent != null) {
                _viewComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity RemoveView() {
            var component = view;
            RemoveComponent(ComponentIds.View);
            _viewComponentPool.Push(component);
            return this;
        }
    }

    public partial class Matcher {
        static IMatcher _matcherView;

        public static IMatcher View {
            get {
                if (_matcherView == null) {
                    var matcher = (Matcher)Matcher.AllOf(ComponentIds.View);
                    matcher.componentNames = ComponentIds.componentNames;
                    _matcherView = matcher;
                }

                return _matcherView;
            }
        }
    }
}
