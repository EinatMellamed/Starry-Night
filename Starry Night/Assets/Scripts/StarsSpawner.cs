using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarsSpawner : MonoBehaviour
{

    [SerializeField] GameObject starPrefab;
    [SerializeField] List<GameObject> stars = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {

        InvokeRepeating("CreateStar", 11, 1);
           
        
      
    }

    // Update is called once per frame
    void Update()
    {
        if(stars.Count>=11)
        {

            CancelInvoke();
        }
    }

    private void CreateStar()
    {

        float tempX = Random.Range(-8f, 8f);
        float tempY = Random.Range(-3.3f, 4.3f);

        Vector3 newPos = new Vector3(tempX, tempY, 0);
       GameObject star = Instantiate(starPrefab, newPos, transform.rotation);
        stars.Add(star);    

    }
}
