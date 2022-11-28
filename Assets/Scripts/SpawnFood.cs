using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFood : MonoBehaviour
{
    //Limites pra comida spawnar
    public Transform borderCima;
    public Transform borderBaixo;
    public Transform borderEsq;
    public Transform borderDir;

    //Prefab da comida
    public GameObject foodPrefab;

    //Função para criar as frutas dentro da arena
    void Spawn()
    {
        //Variáveis que definem a posição utilizando os eixos fornecidos pelas bordas da arena
        int x = (int)Random.Range(borderEsq.position.x, borderDir.position.x);
        int z = (int)Random.Range(borderCima.position.z, borderBaixo.position.z);
        Instantiate(foodPrefab, new Vector3(x + 1f, 0.5f, z + 1f), Quaternion.identity);

    }

    public void Start()
    {
        //Invoca a função Spawn repetidamente
        InvokeRepeating("Spawn", 3, 4);
    }
}