using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class InGamePanel : MonoBehaviour
{
    [SerializeField] private GameObject bulletImage;

    [SerializeField] private Transform layoutGroup;


    private Gun gun;

    private void Start()
    {
        gun = Gun.instance;

        Actions.act_addedBulletToMag += AddBulletOnLayout;
        Actions.act_shooted += RemoveBulletFromLayout;

        
    }


    private void OnDestroy()
    {
        Actions.act_addedBulletToMag -= AddBulletOnLayout;
        Actions.act_shooted -= RemoveBulletFromLayout;
    }

   

    private void AddBulletOnLayout()
    {
        Instantiate(bulletImage, layoutGroup);
    }

    private void RemoveBulletFromLayout()
    {
        Destroy(layoutGroup.GetChild(0)?.gameObject);
    }
}
