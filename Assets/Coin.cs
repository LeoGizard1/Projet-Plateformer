using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private float speed;
    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
        transform.localScale = new Vector3(Mathf.Cos(Time.time * speed),1,1);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        Destroy(gameObject);
    }
}
