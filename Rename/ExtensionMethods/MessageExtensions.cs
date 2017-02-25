using Rename.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rename.ExtensionMethods
{

    public static class MessageExtensions
    {

        public static void AddMessage(this List<Message> list, Message.MessageCategory category, string text)
        {
            list.Add(new Utility.Message() { Category = category, Text = text });
        }

    }

}
