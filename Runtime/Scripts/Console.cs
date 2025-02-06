using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Console : MonoBehaviour
{
    private void OnEnable()
    {
        //AppEvents.OnCursorAdded += CreatePositions;
        //AppEvents.OnCursorUpdated += UpdatePositions;
        //AppEvents.OnCursorRemoved += RemovePosition;
    }

    private void OnDisable()
    {
        //AppEvents.OnCursorAdded -= CreatePositions;
        //AppEvents.OnCursorUpdated -= UpdatePositions;
        //AppEvents.OnCursorRemoved -= RemovePosition;
    }

    void CreatePositions(string msg)
    {
        print(msg);
    }

    void UpdatePositions(string msg)
    {
        print(msg);
    }

    void RemovePosition(int id)
    {
        print(id);
    }
}
