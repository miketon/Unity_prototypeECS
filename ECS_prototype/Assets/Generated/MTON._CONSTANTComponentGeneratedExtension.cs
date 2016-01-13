namespace Entitas {
    public partial class Entity {
        static readonly MTON._CONSTANTComponent _CONSTANTComponent = new MTON._CONSTANTComponent();

        public bool is_CONSTANT {
            get { return HasComponent(ComponentIds._CONSTANT); }
            set {
                if (value != is_CONSTANT) {
                    if (value) {
                        AddComponent(ComponentIds._CONSTANT, _CONSTANTComponent);
                    } else {
                        RemoveComponent(ComponentIds._CONSTANT);
                    }
                }
            }
        }

        public Entity Is_CONSTANT(bool value) {
            is_CONSTANT = value;
            return this;
        }
    }

    public partial class Pool {
        public Entity _CONSTANTEntity { get { return GetGroup(Matcher._CONSTANT).GetSingleEntity(); } }

        public bool is_CONSTANT {
            get { return _CONSTANTEntity != null; }
            set {
                var entity = _CONSTANTEntity;
                if (value != (entity != null)) {
                    if (value) {
                        CreateEntity().is_CONSTANT = true;
                    } else {
                        DestroyEntity(entity);
                    }
                }
            }
        }
    }

    public partial class Matcher {
        static IMatcher _matcher_CONSTANT;

        public static IMatcher _CONSTANT {
            get {
                if (_matcher_CONSTANT == null) {
                    var matcher = (Matcher)Matcher.AllOf(ComponentIds._CONSTANT);
                    matcher.componentNames = ComponentIds.componentNames;
                    _matcher_CONSTANT = matcher;
                }

                return _matcher_CONSTANT;
            }
        }
    }
}
