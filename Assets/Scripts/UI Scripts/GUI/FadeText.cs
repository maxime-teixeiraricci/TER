using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeText : MonoBehaviour {


    
    public GameObject objectSave;
    public Text saveText;
    private Color startColor;
    private Color endColor;
    float FadeoutTime;
    float time = 2f; //Seconds to read the text



    
    void Start()
    {
        //saveText = objectSave.GetComponent<Text>();
        startColor = saveText.color;
        endColor = startColor - new Color(0, 0, 0, 1.0f);
        FadeoutTime = 0.2f;
        //objectSave.SetActive(true);
        //Invoke("Hide", 2);

    }
    
    public void Fade()
    {
        objectSave.SetActive(true);
        Invoke("Fading", 4);

        //objectSave.SetActive(true);
        //saveText.CrossFadeAlpha(0.0f, 0.05f, false);

        //while (saveText.color.a > 0)
        //{
        //saveText.color -= Color.Lerp(startColor, endColor, FadeoutTime);
        //saveText.color = new Color(saveText.color.r, saveText.color.g, saveText.color.b, saveText.color.a - 0.000001f);
        //}
        //objectSave.SetActive(false);
        //saveText.color = new Color(saveText.color.r, saveText.color.g, saveText.color.b, saveText.color.a + 1.0f);

        //while (saveText.color.a > 0)
        //{
        //   saveText.color = new Color(saveText.color.r, saveText.color.g, saveText.color.b, saveText.color.a - (float)(0.1 * Time.deltaTime * 2));
        // }
    }
    
    void Fading()
    {
        Debug.Log("WE GOOOOOOOOOOOOOOOOOOOOOOOOOOOD");
        saveText.color = new Color(saveText.color.r, saveText.color.g, saveText.color.b, 0);
        //objectSave.SetActive(false);
    }
}
