namespace Entitas {
    public partial class Entity {
        static readonly IONeutralComponent iONeutralComponent = new IONeutralComponent();

        public bool isIONeutral {
            get { return HasComponent(ComponentIds.IONeutral); }
            set {
                if (value != isIONeutral) {
                    if (value) {
                        AddComponent(ComponentIds.IONeutral, iONeutralComponent);
                    } else {
                        RemoveComponent(ComponentIds.IONeutral);
                    }
                }
            }
        }

        public Entity IsIONeutral(bool value) {
            isIONeutral = value;
            return this;
        }
    }

    public partial class Pool {
        public Entity iONeutralEntity { get { return GetGroup(Matcher.IONeutral).GetSingleEntity(); } }

        public bool isIONeutral {
            get { return iONeutralEntity != null; }
            set {
                var entity = iONeutralEntity;
                if (value != (entity != null)) {
                    if (value) {
                        CreateEntity().isIONeutral = true;
                    } else {
                        DestroyEntity(entity);
                    }
                }
            }
        }
    }

    public partial class Matcher {
        static IMatcher _matcherIONeutral;

        public static IMatcher IONeutral {
            get {
                if (_matcherIONeutral == null) {
                    var matcher = (Matcher)Matcher.AllOf(ComponentIds.IONeutral);
                    matcher.componentNames = ComponentIds.componentNames;
                    _matcherIONeutral = matcher;
                }

                return _matcherIONeutral;
            }
        }
    }
}
