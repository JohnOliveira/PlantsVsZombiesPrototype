using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour
{
	private void OnTriggerEnter2D(Collider2D c)
    {
        if (c.transform.GetComponent<Flower>() != null)
            gameObject.SetActive(false);
    }
}