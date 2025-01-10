using System;
using System.IO.Ports;
using System.Runtime.InteropServices.ComTypes;
using System.Runtime.Remoting.Messaging;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class GyroHandler : MonoBehaviour
{
    private SerialPort serialPort;
    public string portName = "COM5"; // Switch to needed port
    public int baudRate = 115200;

    //Simulated Data
    public bool useSimData = true;
    private string SimGyroData;
    //private string SimFSRData;

    public string strRecieved;
    public string[] strData = new string[4];
    public string[] strDataRecieved = new string[4];
    public float qw, qx, qy, qz;



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
        }
        else if (serialPort != null && serialPort.IsOpen) // Read and process data from serial port
        {
            try
            {
                ReadGyroScope();
            }
            catch (Exception e)
            {
               
            }
        }
    }
    private void ReadGyroScope()
    {
        strRecieved = serialPort.ReadLine();
        //Debug.Log(strRecieved);
        strData = strRecieved.Split(",");
        if (strData[0] != "" && strData[1] != "" && strData[2] != "" && strData[3] != "") //Makes sure all quaternion data is ready (Values: W,X,Y,Z)
        {
            strDataRecieved[0] = strData[0];
            strDataRecieved[1] = strData[1];
            strDataRecieved[2] = strData[2];
            strDataRecieved[3] = strData[3];

            qw = float.Parse(strDataRecieved[0]);
            qx = float.Parse(strDataRecieved[1]);
            qy = float.Parse(strDataRecieved[2]);
            qz = float.Parse(strDataRecieved[3]);

            transform.rotation = new Quaternion(-qx, -qz, -qy, -qw);
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