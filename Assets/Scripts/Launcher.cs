﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
public class Launcher : MonoBehaviourPunCallbacks
{
    private LoginRegist loginRegist;
    string name;
    string pwd;

    public void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        //Connect();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("CONNECTED TO MASTER!");
        base.OnConnectedToMaster();

        Debug.Log("TRY TO JOIN ROOM");
        Join();
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("joined room success,load scene start game");
        StartGame();

        base.OnJoinedRoom();
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("join room failed, try to create room!");

        Create();

        base.OnJoinRandomFailed(returnCode, message);
    }

    public void Connect()
    {
        loginRegist = new LoginRegist();
        loginRegist.GetConn();
        name = GameObject.Find("Menu/name/name").GetComponent<Text>().text;
        pwd = GameObject.Find("Menu/pwd/pwd").GetComponent<Text>().text;

        if (loginRegist.CheckUser(pwd, name))
        {
            Debug.Log("Trying to connect...");
            PhotonNetwork.GameVersion = "0.0.0";
            PhotonNetwork.ConnectUsingSettings();
        }
        else
        {
            Debug.Log("user dont exit");
            Debug.Log("regist your account");
        }
        loginRegist.Close();
    }

    public void Regist()
    {
        loginRegist = new LoginRegist();
        loginRegist.GetConn();
        name = GameObject.Find("Menu/name/name").GetComponent<Text>().text;
        pwd = GameObject.Find("Menu/pwd/pwd").GetComponent<Text>().text;
        if (loginRegist.Regist(int.Parse(pwd), name) == 1)
        {
            Debug.Log("regist success");
        }
        else
        {
            Debug.Log("user already in game database");
        }
        loginRegist.Close();
    }
    public void Join()
    {
        PhotonNetwork.JoinRandomRoom();
    }

    public void Create()
    {
        PhotonNetwork.CreateRoom("created a new room");
    }

    public void StartGame()
    {
        if (PhotonNetwork.CurrentRoom.PlayerCount == 1)
        {
            //Data.SaveProfile(myProfile);
            PhotonNetwork.LoadLevel(1);
        }
    }

}