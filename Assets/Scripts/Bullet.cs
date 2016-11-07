using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{
	void Update ()
    {
        transform.Translate(1 * Time.deltaTime, 0, 0);	
	}
    private void OnTriggerEnter2D(Collider2D c)
    {
        if (c.transform.GetComponent<Zombie>() != null)
        {
            c.transform.GetComponent<Zombie>().ApplyDamage(1);
            Destroy(gameObject);
        }
    }
}