using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseManager
{
    protected string _state;
    public abstract string state { get; set; }

    public abstract void Initialize();
}
