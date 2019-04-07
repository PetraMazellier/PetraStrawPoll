using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StrawPoll.Controllers;

namespace StrawPoll.Models
{
    public class CreationInvalide
    {
        public int NombreReponseMaximum { set; get; }
       
        public CreationInvalide(int nombreReponseMaximum)
        {
            NombreReponseMaximum = nombreReponseMaximum;
        }
    }
}