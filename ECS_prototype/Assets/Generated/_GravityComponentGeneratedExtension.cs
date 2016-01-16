namespace Entitas {
    public partial class Entity {
        static readonly _GravityComponent _GravityComponent = new _GravityComponent();

        public bool is_Gravity {
            get { return HasComponent(ComponentIds._Gravity); }
            set {
                if (value != is_Gravity) {
                    if (value) {
                        AddComponent(ComponentIds._Gravity, _GravityComponent);
                    } else {
                        RemoveComponent(ComponentIds._Gravity);
                    }
                }
            }
        }

        public Entity Is_Gravity(bool value) {
            is_Gravity = value;
            return this;
        }
    }

    public partial class Pool {
        public Entity _GravityEntity { get { return GetGroup(Matcher._Gravity).GetSingleEntity(); } }

        public bool is_Gravity {
            get { return _GravityEntity != null; }
            set {
                var entity = _GravityEntity;
                if (value != (entity != null)) {
                    if (value) {
                        CreateEntity().is_Gravity = true;
                    } else {
                        DestroyEntity(entity);
                    }
                }
            }
        }
    }

    public partial class Matcher {
        static IMatcher _matcher_Gravity;

        public static IMatcher _Gravity {
            get {
                if (_matcher_Gravity == null) {
                    var matcher = (Matcher)Matcher.AllOf(ComponentIds._Gravity);
                    matcher.componentNames = ComponentIds.componentNames;
                    _matcher_Gravity = matcher;
                }

                return _matcher_Gravity;
            }
        }
    }
}
