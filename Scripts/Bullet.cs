using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Bullet : MonoBehaviour
{
    private GameManager gameManager;

    private BulletManager bulletManager;

    private TransformManager transformManager;

    private Rigidbody rb;

    private Vector3 offset;

    private bool isLeft = true;

    private bool isStelled = false;

    private Vector2 firstMousePos, lastMousePos, diff;

    private Vector3 firstUpPos;
    private void Start()
    {
        gameManager = GameManager.instance;
        bulletManager = BulletManager.instance;
        transformManager = TransformManager.instance;

        bulletManager.AddBulletOnPlane(gameObject);

        rb = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        other.GetComponent<ITouchable>()?.Touch(SelectFuncByTouchableCategory);
        other.GetComponent<ISlider>()?.Touch(CloseGravity);
    }

    private void OnTriggerStay(Collider other)
    {
        other.GetComponent<ISlider>()?.Touch(Slide);
    }

    private void OnTriggerExit(Collider other)
    {
        other.GetComponent<ISlider>()?.Touch(Fall);
    }

    private void CloseGravity()
    {
        rb.useGravity = false;
        rb.velocity = Vector3.zero;
    }

    private void Slide()
    {
        if(isLeft)
            rb.AddForce(-transform.up * 0.1f);
    }

    private void Fall()
    {
        if (isLeft)
        {
            rb.AddForce(-transform.up * 1f);
            rb.useGravity = true;
        }
    }




    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            UpBullet();
        }

        if (Input.GetMouseButton(0))
        {
            DragBullet();
        }

        if (Input.GetMouseButtonUp(0))
        {
            BreakBullet();
        }
       
    }

    private void BreakBullet()
    {
        if (!isLeft)
        {
            DOTween.Kill(1);
            rb.useGravity = true;
            isLeft = true;
        }
    }
    private void UpBullet()
    {
        if (isStatePutBullets())
        {

            Vector3 mousePos = Input.mousePosition;

            mousePos.z = Camera.main.transform.position.z;

            Ray ray = Camera.main.ScreenPointToRay(mousePos);

            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 10))
            {
                if(hit.collider.gameObject == gameObject)
                {

                    firstMousePos = Input.mousePosition;
                    firstUpPos = transform.position;

                    rb.useGravity = false;
                    rb.velocity = Vector3.zero;

                    transform.DOLocalMoveY(-0.491f, 0.4f).SetId(1);

                    isLeft = false;
                }
            }
        }
    }

    private void DragBullet()
    {
        if (!isLeft)
        {
            

            lastMousePos = Input.mousePosition;

            diff = (lastMousePos - firstMousePos) / Screen.width;
            diff *= 1.5f;

            transform.position = Vector3.Lerp(
                transform.position,
                new Vector3(firstUpPos.x - diff.x, transform.position.y, firstUpPos.z - diff.y), 
                0.04f);

           

        }
    }

    private bool isStatePutBullets()
    {
        if (gameManager.GetState() == GameState.PutBullets)
            return true;
        return false;
    }



   

    private void SelectFuncByTouchableCategory(TouchableCategory category, Transform touchableTransform)
    {
        if(category == TouchableCategory.BulletZone)
        {
            SettleToMag(touchableTransform);
        }else if (category == TouchableCategory.Vase)
        {
            Destroy(gameObject);
        }
    }

    private void SettleToMag(Transform touchableTransform)
    {
        if (isLeft && !isStelled)
        {
            isStelled = true;
            transform.parent = touchableTransform.parent;
            Destroy(rb);


            transform.DOLocalMove(transformManager.BulletFirstTransform.localPosition, 0.6f);
            transform.DOLocalRotate(transformManager.BulletFirstTransform.localEulerAngles ,0.6f).OnComplete(()=> {

                transform.DOLocalMove(transformManager.BulletSecondTransform.localPosition,0.5f).OnComplete(() => {

                    bulletManager.AddBulletInMag(gameObject);
                    bulletManager.RemoveBulletOnPlane(gameObject);

                    Actions.act_addedBulletToMag?.Invoke();
                });
            });
        }
    }

}
