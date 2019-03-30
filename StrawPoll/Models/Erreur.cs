using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StrawPoll.Controllers;

namespace StrawPoll.Models
{
    public class ErreurGrave
    {
        
        public string MessageErreur { get; private set; }
        public ErreurGrave (string messageErreur)
        {
            MessageErreur = messageErreur;
        }
    }
}