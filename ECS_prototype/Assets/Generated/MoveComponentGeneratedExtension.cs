using System.Collections.Generic;

namespace Entitas {
    public partial class Entity {
        public MoveComponent move { get { return (MoveComponent)GetComponent(ComponentIds.Move); } }

        public bool hasMove { get { return HasComponent(ComponentIds.Move); } }

        static readonly Stack<MoveComponent> _moveComponentPool = new Stack<MoveComponent>();

        public static void ClearMoveComponentPool() {
            _moveComponentPool.Clear();
        }

        public Entity AddMove(float newSpeed, float newSpeedMax) {
            var component = _moveComponentPool.Count > 0 ? _moveComponentPool.Pop() : new MoveComponent();
            component.speed = newSpeed;
            component.speedMax = newSpeedMax;
            return AddComponent(ComponentIds.Move, component);
        }

        public Entity ReplaceMove(float newSpeed, float newSpeedMax) {
            var previousComponent = hasMove ? move : null;
            var component = _moveComponentPool.Count > 0 ? _moveComponentPool.Pop() : new MoveComponent();
            component.speed = newSpeed;
            component.speedMax = newSpeedMax;
            ReplaceComponent(ComponentIds.Move, component);
            if (previousComponent != null) {
                _moveComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity RemoveMove() {
            var component = move;
            RemoveComponent(ComponentIds.Move);
            _moveComponentPool.Push(component);
            return this;
        }
    }

    public partial class Matcher {
        static IMatcher _matcherMove;

        public static IMatcher Move {
            get {
                if (_matcherMove == null) {
                    var matcher = (Matcher)Matcher.AllOf(ComponentIds.Move);
                    matcher.componentNames = ComponentIds.componentNames;
                    _matcherMove = matcher;
                }

                return _matcherMove;
            }
        }
    }
}
