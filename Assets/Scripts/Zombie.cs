using UnityEngine;
using System.Collections;
using System;

public class Zombie : LifeBase
{
    //States
    private enum States
    {
        WALK,
        ATTACK,
        DEATH
    }
    private States states;

    //Screen
    private float xMin;

    //Damage
    private bool damage;
    private Transform greenBar;

    void Start ()
    {
        //States
        StateManager(States.WALK);

        //Screen
        xMin = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0)).x / 1.5f;

        //Damage
        damage = false;
        greenBar = transform.GetChild(0).transform;

        //Inherit
        totalLife = 10;
        lifeBar = greenBar;
        base.Start();
    }
	void Update ()
    {
        switch (states)
        {
            case States.WALK:
                transform.Translate(-0.05f * Time.deltaTime, 0, 0);
                break;
            case States.ATTACK:
                break;
            case States.DEATH:
                break;
        }
	}

    //States Manager
    private void StateManager(States state)
    {
        states = state;
    }

    //Check Collision
    private void OnCollisionEnter2D(Collision2D c)
    {
        if (c.transform.GetComponent<Flower>() != null)
        {
            GetComponent<Animator>().SetBool("attack", true);
            StateManager(States.ATTACK);
        }
    }
    private void OnCollisionStay2D(Collision2D c)
    {
        if (c.transform.GetComponent<Flower>() != null)
        {
            if (damage)
            {
                c.transform.GetComponent<Flower>().ApplyDamage(2.5f);
                damage = false;
            }
            if (c.transform.GetComponent<Flower>().ReturnBar() <= 0)
            {
                StateManager(States.WALK);
                GetComponent<Animator>().SetBool("attack", false);
            }
        }
    }
    //Apply Damage On Oponent
    private void ApplyDamage()
    {
        damage = true;
    }
    //Destroy It Self When Death Animation Runs Out
    private void DestroyItSelf()
    {
        Destroy(gameObject);
    }
    protected override void OnDamage() {  }
    protected override void OnDestroy()
    {
        transform.GetChild(0).gameObject.SetActive(false);
        transform.GetChild(1).gameObject.SetActive(false);
        GetComponent<Animator>().SetTrigger("die");
        StateManager(States.DEATH);
    }
}