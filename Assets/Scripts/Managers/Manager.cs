using UnityEngine;
using System.Collections;

public class Manager : MonoBehaviour {

    public GameInfo info = new GameInfo();
    public WaveManager wm;
    public InputManager im;
	public GUIManager GUI;
	public GameManager gm;

    public static Manager instance;

	// Use this for initialization
	void Awake () {
        instance = this;
	}
	
    void OnDestroy()
    {
        instance = null;

    }

    public static Manager Get()
    {
        return instance;
    }
}
