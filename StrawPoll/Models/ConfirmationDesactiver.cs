﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StrawPoll.Models
{
    public class ConfirmationDesactiver
    {
        public Sondage NouveauSondage { get; private set; }

        public ConfirmationDesactiver(Sondage nouveauSondage)
        {
            NouveauSondage = nouveauSondage;

        }
    }
}