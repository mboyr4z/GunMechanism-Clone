using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBase : MonoBehaviour, ISlider
{
    public void Touch(Action act)
    {
        act.Invoke();
    }
}
