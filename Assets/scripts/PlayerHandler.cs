using System.Net.Http.Headers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

internal class PlayerHandler : MonoBehaviour
{
    private QueueProcessor queueProcessor = new QueueProcessor();

    public Player[] players = new Player[3];    

    // Start is called before the first frame update
    void Start()
    {
    }

    public void Enqueue(IPlayerData item)
    {
        this.queueProcessor.Enqueue(item);
    }

    // Update is called once per frame
    void Update()
    {
        Snapshot snapshot = queueProcessor.ProcessQueue();    

        // Loop through all the players
        for (int i = 0; i < this.players.Length; i++)
        {
            Player player = this.players[i];
            TeamSnapshot teamSnapshot = snapshot.teams.FirstOrDefault(x => x.teamName == player.teamColour);            
            this.updatePlayerSpeed(teamSnapshot, player);
        }        
    }

    void updatePlayerSpeed(TeamSnapshot teamSnapshot, Player player)
    {   
        if (teamSnapshot == null || teamSnapshot?.teamScore <= 0.0f) 
        {
            return;    
        }
        
        player?.UpdateSpeed(teamSnapshot.teamScore); 
    }
}
