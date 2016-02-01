using System.Collections.Generic;

namespace Entitas {
    public partial class Entity {
        public PlayerComponent player { get { return (PlayerComponent)GetComponent(ComponentIds.Player); } }

        public bool hasPlayer { get { return HasComponent(ComponentIds.Player); } }

        static readonly Stack<PlayerComponent> _playerComponentPool = new Stack<PlayerComponent>();

        public static void ClearPlayerComponentPool() {
            _playerComponentPool.Clear();
        }

        public Entity AddPlayer(int newID) {
            var component = _playerComponentPool.Count > 0 ? _playerComponentPool.Pop() : new PlayerComponent();
            component.ID = newID;
            return AddComponent(ComponentIds.Player, component);
        }

        public Entity ReplacePlayer(int newID) {
            var previousComponent = hasPlayer ? player : null;
            var component = _playerComponentPool.Count > 0 ? _playerComponentPool.Pop() : new PlayerComponent();
            component.ID = newID;
            ReplaceComponent(ComponentIds.Player, component);
            if (previousComponent != null) {
                _playerComponentPool.Push(previousComponent);
            }
            return this;
        }

        public Entity RemovePlayer() {
            var component = player;
            RemoveComponent(ComponentIds.Player);
            _playerComponentPool.Push(component);
            return this;
        }
    }

    public partial class Matcher {
        static IMatcher _matcherPlayer;

        public static IMatcher Player {
            get {
                if (_matcherPlayer == null) {
                    var matcher = (Matcher)Matcher.AllOf(ComponentIds.Player);
                    matcher.componentNames = ComponentIds.componentNames;
                    _matcherPlayer = matcher;
                }

                return _matcherPlayer;
            }
        }
    }
}
