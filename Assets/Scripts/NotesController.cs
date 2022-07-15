using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotesController : MonoBehaviour
{
    [Header("Distances")]
    [SerializeField]
    private float startingXPos;

    [SerializeField]
    private float defaultXDistance;

    [SerializeField]
    private float barDistance = 2;

    [SerializeField]
    private float grandStaffDistanceY;

    [Header("Components")]
    [SerializeField]
    private GameObject barLine;

    [SerializeField]
    private GameObject grandStaff;

    [SerializeField]
    private Transform firstParent;

    [SerializeField]
    private List<Note> allNotes = new List<Note>();

    [SerializeField]
    private NotePrefab[] notePrefabs;

    private float grandStaffDefaultDistY;

    private float grandStaffDefaultDistX;

    private float defaultXPos;

    private bool newStaffBool = false;

    private bool newBar = false;

    public static float distanceY;

    [Serializable]
    private class Note
    {
        public int note;

        public int noteLength;
    }

    private void Start()
    {
        defaultXPos = startingXPos;
        grandStaffDefaultDistY = firstParent.transform.localPosition.y;
        grandStaffDefaultDistX = firstParent.transform.localPosition.x;
        distanceY = grandStaffDistanceY;
        SpawnNotes();

        //add audio/MIDI stuff
        //add ledger lines
        //add count down (with sounds) using states
        //add break symbols
        //look at sasr again for scoring then implement something similar
        //if time, add a more faded colour on the non notes part of staff
        //write to file
        //upload to server once you know how this is done
    }

    private void SpawnNotes()
    {
        int notesInBar = 0;
        int notesInStaff = 0;
        float staffYDist = 0;
        int totalStaffs = 1;
        Transform staffParent = firstParent;
        foreach (Note note in allNotes)
        {
            Note previousNote = null;
            float distMultiplier = 1;

            notesInBar += note.noteLength;
            notesInStaff += note.noteLength;

            if (allNotes.IndexOf(note) != 0)
            {
                previousNote = allNotes[allNotes.IndexOf(note) - 1];
                distMultiplier = previousNote.noteLength;
            }

            NotePrefab notePrefab =
                Instantiate(notePrefabs[note.noteLength - 1],
                staffParent,
                false);
            float posY = notePrefab.ySpawns[note.note] - staffYDist;

            if (newBar)
            {
                newBar = false;
                distMultiplier = barDistance;
            }

            if (allNotes.IndexOf(note) != 0)
            {
                if (!newStaffBool)
                    startingXPos += defaultXDistance * distMultiplier;
                else
                    newStaffBool = false;
            }

            notePrefab.transform.position =
                new Vector3(startingXPos, posY, .5f);

            if (notesInBar == 4)
            {
                newBar = true;
                float barXPos =
                    notePrefab.transform.position.x + note.noteLength;
                GameObject newBarLine =
                    Instantiate(barLine, staffParent, false);
                float barYPos =
                    newBarLine.transform.localPosition.y - staffYDist;
                newBarLine.transform.position =
                    new Vector3(barXPos, barYPos, 1);
                notesInBar = 0;
                startingXPos = barXPos;
            }

            if (notesInStaff % 12 == 0)
            {
                float staffPosY = grandStaffDefaultDistY - grandStaffDistanceY;
                grandStaffDefaultDistY = staffPosY;

                GameObject newStaff = Instantiate(grandStaff, transform);
                staffParent = newStaff.transform;
                newStaff.transform.localPosition =
                    new Vector3(grandStaffDefaultDistX, staffPosY, 1);

                staffYDist = grandStaffDistanceY * totalStaffs;
                totalStaffs++;
                startingXPos = defaultXPos;
                newStaffBool = true;
            }
        }
    }
}
