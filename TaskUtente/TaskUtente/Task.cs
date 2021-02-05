using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TaskUtente
{
    class Task
    {
        
        public string Descrizione { get; set; }

        public DateTime DataScadenza { get; set; }

        public string Importanza { get; set; }

    }

    
}
