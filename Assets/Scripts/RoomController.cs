using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using TMPro;

public class RoomController : MonoBehaviourPunCallbacks
{
    public int gameSceneIndex;
    public GameObject lobbyPanel;
    public GameObject roomPanel;
    public GameObject startButton;
    public Transform playersContainer;
    public GameObject playerListingPrefab;
    public TextMeshProUGUI roomNameDisplay;

    private void ClearPlayerListings()
    {
        for(int i = playersContainer.childCount - 1; i >= 0; i--)
        {
            Destroy(playersContainer.GetChild(i).gameObject);
        }
    }

    private void ListPlayers()
    {
        foreach(Player player in PhotonNetwork.PlayerList)
        {
            GameObject tempListing = Instantiate(playerListingPrefab, playersContainer);
            TextMeshProUGUI tempText = tempListing.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
            tempText.text = player.NickName;
        }
    }

    public override void OnJoinedRoom()
    {
        roomPanel.SetActive(true);
        lobbyPanel.SetActive(false);

        roomNameDisplay.text = PhotonNetwork.CurrentRoom.Name;

        if(PhotonNetwork.IsMasterClient)
        {
            startButton.SetActive(true);
        }
        else
        {
            startButton.SetActive(false);
        }

        ClearPlayerListings();
        ListPlayers();
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        ClearPlayerListings();
        ListPlayers();
    }

    public override void OnPlayerLeftRoom(Player newPlayer)
    {
        ClearPlayerListings();
        ListPlayers();
        if(PhotonNetwork.IsMasterClient)
        {
            startButton.SetActive(true);
        }
    }

    public void StartGame()
    {
        if(PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.CurrentRoom.IsOpen = false;
            PhotonNetwork.LoadLevel(gameSceneIndex);
        }
    }

    private IEnumerator RejoinLobby()
    {
        yield return new WaitForSeconds(1f);
        PhotonNetwork.JoinLobby();
    }

    public void BackOnClick()
    {
        lobbyPanel.SetActive(true);
        roomPanel.SetActive(false);
        PhotonNetwork.LeaveRoom();
        PhotonNetwork.LeaveLobby();
        StartCoroutine(RejoinLobby()); 
    }
}
