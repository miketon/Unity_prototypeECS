namespace Entitas {
    public partial class Entity {
        static readonly GameObjectComponent gameObjectComponent = new GameObjectComponent();

        public bool isGameObject {
            get { return HasComponent(ComponentIds.GameObject); }
            set {
                if (value != isGameObject) {
                    if (value) {
                        AddComponent(ComponentIds.GameObject, gameObjectComponent);
                    } else {
                        RemoveComponent(ComponentIds.GameObject);
                    }
                }
            }
        }

        public Entity IsGameObject(bool value) {
            isGameObject = value;
            return this;
        }
    }

    public partial class Matcher {
        static IMatcher _matcherGameObject;

        public static IMatcher GameObject {
            get {
                if (_matcherGameObject == null) {
                    var matcher = (Matcher)Matcher.AllOf(ComponentIds.GameObject);
                    matcher.componentNames = ComponentIds.componentNames;
                    _matcherGameObject = matcher;
                }

                return _matcherGameObject;
            }
        }
    }
}
