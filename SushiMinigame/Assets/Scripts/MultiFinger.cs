using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;
using System;
using UnityEngine.UIElements;

public class MultiFinger : MonoBehaviour
{
    private SerialPort serialPort;
    public string portName = "COM3"; // switch port
    public int baudRate = 9600;

    // Bones
    public Transform palm;
    public Transform ThumbBase;
    public Transform ThumbTop;
    public Transform IndexBase;
    public Transform IndexTop;
    public Transform MiddleBase;
    public Transform MiddleTop;
    public Transform RingBase;
    public Transform RingTop;
    public Transform PinkyBase;
    public Transform PinkyTop;

    private Transform[] transforms; //Store transforms 
    private float[,] angles = new float[11, 3]; // store angle vales

    public float rotationMultiplier = 1f; // Adjust for sensitivity

    // Simulated Data
    public bool useSimData = true;
    private string SimData;

    private string UpdateSimData() // Reminder: Update for 11 channels
    {
        // Store angle values for 11 sensors/ channels, with X, Y, Z
        float[,] angles = new float[11, 3];

        // Base time multipliers for oscillation 
        float baseMultiplierX = 10f;
        float baseMultiplierY = 5f;
        float baseMultiplierZ = 3f;

        // Unique offsets for each channel 
        float[] offsets = { 0.1f, 1.2f, 2.3f, 3.5f, 4.8f, 5.2f, 6.7f, 7.3f, 8.0f, 9.8f, 10.7f };

        // Generate angles dynamically
        for (int i = 0; i < 11; i++)
        {
            angles[i, 0] = Mathf.PingPong(Time.time * (baseMultiplierX + offsets[i]), 90); // X
            angles[i, 1] = Mathf.PingPong(Time.time * (baseMultiplierY + offsets[i]), 45); // Y
            angles[i, 2] = Mathf.PingPong(Time.time * (baseMultiplierZ + offsets[i]), 30); // Z
        }

        // Match format of Arduino 
        string SimData = "";
        for (int i = 0; i < 11; i++)
        {
            SimData += $"Channel {i}\n" +
                       $"X:{angles[i, 0]} Y:{angles[i, 1]} Z:{angles[i, 2]}\n";
        }

        //Debug.Log(SimData);
        return SimData;
    }

    void Start() // Sec stay same
    {
        // Put the transforms array with the matching bones and order
        transforms = new Transform[]
        {
            palm, ThumbBase, ThumbTop, IndexBase, IndexTop,
            MiddleBase, MiddleTop, RingBase, RingTop,
            PinkyBase, PinkyTop
        };

        if (!useSimData) // if useSimData is true then it wont use this code
        {
            serialPort = new SerialPort(portName, baudRate);
            try
            {
                serialPort.Open();
                serialPort.ReadTimeout = 100;
            }
            catch (System.Exception e)
            {
                Debug.LogError("Error opening serial port: " + e.Message);
            }
        }

        else //will use when theres no real data 
        {
            Debug.Log("Using simulated data");
        }
    }

    void Update()
    {
        if (useSimData)
        {
            SimData = UpdateSimData();
            ProcessData(SimData);
        }
        else if (serialPort != null && serialPort.IsOpen) // Read and process data from serial port
        {
            try
            {
                string serialData = serialPort.ReadLine();
                ProcessData(serialData);
            }
            catch (Exception e)
            {
                Debug.LogWarning("Serial read error: " + e.Message);
            }
        }
    }

    void ProcessData(string data)
    {
        try
        {
            string[] lines = data.Split('\n'); // process each line seperately 
            int currentChannel = -1; // keeps track which sensor/ channel its at

            foreach (string line in lines) // goes line by line
            {
                // Check if the line specifies a channel
                if (line.StartsWith("Channel"))
                {
                    currentChannel = int.Parse(line.Substring(8).Trim()); // takes sensor/ channel number
                }
                else if (line.StartsWith("X:") && currentChannel != -1) //takes data and keeps it in its channel
                {
                    // Takes the X, Y, Z values for the current channel
                    string[] parts = line.Split(' ');

                    float x = float.Parse(parts[0].Substring(2));
                    float y = float.Parse(parts[1].Substring(2));
                    float z = float.Parse(parts[2].Substring(2));

                    // Assign the values to the right angles
                    Movement(currentChannel, x, y, z);
                }
            }
        }
        catch (Exception e)
        {
            Debug.LogWarning("Error processing data: " + e.Message);
        }
    }

    void Movement(int channel, float x, float y, float z)
    {
        // Store the angles for the specified channel
        angles[channel, 0] = x;
        angles[channel, 1] = y;
        angles[channel, 2] = z;

        // Get the transform object for the given channel
        Transform targetTransform = transforms[channel];

        // Apply the rotation to the transform
        targetTransform.localRotation = Quaternion.Euler(x * rotationMultiplier, y * rotationMultiplier, z * rotationMultiplier);

        //Debug.Log($"Rotating Channel {targetTransform}: X={x}, Y={y}, Z={z}");
    }

    void OnApplicationQuit() // Sec stay same
    {
        if (serialPort.IsOpen)
        {
            serialPort.Close();
        }
    }
}