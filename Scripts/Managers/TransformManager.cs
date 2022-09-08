using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformManager : MonoSingleton<TransformManager>
{
    [Header("Bullet")]
    [SerializeField] private Transform bulletFirstTransform;

    [SerializeField] private Transform bulletSecondTransform;
    [Space(20)]


    [Header("Mag")]
    [SerializeField] private Transform magUpTransform;

    [SerializeField] private Transform magUnderGunTransform;

    [SerializeField] private Transform magSettleToGunTransform;
    [Space(20)]


    [Header("Gun")]
    [SerializeField] private Transform gunDragMagTransform;

    [SerializeField] private Transform gunDragSlideTransform;

    [SerializeField] private Transform gunShootTransform;
    [Space(20)]



    [Header("Slide")]

    [SerializeField] private Transform slideFirstTransform;

    [SerializeField] private Transform slideSecondTransform;

    [SerializeField] private Transform slideOnGunTransform;
    [Space(20)]

    [Header("Camera")]
    [SerializeField] private Transform cameraShootTransform;
    
    public Transform BulletFirstTransform { get => bulletFirstTransform;  }
    public Transform BulletSecondTransform { get => bulletSecondTransform; }

    public Transform MagUpTransform { get => magUpTransform; }
    public Transform MagUnderGunTransform { get => magUnderGunTransform; }
    public Transform MagSettleToGunTransform { get => magSettleToGunTransform;}

    public Transform GunDragMagTransform { get => gunDragMagTransform;  }
    public Transform GunDragSlideTransform { get => gunDragSlideTransform;  }
    public Transform GunShootTransform { get => gunShootTransform; }
    public Transform CameraShootTransform { get => cameraShootTransform; }

    public Transform SlideFirstTransform { get => slideFirstTransform;  }
    public Transform SlideSecondTransform { get => slideSecondTransform;}
    public Transform SlideOnGunTransform { get => slideOnGunTransform; }
}
