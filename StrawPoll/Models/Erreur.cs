using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StrawPoll.Controllers;

namespace StrawPoll.Models
{
    public class ErreurGrave
    {    
        public string MessageTitre { get; private set; }
        public string MessageErreur { get; private set; }
        public string CommentaireErreur { get; private set; }
        public ErreurGrave (string messageTitre, string messageErreur,string commentaireErreur)
        {
            MessageTitre = messageTitre;
            MessageErreur = messageErreur;
            CommentaireErreur = commentaireErreur;
        }
    }
}