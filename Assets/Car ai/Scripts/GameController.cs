using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public int numberOfCars;
    public CarstensDrivingSchool school;
    public GameObject carPrefab;
    public GameObject startLine;
    private List<GameObject> carPool = new List<GameObject>();

    //BestneuralNetwork

    // Start is called before the first frame update
    void Start()
    {
        school = new CarstensDrivingSchool("Assets/Car ai/Cars/", "template68421.txt", numberOfCars);
        SpawnCars();
    }

    private void SpawnCars()
    {
        foreach(var car in carPool)
        {
            Destroy(car);
        }
        carPool = new List<GameObject>();

        for (int i = 0; i < numberOfCars; i++)
        {
            GameObject car = Instantiate(carPrefab, startLine.transform.position, startLine.transform.rotation);// Quaternion.Euler(0, -100, 0));
            car.name = "Car " + i;
            AiClient aiClient = car.GetComponent<AiClient>();
            aiClient.network = school.TeamCarsten.Drivers[i].Brain;
            carPool.Add(car);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (carPool.All(car => !car.activeSelf))
        {
            for (int i = 0; i < carPool.Count; i++)
            {
                school.TeamCarsten.Drivers[i].Evaluate(carPool[i].GetComponent<Car>().secondsAlive);
            }
            school.Write();
            school.Select();
            school.Combine();
            SpawnCars();
        }
    }
}
