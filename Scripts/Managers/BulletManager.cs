using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoSingleton<BulletManager>
{
    [SerializeField] private List<GameObject> bulletsOnPlane = new List<GameObject>();

    [SerializeField] private List<GameObject> bulletsInMag = new List<GameObject>();

    private void Start()
    {
        
    }

    public void RemoveBulletOnPlane(GameObject obj)
    {
        bulletsOnPlane.Remove(obj);
    }

    public bool IsThereBulletOnPlane()
    {
        if (bulletsOnPlane.Count == 0)
            return false;
        return true;
    }

    public void AddBulletOnPlane(GameObject obj)
    {
        bulletsOnPlane.Add(obj);
    }

    public void AddBulletInMag(GameObject obj)
    {
        bulletsInMag.Add(obj);
    }

    public bool IsThereBulletInMag()
    {
        if (bulletsInMag.Count == 0)
            return false;
        return true;
    }

    public void RemoveBulletInMag(GameObject obj)
    {
        bulletsInMag.Remove(obj);
    }

    public GameObject RemoveLastBulletFromMag()
    {
        GameObject lastBullet = bulletsInMag[bulletsInMag.Count - 1];
        bulletsInMag.Remove(lastBullet);
        return lastBullet;
    }

}
