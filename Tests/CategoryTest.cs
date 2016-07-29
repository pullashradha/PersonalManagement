using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Xunit;

namespace PersonalManagement
{
  public class CategoryTest : IDisposable
  {
    public CategoryTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=personal_management_test;Integrated Security=SSPI;";
    }
    [Fact]
    public void Test_Empty_DatabaseEmptyAtFirst()
    {
      int dBValues = Category.GetAll().Count;
      Assert.Equal(0, dBValues);
    }
    [Fact]
    public void Test_Equals_EntriesMatch()
    {
      Category firstCategory = new Category ("Pet Chores");
      Category secondCategory = new Category ("Pet Chores");
      Assert.Equal(firstCategory, secondCategory);
    }
    [Fact]
    public void Test_Save_SavesCategoryToDatabase()
    {
      Category newCategory = new Category ("Pet Chores");
      newCategory.Save();
      List<Category> manualCategoryList = new List<Category> {newCategory};
      List<Category> methodCategoryList = Category.GetAll();
      Assert.Equal(manualCategoryList, methodCategoryList);
    }
    [Fact]
    public void Test_GetTasks_ReturnsTasksByCategory()
    {
      Category newCategory = new Category ("Pet Chores");
      newCategory.Save();
      Task newTask = new Task ("Walk the dog", new DateTime(2020,7,25));
      newTask.Save();
      newCategory.AddTask(newTask);
      List<Task> manualTaskList = new List<Task> {newTask};
      List<Task> methodTaskList = newCategory.GetTasks();
      Assert.Equal(manualTaskList, methodTaskList);
    }
    [Fact]
    public void Test_Find_ReturnsCategoryById()
    {
      Category newCategory = new Category ("Pet Chores");
      newCategory.Save();
      Category foundCategory = Category.Find(newCategory.GetId());
      Assert.Equal(newCategory, foundCategory);
    }
    // [Fact]
    // public void Test_FindByName_ReturnsCategoriesByName()
    // {
    //   Category firstCategory = new Category ("Pet Chores");
    //   firstCategory.Save();
    //   Category secondCategory = new Category ("Travel Pet Insurance");
    //   secondCategory.Save();
    //   Category failedCategory = new Category ("Hooray");
    //   failedCategory.Save();
    //   List<Category> manualFoundCategories = new List<Category> {firstCategory, secondCategory};
    //   List<Category> methodFoundCategories = Category.FindByName("Pet");
    //   Console.WriteLine(methodFoundCategories[2].GetName());
    //   List<Category> failList = new List<Category> {failedCategory};
    //   Assert.Equal(failList, methodFoundCategories);
    // }
    [Fact]
    public void Test_Update_UpdatesCategoryEntry()
    {
      Category newCategory = new Category ("Pet Chores");
      newCategory.Save();
      newCategory.SetName("Pet Chores - Meghan");
      newCategory.Update();
      Category foundCategory = Category.Find(newCategory.GetId());
      Category updatedCategory = new Category ("Pet Chores - Meghan");
      Assert.Equal(newCategory.GetName(), foundCategory.GetName());
      Assert.Equal(newCategory.GetName(), updatedCategory.GetName());
    }
    [Fact]
    public void Test_DeleteOne_DeletesOneCategory()
    {
      Category firstCategory = new Category ("Pet Chores");
      firstCategory.Save();
      Category secondCategory = new Category ("Household Chores");
      secondCategory.Save();
      firstCategory.DeleteOne();
      List<Category> reducedCategoryList = new List<Category> {secondCategory};
      List<Category> deletedCategoryList = Category.GetAll();
      Assert.Equal(reducedCategoryList, deletedCategoryList);
    }
    public void Dispose()
    {
      Category.DeleteAll();
      Task.DeleteAll();
    }
  }
}
