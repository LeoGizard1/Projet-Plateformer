using UnityEditor.Search;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class Idle : PlayerState
{
    [SerializeField] float angularSpeed;
  
    [SerializeField] GameObject Point;
    GameObject[] Points;
    [SerializeField] int nbPoints;
    [SerializeField] float timeBetweenTrajPoints;

    void Start()
    {
        Points = new GameObject[nbPoints];
        for (int i = 0; i < nbPoints; i++)
        {
            Points[i] = Instantiate(Point);
        }
    }
    private void Update() 
    {
        if (Mathf.Abs(Move.ReadValue<Vector2>().x) >= 0.05)
        {
            float alpha = - Move.ReadValue<Vector2>().x * Time.deltaTime * angularSpeed;
            float newX = Controller.direction.x*Mathf.Cos(alpha) - Controller.direction.y*Mathf.Sin(alpha);
            float newY = Controller.direction.y * Mathf.Cos(alpha) + Controller.direction.x * Mathf.Sin(alpha);
            Controller.direction = new Vector2(newX, newY);
        }
        Controller.updatePower(Scroll.ReadValue<Vector2>().y);

        //Display jump trajectory
        trajectory();

        // If not on ground we fall
        if (!Controller.Grounded && Controller.IsOnWall == IsOnWall.None)
        {
            Debug.Log("Falling");
            enabled = false;
            GetComponent<Falling>().enabled = true;
        }
        else if (Jump.WasPressedThisFrame() && (Controller.Grounded || Controller.IsOnWall != IsOnWall.None))
        {
            Debug.Log("Jumping");
            enabled = false;
            GetComponent<Jumping>().enabled = true;
        }
    }

    private void OnEnable()
    {
        Rigidbody.velocity = new Vector2(0, 0);
        if (Points == null) return;
        foreach(GameObject p in Points)
        {
            p.SetActive(true);
        }
    }
    private void OnDisable()
    {
        foreach (GameObject p in Points)
        {
            p.SetActive(false);
        }
    }

    private void trajectory()
    {
        for (int i = 0; i < nbPoints; i++)
        {
            Points[i].transform.position = calcTrajPos(i * timeBetweenTrajPoints);
        }
    }

    private Vector2 calcTrajPos(float t)
    {
        Vector2 position = gameObject.transform.position;
        float x = position.x + Controller.direction.x* Controller.power *t;
        float y = position.y + Controller.direction.y* Controller.power *t - Controller.gravity*t*t*0.5f;
        return new Vector2(x, y);
    }
}