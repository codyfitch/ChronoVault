using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BestHTTP;
using BestHTTP.SocketIO;
using System;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class ServerConnectOutside : MonoBehaviour
{
    // Singleton 
    public static ServerConnectOutside s;

    public string socketURI;

    public SocketOptions options;
    public SocketManager socketManager;

    bool roomsRecieved = false; 

    void Start()
    {
        s = this;

        CreateSocketRef();

        // Socket messages that we are listening for 
        socketManager.Socket.On("connect", OnConnect);
        socketManager.Socket.On("hello", OnHello);
        socketManager.Socket.On("roomInfo", OnRoomInfo);
        socketManager.Socket.On("charInfo", OnCharInfo);
        socketManager.Socket.On("VRDisconnect", OnVRDisconnect); 

        socketManager.Socket.Emit("hi");
        Invoke("RequestRooms", 5f); 
    }

    //////////////////////////// Outgoing Socket Messages ////////////////////////////////////////

    public void sendMonsterAdded(String json)
    {
        socketManager.Socket.Emit("addMonster", json); 
    }

    ///////////////////// Handlers for recieved socket messages //////////////////////////////////

    void OnConnect(Socket socket, Packet packet, params object[] args)
    {
        Debug.Log("Connected to Socket.IO server");
    }
    void OnHello(Socket socket, Packet packet, params object[] args)
    {
        Debug.Log("Got nice hello message from server");
    }
    void OnRoomInfo(Socket socket, Packet packet, params object[] args)
    {
        roomsRecieved = true; 
        RoomReconstructor.s.reconstruct(args[0].ToString()); 
    }
    void OnCharInfo(Socket socket, Packet packet, params object[] args)
    {
        RoomReconstructor.s.updatePlayerPosition(args[0].ToString()); 
    }
    
    void OnVRDisconnect(Socket socket, Packet packet, params object[] args)
    {
        Debug.Log("VR Disconnect"); 
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name); 
    }

    /////////////////////////////////////// Timer Script ////////////////////////////////////////////
    
    private void RequestRooms()
    {
        if (!roomsRecieved)
        {
            socketManager.Socket.Emit("requestRooms");
            Invoke("RequestRooms", 5f);
        }
    }


    /////////////////////////////// Socket.io connection utilities /////////////////////////////////
    void OnApplicationQuit()
    {  
        LeaveRoomFromServer();
        DisconnectMySocket();
    }

    public void CreateSocketRef()
    {
        TimeSpan miliSecForReconnect = TimeSpan.FromMilliseconds(1000);

        options = new SocketOptions();
        options.ReconnectionAttempts = 3;
        options.AutoConnect = true;
        options.ReconnectionDelay = miliSecForReconnect;

        socketManager = new SocketManager(new Uri(socketURI), options);
    }

    public void DisconnectMySocket()
    {
        socketManager.GetSocket().Disconnect();
    }

    public void LeaveRoomFromServer()
    {
        socketManager.GetSocket().Emit("leave", OnSendEmitDataToServerCallBack);
    }

    private void OnSendEmitDataToServerCallBack(Socket socket, Packet packet, params object[] args)
    {
        Debug.Log("Send Packet Data : " + packet.ToString());
    }

    public void SetNamespaceForSocket()
    {
        //namespaceForCurrentPlayer = socketNamespace;
        //mySocket = socketManagerRef.GetSocket(“/ Room - 1);
    }

}
