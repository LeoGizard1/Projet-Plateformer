using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PasstroughPlateform : MonoBehaviour
{
    private Transform playerTransform;
    private Rigidbody2D _rigidbody2D;
    // Start is called before the first frame update
    void Start()
    {
        playerTransform = FindObjectOfType<PlayerController>().transform;
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerTransform != null)
        {
            if (playerTransform.position.y - playerTransform.lossyScale.y / 2.0f - transform.position.y > 0)
            {
                _rigidbody2D.simulated = true;
            }
            else
            {
                _rigidbody2D.simulated = false;
            }
        }
    }
}
