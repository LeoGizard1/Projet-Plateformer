using Unity.Mathematics;
using UnityEngine;

public class HeadbandScript : MonoBehaviour
{
    [SerializeField] private float speedFactor;
    [SerializeField] private float maxDeformation;
    private SpriteRenderer renderer;

    private Rigidbody2D rigidbody2d;

    // Start is called before the first frame update
    private void Start()
    {
        rigidbody2d = FindObjectOfType<PlayerController>().GetComponent<Rigidbody2D>();
        renderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    private void Update()
    {
        var v = rigidbody2d.velocity;
        var directionFactor = (Mathf.Abs(v.normalized.x) - Mathf.Abs(v.normalized.y) + 1) / 2.0f;
        var xDeformation = Mathf.Lerp(-maxDeformation, maxDeformation, directionFactor) * v.magnitude / speedFactor;

        renderer.size = new Vector2(1 + xDeformation, 1);

        if (v.x > 0)
            transform.rotation = Quaternion.Euler(Vector3.zero);
        else if (v.x < 0) 
            transform.rotation = quaternion.Euler(new Vector3(0, Mathf.PI, 0));
    }
}