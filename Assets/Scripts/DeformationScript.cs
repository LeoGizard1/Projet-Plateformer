using UnityEngine;

public class DeformationScript : MonoBehaviour
{
    [SerializeField] private float speedFactor;
    [SerializeField] private float maxDeformation;

    private SpriteRenderer renderer;
    private Rigidbody2D rigidbody2d;

    // Start is called before the first frame update
    private void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        var v = rigidbody2d.velocity;
        var directionFactor = (Mathf.Abs(v.normalized.x) - Mathf.Abs(v.normalized.y) + 1) / 2.0f;
        var xDeformation = Mathf.Lerp(-maxDeformation, maxDeformation, directionFactor) * v.magnitude / speedFactor;

        renderer.size = new Vector2(1 + xDeformation, 1 - xDeformation);
    }
}