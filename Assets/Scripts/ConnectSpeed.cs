using System.Collections;
using System.Collections.Generic;
using System;
using TMPro;
using UnityEngine;

public class ConnectSpeed : MonoBehaviour
{
    
   public string DeviceName = "BK6 S-060733";
    //public string ServiceUUID = "0000180a-0000-1000-8000-00805f9b34fb";
    public string ServiceUUID ="00002A5B-0000-1000-8000-00805f9b34fb";
    public string LedUUID = "2A5B";
    public string ButtonUUID = "F1BBD79D-A900-84EC-9C9F-07937C4A7CB3";

    enum States
    {
        None,
        Scan,
        ScanRSSI,
        ReadRSSI,
        Connect,
        RequestMTU,
        Subscribe,
        Unsubscribe,
        Disconnect,
    }

    private bool _connected = false;
    private float _timeout = 0f;
    private States _state = States.None;
    private string _deviceAddress;
    private bool _foundButtonUUID = false;
    private bool _foundLedUUID = false;
    private bool _rssiOnly = false;
    private int _rssi = 0;

    public TextMeshPro StatusText;
    //public Text ButtonPositionText;

    private string StatusMessage
    {
        set
        {
            BluetoothLEHardwareInterface.Log(value);
            StatusText.text = value;
        }
    }

    void Reset()
    {
        _connected = false;
        _timeout = 0f;
        _state = States.None;
        _deviceAddress = null;
        _foundButtonUUID = false;
        _foundLedUUID = false;
        _rssi = 0;
    }

    void SetState(States newState, float timeout)
    {
        _state = newState;
        _timeout = timeout;
        StatusText.text = newState.ToString();
    }

    void StartProcess()
    {
        Reset();
        BluetoothLEHardwareInterface.Initialize(true, false, () =>
        {

            SetState(States.Scan, 0.1f);

        }, (error) =>
        {

            StatusMessage = "Error during initialize: " + error;
        });
    }

    // Use this for initialization
    void Start()
    {
        StartProcess();
    }

    private void ProcessButton(byte[] bytes)
    {
        if (bytes[0] == 0x00){
        }
           // ButtonPositionText.text = "Not Pushed";
        else{
            StatusText.text = bytes[0].ToString();
        }
           // ButtonPositionText.text = "Pushed";
    }

