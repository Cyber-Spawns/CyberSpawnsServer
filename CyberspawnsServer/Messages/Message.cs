using CyberspawnsServer.Core;


namespace CyberspawnsServer
{
    public abstract class Message
    {
        public string ToJSon()
        {
            return SerializationHelper.Serialize(this);
        }
    }
}
