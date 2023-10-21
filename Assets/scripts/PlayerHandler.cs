using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

internal class PlayerHandler : MonoBehaviour
{
    private QueueProcessor queueProcessor = new QueueProcessor();
    private Player _player1;

    // Start is called before the first frame update
    void Start()
    {
        // Get each player object
        this._player1 = GameObject.Find("Player1").GetComponent<Player>();  
        this._player1.setupTeam("BLUE");
        
    }

    public void Enqueue(IPlayerData item)
    {
        this.queueProcessor.Enqueue(item);
    }

    // Update is called once per frame
    void Update()
    {
        if (!this._player1) {
            return;
        }

        Snapshot snapshot = queueProcessor.ProcessQueue();       

        // Get the blue team score
        TeamSnapshot teamSnapshot1 = snapshot.teams.FirstOrDefault(x => x.teamName == "BLUE");
        if (teamSnapshot1 != null && teamSnapshot1?.teamScore > 0.0f)
        {
            this._player1?.UpdateSpeed(teamSnapshot1.teamScore);        
        }
    }
}
