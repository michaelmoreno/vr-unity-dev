using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class HandPresence : MonoBehaviour
{
    private InputDevice targetDevice;
    public List<GameObject> controllerPrefabs;
    private GameObject spawnedController;

    // Start is called before the first frame update
    void Start()
    {
        List<InputDevice> devices = new List<InputDevice>();
        InputDeviceCharacteristics rightControllerCharacteristics = InputDeviceCharacteristics.Right | InputDeviceCharacteristics.Controller;
        InputDevices.GetDevicesWithCharacteristics(rightControllerCharacteristics, devices);
        
        foreach (var item in devices)
        {
            Debug.Log(item.name + item.characteristics);
        }

        targetDevice = devices[0];
        GameObject prefab = controllerPrefabs.Find(controller => controller.name == targetDevice.name);
        if (prefab)
        {
            spawnedController = Instantiate(prefab, transform); // Google Instantiate Method
        }
        else
        {
            Debug.LogError("Did not find corresponding controller");
            spawnedController = Instantiate(controllerPrefabs[0], transform);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (targetDevice.TryGetFeatureValue(CommonUsages.primaryButton, out bool primaryButtonValue) && primaryButtonValue)
        {
            Debug.Log("Pressing primary button");
        }
        if (targetDevice.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue) && triggerValue > 0.1f)
            Debug.Log("Right Trigger pressed " + triggerValue);
        if (targetDevice.TryGetFeatureValue(CommonUsages.primary2DAxis, out Vector2 primary2DAxisValue) && primary2DAxisValue != Vector2.zero)
            Debug.Log("Primary Touchpad " + primary2DAxisValue);
    }
}
