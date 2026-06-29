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
    [SerializeField] private float gameDuration;
    
    private int _points = 0;
    private int _previousMole;
    private MoleBehaviour _cuurentActiveMoles;
    private bool _isGameRunning = false;
    

    private void Start()
    {
        pointText.text = _points.ToString();
    }

    public void GameStart()
    {
        if (_isGameRunning) return; 

        _isGameRunning = true;
        _points = 0;
        pointText.text = _points.ToString();

        StartCoroutine(PlayRound());
        StartCoroutine(GameTimer());
    }

    private IEnumerator PlayRound()
    {
        while (_isGameRunning)
        {
            while (_cuurentActiveMoles != null) yield return null;

            int index;
            do
            {
                index = Random.Range(0, moles.Count);
            } while (index == _previousMole && moles.Count > 1);

            _previousMole = index;
            _cuurentActiveMoles = moles[index];
            _cuurentActiveMoles.RiseUp();

            yield return new WaitForSeconds(riseTime);

            float startTime = Time.time;
            float moleWaitTime = Random.Range(minMoleWaitTime, maxMoleWaitTime);
            while (_cuurentActiveMoles != null && !_cuurentActiveMoles.IsHit && (Time.time - startTime) < moleWaitTime)
            {
                yield return null;
            }

            if (_cuurentActiveMoles != null)
            {
                _cuurentActiveMoles.Hide(moleHideDuration);
                _cuurentActiveMoles = null;
            }

            yield return new WaitForSeconds(0.5f);
        }
    }

    private IEnumerator GameTimer()
    {
        yield return new WaitForSeconds(gameDuration);

        _isGameRunning = false;

        if (_cuurentActiveMoles != null)
        {
            _cuurentActiveMoles.Hide(moleHideDuration);
            _cuurentActiveMoles = null;
        }

        GameEnds();
    }

    private void GameEnds()
    {

    }

    public void AddPoint()
    {
        if (!_isGameRunning) return;
        _points++;
        pointText.text = _points.ToString();
    }
}
