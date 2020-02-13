using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public int numberOfCars;
    public GameObject carPrefab;

    // Start is called before the first frame update
    void Start()
    {
        CarstensDrivingSchool school = new CarstensDrivingSchool("Assets/Car ai/", "template641.txt", numberOfCars);

        for (int i = 0; i < numberOfCars; i++)
        {
            GameObject obj = Instantiate(carPrefab, new Vector3(76, 0.35f, -30), Quaternion.Euler(0, -100, 0));
            AiClient aiClient = obj.GetComponent<AiClient>();
            aiClient.network = school.TeamCarsten.Drivers[i].Brain;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
