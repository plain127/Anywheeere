using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviourPunCallbacks
{
    public Vector3 point;

    // Reference to the three TextMeshPro UI elements to display player nicknames
    public TMP_Text[] playerNickNameTexts = new TMP_Text[3];

    void Start()
    {
        PhotonNetwork.Instantiate("Player1", point, Quaternion.identity);
        UpdatePlayerList(); // Update the player list when the game starts
    }

    void Update()
    {

    }

    // Called when a player joins the room
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        base.OnPlayerEnteredRoom(newPlayer);
        UpdatePlayerList(); // Update the player list when a new player joins
    }

    // Called when a player leaves the room
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        base.OnPlayerLeftRoom(otherPlayer);
        UpdatePlayerList(); // Update the player list when a player leaves
    }

    // Updates the player list UI with the nicknames of the connected players
    void UpdatePlayerList()
    {
        Player[] players = PhotonNetwork.PlayerList;

        // Loop through the TextMeshPro UI elements and assign nicknames in order
        for (int i = 0; i < playerNickNameTexts.Length; i++)
        {
            if (i < players.Length)
            {
                // Assign the player's nickname to the corresponding Text
                playerNickNameTexts[i].text = players[i].NickName;
            }
            else
            {
                // Clear the text if there is no player for this slot
                playerNickNameTexts[i].text = "Waiting...";
            }
        }
    }
}
