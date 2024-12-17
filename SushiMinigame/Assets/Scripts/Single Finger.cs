using System;
using System.IO.Ports;
using System.Runtime.Remoting.Messaging;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class SingleFinger : MonoBehaviour
{
    private SerialPort serialPort;
    public string portName = "COM3"; // Switch to needed port
    public int baudRate = 9600;

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
    private float angle1X, angle1Y, angle1Z; //palm angles
    private float angle2X, angle2Y, angle2Z; //base finger
    private float angle3X, angle3Y, angle3Z; //top finger

    // FSR Data
    //private float fsrForce1, fsrForce2;

    private Vector3 initialPalmRotation;


    void Start() // Sec stay same
    {

        if (!useSimData) // if useSimData is true then it wont use this code
        {
            initialPalmRotation = palm.rotation.eulerAngles;
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
                angle1X = x; angle1Y = z; angle1Z = y;
                RotatePalm(); // where the values of this case are applied 
                break;
            case 1:
                angle2X = -x; angle2Z = -y; angle2Y = -z;
                RotateBaseFinger();
                break;
            case 2:
                angle3X = -x; angle3Z = -y; angle3Y = -z;
                RotateTopFinger();
                break;
        }
    }

void RotatePalm()
{
    // Adjust for the initial rotation of the palm
    float targetX = initialPalmRotation.x + angle1X;
    float targetY = initialPalmRotation.y + angle1Y;
    float targetZ = initialPalmRotation.z + angle1Z;

    Debug.Log($"Raw Palm Rotation: X={targetX}, Y={targetY}, Z={targetZ}");

    // Clamp the adjusted rotation to the specified limits
    float clampedAngleX = Mathf.Clamp(targetX, -80.844f, 78.285f);  // X-axis
    float clampedAngleY = Mathf.Clamp(targetY, -29.243f, 38.661f);  // Y-axis
    float clampedAngleZ = Mathf.Clamp(targetZ, -96.922f, 65.195f);  // Z-axis

    // Apply the clamped rotation
    palm.rotation = Quaternion.Euler(clampedAngleX, clampedAngleY, clampedAngleZ);

    Debug.Log($"Clamped Palm Rotation: X={clampedAngleX}, Y={clampedAngleY}, Z={clampedAngleZ}");
}



    void RotateBaseFinger()
    {
        // Clamp the rotation to the specified limits
        float clampedAngleX = Mathf.Clamp(angle2X, -81.776f, 35.132f); // Up/Down X-axis
        float clampedAngleY = Mathf.Clamp(angle2Y, -2.163f, 0.231f);   // Up/Down Y-axis
        float clampedAngleZ = Mathf.Clamp(angle2Z, -42.48f, 24.462f);  // Side-to-side Z-axis

        // Apply the clamped rotation
        baseFinger.rotation = Quaternion.Euler(clampedAngleX, clampedAngleY, clampedAngleZ);

        Debug.Log($"Clamped Base Finger Rotation: X={clampedAngleX}, Y={clampedAngleY}, Z={clampedAngleZ}");
    }
    void RotateTopFinger()
    {
        // Clamp the rotation to the specified limits
        float clampedAngleX = Mathf.Clamp(angle3X, -78.07f, -0.943f);  // Down to Up X-axis
        float clampedAngleY = Mathf.Clamp(angle3Y, 0.014f, 8.221f);    // Y-axis
        float clampedAngleZ = Mathf.Clamp(angle3Z, -8.416f, -1.735f);  // Z-axis

        // Apply the clamped rotation
        topFinger.rotation = Quaternion.Euler(clampedAngleX, clampedAngleY, clampedAngleZ);

        Debug.Log($"Clamped Top Finger Rotation: X={clampedAngleX}, Y={clampedAngleY}, Z={clampedAngleZ}");
    }

     void OnApplicationQuit() // Sec stay same
    {
        if (serialPort.IsOpen)
        {
            serialPort.Close();
        }
    }
}