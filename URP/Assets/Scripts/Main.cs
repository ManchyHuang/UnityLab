using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

[ExecuteInEditMode]
public class Main : MonoBehaviour
{
    //private void Awake()
    //{
    //    Debug.Log("Awake");
    //}


    //[RuntimeInitializeOnLoadMethod]
    //static void RunOnStart()
    //{
    //    Debug.Log("RuntimeInitializeOnLoadMethod");
    //}

    //[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    //static void RunOnStartBeforeSceneLoad()
    //{
    //    Debug.Log("RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)");
    //}

    //[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    //static void RunOnStartAfterSceneLoad()
    //{
    //    Debug.Log("RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)");
    //}

    //[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
    //static void RunOnStartSubsystemRegistration()
    //{
    //    Debug.Log("RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)");
    //}

    //[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterAssembliesLoaded)]
    //static void RunOnStartAfterAssembliesLoaded()
    //{
    //    Debug.Log("RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterAssembliesLoaded)");
    //}

    //[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSplashScreen)]
    //static void RunOnStartBeforeSplashScreen()
    //{
    //    Debug.Log("RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSplashScreen)");
    //}

    public int value = 0;
    public List<int> ints = null;

    int value1 = 100;
    public List<int> ints1 = null;

    private void Awake()
    {
        Log("Awake");
    }

    private void OnEnable()
    {
        Log("OnEnable");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ++value;
            ++value1;
            Log("Update");
        }
    }
    private void OnDisable()
    {
        Log("OnDisable");
    }


    private void OnDestroy()
    {
        Log("OnDestroy");
    }

    [ContextMenu("ResetValue")]
    void ResetValue()
    {
        value = 99;
        value1 = -99;
        Log("ResetValue");
    }

    public List<int> list;
    List<int> list1;

    [ContextMenu("ResetArray")]
    void ResetArray()
    {
        list  = null;
        list1 = null;
        Log("ResetArray");
    }

    void Log(string message)
    {
        Debug.Log($"{message} -> value:{value} value1:{value1} ints:{ints != null} ints1:{ints1 != null}");
    }
}
