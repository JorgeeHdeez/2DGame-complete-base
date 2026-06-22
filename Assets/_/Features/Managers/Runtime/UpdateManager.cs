using System.Collections.Generic;
using UnityEngine;

namespace Managers.Runtime
{
    public class UpdateManager : MonoBehaviour
    {
        private readonly List<IUpdatable> _updatables = new List<IUpdatable>();
        private readonly List<IUpdatable> _pendingAdd = new List<IUpdatable>();
        private readonly List<IUpdatable> _pendingRemove = new List<IUpdatable>();

        public void Register(IUpdatable updatable)
        {
            if (updatable == null) return;
            _pendingAdd.Add(updatable);
        }

        public void Unregister(IUpdatable updatable)
        {
            if (updatable == null) return;
            _pendingRemove.Add(updatable);
        }

        private void Update()
        {
            HandlePendingChanges();
            HandleTick();
        }

        private void HandlePendingChanges()
        {
            for (int i = 0; i < _pendingAdd.Count; i++)
            {
                _updatables.Add(_pendingAdd[i]);
            }
            _pendingAdd.Clear();

            for (int i = 0; i < _pendingRemove.Count; i++)
            {
                _updatables.Remove(_pendingRemove[i]);
            }
            _pendingRemove.Clear();
        }

        private void HandleTick()
        {
            float deltaTime = Time.deltaTime;
            for (int i = 0; i < _updatables.Count; i++)
            {
                _updatables[i].OnTick(deltaTime);
            }
        }
    }
}