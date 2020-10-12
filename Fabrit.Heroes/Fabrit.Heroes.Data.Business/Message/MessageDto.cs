using System;
using System.Collections.Generic;
using System.Text;

namespace Fabrit.Heroes.Data.Business.Message
{
    public class MessageDto
    {
        public string User { get; set; }
        public string Text { get; set; }
        public string AvatarPath { get; set; }
        public string Hour { get; set; }
    }
}
