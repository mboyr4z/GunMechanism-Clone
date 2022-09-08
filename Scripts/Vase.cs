using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vase : MonoBehaviour, ITouchable
{
    private VaseManager vaseManager;

    private Renderer renderer;

    private GameObject group;

    private void Start()
    {
        vaseManager = VaseManager.instance;
        vaseManager.AddVase(gameObject);

        renderer = GetComponent<Renderer>();
        group = transform.GetChild(0).gameObject;
    }
    public void Touch(Action<TouchableCategory, Transform> action)
    {
        action.Invoke(TouchableCategory.Vase, transform);
        BrokeVase();
    }

    private void BrokeVase()
    {
        vaseManager.RemoveVase(gameObject);
        Physics.gravity = new Vector3(0,-0.1f,0);

        renderer.enabled = false;

        group.SetActive(true);

        foreach (Transform piece in group.transform)
        {
            Rigidbody rb = piece.gameObject.AddComponent<Rigidbody>();
            
            Vector3 direction = (piece.position - group.transform.position).normalized;

            rb.AddForce((-direction + Vector3.up / 5) * 60);
        }

        Destroy(gameObject,3f);
    }

    
}
