using System.Collections.Generic;

namespace Entitas {
    public partial class Entity {
        public ForceComponent force { get { return (ForceComponent)GetComponent(ComponentIds.Force); } }

        public bool hasForce { get { return HasComponent(ComponentIds.Force); } }

        static readonly Stack<ForceComponent> _forceComponentPool = new Stack<ForceComponent>();

        public static void ClearForceComponentPool() {
            _forceComponentPool.Clear();
        }

        public Entity AddForce(float newAccel, float newSpeed, float newSpeedMax, float newMass) {
            var component = _forceComponentPool.Count > 0 ? _forceComponentPool.Pop() : new ForceComponent();
            component.accel = newAccel;
            component.speed = newSpeed;
            component.speedMax = newSpeedMax;
            component.mass = newMass;
            return AddComponent(ComponentIds.Force, component);
        }

        public Entity ReplaceForce(float newAccel, float newSpeed, float newSpeedMax, float newMass) {
            var previousComponent = hasForce ? force : null;
            var component = _forceComponentPool.Count > 0 ? _forceComponentPool.Pop() : new ForceComponent();
            component.accel = newAccel;
            component.speed = newSpeed;
            component.speedMax = newSpeedMax;
            component.mass = newMass;
            ReplaceComponent(ComponentIds.Force, component);
            if (previousComponent != null) {
                _forceComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity RemoveForce() {
            var component = force;
            RemoveComponent(ComponentIds.Force);
            _forceComponentPool.Push(component);
            return this;
        }
    }

    public partial class Matcher {
        static IMatcher _matcherForce;

        public static IMatcher Force {
            get {
                if (_matcherForce == null) {
                    var matcher = (Matcher)Matcher.AllOf(ComponentIds.Force);
                    matcher.componentNames = ComponentIds.componentNames;
                    _matcherForce = matcher;
                }

                return _matcherForce;
            }
        }
    }
}
