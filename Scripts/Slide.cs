using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class Slide : MonoSingleton<Slide>, ISlider
{
    private GameManager gameManager;

    private TransformManager transformManager;

    private Vector2 firstMousePos, lastMousePos;

    private bool isStelled = false; 

    private void Start()
    {
        gameManager = GameManager.instance;
        transformManager = TransformManager.instance;
    }

    public void MoveOnGun()
    {
        transform.parent = Gun.instance.transform.GetChild(0);

        /*Vector3 pos = new Vector3(0,0.176f,0.732f);
        Vector3 rot = new Vector3(0,0,0);
        */

        transform.DOLocalMove(transformManager.SlideFirstTransform.localPosition, 1f);
        transform.DOLocalRotate(transformManager.SlideFirstTransform.localEulerAngles, 1f).OnComplete(() => {     // silahýn önüne geldi

            //Vector3 posInGun = new Vector3(0, 0.176f, 0.408f);

            transform.DOLocalMove(transformManager.SlideSecondTransform.localPosition,0.4f).OnComplete(()=> {  // silaha biraz takýldý
                gameManager.SetState(GameState.DragSlice);
            });
        });

    }

    private void OnMouseDown()
    {
        if (gameManager.GetState() == GameState.DragSlice)
            SetFirstClickPos();
        
    }

    private void SetFirstClickPos()
    {
        firstMousePos = Input.mousePosition;
    }

    private void OnMouseDrag()
    {
        CheckDistanceBetweenTwoMousePosAndDragSlide();
    }

    private void CheckDistanceBetweenTwoMousePosAndDragSlide()
    {

        if (gameManager.GetState() == GameState.DragSlice && !isStelled)
        {
        

            lastMousePos = Input.mousePosition;
            if ((lastMousePos - firstMousePos).x > 3f)
            {
                isStelled = true;
                // Vector3 pos = new Vector3(0, 0.176443f, 0.1266894f);

                transform.DOLocalMove(transformManager.SlideOnGunTransform.localPosition, 0.6f).OnComplete(()=> {
                    Actions.act_gunReady?.Invoke();
                });
            }
        }
    }


    void ISlider.Touch(Action act)
    {
        act.Invoke();
    }
}
