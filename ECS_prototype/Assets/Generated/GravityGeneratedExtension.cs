namespace Entitas {
    public partial class Entity {
        static readonly Gravity gravityComponent = new Gravity();

        public bool isGravity {
            get { return HasComponent(ComponentIds.Gravity); }
            set {
                if (value != isGravity) {
                    if (value) {
                        AddComponent(ComponentIds.Gravity, gravityComponent);
                    } else {
                        RemoveComponent(ComponentIds.Gravity);
                    }
                }
            }
        }

        public Entity IsGravity(bool value) {
            isGravity = value;
            return this;
        }
    }

    public partial class Pool {
        public Entity gravityEntity { get { return GetGroup(Matcher.Gravity).GetSingleEntity(); } }

        public bool isGravity {
            get { return gravityEntity != null; }
            set {
                var entity = gravityEntity;
                if (value != (entity != null)) {
                    if (value) {
                        CreateEntity().isGravity = true;
                    } else {
                        DestroyEntity(entity);
                    }
                }
            }
        }
    }

    public partial class Matcher {
        static IMatcher _matcherGravity;

        public static IMatcher Gravity {
            get {
                if (_matcherGravity == null) {
                    var matcher = (Matcher)Matcher.AllOf(ComponentIds.Gravity);
                    matcher.componentNames = ComponentIds.componentNames;
                    _matcherGravity = matcher;
                }

                return _matcherGravity;
            }
        }
    }
}
