using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectBox : MonoBehaviour
{
    //Selection Square Image Reference
    [SerializeField]
    private RectTransform selectSquareImage;

    //Starting position
    Vector3 startPos;
    //Ending position
    Vector3 endPos;


    // Start is called before the first frame update
    void Start()
    {
        //Start with making the selection square inactive
        selectSquareImage.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //If the player clicks and is in the clickable area
        if(Input.GetMouseButtonDown(0) && Input.mousePosition.y> Screen.height / 4)
        {
            //Shoot a raycast from the camera
            RaycastHit hit;
            if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity))
            {
                //Where the raycast hits is the start position
                startPos = hit.point;
            }
        }

        //If the mouse button is not clicked
        if(Input.GetMouseButtonUp(0))
        {
            //Turn off the selection square
            selectSquareImage.gameObject.SetActive(false);
        }

        //If the player is holding the mouse button and in the clickable area
        if(Input.GetMouseButton(0) && Input.mousePosition.y>Screen.height/4)
        {
            //If the selectrion square is not active
            if(!selectSquareImage.gameObject.activeInHierarchy)
            {
                //Activate the selection square
                selectSquareImage.gameObject.SetActive(true);
            }

            //Set the end position to the mouse position
            endPos = Input.mousePosition;

            //Sets squareStart to the start position
            Vector3 squareStart = Camera.main.WorldToScreenPoint(startPos);
            //Sets the z coordinate of squareStart to 0
            squareStart.z = 0f;
            //Finds the center of the square
            Vector3 center = (squareStart + endPos) / 2f;

            //Puts the square select image in the center
            selectSquareImage.position = center;

            //Creates the sides of the square
            float sizeX = Mathf.Abs(squareStart.x - endPos.x);
            float sizeY = Mathf.Abs(squareStart.y - endPos.y);

            //Sets the size of the square
            selectSquareImage.sizeDelta = new Vector2(sizeX, sizeY);
        }
    }
}
