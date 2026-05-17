using UnityEngine;
using UnityEngine.UI;

public class ImageChanger : MonoBehaviour
{
    public Image displayImage;
    public Sprite imageA;
    public Sprite imageB;
    public Sprite imageC;

    public void ShowImageA()
    {
        displayImage.sprite = imageA;
    }

    public void ShowImageB()
    {
        displayImage.sprite = imageB;
    }

    public void ShowImageC()
    {
        displayImage.sprite = imageC;
    }
}