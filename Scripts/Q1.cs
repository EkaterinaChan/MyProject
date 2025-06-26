using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Q1 : MonoBehaviour
{
    public int questNumber;
    public int[] items;
    public GameObject[] cloudss;
    public GameObject barrier;
    public GameObject key;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (questNumber < items.Length) 
        {   
            if(other.tag != "Player" && other.gameObject.GetComponent<Pickup>().id == items[questNumber])
            {
                questNumber++;
                Destroy(other.gameObject);
                CheckQuest();
            }
        }    
    }

    public void CheckQuest()
    {
        if(questNumber == 3)
        {
            key.SetActive(true);
        }   
    }
}
