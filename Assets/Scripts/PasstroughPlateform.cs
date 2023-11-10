using UnityEngine;

public class PasstroughPlateform : MonoBehaviour
{
    private Rigidbody2D rigidbody2d;

    private Transform playerTransform;

    // Start is called before the first frame update
    private void Start()
    {
        playerTransform = FindObjectOfType<PlayerController>().transform;
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (playerTransform == null) return;
        rigidbody2d.simulated = playerTransform.position.y - playerTransform.lossyScale.y / 2.0f - transform.position.y > 0;
    }
}