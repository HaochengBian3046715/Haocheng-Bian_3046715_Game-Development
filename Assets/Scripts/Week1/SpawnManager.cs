using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{

    public GameObject spawnObj; 
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject spawn = Instantiate(spawnObj,transform.position,transform.rotation);
            spawnObj.SetActive(true);

            Destroy(spawn,10f);
        }
    }
}
