using UnityEngine;
using System.Collections;

public class Sun : MonoBehaviour
{
    //Class
    private GameManager gm;
    private HUD hud;

    //Raycast
    private RaycastHit2D ray;
    private Vector2 rayPos;

    //Screen
    private float yLimit;

    //Random
    private int random;

	void Start ()
    {
        //Class
        gm = FindObjectOfType(typeof(GameManager)) as GameManager;
        hud = FindObjectOfType(typeof(HUD)) as HUD;

        //Raycast
        rayPos = new Vector3();

        //Screen
        yLimit = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0)).y;

        //Random
        random = Random.Range(0, 5);
    }

    void Update ()
    {
        if (transform.position.y > gm.positions[random].position.y)
            transform.Translate(0, -.8f * Time.deltaTime, 0);

        if (Input.GetMouseButtonDown(0))
            CollectItSelf();
    }

    //Collect ItSelf
    private void CollectItSelf()
    {
        rayPos.x = Input.mousePosition.x;
        rayPos.y = Input.mousePosition.y;

        ray = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(rayPos), Vector2.zero);

        if (ray.collider != null)
        {
            if (ray.collider.gameObject == gameObject)
            {
                Destroy(ray.collider.gameObject);
                hud.AddScore();
            }
        }
    }
}