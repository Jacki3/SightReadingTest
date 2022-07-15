using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RhythmLine : MonoBehaviour
{
    [SerializeField]
    private float distanceToMove = 1;

    private float defaultXPos;

    public int movesMade = 0;

    private int movesMadeOnStaff = 0;

    public int totalMovesMade = 0;

    [SerializeField]
    private CameraMovement cameraMovement;

    void Start()
    {
        defaultXPos = transform.position.x;
    }

    public void StartMoving()
    {
        StartCoroutine(MoveToBeat());
    }

    private IEnumerator MoveToBeat()
    {
        float dist;
        float yPos = transform.position.y;
        while (true)
        {
            movesMade++;
            movesMadeOnStaff++;
            totalMovesMade++;

            if (movesMade >= 4)
            {
                dist = distanceToMove * 2;
                movesMade = 0;
            }
            else
                dist = distanceToMove;

            if (movesMadeOnStaff >= 12)
            {
                movesMadeOnStaff = 0;
                dist = distanceToMove;
                yPos -= NotesController.distanceY;
                transform.position = new Vector3(defaultXPos, yPos, 1);
                movesMade++;
                totalMovesMade++;
                movesMadeOnStaff++;
                yield return new WaitForSeconds(AudioController.beatPerSec);
            }

            if (totalMovesMade % 24 == 0)
            {
                cameraMovement.MoveCameraDown();
            }

            transform.position += new Vector3(dist, 0, 0);
            yield return new WaitForSeconds(AudioController.beatPerSec);
        }
    }
}
