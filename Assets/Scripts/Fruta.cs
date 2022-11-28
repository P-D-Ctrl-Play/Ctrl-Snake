using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruta : MonoBehaviour
{
    // Velocidade de rotação da fruta
    public float rotSpeed = 60f;
    void Start()
    {
        //Faz a fruta se autodestruir depois de um tempo
        Destroy(gameObject, 8f);
    }

    // Update is called once per frame
    void Update()
    {
        //Faz a fruta girar parada no seu eixo Y
        transform.Rotate(0, rotSpeed * Time.deltaTime, 0, Space.World);
    }
}
