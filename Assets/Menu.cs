using System;
using UdpKit;
using UnityEngine;

public class Menu : Bolt.GlobalEventListener
{
    public void StartServer() {
        BoltLauncher.StartServer();
    }

    public void StartClient() {
        BoltLauncher.StartClient();
    }

    public override void BoltStartDone() {
        BoltNetwork.EnableLanBroadcast();
        
        if (BoltNetwork.IsServer)
        {
            BoltNetwork.LoadScene("SampleScene");
            string matchName = "Test Match";
            BoltNetwork.SetServerInfo(matchName, null);

        }
        // else
        // {
        //     clientStarted = true;
        // }
        // if (BoltNetwork.IsServer) {
        //     
        //     BoltNetwork.LoadScene("SampleScene");
        // }
    }


    /*
    When the client is started, it connects to Photon Server and pulls information about any 
    visible room that has been registered on the cloud. To get this information we use the 
    SessionListUpdated handler, that is invoked by Bolt on a regular basis when a 
    new room info arrives internally.

    Below, we go through all available sessions, looking 
    for one Photon Session and connects to the first one found. 
    The client will start the punchthrough process with the Host and joins the game.
    */
    public override void SessionListUpdated(Map<Guid, UdpSession> sessionList) {
        foreach (var session in sessionList) {
            UdpSession photonSession = session.Value as UdpSession;
            BoltNetwork.Connect(photonSession, null);
            Debug.LogFormat("Source is", photonSession.Source, "and endpoint is", photonSession.LanEndPoint);

            // if (photonSession.Source == UdpSessionSource.Lan) {
            //     BoltNetwork.Connect(photonSession);
            // }
        }
    }
}
