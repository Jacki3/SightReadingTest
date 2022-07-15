using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private NotesController notesController;

    [SerializeField]
    private RhythmLine rhythmLine;

    [SerializeField]
    private int countDownTime;

    void Start()
    {
        StartCoroutine(DelayStart());
    }

    IEnumerator DelayStart()
    {
        int timeWaited = 0;
        while (true)
        {
            timeWaited++;
            print (timeWaited);
            if (timeWaited >= countDownTime)
            {
                yield return new WaitForSeconds(1);
                print("GO!");
                StartTest();
                break;
            }
            yield return new WaitForSeconds(1);
        }
    }

    private void StartTest()
    {
        rhythmLine.StartMoving();
    }
}
