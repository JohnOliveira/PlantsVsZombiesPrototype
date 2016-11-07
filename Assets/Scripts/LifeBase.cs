using UnityEngine;
using System.Collections;

public abstract class LifeBase : MonoBehaviour
{
    private float currentLife;
    protected float totalLife;

    private Vector3 lifeBarSize;
    private Vector3 currentLifeBarSize;
    protected Transform lifeBar;


	protected void Start ()
    {
        currentLife = totalLife;

        lifeBarSize = lifeBar.localScale;
        currentLifeBarSize = lifeBar.localScale;
    }

    //Damage To All
    public void ApplyDamage (float damage)
    {
        currentLife -= damage;
        OnDamage();

        currentLifeBarSize.x = currentLife * lifeBarSize.x / totalLife;
        lifeBar.localScale = currentLifeBarSize;

        if (currentLifeBarSize.x <= 0)
        {
            OnDestroy();
        }
    }

    protected abstract void OnDamage();
    protected abstract void OnDestroy();

    //Return
    public float ReturnBar()
    {
        return currentLifeBarSize.x;
    }
}