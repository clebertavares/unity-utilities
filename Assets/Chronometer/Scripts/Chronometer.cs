using UnityEngine;
using System.Collections;
using System;
using System.Text;

public class Chronometer : MonoBehaviour 
{
	#region Delegates and Events
    public delegate void ChronometerEvent();
	public static event ChronometerEvent OnStartTime;
    public static event ChronometerEvent OnResumeTime;
	public static event ChronometerEvent OnStopTime;
	public static event ChronometerEvent OnPauseTime;
	#endregion

    #region Fields
    public ChronometerType type = ChronometerType.CountDown;
    public float maxSecond = 0;
    private float startTime = 0.0f;
    private float endTime = 0.0f;

	private bool paused = false;
    private bool stopped = true;
    #endregion

    void Update()
    {
        if (!stopped)
            endTime = Time.time;
    }

    #region Methods
    public void StartTime()
    {
        stopped = false;
        startTime = Time.time;
        if (OnStartTime != null)
            OnStartTime();

#if UNITY_EDITOR
        Debug.Log("Chronometer: Start Time");
#endif
    }

    public void ResumeTime()
    {
        paused = false;
        Time.timeScale = 1.0f;
        if (OnResumeTime != null)
            OnResumeTime();

#if UNITY_EDITOR
        Debug.Log("Chronometer: Resume Time");
#endif
    }

    public void PauseTime()
    {
        paused = true;
        Time.timeScale = 0.0f;
        if (OnPauseTime != null)
            OnPauseTime();

#if UNITY_EDITOR
        Debug.Log("Chronometer: Pause Time");
#endif
    }

    public void StopTime()
    {
        stopped = true;
        if (OnStopTime != null)
            OnStopTime();

#if UNITY_EDITOR
        Debug.Log("Chronometer: Stop Time");
#endif
    }
    #endregion

    public int TotalSeconds
    {
        get
        {
            if (type == ChronometerType.StopWatch)
                return Mathf.CeilToInt(endTime - startTime);
            else
                return Mathf.CeilToInt(Mathf.Clamp(maxSecond - (endTime - startTime), 0.0f, maxSecond));   
        }
    }

    public bool IsPaused
    {
        get
        {
            return this.paused;
        }
    }

    public bool IsStopped
    {
        get
        {
            return this.stopped;
        }
    }
}

public enum ChronometerType
{
    CountDown,
    StopWatch
}

