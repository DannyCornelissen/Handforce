using System;
using System.IO.Ports;
using System.Runtime.Remoting.Messaging;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class SingleFinger : MonoBehaviour
{
    private SerialPort serialPort;
    public string portName = ""; // Switch to needed port
    public int baudRate = 115200;

    //Simulated Data
    public bool useSimData = true;
    private string SimGyroData;

    //private string SimFSRData;

    // Bones
    public Transform palm;
    public Transform baseFinger; //base finger
    public Transform topFinger; //top finger

    public float rotationMultiplier = 1f; // Adjust for sensitivity

    // Gyroscope Data
    private float angle1X, angle1Y, angle1Z, angle1W; //palm angles
    private float angle2X, angle2Y, angle2Z, angle2W;//base finger
    private float angle3X, angle3Y, angle3Z, angle3W; //top finger

    // FSR Data
    //private float fsrForce1, fsrForce2;



    void Start() // Sec stay same
    {

        if (!useSimData) // if useSimData is true then it wont use this code
        {
            serialPort = new SerialPort(portName, baudRate);
            try
            {
                serialPort.Open();
                serialPort.ReadTimeout = 5;
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
            SimGyroData = UpdateSimGyroData();
            //SimFSRData = UpdateSimFSRData();
            ProcessData(SimGyroData);
            //ProcessFSRData(SimFSRData);
        }
        else if (serialPort != null && serialPort.IsOpen) // Read and process data from serial port
        {
            try
            {
                string serialData = serialPort.ReadLine();
                Debug.Log($"Arduino Input:{serialData}");
                //if (serialData.StartsWith("FSR"))
                //{
                //ProcessFSRData(serialData);
                //}
                //else
                //{
                ProcessData(serialData);
                //}
            }
            catch (Exception e)
            {
                Debug.LogWarning("Serial read error: " + e.Message);
            }
        }
    }

    private string UpdateSimGyroData()
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
        string SimGyroData =
            $"Channel 0\n" +
            $"X:{angle1X} Y:{angle1Y} Z:{angle1Z}\n" +
            $"Channel 1\n" +
            $"X:{angle2X} Y:{angle2Y} Z:{angle2Z}\n" +
            $"Channel 2\n" +
            $"X:{angle3X} Y:{angle3Y} Z:{angle3Z}";
        //Debug.Log($"Simulated Gyro Data{SimGyroData}");
        return SimGyroData;
    }
    //private string UpdateSimFSRData()
    //{
    //fsrForce1 = Mathf.PingPong(Time.time, 10);
    //fsrForce2 = Mathf.PingPong(Time.time * 0.5f, 5);

    //string SimFSRData =
    //$"FSR1: Force = {fsrForce1} N\nFSR2: Force = {fsrForce2} N";
    //Debug.Log($"Simulated FSR Data{SimFSRData}");
    //return SimFSRData;
    //}
    //void ProcessFSRData(string data)
    //{
    //string[] lines = data.Split('\n');

    //if (lines.Length >= 2)
    //{
    // Extracting the force values from the lines
    //fsrForce1 = float.Parse(lines[0].Substring(lines[0].IndexOf("Force =") + 8).Trim());
    //fsrForce2 = float.Parse(lines[1].Substring(lines[1].IndexOf("Force =") + 8).Trim());
    //}
    //else
    //{
    //Debug.LogWarning("Invalid FSR data format.");
    //}
    //}


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
                    currentChannel = int.Parse(line.Substring(7).Trim()); // takes sensor/ channel number
                }
                else if (line.StartsWith("W:") && currentChannel != -1) //takes data and keeps it in its channel 
                {
                    // Takes the X, Y, Z values for the current channel
                    string[] parts = line.Split(',');

                    float w = float.Parse(parts[0].Substring(2));
                    float x = float.Parse(parts[1].Substring(2));
                    float y = float.Parse(parts[2].Substring(2));
                    float z = float.Parse(parts[3].Substring(2));

                    // Assign the values to the right angles
                    AssignAngles(currentChannel, x, y, z, w);
                    //Debug.Log($"{currentChannel} {x} {y} {z} {w}");
                }
            }
        }
        catch (Exception e)
        {
            Debug.LogWarning("Error processing data: " + e.Message);
        }
    }

    void AssignAngles(int channel, float x, float y, float z, float w)
    {
        switch (channel)
        {
            case 1:
                baseFinger.localRotation = new Quaternion(x, 0, y, w);
                break;
            case 2:
                palm.rotation = new Quaternion(x, -z, y, w);
                break;
            case 0:
                topFinger.localRotation = new Quaternion(x, 0, 0, w);
                break;
        }
    }


    void OnApplicationQuit() // Sec stay same
    {
        if (serialPort != null && serialPort.IsOpen)
        {
            serialPort.Close();
        }
    }
}