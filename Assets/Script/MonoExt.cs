using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class MonoExt : SerializedMonoBehaviour
{
    protected List<IDisposable> disposables = null;

    public virtual void Initialize(object data = null)
    {
        disposables = new List<IDisposable>();
    }

    public virtual void Dispose()
    {
        disposables?.ForEach(p => p?.Dispose());
        disposables?.Clear();
    }

    public virtual void OnSubscriptionSet()
    {

    }

    protected virtual void AddEvent<T>(Subject<T> subject, Action<T> action)
    {
        //subject.Subscribe(action);
        subject.Subscribe(action).AddTo(disposables);
    }

    private void OnDisable()
    {
        Dispose();   
    }
}
