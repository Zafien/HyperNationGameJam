using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FuncPredicate : IPredicate
{
    readonly Func<bool> func;

    public FuncPredicate(Func<bool> func)
    {
        this.func = func;
    }

    public bool Evaluate() => func.Invoke();
}

//public class ActionPredicate : IPredicate
//{
//    readonly Action<bool> action;

//    public ActionPredicate(Action<bool> action)
//    {
//        this.action = action;
//    }

//    public bool Evaluate() => action.Invoke();
//}
