using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pointer = cam.ScreenToWorldPoint(Input.mousePosition);
        pointer = new Vector3(pointer.x, pointer.y, 0);
        transform.position = pointer;
    }
}
