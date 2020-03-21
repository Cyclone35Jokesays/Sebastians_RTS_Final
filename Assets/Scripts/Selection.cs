using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Selection : MonoBehaviour
{
    public LayerMask clickable;
    public List<GameObject> cubes;
    //private Vector3 mousePoint;
    public ParticleSystem pointOfInterest;
    public ParticleSystem EnemyInterest;
    public GameObject target;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;

            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity, clickable))
            {
                if (cubes.Contains(hit.collider.gameObject) == false)
                {
                    hit.collider.GetComponent<Unit>().SelectChange();
                    hit.collider.GetComponent<Unit1>().SelectChange();
                    cubes.Add(hit.collider.gameObject);
                }

                else if (cubes.Contains(hit.collider.gameObject) == true)
                {
                    hit.collider.GetComponent<Unit>().DeselectChange();
                    hit.collider.GetComponent<Unit1>().DeselectChange();
                    cubes.Remove(hit.collider.gameObject);
                }
            }     
        }      

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            foreach (var Cube in cubes)
            {
                Cube.GetComponent<Unit>().DeselectChange();
                Cube.GetComponent<Unit1>().DeselectChange();
            }
            cubes.Clear();
        }

        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity))
            {
                foreach (var Cube in cubes)
                {   
                    if (hit.collider.tag == "Ground")
                    {                 
                        Instantiate(pointOfInterest, hit.point, Quaternion.identity);
                        Cube.GetComponent<Unit>().OrderTo(hit.point);
                        Cube.GetComponent<Unit1>().OrderTo(hit.point);
                    }

                    else if (hit.collider.tag == "Enemy")
                    {
                        Instantiate(EnemyInterest, hit.point, Quaternion.identity);
                        Cube.GetComponent<Unit>().AttackAction(target);
                        Cube.GetComponent<Unit1>().AttackAction(target);
                    }
                }
            }
        }
    }

    public void attack()
    {

    }
}
