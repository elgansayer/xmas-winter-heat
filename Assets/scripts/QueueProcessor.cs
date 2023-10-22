using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// add using linq
using System.Linq;

internal class QueueProcessor
{
    private Queue<IPlayerData> queue = new Queue<IPlayerData>();

    public void Enqueue(IPlayerData item)
    {
        queue.Enqueue(item);
    }

    public IPlayerData Dequeue()
    {
        return queue.Dequeue();
    }

    public int Count()
    {
        return queue.Count;
    }

    public Snapshot ProcessQueue()
    {
        Snapshot snapshot = new Snapshot();

        while (queue.Count > 0)
        {
            IPlayerData item = Dequeue();
            ProcessItem(ref snapshot, item);
        }

        return snapshot;
    }

    public void ProcessItem(ref Snapshot snapshot, IPlayerData item)
    {    
        string teamName = item.player.team.name.Trim();
        TeamSnapshot teamSnapshot = snapshot.teams.FirstOrDefault(x => x.teamName == teamName );
        teamSnapshot.teamScore = item.toggle;
    }
}

public class Snapshot {
    public TeamSnapshot[] teams = { new TeamSnapshot("RED", 0.0f), new TeamSnapshot("BLUE", 0.0f), new TeamSnapshot("GREEN", 0.0f), new TeamSnapshot("ORANGE", 0.0f) };
}

public class TeamSnapshot
{
    public string teamName;
    public float teamScore;

    public TeamSnapshot(string teamName, float teamScore)
    {
        this.teamName = teamName;
        this.teamScore = teamScore;
    }
}
