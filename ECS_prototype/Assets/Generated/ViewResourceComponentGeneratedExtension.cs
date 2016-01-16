using System.Collections.Generic;

namespace Entitas {
    public partial class Entity {
        public ViewResourceComponent viewResource { get { return (ViewResourceComponent)GetComponent(ComponentIds.ViewResource); } }

        public bool hasViewResource { get { return HasComponent(ComponentIds.ViewResource); } }

        static readonly Stack<ViewResourceComponent> _viewResourceComponentPool = new Stack<ViewResourceComponent>();

        public static void ClearViewResourceComponentPool() {
            _viewResourceComponentPool.Clear();
        }

        public Entity AddViewResource(string newName) {
            var component = _viewResourceComponentPool.Count > 0 ? _viewResourceComponentPool.Pop() : new ViewResourceComponent();
            component.name = newName;
            return AddComponent(ComponentIds.ViewResource, component);
        }

        public Entity ReplaceViewResource(string newName) {
            var previousComponent = hasViewResource ? viewResource : null;
            var component = _viewResourceComponentPool.Count > 0 ? _viewResourceComponentPool.Pop() : new ViewResourceComponent();
            component.name = newName;
            ReplaceComponent(ComponentIds.ViewResource, component);
            if (previousComponent != null) {
                _viewResourceComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity RemoveViewResource() {
            var component = viewResource;
            RemoveComponent(ComponentIds.ViewResource);
            _viewResourceComponentPool.Push(component);
            return this;
        }
    }

    public partial class Matcher {
        static IMatcher _matcherViewResource;

        public static IMatcher ViewResource {
            get {
                if (_matcherViewResource == null) {
                    var matcher = (Matcher)Matcher.AllOf(ComponentIds.ViewResource);
                    matcher.componentNames = ComponentIds.componentNames;
                    _matcherViewResource = matcher;
                }

                return _matcherViewResource;
            }
        }
    }
}
