using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using UnityEngine;

public class MoleGame : MonoBehaviour
{
    [SerializeField] private List<MoleBehaviour> moles = new List<MoleBehaviour>();
    [SerializeField] private TextMeshProUGUI pointText;
    [SerializeField] private float riseTime = 0.3f;
    [SerializeField] private float moleHideDuration = 0.5f;
    [SerializeField] private float minMoleWaitTime = 0.5f;
    [SerializeField] private float maxMoleWaitTime = 1f;

    private int _points = 0;
    private int _previousMole;
    private MoleBehaviour _cuurentActiveMole;

    private void Start()
    {
        GameStart();
        pointText.text = _points.ToString();
    }

    public void GameStart()
    {
        _points = 0;
        pointText.text = _points.ToString();
        StartCoroutine(PlayRound());
    }

    private IEnumerator PlayRound()
    {
        while (true)
        {
            while (_cuurentActiveMole != null) yield return null;

            int index;
            do
            {
                index = Random.Range(0, moles.Count);
            } while (index == _previousMole && moles.Count > 1);

            _previousMole = index;
            _cuurentActiveMole = moles[index];
            _cuurentActiveMole.RiseUp();

            yield return new WaitForSeconds(riseTime);

            float startTime = Time.time;
            float moleWaitTime = Random.Range(minMoleWaitTime, maxMoleWaitTime);
            while(_cuurentActiveMole != null && !_cuurentActiveMole.IsHit && (Time.time - startTime) < moleWaitTime)
            {
                yield return null;
            }

            if(_cuurentActiveMole != null)
            {
                _cuurentActiveMole.Hide(moleHideDuration);
                _cuurentActiveMole = null;
            }

            yield return new WaitForSeconds(0.5f);
        }
    }


    public void AddPoint()
    {
        _points++;
        pointText.text = _points.ToString();
    }
}
