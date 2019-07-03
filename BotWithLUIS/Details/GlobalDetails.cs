
using Microsoft.Bot.Schema;
using System;
using System.Collections.Generic;

namespace BotWithLUIS
{
    public class GlobalDetails
    {
        public string InfoRequest { get; set; }
        public string InfoRespond { get; set; }
        public Boolean ConversationStarted { get; set; }
        public Boolean isChatFinished { get; set; }
        public string ActualIntent { get; set; }
        public List<Attachment> attachments;
        public IMessageActivity Reply;

    }
}
