using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Xunit;

namespace PersonalManagement
{
  public class TaskTest : IDisposable
  {
    public TaskTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=personal_management_test;Integrated Security=SSPI;";
    }
    [Fact]
    public void Test_Empty_DatabaseEmptyAtFirst()
    {
      int dBValues = Task.GetAll().Count;
      Assert.Equal(0, dBValues);
    }
    [Fact]
    public void Test_Equals_EntriesMatch()
    {
      Task firstTask = new Task ("Walk the dog", new DateTime(2020,7,25));
      Task secondTask = new Task ("Walk the dog", new DateTime(2020,7,25));
      Assert.Equal(firstTask, secondTask);
    }
    [Fact]
    public void Test_Save_SavesTaskToDatabase()
    {
      Task newTask = new Task ("Walk the dog", new DateTime(2020,7,25));
      newTask.Save();
      List<Task> manualTaskList = new List<Task> {newTask};
      List<Task> methodTaskList = Task.GetAll();
      Assert.Equal(manualTaskList, methodTaskList);
    }
    [Fact]
    public void Test_GetCategories_ReturnsAllCategoriesByTask()
    {
      Task newTask = new Task ("Walk the dog", new DateTime(2020,7,25));
      newTask.Save();
      Category newCategory = new Category ("Pet Chores");
      newCategory.Save();
      newTask.AddCategory(newCategory);
      List<Category> manualCategoryList = new List<Category> {newCategory};
      List<Category> methodCategoryList = newTask.GetCategories();
      Assert.Equal(manualCategoryList, methodCategoryList);
    }
    [Fact]
    public void Test_Find_ReturnsTaskById()
    {
      Task newTask = new Task ("Walk the dog", new DateTime(2020,7,25));
      newTask.Save();
      Task foundTask = Task.Find(newTask.GetId());
      Assert.Equal(newTask, foundTask);
    }
    [Fact]
    public void Test_FindByDate_FindsTaskByDueDate()
    {
      Task newTask = new Task ("Walk the dog", new DateTime(2020,7,25));
      newTask.Save();
      Task foundTask = Task.FindByDate(new DateTime(2020,7,25));
      Assert.Equal(newTask, foundTask);
    }
    [Fact]
    public void Test_Update_UpdatesTaskEntry()
    {
      Task newTask = new Task ("Walk the dog", new DateTime(2020,7,25));
      newTask.Save();
      newTask.SetDueDate(new DateTime(2020,7,26));
      newTask.Update();
      Task foundTask = Task.Find(newTask.GetId());
      Task updatedTask = new Task ("Walk the dog", new DateTime(2020,7,26));
      Assert.Equal(newTask.GetDueDate(), foundTask.GetDueDate());
      Assert.Equal(newTask.GetDueDate(), updatedTask.GetDueDate());
    }
    [Fact]
    public void Test_DeleteOne_DeletesOneTask()
    {
      Task firstTask = new Task ("Walk the dog", new DateTime(2020,7,25));
      firstTask.Save();
      Task secondTask = new Task ("Feed the cat", new DateTime(2020,7,25));
      secondTask.Save();
      firstTask.DeleteOne();
      List<Task> reducedTaskList = new List<Task> {secondTask};
      List<Task> deletedTaskList = Task.GetAll();
      Assert.Equal(reducedTaskList, deletedTaskList);
    }
    public void Dispose()
    {
      Task.DeleteAll();
      Category.DeleteAll();
    }
  }
}
