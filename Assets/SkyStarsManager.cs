using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyStarsManager : MonoBehaviour
{
    public bool starsEnabled = true;
    [SerializeField] private GameObject Sky;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //NOT WORKING
        if(starsEnabled){
            foreach (Transform star in Sky.transform)
            {
                star.gameObject.GetComponent<Light>().enabled = true;
            }
            new WaitForSeconds(3f);
            foreach (Transform star in Sky.transform)
            {
                star.gameObject.GetComponent<Light>().enabled = false;
            }
            starsEnabled = false;
        }
    }
}
