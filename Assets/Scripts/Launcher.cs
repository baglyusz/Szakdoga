using Photon.Pun;
using UnityEngine;
using Photon.Realtime;

public class Launcher : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private byte maxPlayersPerRoom = 4;

    [SerializeField]
    private GameObject progressLabel;

    [SerializeField]
    private GameObject controlPanel;

    private const string GameVersion = "1";

    private bool _isConnecting;

    private void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    private void Start()
    {
        progressLabel.SetActive(false);
        controlPanel.SetActive(true);
    }

    public void Connect()
    {
        Debug.Log("Connecting..");
        progressLabel.SetActive(true);
        controlPanel.SetActive(false);

        if (PhotonNetwork.IsConnected)
        {
            Debug.Log("Joining random room..");
            PhotonNetwork.JoinRandomRoom();
        }
        else
        {
            _isConnecting = PhotonNetwork.ConnectUsingSettings();
            PhotonNetwork.GameVersion = GameVersion;
        }
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to server.");
        if (_isConnecting)
        {
            PhotonNetwork.JoinRandomRoom();
            _isConnecting = false;
        }
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        progressLabel.SetActive(false);
        controlPanel.SetActive(true);
        Debug.LogWarningFormat(" Disconnected because of: {0}", cause);
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("Failed to join room. Creating room...");
        PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = maxPlayersPerRoom });
    }

    public override void OnJoinedRoom()
    {   
        //if (PhotonNetwork.CurrentRoom.PlayerCount == 1)
        //{
            Debug.Log("Loading game scene");
            PhotonNetwork.LoadLevel("NetworkingTest");
        //}
    }
}
