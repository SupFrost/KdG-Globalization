using System;
using System.Collections.Generic;
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
      Console.WriteLine("1) "  + Resource.ShowAllTickets);
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
      Console.WriteLine("0) " + Resource.cancel);
      try
      {
        DetectLanguageAction();
      }
      catch (Exception e)
      {
        Console.WriteLine();
        Console.WriteLine(Resource.ThereWasAnUnexpectedError);
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
              Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("nl");
              Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;
              break;
            case 2:
              Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en");
              Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;
              break;
            case 0:
              break;
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
      foreach (var t in mgr.GetTickets())
        Console.WriteLine(t.GetInfo());
    }

    private static void ActionShowTicketDetails()
    {
      Console.Write(Resource.TicketNumber + ": ");
      var input = int.Parse(Console.ReadLine());

      var t = mgr.GetTicket(input);
      PrintTicketDetails(t);
    }

    private static void PrintTicketDetails(Ticket ticket)
    {
      Console.WriteLine("{0,-15}: {1}", Resource.Ticket, ticket.TicketNumber);
      Console.WriteLine("{0,-15}: {1}", Resource.User, ticket.AccountId);
      Console.WriteLine("{0,-15}: {1}", Resource.Date, ticket.DateOpened.ToString(Thread.CurrentThread.CurrentUICulture.DateTimeFormat));
      Console.WriteLine("{0,-15}: {1}", Resource.State, ticket.State);

      if (ticket is HardwareTicket)
        Console.WriteLine("{0,-15}: {1}", Resource.Machine, ((HardwareTicket) ticket).DeviceName);

      Console.WriteLine("{0,-15}: {1}", Resource.QuestionProblem, ticket.Text);
    }

    private static void ActionShowTicketResponses()
    {
      Console.Write(Resource.TicketNumber + ": ");
      var input = int.Parse(Console.ReadLine());

      //IEnumerable<TicketResponse> responses = mgr.GetTicketResponses(input);
      // via Web API-service
      var responses = srv.GetTicketResponses(input);
      if (responses != null) PrintTicketResponses(responses);
    }

    private static void PrintTicketResponses(IEnumerable<TicketResponse> responses)
    {
      foreach (var r in responses)
        Console.WriteLine(r.GetInfo());
    }

    private static void ActionCreateTicket()
    {
      var accountNumber = 0;
      var problem = "";
      var device = "";

      Console.Write(Resource.IsItAHardwareProblem);
      var isHardwareProblem = Console.ReadLine().ToLower() == Resource.Y;
      if (isHardwareProblem)
      {
        Console.Write(Resource.NameOfTheUnit + ": ");
        device = Console.ReadLine();
      }

      Console.Write(Resource.UserNumber + ": ");
      accountNumber = int.Parse(Console.ReadLine());
      Console.Write(Resource.Problem + ": ");
      problem = Console.ReadLine();

      if (!isHardwareProblem)
        mgr.AddTicket(accountNumber, problem);
      else
        mgr.AddTicket(accountNumber, device, problem);
    }

    private static void ActionAddResponseToTicket()
    {
      Console.Write(Resource.TicketNumber + ": ");
      var ticketNumber = int.Parse(Console.ReadLine());
      Console.Write(Resource.Answer + ": ");
      var response = Console.ReadLine();

      //mgr.AddTicketResponse(ticketNumber, response, false);
      // via WebAPI-service
      srv.AddTicketResponse(ticketNumber, response, false);
    }
  }
}