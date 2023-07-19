using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] List<GameObject> UIPanels= new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CloseAllUIPanels()
    {

        foreach (GameObject panel in UIPanels)
        {
            panel.SetActive(false);
            
        }
    }
}
