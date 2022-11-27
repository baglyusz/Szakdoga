using Managers;
using Photon.Pun;
using Photon.Realtime;

public class TestConnect : MonoBehaviourPunCallbacks
{
    private void Start()
    {
        print("Connecting to server..");
        PhotonNetwork.NickName = MasterManager.GameSettings.NickName; //local sent to the server
        PhotonNetwork.GameVersion = MasterManager.GameSettings.GameVersion; //Locks users to this version
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        print("Connected to server.");
        print(PhotonNetwork.LocalPlayer.NickName); // version on the server
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        print("Disconnected from server for reason " + cause.ToString());
    }
}