    // Update is called once per frame
    void Update()
    {
        if (_timeout > 0f)
        {
            _timeout -= Time.deltaTime;
            if (_timeout <= 0f)
            {
                _timeout = 0f;

                switch (_state)
                {
                    case States.None:
                        break;

                    case States.Scan:
                        StatusMessage = "Scanning for " + DeviceName;

                        BluetoothLEHardwareInterface.ScanForPeripheralsWithServices(null, (address, name) =>
                        {
                            // if your device does not advertise the rssi and manufacturer specific data
                            // then you must use this callback because the next callback only gets called
                            // if you have manufacturer specific data

                            if (!_rssiOnly)
                            {
                                if (name.Contains(DeviceName))
                                {
                                    StatusMessage = "Found " + name;

                                    // found a device with the name we want
                                    // this example does not deal with finding more than one
                                    _deviceAddress = address;
                                    SetState(States.Connect, 0.5f);
                                }
                            }

                        }, (address, name, rssi, bytes) =>
                        {

                            // use this one if the device responses with manufacturer specific data and the rssi

                            if (name.Contains(DeviceName))
                            {
                                StatusMessage = "Found " + name;

                                if (_rssiOnly)
                                {
                                    _rssi = rssi;
                                }
                                else
                                {
                                    // found a device with the name we want
                                    // this example does not deal with finding more than one
                                    _deviceAddress = address;
                                    SetState(States.Connect, 0.5f);
                                }
                            }

                        }, _rssiOnly); // this last setting allows RFduino to send RSSI without having manufacturer data

                        if (_rssiOnly)
                            SetState(States.ScanRSSI, 0.5f);
                        break;

                    case States.ScanRSSI:
                        break;

                    case States.ReadRSSI:
                        StatusMessage = $"Call Read RSSI";
                        BluetoothLEHardwareInterface.ReadRSSI(_deviceAddress, (address, rssi) =>
                        {
                            StatusMessage = $"Read RSSI: {rssi}";
                        });

                        SetState(States.ReadRSSI, 2f);
                        break;

                    case States.Connect:
                        StatusMessage = "Connecting...";

                        // set these flags
                        _foundButtonUUID = false;
                        _foundLedUUID = false;

                        // note that the first parameter is the address, not the name. I have not fixed this because
                        // of backwards compatiblity.
                        // also note that I am note using the first 2 callbacks. If you are not looking for specific characteristics you can use one of
                        // the first 2, but keep in mind that the device will enumerate everything and so you will want to have a timeout
                        // large enough that it will be finished enumerating before you try to subscribe or do any other operations.
                        BluetoothLEHardwareInterface.ConnectToPeripheral(_deviceAddress, null, null, (address, serviceUUID, characteristicUUID) =>
                        {
                            StatusMessage = "Connected...";

                            BluetoothLEHardwareInterface.StopScan();

                            if (IsEqual(serviceUUID, ServiceUUID))
                            {
                                StatusMessage = "Found Service UUID";

                                _foundButtonUUID = _foundButtonUUID || IsEqual(characteristicUUID, ButtonUUID);
                                _foundLedUUID = _foundLedUUID || IsEqual(characteristicUUID, LedUUID);

                                // if we have found both characteristics that we are waiting for
                                // set the state. make sure there is enough timeout that if the
                                // device is still enumerating other characteristics it finishes
                                // before we try to subscribe
                                if (_foundButtonUUID && _foundLedUUID)
                                {
                                    _connected = true;
                                    SetState(States.RequestMTU, 2f);
                                }
                            }
                        });
                        break;

                    case States.RequestMTU:
                        StatusMessage = "Requesting MTU";

                        BluetoothLEHardwareInterface.RequestMtu(_deviceAddress, 185, (address, newMTU) =>
                        {
                            StatusMessage = "MTU set to " + newMTU.ToString();

                            SetState(States.Subscribe, 0.1f);
                        });
                        break;

                    case States.Subscribe:
                        StatusMessage = "Subscribing to characteristics...";

                        BluetoothLEHardwareInterface.SubscribeCharacteristicWithDeviceAddress(_deviceAddress, ServiceUUID, ButtonUUID, (notifyAddress, notifyCharacteristic) =>
                        {
                            StatusMessage = "Waiting for user action (1)...";
                            _state = States.None;

                            // read the initial state of the button
                            BluetoothLEHardwareInterface.ReadCharacteristic(_deviceAddress, ServiceUUID, ButtonUUID, (characteristic, bytes) =>
                            {
                                ProcessButton(bytes);
                            });

                            SetState(States.ReadRSSI, 1f);

                        }, (address, characteristicUUID, bytes) =>
                        {
                            if (_state != States.None)
                            {
                                // some devices do not properly send the notification state change which calls
                                // the lambda just above this one so in those cases we don't have a great way to
                                // set the state other than waiting until we actually got some data back.
                                // The esp32 sends the notification above, but if yuor device doesn't you would have
                                // to send data like pressing the button on the esp32 as the sketch for this demo
                                // would then send data to trigger this.
                                StatusMessage = "Waiting for user action (2)...";

                                SetState(States.ReadRSSI, 1f);
                            }

                            // we received some data from the device
                            ProcessButton(bytes);
                        });
                        break;

                    case States.Unsubscribe:
                        BluetoothLEHardwareInterface.UnSubscribeCharacteristic(_deviceAddress, ServiceUUID, ButtonUUID, null);
                        SetState(States.Disconnect, 4f);
                        break;

                    case States.Disconnect:
                        StatusMessage = "Commanded disconnect.";

                        if (_connected)
                        {
                            BluetoothLEHardwareInterface.DisconnectPeripheral(_deviceAddress, (address) =>
                            {
                                StatusMessage = "Device disconnected";
                                BluetoothLEHardwareInterface.DeInitialize(() =>
                                {
                                    _connected = false;
                                    _state = States.None;
                                });
                            });
                        }
                        else
                        {
                            BluetoothLEHardwareInterface.DeInitialize(() =>
                            {
                                _state = States.None;
                            });
                        }
                        break;
                }
            }
        }
    }

    private bool ledON = false;
    public void OnLED()
    {
        ledON = !ledON;
        if (ledON)
        {
            SendByte((byte)0x01);
        }
        else
        {
            SendByte((byte)0x00);
        }
    }

    string FullUUID(string uuid)
    {
        string fullUUID = uuid;
        if (fullUUID.Length == 4)
            fullUUID = "0000" + uuid + "-0000-1000-8000-00805f9b34fb";

        return fullUUID;
    }

    bool IsEqual(string uuid1, string uuid2)
    {
        if (uuid1.Length == 4)
            uuid1 = FullUUID(uuid1);
        if (uuid2.Length == 4)
            uuid2 = FullUUID(uuid2);

        return (uuid1.ToUpper().Equals(uuid2.ToUpper()));
    }

    void SendByte(byte value)
    {
        byte[] data = { value };
        BluetoothLEHardwareInterface.WriteCharacteristic(_deviceAddress, ServiceUUID, LedUUID, data, data.Length, true, (characteristicUUID) =>
        {

            BluetoothLEHardwareInterface.Log("Write Succeeded");
        });
    }
}