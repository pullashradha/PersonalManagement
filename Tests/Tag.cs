using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Xunit;

namespace PersonalManagement
{
  public class TagTest : IDisposable
  {
    public TagTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=personal_management_test;Integrated Security=SSPI;";
    }
    [Fact]
    public void Test_Empty_DatabaseEmptyAtFirst()
    {
      int dBValues = Tag.GetAll().Count;
      Assert.Equal(0, dBValues);
    }
    [Fact]
    public void Test_Equals_EntriesMatch()
    {
      Tag firstTag = new Tag ("~Questions");
      Tag secondTag = new Tag ("~Questions");
      Assert.Equal(firstTag, secondTag);
    }
    [Fact]
    public void Test_Save_SavesTagToDatabase()
    {
      Tag newTag = new Tag ("~Questions");
      newTag.Save();
      List<Tag> manualTagList = new List<Tag> {newTag};
      List<Tag> methodTagList = Tag.GetAll();
      Assert.Equal(manualTagList, methodTagList);
    }
    [Fact]
    public void Test_Find_ReturnsTagById()
    {
      Tag newTag = new Tag ("~Questions");
      newTag.Save();
      Tag foundTag = Tag.Find(newTag.GetId());
      Assert.Equal(newTag, foundTag);
    }
    [Fact]
    public void Test_Update_UpdatesTagEntry()
    {
      Tag newTag = new Tag ("~QUESTIONS");
      newTag.Save();
      newTag.SetName("~Questions");
      newTag.Update();
      Tag foundTag = Tag.Find(newTag.GetId());
      Tag updatedTag = new Tag ("~Questions");
      Assert.Equal(newTag.GetName(), foundTag.GetName());
      Assert.Equal(newTag.GetName(), updatedTag.GetName());
    }
    [Fact]
    public void Test_DeleteOne_DeletesOneTag()
    {
      Tag firstTag = new Tag ("~Questions");
      firstTag.Save();
      Tag secondTag = new Tag ("~Science");
      secondTag.Save();
      firstTag.DeleteOne();
      List<Tag> reducedTagList = new List<Tag> {secondTag};
      List<Tag> deletedTagList = Tag.GetAll();
      Assert.Equal(reducedTagList, deletedTagList);
    }
    public void Dispose()
    {
      Tag.DeleteAll();
    }
  }
}
