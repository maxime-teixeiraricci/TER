using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageLineScript : MonoBehaviour
{
    public GameObject _sender;
    public GameObject _receiver;
    public Color _color;
    public int _nbTick;

    private int _countTick = 0;
    private Gradient g;

    // Use this for initialization
    void Start ()
    {
        g = new Gradient();
        
        
        _color = GameObject.Find("GameManager").GetComponent<TeamManager>()._teams[_sender.GetComponent<Stats>()._teamIndex]._color;
    }
	
	// Update is called once per frame
	void Update ()
    {
        _countTick++;
        if (!(_sender && _receiver ))
        {
            Destroy(gameObject);
        }
        Vector3[] pos = new Vector3[] { _sender.transform.position, _receiver.transform.position };
        GetComponent<LineRenderer>().SetPositions(pos);
        float t = (1.0f*_countTick) / _nbTick;
        
        GradientColorKey[] gck = new GradientColorKey[1] { new GradientColorKey(_color, 1) };
        GradientAlphaKey[] gak = new GradientAlphaKey[1] { new GradientAlphaKey(FunctionTime(t), 1) };
        g.SetKeys(gck, gak);
        GetComponent<LineRenderer>().colorGradient = g;
        if (_countTick >= _nbTick)
        {
            Destroy(gameObject);
        }
    }

    float FunctionTime(float x)
    {
        return 1 -((x * Mathf.Exp(x)) / Mathf.Exp(1));
    }
}
