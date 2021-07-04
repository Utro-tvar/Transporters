using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    public static event System.Action FixedUpdateCall;

    void FixedUpdate()
    {
        FixedUpdateCall?.Invoke();
    }
}
