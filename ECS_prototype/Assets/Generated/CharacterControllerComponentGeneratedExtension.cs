using System.Collections.Generic;

namespace Entitas {
    public partial class Entity {
        public CharacterControllerComponent characterController { get { return (CharacterControllerComponent)GetComponent(ComponentIds.CharacterController); } }

        public bool hasCharacterController { get { return HasComponent(ComponentIds.CharacterController); } }

        static readonly Stack<CharacterControllerComponent> _characterControllerComponentPool = new Stack<CharacterControllerComponent>();

        public static void ClearCharacterControllerComponentPool() {
            _characterControllerComponentPool.Clear();
        }

        public Entity AddCharacterController(UnityEngine.CharacterController newCbody) {
            var component = _characterControllerComponentPool.Count > 0 ? _characterControllerComponentPool.Pop() : new CharacterControllerComponent();
            component.cbody = newCbody;
            return AddComponent(ComponentIds.CharacterController, component);
        }

        public Entity ReplaceCharacterController(UnityEngine.CharacterController newCbody) {
            var previousComponent = hasCharacterController ? characterController : null;
            var component = _characterControllerComponentPool.Count > 0 ? _characterControllerComponentPool.Pop() : new CharacterControllerComponent();
            component.cbody = newCbody;
            ReplaceComponent(ComponentIds.CharacterController, component);
            if (previousComponent != null) {
                _characterControllerComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity RemoveCharacterController() {
            var component = characterController;
            RemoveComponent(ComponentIds.CharacterController);
            _characterControllerComponentPool.Push(component);
            return this;
        }
    }

    public partial class Matcher {
        static IMatcher _matcherCharacterController;

        public static IMatcher CharacterController {
            get {
                if (_matcherCharacterController == null) {
                    var matcher = (Matcher)Matcher.AllOf(ComponentIds.CharacterController);
                    matcher.componentNames = ComponentIds.componentNames;
                    _matcherCharacterController = matcher;
                }

                return _matcherCharacterController;
            }
        }
    }
}
