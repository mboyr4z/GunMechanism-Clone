using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VaseManager : MonoSingleton<VaseManager>
{
    private List<GameObject> vases = new List<GameObject>();
    
    public void RemoveVase(GameObject obj)
    {
        vases.Remove(obj);
    }

    public void AddVase(GameObject obj)
    {
        vases.Add(obj);
    }

    public int GetVaseCount()
    {
        return vases.Count;
    }   
}
