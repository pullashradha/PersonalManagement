using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace PersonalManagement
{
  public class Task
  {
    private int _id;
    private string _description;
    private DateTime? _dueDate;
    public Task (string Description, DateTime? DueDate, int Id = 0)
    {
      _id = Id;
      _description = Description;
      _dueDate = DueDate;
    }
    public int GetId()
    {
      return _id;
    }
    public string GetDescription()
    {
      return _description;
    }
    public void SetDescription (string newDescription)
    {
      _description = newDescription;
    }
    public DateTime? GetDueDate()
    {
      return _dueDate;
    }
    public void SetDueDate (DateTime? newDueDate)
    {
      _dueDate = newDueDate;
    }
    public override bool Equals (System.Object otherTask)
    {
      if (otherTask is Task)
      {
        Task newTask = (Task) otherTask;
        bool idEquality = (this.GetId() == newTask.GetId());
        bool descriptionEquality = (this.GetDescription() == newTask.GetDescription());
        bool dueDateEquality = (this.GetDueDate() == newTask.GetDueDate());
        return (idEquality && descriptionEquality && dueDateEquality);
      }
      else
      {
        return false;
      }
    }
    public void Save()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlDataReader rdr;
      SqlCommand cmd = new SqlCommand ("INSERT INTO tasks (description, due_date) OUTPUT INSERTED.id VALUES (@TaskDescription, @TaskDueDate);", conn);
      SqlParameter descriptionParameter = new SqlParameter();
      descriptionParameter.ParameterName = "@TaskDescription";
      descriptionParameter.Value = this.GetDescription();
      SqlParameter dueDateParameter = new SqlParameter();
      dueDateParameter.ParameterName = "@TaskDueDate";
      dueDateParameter.Value = this.GetDueDate();
      cmd.Parameters.Add(descriptionParameter);
      cmd.Parameters.Add(dueDateParameter);
      rdr = cmd.ExecuteReader();
      while (rdr.Read())
      {
        this._id = rdr.GetInt32(0);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
    }
    public static List<Task> GetAll()
    {
      List<Task> allTasks = new List<Task> {};
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlDataReader rdr;
      SqlCommand cmd = new SqlCommand ("SELECT * FROM tasks;", conn);
      rdr = cmd.ExecuteReader();
      while (rdr.Read())
      {
        int taskId = rdr.GetInt32(0);
        string taskDescription = rdr.GetString(1);
        DateTime? taskDueDate = rdr.GetDateTime(2);
        Task newTask = new Task (taskDescription,taskDueDate, taskId);
        allTasks.Add(newTask);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return allTasks;
    }
    public static Task Find (int queryId)
    {
      List<Task> foundTasks = new List<Task> {};
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlDataReader rdr;
      SqlCommand cmd = new SqlCommand ("SELECT * FROM tasks WHERE id = @QueryId;", conn);
      SqlParameter idParameter = new SqlParameter();
      idParameter.ParameterName = "@QueryId";
      idParameter.Value = queryId;
      cmd.Parameters.Add(idParameter);
      rdr = cmd.ExecuteReader();
      while (rdr.Read())
      {
        int taskId = rdr.GetInt32(0);
        string taskDescription = rdr.GetString(1);
        DateTime? taskDueDate = rdr.GetDateTime(2);
        Task foundTask = new Task (taskDescription, taskDueDate, taskId);
        foundTasks.Add(foundTask);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return foundTasks[0];
    }
    public void Update ()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand ("UPDATE tasks SET description = @NewTaskDescription, due_date = @NewTaskDueDate WHERE id = @TaskId;", conn);
      SqlParameter newDescriptionParameter = new SqlParameter();
      newDescriptionParameter.ParameterName = "@NewTaskDescription";
      newDescriptionParameter.Value = this.GetDescription();
      SqlParameter newDueDateParameter = new SqlParameter();
      newDueDateParameter.ParameterName = "@NewTaskDueDate";
      newDueDateParameter.Value = this.GetDueDate();
      SqlParameter idParameter = new SqlParameter();
      idParameter.ParameterName = "@TaskId";
      idParameter.Value = this.GetId();
      cmd.Parameters.Add(newDescriptionParameter);
      cmd.Parameters.Add(newDueDateParameter);
      cmd.Parameters.Add(idParameter);
      cmd.ExecuteNonQuery();
      if (conn != null)
      {
        conn.Close();
      }
    }
    public void DeleteOne()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand ("DELETE FROM tasks WHERE id = @TaskId;", conn);
      SqlParameter idParameter = new SqlParameter();
      idParameter.ParameterName = "@TaskId";
      idParameter.Value = this.GetId();
      cmd.Parameters.Add(idParameter);
      cmd.ExecuteNonQuery();
    }
    public static void DeleteAll()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand ("DELETE FROM tasks;", conn);
      cmd.ExecuteNonQuery();
    }
  }
}
