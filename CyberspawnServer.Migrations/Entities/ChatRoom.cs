using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyberspawnServer.Migrations.Entities
{
    public class ChatRoom
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string[] UserIds { get; set; }
        public string UpdatedAt { get; set; }
        public string CreatedAt { get; set; }
    }
}
