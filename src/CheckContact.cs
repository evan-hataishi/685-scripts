using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckContact : MonoBehaviour
{

    // speaker is static, so we don't actually have to create a new pointer to it
    public GameObject speaker;

    // Start is called before the first frame update
    void Start()
    {
        speaker = GameObject.Find ("speaker");
        print(gameObject.transform.position);
        print(speaker.transform.position);
        Vector3 dirFromAtoB = (gameObject.transform.position - speaker.transform.position).normalized;
        float dotProd = Vector3.Dot(dirFromAtoB, speaker.transform.forward);

        print(dotProd);
        
        if(dotProd > 0.9) {
          // ObjA is looking mostly towards ObjB
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
