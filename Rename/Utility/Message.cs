using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rename.Utility
{

    public class Message
    {

        public enum MessageCategory
        {
            Information,
            Warning,
            Error
        }

        public MessageCategory Category { get; set; }
        public string Text { get; set; }

        //public Message (MessageCategory category, string text)
        //{
        //    this.Category = category;
        //    this.Text = text;
        //}

    }

}
