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
      Task firstTask = new Task ("Walk the dog");
      Task secondTask = new Task ("Walk the dog");
      Assert.Equal(firstTask, secondTask);
    }
    [Fact]
    public void Test_Save_SavesTaskToDatabase()
    {
      Task newTask = new Task ("Walk the dog");
      newTask.Save();
      List<Task> manualTaskList = new List<Task> {newTask};
      List<Task> methodTaskList = Task.GetAll();
      Assert.Equal(manualTaskList, methodTaskList);
    }
    [Fact]
    public void Test_Find_ReturnsTaskById()
    {
      Task newTask = new Task ("Walk the dog");
      newTask.Save();
      Task foundTask = Task.Find(newTask.GetId());
      Assert.Equal(newTask, foundTask);
    }
    [Fact]
    public void Test_Update_UpdatesTaskEntry()
    {
      Task newTask = new Task ("Walk the dog");
      newTask.Save();
      newTask.SetName("Walk the dog in the park");
      newTask.Update();
      Task foundTask = Task.Find(newTask.GetId());
      Task updatedTask = new Task ("Walk the dog in the park");
      Assert.Equal(newTask.GetName(), foundTask.GetName());
      Assert.Equal(newTask.GetName(), updatedTask.GetName());
    }
    [Fact]
    public void Test_DeleteOne_DeletesOneTask()
    {
      Task firstTask = new Task ("Walk the dog");
      firstTask.Save();
      Task secondTask = new Task ("Feed the cat");
      secondTask.Save();
      firstTask.DeleteOne();
      List<Task> reducedTaskList = new List<Task> {secondTask};
      List<Task> deletedTaskList = Task.GetAll();
      Assert.Equal(reducedTaskList, deletedTaskList);
    }
    public void Dispose()
    {
      Task.DeleteAll();
    }
  }
}
