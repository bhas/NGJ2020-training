using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public int numberOfCars;
    public CarstensDrivingSchool school;
    public GameObject carPrefab;
    private List<GameObject> allCars = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        school = new CarstensDrivingSchool("Assets/Car ai/", "template641.txt", numberOfCars);
        SpawnCars();
    }

    private void SpawnCars()
    {
        foreach(var car in allCars)
        {
            Destroy(car);
        }
        allCars = new List<GameObject>();

        for (int i = 0; i < numberOfCars; i++)
        {
            GameObject obj = Instantiate(carPrefab, new Vector3(76, 0.35f, -30), Quaternion.Euler(0, -100, 0));
            obj.name = "Car " + i;
            AiClient aiClient = obj.GetComponent<AiClient>();
            aiClient.network = school.TeamCarsten.Drivers[i].Brain;
            allCars.Add(obj);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (allCars.All(car => !car.activeSelf))
        {
            for (int i = 0; i < allCars.Count; i++)
            {
                school.TeamCarsten.Drivers[i].Score = allCars[i].GetComponent<Car>().secondsAlive;
            }
            school.Select();
            school.Combine();
            SpawnCars();
        }
    }
}
