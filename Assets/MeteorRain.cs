using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorRain : MonoBehaviour
{

    [SerializeField] private GameObject meteor;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        while(true) {
            yield return new WaitForSeconds(3f);
            Create();
        }     
    }

    private void Create()
    {
        GameObject meteorObject = Instantiate(meteor, transform.position,transform.rotation);
        meteorObject.GetComponent<ConstantForce>().force = new Vector3(UnityEngine.Random.Range(-10.0f, 10.0f), UnityEngine.Random.Range(-10.0f, 0f), 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
