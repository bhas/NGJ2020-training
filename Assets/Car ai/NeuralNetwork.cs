using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class NeuralNetwork
{
    public int[] Structure; // For debugging only.
    public Layer[] Layers;

    public NeuralNetwork(string filePath)
    {
        string[] lines = File.ReadAllLines(filePath);

        // create layers
        var values = lines[0].Split(' ');
        Layers = new Layer[values.Length - 1];
        for (int i = 1; i < values.Length; i++)
        {
            var numberOfNodes = int.Parse(values[i]);
            Layers[i - 1] = new Layer(numberOfNodes, i);
        }

        // set weights
        for (int i = 1; i < lines.Length; i++)
        {
            values = lines[i].Split(' ');
            var layerId = int.Parse(values[0]);
            var nodeId = int.Parse(values[1]);
            float[] weights = new float[values.Length - 2];
            for (int j = 2; j < values.Length; j++)
            {
                weights[j - 2] = float.Parse(values[j]);
            }
            Layers[layerId].Nodes[nodeId].Weights = weights;
        }
    }

    public float[] Evaluate(float[] inputs)
    {
        float[] output = inputs;
        for (int i = 0; i < Layers.Length; i++)
        {
            output = Layers[i].Evaluate(output);
        }
        return output;
    }
}

public class Layer
{
    public int Index; // For debugging only.
    public Node[] Nodes;

    public Layer(int numberOfNodes, int index)
    {
        Index = index;
        Nodes = new Node[numberOfNodes];
        for (int i = 0; i < numberOfNodes; i++)
        {
            Nodes[i] = new Node(i);
        }
    }

    public float[] Evaluate(float[] inputs)
    {
        float[] output = new float[Nodes.Length];
        for (int i = 0; i < Nodes.Length; i++)
        {
            output[i] = Nodes[i].Evaluate(inputs);
        }
        return output;
    }
}

public class Node
{
    public int Id; // For debugging only.
    public float[] Weights;

    public Node(int id)
    {
        Id = id;
    }

    public float Evaluate(float[] inputs)
    {
        float output = 0;
        for (int i = 0; i < inputs.Length; i++)
        {
            output += inputs[i] * Weights[i];
        }
        return output;
    }
}