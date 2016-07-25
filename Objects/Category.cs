using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace PersonalManagement
{
  public class Category
  {
    private int _id;
    private string _name;
    public Category (string Name, int Id = 0)
    {
      _id = Id;
      _name = Name;
    }
    public int GetId()
    {
      return _id;
    }
    public string GetName()
    {
      return _name;
    }
    public void SetName (string newName)
    {
      _name = newName;
    }
    public override bool Equals (System.Object otherCategory)
    {
      if (otherCategory is Category)
      {
        Category newCategory = (Category) otherCategory;
        bool idEquality = (this.GetId() == newCategory.GetId());
        bool nameEquality = (this.GetName() == newCategory.GetName());
        return (idEquality && nameEquality);
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
      SqlCommand cmd = new SqlCommand ("INSERT INTO categories (name) OUTPUT INSERTED.id VALUES (@CategoryName);", conn);
      SqlParameter nameParameter = new SqlParameter();
      nameParameter.ParameterName = "@CategoryName";
      nameParameter.Value = this.GetName();
      cmd.Parameters.Add(nameParameter);
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
    public static List<Category> GetAll()
    {
      List<Category> allCategories = new List<Category> {};
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlDataReader rdr;
      SqlCommand cmd = new SqlCommand ("SELECT * FROM categories;", conn);
      rdr = cmd.ExecuteReader();
      while (rdr.Read())
      {
        int categoryId = rdr.GetInt32(0);
        string categoryName = rdr.GetString(1);
        Category newCategory = new Category (categoryName, categoryId);
        allCategories.Add(newCategory);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return allCategories;
    }
    public void AddTask (Task newTask)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlDataReader rdr;
      SqlCommand cmd = new SqlCommand ("INSERT INTO todo_list (task_id, category_id) VALUES (@TaskId, @CategoryId);", conn);
      SqlParameter taskIdParameter = new SqlParameter();
      taskIdParameter.ParameterName = "@TaskId";
      taskIdParameter.Value = newTask.GetId();
      SqlParameter categoryIdParameter = new SqlParameter();
      categoryIdParameter.ParameterName = "@CategoryId";
      categoryIdParameter.Value = this.GetId();
      cmd.Parameters.Add(taskIdParameter);
      cmd.Parameters.Add(categoryIdParameter);
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
    public List<Task> GetTasks()
    {
      List<Task> allTasks = new List<Task> {};
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlDataReader rdr;
      SqlCommand cmd = new SqlCommand ("SELECT tasks.* FROM tasks JOIN todo_list ON (tasks.id = todo_list.task_id) JOIN categories ON (categories.id = todo_list.category_id) WHERE categories.id = @CategoryId;", conn);
      SqlParameter idParameter = new SqlParameter();
      idParameter.ParameterName = "@CategoryId";
      idParameter.Value = this.GetId();
      cmd.Parameters.Add(idParameter);
      rdr = cmd.ExecuteReader();
      while (rdr.Read())
      {
        int taskId = rdr.GetInt32(0);
        string taskName = rdr.GetString(1);
        DateTime taskDueDate = rdr.GetDateTime(2);
        Task newTask = new Task (taskName, taskDueDate, taskId);
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
    public static Category Find (int queryId)
    {
      List<Category> foundCategories = new List<Category> {};
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlDataReader rdr;
      SqlCommand cmd = new SqlCommand ("SELECT * FROM categories WHERE id = @QueryId;", conn);
      SqlParameter idParameter = new SqlParameter();
      idParameter.ParameterName = "@QueryId";
      idParameter.Value = queryId;
      cmd.Parameters.Add(idParameter);
      rdr = cmd.ExecuteReader();
      while (rdr.Read())
      {
        int categoryId = rdr.GetInt32(0);
        string categoryName = rdr.GetString(1);
        Category foundCategory = new Category (categoryName, categoryId);
        foundCategories.Add(foundCategory);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return foundCategories[0];
    }
    public static Category FindByName (string queryName)
    {
      List<Category> foundCategories = new List<Category> {};
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlDataReader rdr;
      SqlCommand cmd = new SqlCommand ("SELECT * FROM categories WHERE name = @QueryName;", conn);
      SqlParameter nameParameter = new SqlParameter();
      nameParameter.ParameterName = "@QueryName";
      nameParameter.Value = queryName;
      cmd.Parameters.Add(nameParameter);
      rdr = cmd.ExecuteReader();
      while (rdr.Read())
      {
        int categoryId = rdr.GetInt32(0);
        string categoryName = rdr.GetString(1);
        Category foundCategory = new Category (categoryName, categoryId);
        foundCategories.Add(foundCategory);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return foundCategories[0];
    }
    public void Update ()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand ("UPDATE categories SET name = @NewCategoryName WHERE id = @CategoryId;", conn);
      SqlParameter newNameParameter = new SqlParameter();
      newNameParameter.ParameterName = "@NewCategoryName";
      newNameParameter.Value = this.GetName();
      SqlParameter idParameter = new SqlParameter();
      idParameter.ParameterName = "@CategoryId";
      idParameter.Value = this.GetId();
      cmd.Parameters.Add(newNameParameter);
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
      SqlCommand cmd = new SqlCommand ("DELETE FROM categories WHERE id = @CategoryId;", conn);
      SqlParameter idParameter = new SqlParameter();
      idParameter.ParameterName = "@CategoryId";
      idParameter.Value = this.GetId();
      cmd.Parameters.Add(idParameter);
      cmd.ExecuteNonQuery();
    }
    public static void DeleteAll()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand ("DELETE FROM categories;", conn);
      cmd.ExecuteNonQuery();
    }
  }
}
