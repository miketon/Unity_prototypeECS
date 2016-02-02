using System.Collections.Generic;

namespace Entitas {
    public partial class Entity {
        public _CharacterControllerComponent _CharacterController { get { return (_CharacterControllerComponent)GetComponent(ComponentIds._CharacterController); } }

        public bool has_CharacterController { get { return HasComponent(ComponentIds._CharacterController); } }

        static readonly Stack<_CharacterControllerComponent> __CharacterControllerComponentPool = new Stack<_CharacterControllerComponent>();

        public static void Clear_CharacterControllerComponentPool() {
            __CharacterControllerComponentPool.Clear();
        }

        public Entity Add_CharacterController(int newID, UnityEngine.CharacterController newBody) {
            var component = __CharacterControllerComponentPool.Count > 0 ? __CharacterControllerComponentPool.Pop() : new _CharacterControllerComponent();
            component.ID = newID;
            component.body = newBody;
            return AddComponent(ComponentIds._CharacterController, component);
        }

        public Entity Replace_CharacterController(int newID, UnityEngine.CharacterController newBody) {
            var previousComponent = has_CharacterController ? _CharacterController : null;
            var component = __CharacterControllerComponentPool.Count > 0 ? __CharacterControllerComponentPool.Pop() : new _CharacterControllerComponent();
            component.ID = newID;
            component.body = newBody;
            ReplaceComponent(ComponentIds._CharacterController, component);
            if (previousComponent != null) {
                __CharacterControllerComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity Remove_CharacterController() {
            var component = _CharacterController;
            RemoveComponent(ComponentIds._CharacterController);
            __CharacterControllerComponentPool.Push(component);
            return this;
        }
    }

    public partial class Matcher {
        static IMatcher _matcher_CharacterController;

        public static IMatcher _CharacterController {
            get {
                if (_matcher_CharacterController == null) {
                    var matcher = (Matcher)Matcher.AllOf(ComponentIds._CharacterController);
                    matcher.componentNames = ComponentIds.componentNames;
                    _matcher_CharacterController = matcher;
                }

                return _matcher_CharacterController;
            }
        }
    }
}
