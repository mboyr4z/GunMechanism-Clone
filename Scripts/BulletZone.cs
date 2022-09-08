using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletZone : MonoBehaviour, ITouchable
{
    public void Touch(Action<TouchableCategory, Transform> action)
    {
        action.Invoke(TouchableCategory.BulletZone, transform);
    }
}
