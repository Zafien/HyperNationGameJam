using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace NF.Utilities
{
    public class BasePooler<T> where T : Component
    {
        private T _prefab;
        private ObjectPool<T> _pool;

        private ObjectPool<T> Pool
        {
            get
            {
                if (_pool == null) throw new InvalidOperationException("You need to call InitPool before using it.");
                return _pool;
            }
            set => _pool = value;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="prefab">Object to have pools</param>
        /// <param name="initial">how many objects present in scene</param>
        /// <param name="max">maximum objects in present in scene</param>
        /// <param name="collectionChecks">collection checks will throw errors if we try to release an item that is already in the pool.</param>
        public void InitPool(T prefab, int initial = 10, int max = 20, bool collectionChecks = false)
        {
            _prefab = prefab;
            Pool = new ObjectPool<T>(
                CreateSetup,
                GetSetup,
                ReleaseSetup,
                DestroySetup,
                collectionChecks,
                initial,
                max);
        }

        #region Overrides

        public virtual T CreateSetup() => UnityEngine.Object.Instantiate(_prefab);
        public virtual void GetSetup(T obj) => obj.gameObject.SetActive(true);
        public virtual void ReleaseSetup(T obj) => obj.gameObject.SetActive(false);
        public virtual void DestroySetup(T obj) => UnityEngine.Object.Destroy(obj);

        #endregion

        #region Getters

        // Retrieve/activate the object from pool 
        public T Get() => Pool.Get();
        // Release/deactivate object from pool
        public void Release(T obj) => Pool.Release(obj);

        #endregion
    }
}