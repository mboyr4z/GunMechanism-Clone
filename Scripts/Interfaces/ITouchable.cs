using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITouchable 
{
    public void Touch(Action<TouchableCategory, Transform> action);
}
