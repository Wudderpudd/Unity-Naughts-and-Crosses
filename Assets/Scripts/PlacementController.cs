using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementController : MonoBehaviour
{
    public GameObject cross;
    public GameObject crossPreview;
    public GameObject naught;
    public GameObject naughtPreview;
    private GameObject hoverPreview;
    private GameObject playerObject;
    private GameObject AIObject;
    private GameObject playerObjectPreview;
    public BoardController boardController;
    public int xCoordinate;
    public int yCoordinate;
    public int[] coordinates = new int[2];
    public int playerTeamID;

    void Awake()
    {
        boardController = GetComponentInParent<BoardController>();
        coordinates[0] = xCoordinate;
        coordinates[1] = yCoordinate;

        if (boardController.getPlayerTeamCross() == true)
        {
            playerObject = cross;
            AIObject = naught;
            playerObjectPreview = crossPreview;
            playerTeamID = 2;
        }
        else
        {
            playerObject = naught;
            AIObject = cross;
            playerObjectPreview = naughtPreview;
            playerTeamID = 1;
        }
    }

    private void OnMouseEnter()
    {
        if (boardController.isPlayerTurn() == true && boardController.spotIsOpen(xCoordinate, yCoordinate) == true)
        {
            hoverPreview = Instantiate(playerObjectPreview, gameObject.transform.position, gameObject.transform.rotation) as GameObject;
        }
    }

    private void OnMouseExit()
    {
        Destroy(hoverPreview);
    }
    private void OnMouseDown()
    {
        if (boardController.isPlayerTurn() == true && boardController.spotIsOpen(xCoordinate, yCoordinate) == true)
        {
            Destroy(hoverPreview);
            Instantiate(playerObject, gameObject.transform.position, gameObject.transform.rotation);
            boardController.makePlayerTurn(xCoordinate, yCoordinate);
        }
    }

    private void OnMouseUp()
    {
        
    }

    public void placeAIObject()
    {
        Instantiate(AIObject, gameObject.transform.position, gameObject.transform.rotation);
    }
}

