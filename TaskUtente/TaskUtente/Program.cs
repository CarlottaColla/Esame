using System;

namespace TaskUtente
{
    class Program
    {
        static void Main(string[] args)
        {
            //inizializzo i task salvati sul file
            Task[] task = GestioneTask.LeggiTaskDaFile();
            int operazione = 0;
            Console.WriteLine("Benvenuto!");
            do
            {
                Console.WriteLine("Ecco una lista delle operazioni che puoi compiere:");
                Console.WriteLine("1 - Visualizzare i task;");
                Console.WriteLine("2 - Aggiungere un nuovo task;");
                Console.WriteLine("3 - Eliminare un task;");
                Console.WriteLine("4 - Filtrare i task per importanza;");
                Console.WriteLine("5 - Salva i dati e esci dal programma.");
                Console.WriteLine("Scrivi il numero corrispondente all'operazione:");
                //ciclo do while per controllare la correttezza
                do
                {
                    operazione = Convert.ToInt32(Console.ReadLine());

                } while (operazione < 0 && operazione > 5);
                //switch per eseguire l'operazione corretta
                switch (operazione)
                {
                    case 1:
                        GestioneTask.StampaTask(task);
                        break;
                    case 2:
                        GestioneTask.AggiungiTask(ref task);
                        GestioneTask.StampaTask(task);
                        break;
                    case 3:
                        GestioneTask.EliminaTask(ref task);
                        GestioneTask.StampaTask(task);
                        break;
                    case 4:
                        GestioneTask.FiltraPerImportanza(task);
                        break;
                    case 5:
                        GestioneTask.SalvaInFile(task);
                        Console.WriteLine("Arrivederci!");
                        break;
                    case 0:
                        Console.WriteLine("Errore, riprovare: ");
                        break;

                }

            } while (operazione != 5);


        }
    }
}
