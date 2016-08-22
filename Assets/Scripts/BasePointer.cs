using UnityEngine;
using System.Collections;

public struct PointerArgs
{
    public Transform target;
    public float distance;
    public Vector3 position;
}

public class BasePointer : MonoBehaviour {

    // tip location is 2 * pointer scale + half of tip scale

    // Change length of pointer based on raycast hit and the distance from it
    public GameObject pointer;
    public GameObject pointerTip;

    private RaycastHit hit;
    private Transform target;

    public float width = .01f;
    private float cacheWidth;

    public float length = 15f;
    private float cacheLength;

    public float pointerTipSize = .05f;
    private float cachepointerTipSize;
	// Use this for initialization
	void Start () {
        cacheWidth = width;
        cacheLength = length;
        cachepointerTipSize = pointerTipSize;

        UpdateSizeOfPointer(width, length, pointerTipSize);
    }
	
	// Update is called once per frame
	void Update () {

        CheckRaycast();
        UpdateSizeOfPointer(width, hit.distance / 2 - pointerTipSize, pointerTipSize);

        if (width == cacheWidth && cacheLength == length && pointerTipSize == cachepointerTipSize)
            return;
        cacheWidth = width;
        cacheLength = length;
        cachepointerTipSize = pointerTipSize;

        //UpdateSizeOfPointer(width, length, pointerTipSize);
	}

    public bool IsValid()
    {
        return Physics.Raycast(transform.position, transform.forward * length, out hit);
    }

    public virtual PointerArgs GetPointerArgs()
    {
        PointerArgs e = new PointerArgs();
        e.target = target;
        e.distance = hit.distance;
        e.position = hit.point;
        return e;
    }

    private void CheckRaycast()
    {
        if (Physics.Raycast(transform.position, transform.forward * length, out hit))
        {
            if (hit.distance < length)
            {
                target = hit.transform;
                
            }
        }
    }

    protected virtual void UpdateSizeOfPointer(float newWidth, float newLength, float newPointerTipSize)
    {
        
        pointer.transform.localScale = new Vector3(newWidth, newLength, newWidth);
        pointerTip.transform.localScale = new Vector3(newPointerTipSize, newPointerTipSize, newPointerTipSize);
        pointerTip.transform.localPosition = new Vector3(0,0,  pointer.transform.localScale.y * 2 + pointerTip.transform.localScale.z / 2 );
    }

    
}
