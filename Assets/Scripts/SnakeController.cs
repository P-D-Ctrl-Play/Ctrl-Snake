using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeController : MonoBehaviour {

    // Variáveis do corpo
    public float MoveSpeed = 5;
    public float SteerSpeed = 180;
    public float BodySpeed = 5;
    public int Gap = 10;

    //Referência de script externo
    private ControladorJogo controladorJogo;

    // Referencias de prefab
    public GameObject BodyPrefab;

    // Listas
    private List<GameObject> BodyParts = new List<GameObject>();
    private List<Vector3> PositionsHistory = new List<Vector3>();

    void Start() {

        //Procura pela referência do objeto na hierarquia
        controladorJogo = GameObject.Find("Controlador").GetComponent<ControladorJogo>();

        //Inicia o game com uma parte do corpo
        GrowSnake();
       
    }

    void Update() {

        // Move em frente
        transform.position += transform.forward * MoveSpeed * Time.deltaTime;

        // Direcionais
        float steerDirection = Input.GetAxis("Horizontal"); // Returns value -1, 0, or 1
        transform.Rotate(Vector3.up * steerDirection * SteerSpeed * Time.deltaTime);

        // Armazena posiçãpo
        PositionsHistory.Insert(0, transform.position);

        // Move as partes do corpo
        int index = 0;
        foreach (var body in BodyParts) {
            Vector3 point = PositionsHistory[Mathf.Clamp(index * Gap, 0, PositionsHistory.Count - 1)];

            // Move as partes do corpo de acordo com o caminho da cobra
            Vector3 moveDirection = point - body.transform.position;
            body.transform.position += moveDirection * BodySpeed * Time.deltaTime;

            // Rotaciona as partes do corpo de acordo com o caminho da cobra
            body.transform.LookAt(point);

            index++;
        }
    }

    //Função para adicionar partes do corpo
    private void GrowSnake() {
        // Instancia a parte do corpo e
        // adiciona na lista
        GameObject body = Instantiate(BodyPrefab);
        BodyParts.Add(body);
    }

    //Função de colisão gatilho
    private void OnTriggerEnter(Collider other)
    {
        //Verifica se pegou a comida e instancia uma parte nova do corpo
        if (other.gameObject.CompareTag("food"))
        {
            // Instancia a parte do corpo e
            // adiciona na lista
            Debug.Log("Comeu");
            GrowSnake();

            //Adiciona score
            controladorJogo.pontuacao++;

            //Destroi a fruta
            Destroy(other.gameObject);


        }

        //Verifica se bateu em uma borda
        if (other.gameObject.CompareTag("borda"))
        {
            Debug.Log("Bateu");
            //Chama o fim do jogo se bater na borda com a cabeça
            Destroy(gameObject);
            controladorJogo.FimDeJogo();
        }

        //Verifica se bateu no corpo
        if (other.gameObject.CompareTag("corpo"))
        {
            Debug.Log("Bateu no corpo");
            //Chama o fim do jogo se bater na borda com a cabeça
            Destroy(gameObject);
            controladorJogo.FimDeJogo();
        }
    }
}