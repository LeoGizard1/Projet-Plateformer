using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.IO.Ports;
using UnityEngine.Serialization;

public class SerialHandler : MonoBehaviour
{
    
    enum MsgType : byte {
        AnglePotentiometerMsg,
        PowerPotentiometerMsg,
        ButtonPressedMsg,
    }

    public float AngleValue;
    public float PowerValue;

    private PlayerController playerController;
    private SerialPort serial;
    
    private int baudrate = 115200;
    [SerializeField] private String serialPort = "COM7";
    
    // Start is called before the first frame update
    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
        serial = new SerialPort(serialPort, baudrate);
        serial.NewLine = "\n";
        serial.Open();
        if (!serial.IsOpen)
        {
            Debug.LogError("Failed to connect to serial");
        }

        if (!playerController)
        {
            Debug.LogError("Player controller not found");
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        while (serial.BytesToRead > 0)
        {



            byte msgID = (byte)serial.ReadByte();
            Debug.Log((MsgType)msgID);

            Byte[] bytes = new byte[2];
            UInt16 value;
            int readBytes = 0;

            switch ((MsgType)msgID)
            {
                case MsgType.AnglePotentiometerMsg:
                    while (readBytes < 2)
                    {
                        readBytes += serial.Read(bytes, 0, 2 - readBytes);
                    }
                    value = BitConverter.ToUInt16(bytes, 0);
                    AngleValue = value / 1023f;
                    playerController.updateDirection(AngleValue);
                    break;
                case MsgType.PowerPotentiometerMsg:
                    while (readBytes < 2)
                    {
                        readBytes += serial.Read(bytes, 0, 2 - readBytes);
                    }
                    value = BitConverter.ToUInt16(bytes, 0);
                    PowerValue = value / 1023f;
                    playerController.updatePower(PowerValue);
                    break;
                case MsgType.ButtonPressedMsg:
                    Debug.Log("Jump");
                    playerController.jumpPressed = true;
                    break;
            }
        }
    }
}
