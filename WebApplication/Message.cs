using System;
using System.Collections.Generic;

#nullable disable

namespace WebApplication
{
    public partial class Message
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Message1 { get; set; }
        public DateTime? Time { get; set; }
    }
}
