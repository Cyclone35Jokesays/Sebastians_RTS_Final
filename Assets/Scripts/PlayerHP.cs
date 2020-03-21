using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHP : MonoBehaviour
{
    public float health = 100;
    public GameObject blood;

    private void Update()
    {
        if (health <= 0)
        {
           // Instantiate(blood, transform.position, Quaternion.identity);

           
        }
    }
    public void Damage()
    {
        health -= 10;
    }
}
