namespace Entitas {
    public partial class Entity {
        static readonly IO_OnFirstPressComponent iO_OnFirstPressComponent = new IO_OnFirstPressComponent();

        public bool isIO_OnFirstPress {
            get { return HasComponent(ComponentIds.IO_OnFirstPress); }
            set {
                if (value != isIO_OnFirstPress) {
                    if (value) {
                        AddComponent(ComponentIds.IO_OnFirstPress, iO_OnFirstPressComponent);
                    } else {
                        RemoveComponent(ComponentIds.IO_OnFirstPress);
                    }
                }
            }
        }

        public Entity IsIO_OnFirstPress(bool value) {
            isIO_OnFirstPress = value;
            return this;
        }
    }

    public partial class Matcher {
        static IMatcher _matcherIO_OnFirstPress;

        public static IMatcher IO_OnFirstPress {
            get {
                if (_matcherIO_OnFirstPress == null) {
                    var matcher = (Matcher)Matcher.AllOf(ComponentIds.IO_OnFirstPress);
                    matcher.componentNames = ComponentIds.componentNames;
                    _matcherIO_OnFirstPress = matcher;
                }

                return _matcherIO_OnFirstPress;
            }
        }
    }
}
