using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Atirar : MonoBehaviour
{
    public GameObject PontoDeTiro;

    public GameObject TirinhoPrefab;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("e"))
        {
            PewPew();
        }
        
    }

    public void PewPew()
    {
        Instantiate(TirinhoPrefab, PontoDeTiro.transform.position, PontoDeTiro.transform.rotation);
    }
}
