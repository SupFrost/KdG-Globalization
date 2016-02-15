using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using SC.BL;
using SC.BL.Domain;
using SC.UI.CA.ExtensionMethods;
using TranslationTier;

namespace SC.UI.CA
{
    internal class Program
    {
        private static bool quit;
        private static readonly ITicketManager mgr = new TicketManager();
        private static readonly Service srv = new Service();

        private static void Main(string[] args)
        {
            while (!quit)
                ShowMenu();
        }

        private static void ShowMenu()
        {
            Console.WriteLine("=================================");
            Console.WriteLine(Resource.HELPDESKSUPPORTCENTER);
            Console.WriteLine("=================================");
            Console.WriteLine("1) " + Resource.ShowAllTickets);
            Console.WriteLine("2) " + Resource.ShowDetailsOfATicket);
            Console.WriteLine("3) " + Resource.ShowAnswersToATicket);
            Console.WriteLine("4) " + Resource.CreateANewTicket);
            Console.WriteLine("5) " + Resource.GiveAnAnswerToATicket);
            Console.WriteLine("6) " + Resource.MarkTicketAsClosed);
            Console.WriteLine("7) " + Resource.Language);
            Console.WriteLine("0) " + Resource.Exit);
            try
            {
                DetectMenuAction();
            }
            catch (Exception e)
            {
                Console.WriteLine();
                Console.WriteLine(Resource.ThereWasAnUnexpectedError);
                Console.WriteLine(e.InnerException);
                Console.WriteLine();
            }
        }

        private static void DetectMenuAction()
        {
            var inValidAction = false;
            do
            {
                Console.Write(Resource.Choice + " : ");
                var input = Console.ReadLine();
                int action;
                if (int.TryParse(input, out action))
                {
                    switch (action)
                    {
                        case 1:
                            PrintAllTickets();
                            break;
                        case 2:
                            ActionShowTicketDetails();
                            break;
                        case 3:
                            ActionShowTicketResponses();
                            break;
                        case 4:
                            ActionCreateTicket();
                            break;
                        case 5:
                            ActionAddResponseToTicket();
                            break;
                        case 6:
                            ActionCloseTicket();
                            break;
                        case 7:
                            ActionLanguague();
                            break;
                        case 8:
                            ShowMenu();
                            break;
                        case 0:
                            quit = true;
                            return;
                        default:
                            Console.WriteLine(Resource.NoAValidChoice);
                            inValidAction = true;
                            break;
                    }
                }
            } while (inValidAction);
        }

        private static void ActionLanguague()
        {
            Console.WriteLine("=================================");
            Console.WriteLine(Resource.LanguageChoice);
            Console.WriteLine("=================================");
            Console.WriteLine("1) " + Resource.dutch);
            Console.WriteLine("2) " + Resource.english);
            Console.WriteLine("0) " + Resource.Exit);
            try
            {
                DetectLanguageAction();
            }
            catch (Exception e)
            {
                Console.WriteLine();
                Console.WriteLine(Resource.ThereWasAnUnexpectedError);
                Console.WriteLine(e.InnerException);
                Console.WriteLine();
            }
        }

        private static void DetectLanguageAction()
        {
            var inValidAction = false;
            do
            {
                Console.Write(Resource.Choice + " : ");
                var input = Console.ReadLine();
                int action;
                if (int.TryParse(input, out action))
                {
                    switch (action)
                    {
                        case 1:
                            Thread.CurrentThread.CurrentCulture = new CultureInfo("nl");
                            Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;
                            break;
                        case 2:
                            Thread.CurrentThread.CurrentCulture = new CultureInfo("en");
                            Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;
                            break;
                        case 0:
                            quit = true;
                            return;
                        default:
                            Console.WriteLine(Resource.NoAValidChoice);
                            inValidAction = true;
                            break;
                    }
                }
            } while (inValidAction);
        }

        private static void ActionCloseTicket()
        {
            Console.Write(Resource.TicketNumber + ": ");
            var input = int.Parse(Console.ReadLine());

            //mgr.ChangeTicketStateToClosed(input);
            // via WebAPI-service
            srv.ChangeTicketStateToClosed(input);
        }

        private static void PrintAllTickets()
        {
            foreach (Ticket t in mgr.GetTickets())
                Console.WriteLine(t.GetInfo());
        }

        private static void ActionShowTicketDetails()
        {
            Console.Write(Resource.TicketNumber + ": ");
            var input = int.Parse(Console.ReadLine());

            Ticket t = mgr.GetTicket(input);
            PrintTicketDetails(t);
        }

        private static void PrintTicketDetails(Ticket ticket)
        {
            Console.WriteLine("{0,-15}: {1}", "Ticket", ticket.TicketNumber);
            Console.WriteLine("{0,-15}: {1}", "Gebruiker", ticket.AccountId);
            Console.WriteLine("{0,-15}: {1}", "Datum", ticket.DateOpened.ToString("dd/MM/yyyy"));
            Console.WriteLine("{0,-15}: {1}", "Status", ticket.State);

            if (ticket is HardwareTicket)
                Console.WriteLine("{0,-15}: {1}", "Toestel", ((HardwareTicket) ticket).DeviceName);

            Console.WriteLine("{0,-15}: {1}", "Vraag/probleem", ticket.Text);
        }

        private static void ActionShowTicketResponses()
        {
            Console.Write("Ticketnummer: ");
            var input = int.Parse(Console.ReadLine());

            //IEnumerable<TicketResponse> responses = mgr.GetTicketResponses(input);
            // via Web API-service
            var responses = srv.GetTicketResponses(input);
            if (responses != null) PrintTicketResponses(responses);
        }

        private static void PrintTicketResponses(IEnumerable<TicketResponse> responses)
        {
            foreach (TicketResponse r in responses)
                Console.WriteLine(r.GetInfo());
        }

        private static void ActionCreateTicket()
        {
            var accountNumber = 0;
            var problem = "";
            var device = "";

            Console.Write("Is het een hardware probleem (j/n)? ");
            var isHardwareProblem = Console.ReadLine().ToLower() == "j";
            if (isHardwareProblem)
            {
                Console.Write("Naam van het toestel: ");
                device = Console.ReadLine();
            }

            Console.Write("Gebruikersnummer: ");
            accountNumber = int.Parse(Console.ReadLine());
            Console.Write("Probleem: ");
            problem = Console.ReadLine();

            if (!isHardwareProblem)
                mgr.AddTicket(accountNumber, problem);
            else
                mgr.AddTicket(accountNumber, device, problem);
        }

        private static void ActionAddResponseToTicket()
        {
            Console.Write("Ticketnummer: ");
            var ticketNumber = int.Parse(Console.ReadLine());
            Console.Write("Antwoord: ");
            var response = Console.ReadLine();

            //mgr.AddTicketResponse(ticketNumber, response, false);
            // via WebAPI-service
            srv.AddTicketResponse(ticketNumber, response, false);
        }
    }
}