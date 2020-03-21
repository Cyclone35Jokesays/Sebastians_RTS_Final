using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuUI1 : MonoBehaviour
{
    public GameObject winCondition;
    public GameObject loseCondition;

    private void Awake()
    {
        Unit[] units = FindObjectsOfType<Unit>();
        Unit1[] unit = FindObjectsOfType<Unit1>();

        Enemy[] enemy = FindObjectsOfType<Enemy>();
        Enemy1[] enemies = FindObjectsOfType<Enemy1>();
    }

    private void Update()
    {
        Unit[] units = FindObjectsOfType<Unit>();
        Unit1[] unit = FindObjectsOfType<Unit1>();

        if (units.Length < 0 && unit.Length < 0)
        {
            loseCondition.SetActive(true);
        }

        Enemy[] enemy = FindObjectsOfType<Enemy>();
        Enemy1[] enemies = FindObjectsOfType<Enemy1>();

        if (enemy.Length < 0 && enemies.Length < 0)
        {
            winCondition.SetActive(true);
        }
    }
}
