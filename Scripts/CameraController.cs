using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraController : MonoSingleton<CameraController>
{
    [SerializeField] float mouseSensitivity;

    private TransformManager transformManager;

    private float mouseX, mouseY;

    private float rotationX = 0, rotationY = 0;

    private bool isCamInAimMode = false;


    private void Start()
    {
        transformManager = TransformManager.instance;
    }


    public void MoveShootPos()
    {
        transform.DOLocalMove(transformManager.CameraShootTransform.localPosition, 0.6f);
        transform.DOLocalRotate(transformManager.CameraShootTransform.localEulerAngles, 0.6f).OnComplete(()=> {

            Gun.instance.transform.SetParent(transform);

            isCamInAimMode = true;
        });
    }

    private void Update()
    {
        if (isCamInAimMode)
        {
            
            mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

            rotationY += mouseX;
            rotationX += -mouseY;

            transform.localEulerAngles = new Vector3(rotationX, rotationY + 180 ,0);
        }
    }


}
