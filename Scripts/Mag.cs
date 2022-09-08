using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class Mag : MonoSingleton<Mag>, ISlider
{
    private GameManager gameManager;

    private BulletManager bulletManager;

    private TransformManager transformManager;

    private Gun gun;

    private Collider collider;

    private Vector2 firstMousePos, lastMousePos;

    private void Start()
    {
        gameManager = GameManager.instance;
        bulletManager = BulletManager.instance;
        transformManager = TransformManager.instance;
        gun = Gun.instance;

        collider = GetComponent<Collider>();

        Actions.act_addedBulletToMag += GoUnderTheGun;
    }

    private void OnDestroy()
    {
        Actions.act_addedBulletToMag -= GoUnderTheGun;
    }


    private void OnMouseDown()
    {
        if(gameManager.GetState() == GameState.LiftUpMagazine)
            UpMag();
        if (gameManager.GetState() == GameState.DragMagazine)
            GetFirstClickPos();
    }

    private void OnMouseDrag()
    {
        CheckDistanceBetweenTwoMousePosAndDragMag();
    }

    private void CheckDistanceBetweenTwoMousePosAndDragMag()
    {

        if(gameManager.GetState() == GameState.DragMagazine)
        {
            lastMousePos = Input.mousePosition;
            if ((lastMousePos - firstMousePos).y > 30f)
                SettleMagToGun();
            
        }
        
    }

    private void SettleMagToGun()
    {
        
        transform.parent = Gun.instance.transform.GetChild(0);

        transform.DOLocalMove(transformManager.MagSettleToGunTransform.localPosition, 0.6f);
        transform.DOLocalRotate(transformManager.MagSettleToGunTransform.localEulerAngles, 0.6f).OnComplete(()=> {
            Actions.act_settledMagToGun?.Invoke();
        });
        

        
    }

    private void GetFirstClickPos()
    {
        firstMousePos = Input.mousePosition;
    }

    private void UpMag()
    {
        transform.DORotate(transformManager.MagUpTransform.localEulerAngles, 0.7f);
        transform.DOMove(transformManager.MagUpTransform.localPosition,0.7f).OnComplete(()=> {
            gameManager.SetState(GameState.PutBullets);
        });
    }

    private void GoUnderTheGun()
    {
        if (!bulletManager.IsThereBulletOnPlane())
        {
            transform.DOMove( transformManager.MagUnderGunTransform.localPosition, 0.4f);
            transform.DOLocalRotate( transformManager.MagUnderGunTransform.localEulerAngles, 0.4f).OnComplete(()=> {
                gun.MoveDragPosOfMag();
            });
        }
    }

    public void Touch(Action act)
    {
        act.Invoke();
    }
}
