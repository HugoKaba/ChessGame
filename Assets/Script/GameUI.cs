using System;
using TMPro;
using UnityEngine;

public enum CameraAngle
{
    menu = 0,
    whiteTeam=1,
    blackTeam=2,
}

public class GameUI : MonoBehaviour
{
    public static GameUI Instance { set; get; }

    public Server server;
    public Client client;

    [SerializeField] private Animator menuAnimator;
    [SerializeField] private TMP_InputField addressInput;
    [SerializeField] private GameObject[] cameraAngles;

    public Action<bool> SetlocalGame;


    private void Awake()
    {
        Instance = this;
        RegisterEvents();
    }
//cameras
public void ChangeCamera(CameraAngle index)
{
        for (int i = 0; i < cameraAngles.Length; i++)
            cameraAngles[i].SetActive(false);
        cameraAngles[(int)index].SetActive(true);
}

//button
    public void OnLocalGameButton()
    {
        menuAnimator.SetTrigger("InGameMenu");
        SetlocalGame?.Invoke(true);
        server.Init(8007);
        client.Init("127.0.0.1", 8007);
    }
    public void OnOnlineGameButton()
    {
        menuAnimator.SetTrigger("OnlineMenu");

    }

    public void OnOnlineHostButton()
    {
        server.Init(8007);
        SetlocalGame?.Invoke(false);
        client.Init("127.0.0.1", 8007);
        menuAnimator.SetTrigger("HostMenu");

    }
    public void OnOnlineConnectButton()
    {
        SetlocalGame?.Invoke(false);
        client.Init(addressInput.text, 8007);
    }
    public void OnOnlineBackButton()
    {
        menuAnimator.SetTrigger("StartMenu");

    }
    public void OnHostBackButton()
    {
        server.Shutdown();
        client.Shutdown();
        menuAnimator.SetTrigger("OnlineMenu");
    }

    #region 
    private void RegisterEvents()
    {
        NetUtility.C_START_GAME += OnStartGameClient;
    }
    private void UnregisterEvents()
    {
        NetUtility.C_START_GAME -= OnStartGameClient;
    }

    private void OnStartGameClient(NetMessage message)
    {
        menuAnimator.SetTrigger("InGameMenu");
    }
    #endregion
}
