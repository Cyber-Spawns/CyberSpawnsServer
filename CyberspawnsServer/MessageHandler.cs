using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CyberspawnsServer.Core;
using Newtonsoft.Json;

namespace CyberspawnsServer
{

    public class MessageProxy
    {
        public short messageID;
        public object messageBody;
    }

    public class MessageHandler : Singliton<MessageHandler>
    {
        public delegate Task HandleAsync(Client client, object messageBody, object id, CancellationToken cancellationToken);

        public MessageProxy messageProxy;
        public NetworkManager networkManager;
        private readonly Dictionary<short, HandleAsync> _handlers;
        

        public MessageHandler(NetworkManager manager)
        {
            Instance = this;
            this.networkManager = manager;
            messageProxy = new MessageProxy();
            _handlers = new Dictionary<short, HandleAsync>();
            RegisterMessages();

            
        }

        public static object SerializeMessage(short messageID, object message)
        {
            string body = JsonConvert.SerializeObject(new { messageID = messageID, messageBody = message });
            MessageProxy proxy = new MessageProxy
            {
                messageID = messageID,
                messageBody = body
            };
            return JsonConvert.SerializeObject(proxy);
        }

        

        public void RegisterMessages(short messageID, HandleAsync handle) 
        {
            _handlers.Add(messageID, handle);
        }

        private void RegisterMessages()
        {
            
        }


        public async void HandleMessageAsync(Client client, object messageBody, object id)
        {
            using(var source = new CancellationTokenSource())
            {
                var token = source.Token;
                MessageProxy? proxy = JsonConvert.DeserializeObject<MessageProxy>(messageBody.ToString());

                if (_handlers.TryGetValue(proxy.messageID, out HandleAsync handle))
                {
                    try
                    {
                        await handle(client, proxy.messageBody, id, token);
                    }
                    catch (Exception)
                    {
                        NetworkManager.PublishMessage(MessageEvents.SYSTEM_MESSAGE, "Failed", client, id);
                    }
                }
                else
                {
                    Console.WriteLine("Message Handle dose noe exist");
                }
            }
        }


        
    }
}
