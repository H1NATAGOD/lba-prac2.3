using System.Runtime.InteropServices.JavaScript;
using Npgsql;

namespace GarageConsoleApp;


public static class DatabaseRequests
{
    static string formattedDate = "";
    public static void GetAllUserQuests(int numberID)
    {
        var querySql = $"SELECT datequest, quest, completed FROM sidequests WHERE ID_user = {numberID} ";


        using var cmd = new NpgsqlCommand(querySql, DatabaseService.GetSqlConnection());


        using var reader = cmd.ExecuteReader();


        while (reader.Read())
        {
            Console.WriteLine($"Дата: {reader[0]} \nЗадание: {reader[1]} \nЗавершено: {reader[2]}");
        }
    }

    public static void Registr(string login, string password)
    {
        var querySql = $"insert into userside (login, password) values ('{login}','{password}')";


        using var cmd = new NpgsqlCommand(querySql, DatabaseService.GetSqlConnection());


        using var reader = cmd.ExecuteReader();

    }


    public static int Entrance(string login, string password)
    {
        var querySql = $"select id from userside where login = '{login}' and password = '{password}'";


        using var cmd = new NpgsqlCommand(querySql, DatabaseService.GetSqlConnection());


        using var reader = cmd.ExecuteReader();

        if (reader.Read())
        {
            int userID = reader.GetInt32(0);
            Console.WriteLine("Вход выполнен успешно");
            return userID;
        }
        else        {
            Console.WriteLine("Неверный логин или пароль");
            return -1;
        }
    }




   

      public static void AddNewQuest(string quest, int userID)
    {
        Console.WriteLine("Введите дату выполнения задания в формате (гггг-мм-дд чч:мм):");
        DateTime dateOfCompletion;
        string formattedDate;
        try        {
            dateOfCompletion = DateTime.Parse(Console.ReadLine());
            formattedDate = dateOfCompletion.ToString("yyyy-MM-dd HH:mm:ss");
        }
        catch        {
            Console.WriteLine("Неверный формат даты");
            return;
        }

        var querySql =
            $"INSERT INTO sidequests(datequest, quest, completed, id_user) VALUES ('{formattedDate}', '{quest}', false, {userID})";
        using var cmd = new NpgsqlCommand(querySql, DatabaseService.GetSqlConnection());
        cmd.ExecuteNonQuery();
    }
      
      public static void DeleteQuest(int userID, string namequest)
      {
          var querySql = $"DELETE FROM sidequests WHERE id_user = {userID} and datequest = {namequest}";
          using var cmd = new NpgsqlCommand(querySql, DatabaseService.GetSqlConnection());
          cmd.ExecuteNonQuery();
      }
      
      
      public static void TodayQuest(int userID)
      {
          formattedDate = DateTime.Today.ToString("yyyy-MM-dd HH:mm:ss");
            
          var querySql = $"SELECT * FROM sidequests where datequest = '{formattedDate}' and id_user = {userID}";
          using var cmd = new NpgsqlCommand(querySql, DatabaseService.GetSqlConnection());
          using var reader = cmd.ExecuteReader();
          while (reader.Read())
          {
              Console.WriteLine(
                  $"Название: {reader[1]} \nЗадача: {reader[2]} \nДата выполнения: {reader[3]}");
          }
      }
      public static void TommorowQuest(int userID)
      {
          formattedDate = DateTime.Today.AddDays(1).ToString("yyyy-MM-dd HH:mm:ss");
        
          var querySql = $"SELECT * FROM sidequests where datequest = '{formattedDate}' and id_user = {userID}";
          using var cmd = new NpgsqlCommand(querySql, DatabaseService.GetSqlConnection());
          using var reader = cmd.ExecuteReader();
          while (reader.Read())
          {
              Console.WriteLine(
                  $"Название: {reader[1]} \nЗадача: {reader[2]} \nДата выполнения: {reader[3]}");
          }
      }

      public static void WeekQuest(int userID)
      {
          int weekConvert = (int)DateTime.Today.DayOfWeek;
          if (weekConvert == 0)
          {
              weekConvert = 7;
          }

          formattedDate = DateTime.Today.ToString("yyyy-MM-dd HH:mm:ss");
          string formattedDateWeekConvert = DateTime.Today.AddDays(7 - weekConvert).ToString("yyyy-MM-dd HH:mm:ss");
        
          var querySql =
              $"SELECT * FROM sidequests where (datequest >= '{formattedDate}' AND datequest <= '{formattedDateWeekConvert}') and id_user = {userID}";
          using var cmd = new NpgsqlCommand(querySql, DatabaseService.GetSqlConnection());
          using var reader = cmd.ExecuteReader();
          while (reader.Read())
          {
              Console.WriteLine(
                  $"Название: {reader[1]} \nЗадача: {reader[2]} \nДата выполнения: {reader[3]}");
          }
      }
}
