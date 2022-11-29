//using Photon.Pun;
//using System.Collections.Generic;
//using System.Linq;
//using UnityEngine;

//public class GameManager : MonoBehaviourPunCallbacks
//{
//    [Header("Status")]
//    public bool gameEnded = false;

//    [Header("Players")]
//    public string playerPrefabLocation;

//    public Transform[] spawnPoints;

//    public PlayerController[] players;

//    private int playersInGame;

//    private List<int> pickedSpawnIndex;

//    [Header("Reference")]
//    public GameObject imageTarget;
    
//    //instance
//    public static GameManager Instance;

//    private void Awake()
//    {
//        Instance = this;
//    }

//    public PhotonView view;

//    private void Start()
//    {
//        pickedSpawnIndex = new List<int>();
//        players = new PlayerController[PhotonNetwork.PlayerList.Length];
//        photonView.RPC("ImInGame", RpcTarget.AllBuffered);
//       // DefaultObserverEventHandler.isTracking = false;
//    }

//    private void Update()
//    {
//        //Debug.Log("is tracking" + DefaultObserverEventHandler.isTracking);

//        foreach (var gameObj in FindObjectsOfType(typeof(GameObject)).Cast<GameObject>())
//        {
//            if (gameObj.name == "Player(Clone)")
//            {
//                gameObj.transform.SetParent(imageTarget.transform);
//            }
//        }

//        for (int i = 1; i < imageTarget.transform.childCount; i++)
//        {
          
//            //imageTarget.transform.GetChild(i).gameObject.SetActive(DefaultObserverEventHandler.isTracking);
//        }
//    }

//    [PunRPC]
//    void ImInGame()
//    {
//        playersInGame++;
//        if (playersInGame == PhotonNetwork.PlayerList.Length)
//        {
//            SpawnPlayer();
//        }
//    }

//    void SpawnPlayer(){
       
//        var rand = Random.Range(0, spawnPoints.Length);

//        while (pickedSpawnIndex.Contains(rand))
//        {
//            rand = Random.Range(0, spawnPoints.Length);
//        }

//        pickedSpawnIndex.Add(rand);
        
//        GameObject playerObject = PhotonNetwork.Instantiate(playerPrefabLocation, spawnPoints[rand].position, Quaternion.identity);
                                                                                                                                                              
//        PlayerController playerScript = playerObject.GetComponent<PlayerController>();

//        playerScript.photonView.RPC("Initialize", RpcTarget.All, PhotonNetwork.LocalPlayer);
//    }

//    public PlayerController GetPlayer(int playerID)
//    {
//        return players.First(x => x.id == playerID);
//    }

//    public PlayerController GetPlayer(GameObject playerObj)
//    {
//        return players.First(x => x.gameObject == playerObj);
//    }
//}