using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class HUD : MonoBehaviour
{
    //Build Tile Grid
    private List<GameObject> tileList;
    private GameObject newPrefab;
    private int h;
    private int v;

    //Flowers
    private List<GameObject> flowerList;
    private List<Button> buttonsList;
    private GameObject newFlower;
    private int flowerIndex;

    //Raycast
    private RaycastHit2D ray;
    private Vector2 rayPos;
    private int t;

    //Increase Score
    private List<int> scoreList;
    private Text scoreText;
    private int score;
    private int s;

    //Progress Bar And Head
    private Image bar;
    private Image head;
    private Vector3 headPos;

    private Vector2 currentSizeBar;
    private Vector2 sizeBar;
    private float currentBar;
    public float totalBar;

    void Start ()
    {
        //Build Tile Grid
        tileList = new List<GameObject>();
	    for (h = 0; h < 9; h++)
        {
            for (v = 0; v < 5; v++)
            {
                newPrefab = Instantiate(Resources.Load("Tile")) as GameObject;
                tileList.Add(newPrefab);
                newPrefab.transform.position = new Vector3
                (newPrefab.transform.position.x + newPrefab.GetComponent<SpriteRenderer>().bounds.size.x * 1.15f * h,
                 newPrefab.transform.position.y - newPrefab.GetComponent<SpriteRenderer>().bounds.size.y * 1.2f * v,
                 newPrefab.transform.position.z);
                newPrefab.SetActive(false);
            }
        }

        //Flowers
        flowerList = new List<GameObject> { Resources.Load("Flower0") as GameObject,
                                            Resources.Load("Flower1") as GameObject };
        buttonsList = new List<Button> { transform.GetChild(2).GetComponent<Button>(),
                                         transform.GetChild(1).GetComponent<Button>() };
        foreach (Button b in buttonsList)
        {
            b.interactable = false;
        }
        newFlower = null;
        flowerIndex = 0;

        //Raycast
        rayPos = new Vector3();
        t = 0;

        //Increase Score
        scoreList = new List<int> { 50, 100 };
        scoreText = transform.GetChild(0).GetChild(0).GetComponent<Text>();
        score = 50;
        scoreText.text = score.ToString("000");
        s = 0;
        EnableButtons();

        //Progress Bar And Head
        bar = transform.GetChild(3).transform.GetComponent<Image>();
        head = transform.GetChild(5).transform.GetComponent<Image>();

        //
        headPos = new Vector3();
        headPos.y = head.transform.localPosition.y;
        headPos.z = head.transform.localPosition.z;

        currentBar = 0;
        currentSizeBar = bar.rectTransform.sizeDelta;
        sizeBar = bar.rectTransform.sizeDelta;

        bar.rectTransform.sizeDelta = new Vector2(0, bar.rectTransform.sizeDelta.y);

        headPos.x = bar.transform.localPosition.x - bar.rectTransform.sizeDelta.x;
        head.transform.localPosition = headPos;
    }

    void Update ()
    {
        if (Input.GetMouseButtonDown(0))
            PlantIt();
	}

    //Show And Hide Tile Grid
    private void TileGrid(bool enable)
    {
        foreach (GameObject t in tileList)
        {
            t.SetActive(enable);
        }
    }

    //Plant It
    private void PlantIt()
    {
        rayPos.x = Input.mousePosition.x;
        rayPos.y = Input.mousePosition.y;
        ray = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(rayPos), Vector2.zero);

        if (ray.collider != null)
        {
            for (t = 0; t < tileList.Count; t++)
            {
                if (ray.collider.gameObject == tileList[t].gameObject)
                {
                    newFlower = Instantiate
                        (flowerList[flowerIndex].gameObject,
                         tileList[t].transform.position,
                         tileList[t].transform.rotation) as GameObject;
                    TileGrid(false);
                }
            }
        }
    }

    //Enable Buttons
    private void EnableButtons()
    {
        for (s = 0; s < scoreList.Count; s++)
        {
            if (score >= scoreList[s])
            {
                buttonsList[s].interactable = true;
            }
            else
            {
                buttonsList[s].interactable = false;
            }
        }
    }

    //---------Public Methods---------\\
    //DefinehefFlower
    public void DefineFlower(int index)
    {
        flowerIndex = index;
    }
        
    //Add Score
    public void AddScore()
    {
        score += 25;
        scoreText.text = score.ToString("000");
        EnableButtons();
    }

    //Decrease Score
    public void DecreaseScore(int number)
    {
        score -= number;
        scoreText.text = score.ToString("000");
        TileGrid(true);
        EnableButtons();
    }

    //Decrease Progress Bar
    public void ProgressBar()
    {
        currentBar++;
        currentSizeBar.x = currentBar * sizeBar.x / totalBar;
        bar.rectTransform.sizeDelta = currentSizeBar; 
        
        //Head Following Bar
        headPos.x = bar.transform.localPosition.x - bar.rectTransform.sizeDelta.x;
        head.transform.localPosition = headPos;
    }

    //
    //Return Flower Index
    public int ReturnFlowerIndex()
    {
        return flowerIndex;
    }
}