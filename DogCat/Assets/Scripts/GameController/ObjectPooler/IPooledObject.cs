using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPooledObject
{
    void _OnObjectSpawn();
    void _OnObjectReturn();
}
