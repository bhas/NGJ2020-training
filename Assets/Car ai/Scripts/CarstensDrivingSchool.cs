using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarstensDrivingSchool
{
    public Team TeamCarsten;
    private string Folder;
    private string File;
    private System.Random Rand = new System.Random();

    public CarstensDrivingSchool(string folder, string file, int size)
    {
        Folder = folder;
        File = file;
        TeamCarsten = new Team(Rand, folder + file, size);
    }

    public void Teach(int lessons, float duration)
    {
        for (int i = 0; i < lessons; i++)
        {
            // Drive(duration);
            Select();
            Combine();
        }
        Best();
    }

    public void Select()
    {
        float averageScore = 0;
        for (int i = 0; i < TeamCarsten.Size; i++)
        {
            averageScore += TeamCarsten.Drivers[i].Score;
        }
        averageScore = averageScore / TeamCarsten.Size;
        int j = 0;
        while (j < TeamCarsten.Size)
        {
            if (averageScore > TeamCarsten.Drivers[j].Score)
            {
                TeamCarsten.Drivers.RemoveAt(j);
                TeamCarsten.Size--;
            }
            else
            {
                TeamCarsten.Drivers[j].Level++;
            }
            j++;
        }
    }

    public void Combine()
    {
        //System.Random random = new System.Random();
        int numberOfDrivers = TeamCarsten.Drivers.Count;
        int driverId = numberOfDrivers;
        while (driverId < TeamCarsten.Size)
        {
            Driver Senna = TeamCarsten.Drivers[Rand.Next(0, numberOfDrivers - 1)];
            Driver Hamilton = TeamCarsten.Drivers[Rand.Next(0, numberOfDrivers - 1)];
            if (Senna.Id != Hamilton.Id)
            {
                Driver Schumacher = new Driver(Rand, Folder + File, driverId, false);
                for (int i = 0; i < Schumacher.Brain.Layers.Length; i++)
                {
                    for (int j = 0; j < Schumacher.Brain.Layers[i].Nodes.Length; j++)
                    {
                        for (int k = 0; k < Schumacher.Brain.Layers[i].Nodes[j].Weights.Length; k++)
                        {
                            float weight = (float)Rand.NextDouble() < 0.5 ? Senna.Brain.Layers[i].Nodes[j].Weights[k] : Hamilton.Brain.Layers[i].Nodes[j].Weights[k];
                            Schumacher.Brain.Layers[i].Nodes[j].Weights[k] = weight;
                        }
                        Schumacher.Randomize(Rand);
                    }
                }
                TeamCarsten.Drivers.Add(Schumacher);
                driverId++;
            }
        }
    }

    public void Best()
    {
        int driverId = 0;
        float bestScore = 0;
        for (int i = 0; i < TeamCarsten.Drivers.Count; i++)
        {
            if (TeamCarsten.Drivers[i].Score > bestScore)
            {
                driverId = i;
                bestScore = TeamCarsten.Drivers[i].Score;
            }
        }
        string path = Folder + "driver" + driverId + ".txt";
        TeamCarsten.Drivers[driverId].Brain.Write(path);
    }
}

public class Team
{
    public int Size;
    public List<Driver> Drivers = new List<Driver>();

    public Team(System.Random rand, string path, int size)
    {
        Size = size;
        for (int i = 0; i < size; i++)
        {
            Driver driver = new Driver(rand, path, i);
            Drivers.Add(driver);
        }
    }
}

public class Driver
{
    public int Id;
    public int Level;
    public float Score;
    public NeuralNetwork Brain;

    public Driver(System.Random rand, string path, int id, bool randomize = true)
    {
        this.id = id;
        Level = 0;
        Score = 0;
        Brain = new NeuralNetwork(path);
        if (randomize)
        {
            Randomize(rand);
        }
    }

    public void Randomize(System.Random rand)
    {
        //System.Random random = new System.Random();
        for (int i = 0; i < Brain.Layers.Length; i++)
        {
            for (int j = 0; j < Brain.Layers[i].Nodes.Length; j++)
            {
                for (int k = 0; k < Brain.Layers[i].Nodes[j].Weights.Length; k++)
                {
                    if (0.5 > (float)rand.NextDouble())
                    {
                        float factor = (float)rand.NextDouble() < 0.5 ? (float)(rand.NextDouble() * 4 - 2) : 1;
                        Brain.Layers[i].Nodes[j].Weights[k] = Brain.Layers[i].Nodes[j].Weights[k] * factor;
                    }
                }
            }
        }
    }

    public float Evaluate(float score)
    {
        Score = score; // calculate Value from e.g. distance driven, remove from here and update score externally
        return Score;
    }
}