using System;
using System.IO.Ports;
using System.Runtime.Remoting.Messaging;
using UnityEngine;

public class SingleFinger : MonoBehaviour
{
    private SerialPort serialPort;
    public string portName = "COM3"; // Switch to needed port
    public int baudRate = 9600;

    //Simulated Data
    public bool useSimData = true;
    private string SimData;

    // Bones
    public Transform palm;
    // Assuming these are names of bones in base and top of finger
    public Transform Index2L; //base finger
    public Transform Index3L; //top finger

    public float rotationMultiplier = 1f; // Adjust for sensitivity

    // E - adding y+z angles
    private float angle1X, angle1Y, angle1Z; //palm angles
    private float angle2X, angle2Y, angle2Z; //base finger
    private float angle3X, angle3Y, angle3Z; //top finger

    void Start() // Sec stay same
    {
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
                Debug.Log($"Arduino Input:{serialData}");
                ProcessData(serialData);
            }
            catch (Exception e)
            {
                Debug.LogWarning("Serial read error: " + e.Message);
            }
        }
    }

    private string UpdateSimData()
    {
        angle1X = Mathf.PingPong(Time.time * 10, 90);
        angle1Y = Mathf.PingPong(Time.time * 5, 45);
        angle1Z = Mathf.PingPong(Time.time * 3, 30);

        angle2X = Mathf.PingPong(Time.time * 7, 60);
        angle2Y = Mathf.PingPong(Time.time * 4, 20);
        angle2Z = Mathf.PingPong(Time.time * 6, 15);

        angle3X = Mathf.PingPong(Time.time * 5, 45);
        angle3Y = Mathf.PingPong(Time.time * 3, 25);
        angle3Z = Mathf.PingPong(Time.time * 2, 10);
        // Reformat to match Arduino output
        string SimData =
            $"Channel 0\n" +
            $"X:{angle1X} Y:{angle1Y} Z:{angle1Z}\n" +
            $"Channel 1\n" +
            $"X:{angle2X} Y:{angle2Y} Z:{angle2Z}\n" +
            $"Channel 2\n" +
            $"X:{angle3X} Y:{angle3Y} Z:{angle3Z}";
        return SimData;
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
                    AssignAngles(currentChannel, x, y, z);
                }
            }
        }
        catch (Exception e)
        {
            Debug.LogWarning("Error processing data: " + e.Message);
        }
    }

    void AssignAngles(int channel, float x, float y, float z)
    {
        switch (channel)
        {
            case 0:
                angle1X = x; angle1Y = y; angle1Z = z;
                RotatePalm(); // where the values of this case are applied 
                break;
            case 1:
                angle2X = x; angle2Y = y; angle2Z = z;
                RotateBaseFinger();
                break;
            case 2:
                angle3X = x; angle3Y = y; angle3Z = z;
                RotateTopFinger();
                break;
        }
    }

    void RotatePalm()
    {
        Debug.Log($"Rotating Palm: X={angle1X}, Y={angle1Y}, Z={angle1Z}");
        // Rotate the palm based on the gyroscope angle
        palm.localRotation = Quaternion.Euler(angle1X, angle1Y , angle1Z);   
    }
    void RotateBaseFinger()
    {
        Debug.Log($"Rotating Base Finger: X={angle2X}, Y={angle2Y}, Z={angle2Z}");
        // Rotate the palm based on the gyroscope angle
        Index2L.localRotation = Quaternion.Euler(angle2X, angle2Y, angle2Z);
    }
    void RotateTopFinger()
    {
        Debug.Log($"Rotating Top Finger: X={angle3X}, Y={angle3Y}, Z={angle3Z}");
        // Rotate the palm based on the gyroscope angle
        Index3L.localRotation = Quaternion.Euler(angle3X, angle3Y, angle3Z);
    }

    void OnApplicationQuit() // Sec stay same
    {
        if (serialPort.IsOpen)
        {
            serialPort.Close();
        }
    }
}