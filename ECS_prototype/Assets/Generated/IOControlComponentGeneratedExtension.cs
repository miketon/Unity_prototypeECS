namespace Entitas {
    public partial class Entity {
        static readonly IOControlComponent iOControlComponent = new IOControlComponent();

        public bool isIOControl {
            get { return HasComponent(ComponentIds.IOControl); }
            set {
                if (value != isIOControl) {
                    if (value) {
                        AddComponent(ComponentIds.IOControl, iOControlComponent);
                    } else {
                        RemoveComponent(ComponentIds.IOControl);
                    }
                }
            }
        }

        public Entity IsIOControl(bool value) {
            isIOControl = value;
            return this;
        }
    }

    public partial class Matcher {
        static IMatcher _matcherIOControl;

        public static IMatcher IOControl {
            get {
                if (_matcherIOControl == null) {
                    var matcher = (Matcher)Matcher.AllOf(ComponentIds.IOControl);
                    matcher.componentNames = ComponentIds.componentNames;
                    _matcherIOControl = matcher;
                }

                return _matcherIOControl;
            }
        }
    }
}
