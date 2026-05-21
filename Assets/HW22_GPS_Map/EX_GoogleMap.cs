using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class EX_GoogleMap : MonoBehaviour
{
    public RawImage MapImage;
    public EX_GPS gps; // GPS 스크립트 연결용

    string BaseURL = "https://maps.googleapis.com/maps/api/staticmap?";
    string URL = "";
    public int zoom = 14;
    public int mapWidth = 400;
    public int mapHeight = 400;
    string APIKey = "AIzaSyDYoiRMv-wgYHmAL0W2N_jCW-1G1EiYWXA";

    private void Start()
    {
        StartCoroutine(WaitForGPSThenLoadMap());
    }

    IEnumerator WaitForGPSThenLoadMap()
    {
        // GPS 초기화 기다리기 (최대 25초)
        float waited = 0f;
        while (gps.latitude == 0 && gps.longitude == 0 && waited < 25f)
        {
            yield return new WaitForSeconds(1f);
            waited++;
        }

        LoadMap();
    }

    public void LoadMap()
    {
        StartCoroutine(ILoadMap());
    }

    IEnumerator ILoadMap()
    {
        double lat = (gps.latitude != 0) ? gps.latitude : 37.566827;
        double lon = (gps.longitude != 0) ? gps.longitude : 126.978113;

        URL = BaseURL + "center=" + lat + "," + lon +
            "&zoom=" + zoom.ToString() +
            "&size=" + mapWidth.ToString() + "x" + mapHeight.ToString() +
            "&key=" + APIKey +
            "&maptype=terrain" +
            "&markers=size:mid|color:red|label:H|" + lat + "," + lon;

        URL = UnityWebRequest.UnEscapeURL(URL);

        UnityWebRequest www = UnityWebRequestTexture.GetTexture(URL);
        yield return www.SendWebRequest();

        if (www.error == null)
        {
            MapImage.texture = DownloadHandlerTexture.GetContent(www);
        }
        else
        {
            print("Failed: " + www.error);
        }
    }
}