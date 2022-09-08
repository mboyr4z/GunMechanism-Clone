using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class Gun : MonoBehaviour, ISlider
{
    public static Gun instance;

    [SerializeField] private Transform bulletOutPoint;
    
    private GameManager gameManager;

    private BulletManager bulletManager;

    private TransformManager transformManager;

    private Collider collider;

    private float fireRate = 0.5f;

    private float lastFireTime = -1f;


    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        bulletManager = BulletManager.instance;
        gameManager = GameManager.instance;
        transformManager = TransformManager.instance;

        collider = GetComponent<Collider>();
        
        Actions.act_settledMagToGun += MoveDragPosOfSlide;
        Actions.act_gunReady += MoveShootPos;
    }

    private void OnDestroy()
    {
        Actions.act_settledMagToGun -= MoveDragPosOfSlide;
        Actions.act_gunReady -= MoveShootPos;

    }

    private void MoveShootPos()
    {
        transform.DOMove(transformManager.GunShootTransform.localPosition, 1f);
        transform.DOLocalRotate(transformManager.GunShootTransform.localEulerAngles, 1f).OnComplete(() => {
            gameManager.SetState(GameState.Shoot);
            CameraController.instance.MoveShootPos();
        });

    }

    public void MoveDragPosOfMag()
    {
        if (!bulletManager.IsThereBulletOnPlane())
        {
            collider.enabled = false;

            transform.DOMove(transformManager.GunDragMagTransform.localPosition, 0.2f);
            transform.DOLocalRotate(transformManager.GunDragMagTransform.localEulerAngles, 0.2f).OnComplete(() => {

                gameManager.SetState(GameState.DragMagazine);
                Mag.instance.transform.parent = transform.GetChild(0);

            });
        }
    }

    private void MoveDragPosOfSlide()
    {
        transform.DOMove(transformManager.GunDragSlideTransform.localPosition, 0.6f);
        transform.DOLocalRotate(transformManager.GunDragSlideTransform.localEulerAngles, 0.6f).OnComplete(() => {
            Slide.instance.MoveOnGun();
        });
    }


    private void Update()
    {
        if (Input.GetKey(KeyCode.Space) || Input.GetMouseButtonUp(0))
        {
            Shoot();
        }
    }


    public void Shoot()
    {
        if(gameManager.GetState() == GameState.Shoot)
        {
            if (bulletManager.IsThereBulletInMag())
            {
                if (Time.time - lastFireTime > fireRate)
                {
                    lastFireTime = Time.time;

                    GameObject bullet = bulletManager.RemoveLastBulletFromMag();
                    Rigidbody rb = bullet.AddComponent<Rigidbody>();

                    rb.useGravity = false;

                    bullet.transform.position = bulletOutPoint.position;
                    bullet.transform.forward = -transform.forward;

                    rb.AddForce(bullet.transform.forward * 1000);

                    gameManager.CheckIsFinishGame();
                    Actions.act_shooted?.Invoke();

                }
               
            }
          
        }
    }

    public void Touch(Action act)
    {
        act.Invoke();
    }
}
