using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace TaskUtente
{
    class GestioneTask
    {
        //path del file
        public static string path { get; } = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Task.csv");
        
        //Funzione per stampare correttamente a console
        public static void StampaTask (Task[] tastkDaStampare)
        {
            Console.WriteLine("Descrizione - Data di scadenza - Importanza");
            for (int i = 0; i < tastkDaStampare.Length; i++)
            {
                Console.WriteLine(tastkDaStampare[i].Descrizione + " - " + tastkDaStampare[i].DataScadenza +
                    " - " + tastkDaStampare[i].Importanza);
            }
        }

        //Funzione per leggere i task salvati sul file
        public static Task[] LeggiTaskDaFile ()
        {
            string tuttoIlFile;
            string[] fileSplit;
            string[] riga;
            Task[] taskSalvati;

            try
            {
                using(StreamReader reader = File.OpenText(path))
                {
                    //salvo tutto il file in una stringa
                    tuttoIlFile = reader.ReadToEnd();
                }
                //salvo una riga del file in ogni elemento dell'array
                fileSplit = tuttoIlFile.Split("\r\n");

                //assegno la dimensione all'array: aggiungo un -1 per togliere l'intestazione
                taskSalvati = new Task[fileSplit.Length - 1];

                //ciclo for per salvare i dati in un array di Task, parte da 1 perchè la prima riga è l'intestazione
                for (int i = 1; i < fileSplit.Length; i++)
                {
                    //l'array riga salva ogni elemento seperato da virgola
                    riga = fileSplit[i].Split(",");
                    taskSalvati[i - 1] = new Task()
                    {
                        Descrizione = riga[0],
                        DataScadenza = Convert.ToDateTime(riga[1]),
                        Importanza = riga[2]
                    };

                }
                return taskSalvati;

            }
            catch (Exception e)
            {
                Console.WriteLine("Errore : " + e.Message);
                throw;
            }
        }
        
        //aggiungere un nuovo Task (non verrà salvato sul file, il salvataggio lo effettu a fine del programma
        public static void AggiungiTask (ref Task[] tuttiITask)
        {
            //Variabili per controllare la correttezza dei dati inseriti
            DateTime dataAttuale = DateTime.Now;
            DateTime dataDaControllare = new DateTime();
            string controlloDescrizione;
            bool descrizioneCorretta = true;

            Task nuovoTask = new Task();
            
            //gestico l'interazione con l'utente all'interno della funzione
            Console.WriteLine("Inserisci i dati del nuovo Task:");
            do
            {
                descrizioneCorretta = true;
                Console.WriteLine("Inserisci la descrizione univoca:");
                controlloDescrizione = Console.ReadLine();
                for (int i = 0; i < tuttiITask.Length; i++)
                {
                    if (controlloDescrizione == tuttiITask[i].Descrizione)
                    {
                        Console.WriteLine("La descrizione inserita non è univoca, riprovare");
                        descrizioneCorretta = false;
                        break;
                    }

                }

            } while (descrizioneCorretta == false);
            nuovoTask.Descrizione = controlloDescrizione;
            //controllo della data
            do
            {
                Console.WriteLine("Inserisci la data di scadenza:");
                dataDaControllare = Convert.ToDateTime(Console.ReadLine());
                if (dataDaControllare < dataAttuale)
                    Console.WriteLine("La data inserita non è corretta, " +
                        "inserisci una data posteriore o uguale a quella attuale.");
            } while (dataDaControllare < dataAttuale);
            nuovoTask.DataScadenza = dataDaControllare;
            Console.WriteLine("Inserisci l'importanza:");
            nuovoTask.Importanza = Console.ReadLine();

            //Aumento la dimensione dell'array e aggiungo il nuovo task
            Array.Resize(ref tuttiITask, tuttiITask.Length + 1);
            tuttiITask[tuttiITask.Length - 1] = nuovoTask;

        }

        //Eliminare un task
        public static void EliminaTask (ref Task[] tuttiITask)
        {
            //gestico internamente l'interazione con l'utente
            //arraylist in cui aggiungo tutti i task da tenere
            ArrayList taskDaTenere = new ArrayList();
            //variabili per il controllo della correttezza
            bool descrizioneCorretta = true;
            string controlloDescrizione;
            //Controllo che la descrizione inserita sia corretta
            do
            {
                if (descrizioneCorretta == false)
                {
                    Console.WriteLine("Nessun Task corrispondente, riprovare");
                }
                descrizioneCorretta = false;
                Console.WriteLine("Inserisci la descrizione del Task da eliminare: ");
                controlloDescrizione = Console.ReadLine();
                for (int i = 0; i < tuttiITask.Length; i++)
                {
                    if (controlloDescrizione == tuttiITask[i].Descrizione)
                    {
                        descrizioneCorretta = true;
                        break;
                    }
                }
            } while(descrizioneCorretta == false);
            for(int i = 0; i < tuttiITask.Length; i++)
            {
                if (controlloDescrizione != tuttiITask[i].Descrizione)
                    taskDaTenere.Add(tuttiITask[i]);
            }
            tuttiITask = (Task[])taskDaTenere.ToArray(typeof(Task));
        }


        //Filtrare i task per importanza
        public static void FiltraPerImportanza(Task[] tuttiITask)
        {
            //Contatore per contare i task filtrati (se sono 0 scrivo qualcosa)
            int count = 0;
            //variabili per il controllo della correttezza
            string importanzaInserita;
            bool valida;
            //gestisco internamente l'interazione con l'utente
            Console.WriteLine("Per quale importanza vuoi filtrare?\nRicorda i valori disponibili sono: " +
                "Alto, Medio o Basso.");
            //ciclo do while per controllare la correttezza del dato inserito
            do
            {
                valida = true;
                importanzaInserita = Console.ReadLine();
                if (importanzaInserita != "Alto" && importanzaInserita != "Medio" && importanzaInserita != "Basso")
                {
                    Console.WriteLine("L'importanza inserita non è valida riprova:");
                    valida = false;
                }
            } while (valida == false);
            //ciclo for per stampare i task con l'importanza inserita dall'utente
            Console.WriteLine("Descrizione - Data di scadenza - Importanza");
            for (int i = 0; i < tuttiITask.Length; i++)
            {
                if(tuttiITask[i].Importanza == importanzaInserita)
                {
                    Console.WriteLine(tuttiITask[i].Descrizione + " - " + tuttiITask[i].DataScadenza +
                    " - " + tuttiITask[i].Importanza);
                    count++;
                }

            }
            if (count == 0)
                Console.WriteLine("Nessun Task ha l'importanza inserita.");

        }

        //salvare i dati in file
        public static void SalvaInFile (Task[] taskDaSalvare)
        {
            //Il formato del file sarà:
            //Descrizione,DataScadenza,Importanza
            //Task1,06/02/2021 10:40:00,Alto

            try
            {
                using(StreamWriter file = File.CreateText(path))
                {
                    file.WriteLine("Descrizione,DataScadenza,Importanza");
                    for(int i = 0; i < taskDaSalvare.Length; i++)
                    {
                        file.WriteLine(taskDaSalvare[i].Descrizione + "," + taskDaSalvare[i].DataScadenza +
                            "," + taskDaSalvare[i].Importanza);
                    }
                }
                Console.WriteLine("Task salvati con successo!");

            }
            catch (Exception e)
            {
                Console.WriteLine("Errore: " + e.Message);
                throw;
            }


        }
    }
}
