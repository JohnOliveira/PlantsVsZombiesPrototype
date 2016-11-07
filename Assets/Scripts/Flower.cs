using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Flower : LifeBase
{
    //Enum
    private enum FlowerBehaviours
    {
        FLOWER_0,
        FLOWER_1
    }
    private FlowerBehaviours fb;

    //Class
    private HUD hud;

    //Raycast
    private RaycastHit2D[] ray;
    private Vector2 rayPos;
    private float mathRay;
    private float xLimit;
    private Transform greenBar;

    //List
    private List<GameObject> elementsList;
    private GameObject newBullet;
    private List<float> elementsCounter;
    private Vector3 listPos;

	void Start ()
    {
        //Class
        hud = FindObjectOfType(typeof(HUD)) as HUD;

        //Raycast
        rayPos = new Vector2();
        xLimit = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, 0)).x -
            GetComponent<SpriteRenderer>().bounds.size.x;
        greenBar = transform.GetChild(0).transform;

        //List
        elementsList = new List<GameObject> { Resources.Load("Bullet") as GameObject,
                                              Resources.Load("Sun") as GameObject };
        newBullet = null;
        elementsCounter = new List<float> { 0, 0 };
        listPos = new Vector3();

        //Inherit
        totalLife = 10;
        lifeBar = greenBar;
        base.Start();

        switch(hud.ReturnFlowerIndex())
        {
            case 0:
                ChangeBehaviour(FlowerBehaviours.FLOWER_0);
                break;
            case 1:
                ChangeBehaviour(FlowerBehaviours.FLOWER_1);
                break;
        }
    }
	void Update ()
    {
        switch (fb)
        {
            case FlowerBehaviours.FLOWER_0:
                Behaviour0();
                break;
            case FlowerBehaviours.FLOWER_1:
                Behaviour1();
                break;
        }        
    }

    protected override void OnDamage() { }
    protected override void OnDestroy() { Destroy(gameObject); }

    //Change Behaviour
    private void ChangeBehaviour(FlowerBehaviours behaviour)
    {
        fb = behaviour;
    }

    //Spawn Behaviour 0
    private void Behaviour0()
    {
        rayPos.x = transform.position.x + GetComponent<SpriteRenderer>().bounds.size.x / 2;
        rayPos.y = transform.position.y;

        mathRay = Mathf.Abs(transform.position.x - xLimit);
        ray = Physics2D.RaycastAll(rayPos, transform.right, mathRay);

        Debug.DrawRay(rayPos, transform.right * mathRay, Color.red);

        for (int i = 0; i < ray.Length; i++)
        {
            if (ray[i].collider != null)
                if (ray[i].collider.GetComponent<Zombie>() != null)
                {
                    elementsCounter[0] += Time.deltaTime;
                    if (elementsCounter[0] > 3)
                    {
                        GetComponent<Animator>().SetTrigger("attack");

                        elementsCounter[0] = 0;
                    }
                    break;
                }
        }
    }

    //Spawn Behaviour 1
    private void Behaviour1()
    {
        elementsCounter[1] += Time.deltaTime;
        if (elementsCounter[1] > 20)
        {
            listPos.x = transform.position.x;
            listPos.y = transform.position.y;
            listPos.z = elementsList[1].transform.position.z;

            newBullet = Instantiate
                (elementsList[1].gameObject, listPos, transform.rotation) as GameObject;

            newBullet.transform.localScale = newBullet.transform.localScale / 2;

            elementsCounter[1] = 0;
        }
    }

    //To Spawn Through End Animation
    private void Spawn()
    {
        listPos.x = transform.position.x + GetComponent<SpriteRenderer>().bounds.size.x / 2;
        listPos.y = transform.position.y + GetComponent<SpriteRenderer>().bounds.size.y / 3.2f;
        listPos.z = transform.position.z;

        newBullet = Instantiate
            (elementsList[0].gameObject, listPos, transform.rotation) as GameObject;
    }
}