using UnityEngine;
using System.Collections.Generic;
using System.Diagnostics;
using TMPro;

public class MoleGame : MonoBehaviour
{
    [SerializeField] private List<MoleBehaviour> moles = new List<MoleBehaviour>();
    [SerializeField] private TextMeshProUGUI pointText;

    private int _points = 0;
    private int _previousMole;
    

    private void Start()
    {
        GameStart();
        pointText.text = _points.ToString();
    }

    public void GameStart()
    {
        _points = 0;
        DrawMole();
    }

    public void DrawMole()
    {
        int index;
        index = Random.Range(0, moles.Count);

        do
        {
            index = Random.Range(0, moles.Count);
        } while (_previousMole == index);

        MoleBehaviour chosenMole = moles[index];
        chosenMole.RiseUp();

        _previousMole = index;
    }

    public void GetPoint()
    {
        _points++;
        pointText.text = _points.ToString();
    }
}
