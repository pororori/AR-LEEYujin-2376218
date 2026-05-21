using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EX_GPS : MonoBehaviour
{
    public double latitude, longitude, altitude, 
                  horizontalAccuracy, verticalAccuracy, timestamp;

    int waitTime = 20;
    bool keepAlive = true;

    [Range(10, 150)]
    public int fontSize = 30;
    public Color color = new Color(.0f, .0f, .0f, 1.0f);
    public float width, height;
    string debugMessage = "";
    int counter;

    void Start()
    {
        Get_GPS();
    }

    public void Get_GPS()
    {
        StartCoroutine(IGet_GPS());
    }

    public void Stop_GPS()
    {
        Input.location.Stop();
    }

    IEnumerator IGet_GPS()
    {
        debugMessage = "";

        if (!Input.location.isEnabledByUser)
        {
            debugMessage = "Location not enabled on device or app does not have permission";
            print(debugMessage);
            yield break;
        }

        Input.location.Start(10f, 10f);

        int maxWait = waitTime;
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return new WaitForSeconds(1);
            maxWait--;
        }

        if (maxWait < 1)
        {
            debugMessage = "Timed out";
            print(debugMessage);
            yield break;
        }

        if (Input.location.status == LocationServiceStatus.Failed)
        {
            debugMessage = "Unable to determine device location";
            print(debugMessage);
            yield break;
        }
        else
        {
            latitude = Input.location.lastData.latitude;
            longitude = Input.location.lastData.longitude;
            altitude = Input.location.lastData.altitude;
            horizontalAccuracy = Input.location.lastData.horizontalAccuracy;
            verticalAccuracy = Input.location.lastData.verticalAccuracy;
            timestamp = Input.location.lastData.timestamp;
            counter++;

            debugMessage =
                "Latitude=" + latitude +
                "\nLongitude=" + longitude +
                "\nAltitude=" + altitude +
                "\nHorizontal Accuracy=" + horizontalAccuracy +
                "\nVertical Accuracy=" + verticalAccuracy +
                "\nTimeStamp=" + timestamp +
                "\nCounter=" + counter;
            print(debugMessage);
        }

        while (keepAlive)
        {
            latitude = Input.location.lastData.latitude;
            longitude = Input.location.lastData.longitude;
            altitude = Input.location.lastData.altitude;
            horizontalAccuracy = Input.location.lastData.horizontalAccuracy;
            verticalAccuracy = Input.location.lastData.verticalAccuracy;
            timestamp = Input.location.lastData.timestamp;
            counter++;

            debugMessage =
                "Latitude=" + latitude +
                "\nLongitude=" + longitude +
                "\nAltitude=" + altitude +
                "\nHorizontal Accuracy=" + horizontalAccuracy +
                "\nVertical Accuracy=" + verticalAccuracy +
                "\nTimeStamp=" + timestamp +
                "\nCounter=" + counter;
            print(debugMessage);

            yield return new WaitForSeconds(10.0f);
        }
    }

    void OnGUI()
    {
        GUIStyle style = new GUIStyle();
        style.fontSize = fontSize;
        style.normal.textColor = color;
        style.alignment = TextAnchor.UpperLeft;
        GUI.Label(new Rect(0, 0, width, height), debugMessage, style);
    }
}