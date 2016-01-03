namespace Entitas {
    public partial class Entity {
        static readonly PowerUpAttributesComponent powerUpAttributesComponent = new PowerUpAttributesComponent();

        public bool isPowerUpAttributes {
            get { return HasComponent(ComponentIds.PowerUpAttributes); }
            set {
                if (value != isPowerUpAttributes) {
                    if (value) {
                        AddComponent(ComponentIds.PowerUpAttributes, powerUpAttributesComponent);
                    } else {
                        RemoveComponent(ComponentIds.PowerUpAttributes);
                    }
                }
            }
        }

        public Entity IsPowerUpAttributes(bool value) {
            isPowerUpAttributes = value;
            return this;
        }
    }

    public partial class Matcher {
        static IMatcher _matcherPowerUpAttributes;

        public static IMatcher PowerUpAttributes {
            get {
                if (_matcherPowerUpAttributes == null) {
                    var matcher = (Matcher)Matcher.AllOf(ComponentIds.PowerUpAttributes);
                    matcher.componentNames = ComponentIds.componentNames;
                    _matcherPowerUpAttributes = matcher;
                }

                return _matcherPowerUpAttributes;
            }
        }
    }
}
