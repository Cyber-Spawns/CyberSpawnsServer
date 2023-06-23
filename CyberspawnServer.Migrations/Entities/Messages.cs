using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyberspawnServer.Migrations.Entities
{
    public class Messages
    {
        public int Id { get; set; }
        public string SenderId { get; set; }
        public string ReceiveId { get; set; }
        public string Content { get; set; }
        public Guid ChatRoomId { get; set; }
        public string MediaUrl { get; set; }
        public DateTime Timestamp { get; set; }

    }
}
