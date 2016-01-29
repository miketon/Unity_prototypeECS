namespace Entitas {
    public partial class Entity {
        static readonly IO_ControllableComponent iO_ControllableComponent = new IO_ControllableComponent();

        public bool isIO_Controllable {
            get { return HasComponent(ComponentIds.IO_Controllable); }
            set {
                if (value != isIO_Controllable) {
                    if (value) {
                        AddComponent(ComponentIds.IO_Controllable, iO_ControllableComponent);
                    } else {
                        RemoveComponent(ComponentIds.IO_Controllable);
                    }
                }
            }
        }

        public Entity IsIO_Controllable(bool value) {
            isIO_Controllable = value;
            return this;
        }
    }

    public partial class Matcher {
        static IMatcher _matcherIO_Controllable;

        public static IMatcher IO_Controllable {
            get {
                if (_matcherIO_Controllable == null) {
                    var matcher = (Matcher)Matcher.AllOf(ComponentIds.IO_Controllable);
                    matcher.componentNames = ComponentIds.componentNames;
                    _matcherIO_Controllable = matcher;
                }

                return _matcherIO_Controllable;
            }
        }
    }
}
