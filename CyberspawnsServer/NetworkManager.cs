using CyberspawnsServer.Core;
using ENet;
using Newtonsoft.Json;
using QNetLib;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CyberspawnsServer
{
    public class NetworkManager
    {
        public NetworkManager() {
            connectedClientsWithEndpoint = new ConcurrentDictionary<EndPoint, Client>();
        }

        private Server server;
        public ConcurrentDictionary<EndPoint, Client> connectedClientsWithEndpoint;// Set of pending connections
        private static PongEvent pong = new();


        public void StartServer(string ip, int port)
        {
            server = new Server(this);
            server.StartServer(ip, port);

        }

        public void Update()
        {
            if (Core.Timer.CheckTick())
            {
                
                foreach (KeyValuePair<EndPoint, Client> item in connectedClientsWithEndpoint)
                {
                    double responsDifference = Core.Timer.TotalsecondsSinceStart - item.Value.lastPongTime;
                    if (responsDifference >= 5) //TODO : implement a better expire value
                    {
                        //Dont bother sending ping just kill the client
                        item.Value.Disconnect();
                        continue;
                    }
                    item.Value.SendPing();
                }
            }
        }

        public void OnReciveData(byte[] payload, int lenght, EndPoint sender)
        {
            if (TryGetClient(sender, out Client client))
            {
                try
                {
                    string msg = Encoding.ASCII.GetString(payload, 0, lenght);
                    Datagram? datagram = JsonConvert.DeserializeObject<Datagram>(msg);
                    HandleIncommingData(client, datagram);
                }
                catch (Exception e)
                {
                    Logger.LogError(e);
                }

            }
        }

        public void HandleIncommingData(Client client, Datagram datagram)
        {
            EventType type = (EventType)Convert.ToInt32(datagram.type);
            switch (type)
            {
                case EventType.Login:
                    LoginEvent login = JsonConvert.DeserializeObject<LoginEvent>(datagram.body.ToString());
                    

                    break;
                case EventType.Pong:
                case EventType.Ping:
                    client.lastPongTime = Core.Timer.TotalsecondsSinceStart;
                    break;
                case EventType.Message:
                    MessageHandler.Instance.HandleMessageAsync(client, datagram.body, datagram.id);
                    break;
            }
        }

  

        public bool TryGetClient(EndPoint ID, out Client client)
        {
            return connectedClientsWithEndpoint.TryGetValue(ID, out client);
        }


        public void OnClientConnected(Client client)
        {
            connectedClientsWithEndpoint.TryAdd(client.ClientEndpoint, client);
            client.SendDataGram(new Datagram(EventType.Connection, true));
            Logger.LogInfo("New Connection Waiting for logging");
        }

        public void OnClientDisconnected(int NetId, EndPoint endPoint, DisconnectionReasons disconnectionReasons)
        {
            Logger.LogInfo(" DisConnection");
            if (connectedClientsWithEndpoint.ContainsKey(endPoint))
            {
                connectedClientsWithEndpoint.TryRemove(endPoint, out Client c);
            }
        }

        public static void PublishMessage(short messageId, object body, Client client, object id)
        {
            Datagram gram = new Datagram(EventType.Message, MessageHandler.SerializeMessage(messageId, body), id);
            client.SendDataGram(gram);
        }

        public void Stop()
        {
            server.StopServer();
        }

    }
}
