using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonHoverBigger : MonoBehaviour
{   //comment for test commit
    public float normalSize = 1f; // the normal size of the button
    public float enlargedSize = 1.2f; // the size of the button when it's hovered over
    private Vector3 normalScale; // the scale vector for the normal size
    private Vector3 enlargedScale; // the scale vector for the enlarged size
    private Image image; // the image component of the button

    void Start()
    {
        // get the image component of the button
        image = GetComponent<Image>();
        // set the normal and enlarged scale vectors
        normalScale = transform.localScale;
        enlargedScale = normalScale * enlargedSize;
    }

    public void OnPointerEnter()
    {
        // change the scale of the button to the enlarged size
        transform.localScale = enlargedScale;
    }

    public void OnPointerExit()
    {
        // change the scale of the button back to the normal size
        transform.localScale = normalScale;
    }
    // test commit by Dat
}
