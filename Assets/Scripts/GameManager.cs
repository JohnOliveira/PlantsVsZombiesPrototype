using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    //Class
    private HUD hud;

    //Screen
    private float yLimit;
    private float xMin;
    private float xMax;

    //Elements Spawn
    private GameObject newSun;
    private GameObject newZombie;
    private int c;
    private int z;
    private int i;
    private Vector3 sunPos;
    private Vector3 zombiesPos;
    private float randomSun;
    private int randomZombie;
    public Transform[] positions;

    private int totalZombiePerLevel;
    private int zombieLimit;

    //Lists
    private List<GameObject> elementsList;
    private List<float> counter;

    void Start ()
    {
        //Screen Resize
        Camera.main.projectionMatrix = Matrix4x4.Ortho(-3f * 1.6f, 3f * 1.6f, -3f, 3f, 0.3f, 5f);

        //Class
        hud = FindObjectOfType(typeof(HUD)) as HUD;

        //Screen
        yLimit = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, 0)).y;
        xMin = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0)).x / 1.5f;
        xMax = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, 0)).x / 1.2f;

        //Elements Spawn
        elementsList = new List<GameObject> { Resources.Load("Sun") as GameObject,
                                              Resources.Load("Zombie0") as GameObject };
        counter = new List<float> { 0, 0 };
        newSun = null;
        newZombie = null;
        c = 0;
        sunPos = new Vector3();
        zombiesPos = new Vector3();
        randomSun = 0;
        randomZombie = 0;

        totalZombiePerLevel = 5;
        zombieLimit = 0;
        hud.totalBar = totalZombiePerLevel;
    }

	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();

        CounterManager();
    }

    //Counter Manager
    private void CounterManager()
    {
        for (c = 0; c < counter.Count; c++)
        {
            counter[c] += Time.deltaTime;
        }

        //Sun
        if (counter[0] > 10)
        {
            randomSun = Random.Range(xMin, xMax);
            sunPos.x = randomSun;
            sunPos.y = yLimit + elementsList[0].GetComponent<SpriteRenderer>().bounds.size.y;
            sunPos.z = elementsList[0].transform.position.z;

            newSun = Instantiate
                (elementsList[0].gameObject, sunPos, elementsList[0].transform.rotation) as GameObject;

            counter[0] = 0;
        }
        //Zombie
        if (counter[1] > 17)
        {
            if (zombieLimit < totalZombiePerLevel)
            {
                randomZombie = Random.Range(0, 5);

                zombiesPos = positions[randomZombie].position;
                newZombie = Instantiate(elementsList[1].gameObject, zombiesPos, transform.rotation) as GameObject;

                hud.ProgressBar();

                zombieLimit++;
            }
            counter[1] = 0;
        }
    }
}