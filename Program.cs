namespace GarageConsoleApp;

/// <summary>
/// Класс Program
/// здесь описываем логику приложения
/// вызываем нужные методы, пишем обработчики и т.д.
/// </summary>
public class Program
{
 public static void Main(string[] args)
 {
  int oneplace = 0;
  int userID = 0;
  string? bang1 = Console.ReadLine();

  while (bang1 != "Q")
  {
   switch (bang1)
   {
    case "1":
     userID = DatabaseRequests.Entrance(Console.ReadLine(), Console.ReadLine());
     Console.WriteLine(userID);
     break;
    case "2":
     DatabaseRequests.Registr(Console.ReadLine(), Console.ReadLine());
     break;
   }

   string bang2 = Console.ReadLine();


   while (bang2 != "Q")
   {
    switch (bang2)
    {


     case "1":
        DatabaseRequests.GetAllUserQuests(userID);
      break;

     case "2":
      Console.WriteLine("Введите описание задания:");
      string questDescription = Console.ReadLine();

      DatabaseRequests.AddNewQuest(questDescription, userID);
      break;

      case "3":
       DatabaseRequests.TodayQuest(userID);
       break;
      
      case "4":
       DatabaseRequests.TommorowQuest(userID);
       break;
      
      case "5":
       DatabaseRequests.WeekQuest(userID);
       break;
      
      case "6":
       DatabaseRequests.DeleteQuest(userID);
       break;


     default:
       Console.WriteLine($"Извините, технические шоколадки(");
      break;




    }

    bang2 = Console.ReadLine();
   }

   bang1 = Console.ReadLine();
  }
 }
}
  

