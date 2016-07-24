using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Xunit;

namespace PersonalManagement
{
  public class NoteTest : IDisposable
  {
    public NoteTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=personal_management_test;Integrated Security=SSPI;";
    }
    [Fact]
    public void Test_Empty_DatabaseEmptyAtFirst()
    {
      int dBValues = Note.GetAll().Count;
      Assert.Equal(0, dBValues);
    }
    [Fact]
    public void Test_Equals_EntriesMatch()
    {
      Note firstNote = new Note ("Random Questions", "Do caterpillars know that they will turn into butterflies?");
      Note secondNote = new Note ("Random Questions", "Do caterpillars know that they will turn into butterflies?");
      Assert.Equal(firstNote, secondNote);
    }
    [Fact]
    public void Test_Save_SavesNoteToDatabase()
    {
      Note newNote = new Note ("Random Questions", "Do caterpillars know that they will turn into butterflies?");
      newNote.Save();
      List<Note> manualNoteList = new List<Note> {newNote};
      List<Note> methodNoteList = Note.GetAll();
      Assert.Equal(manualNoteList, methodNoteList);
    }
    [Fact]
    public void Test_GetTags_ReturnsAllTagsByNote()
    {
      Note newNote = new Note ("Random Questions", "Do caterpillars know that they will turn into butterflies?");
      newNote.Save();
      Tag firstTag = new Tag ("~Science");
      firstTag.Save();
      Tag secondTag = new Tag ("~Questions");
      secondTag.Save();
      newNote.AddTag(firstTag);
      newNote.AddTag(secondTag);
      List<Tag> manualTagList = new List<Tag> {firstTag, secondTag};
      List<Tag> methodTagList = newNote.GetTags();
      Assert.Equal(manualTagList, methodTagList);
    }
    [Fact]
    public void Test_Find_ReturnsNoteById()
    {
      Note newNote = new Note ("Random Questions", "Do caterpillars know that they will turn into butterflies?");
      newNote.Save();
      Note foundNote = Note.Find(newNote.GetId());
      Assert.Equal(newNote, foundNote);
    }
    [Fact]
    public void Test_Update_UpdatesNoteEntry()
    {
      Note newNote = new Note ("Random Questions", "Do caterpillars know that they will turn into butterflies?");
      newNote.Save();
      newNote.SetTitle("Random Science Questions");
      newNote.Update();
      Note foundNote = Note.Find(newNote.GetId());
      Note updatedNote = new Note ("Random Science Questions", "Do caterpillars know that they will turn into butterflies?");
      Assert.Equal(newNote.GetTitle(), foundNote.GetTitle());
      Assert.Equal(newNote.GetTitle(), updatedNote.GetTitle());
    }
    [Fact]
    public void Test_DeleteOne_DeletesOneNote()
    {
      Note firstNote = new Note ("Random Questions", "Do caterpillars know that they will turn into butterflies?");
      firstNote.Save();
      Note secondNote = new Note ("Birthday Present Ideas", "1. Adopt a dog, 2. Adopt a cat");
      secondNote.Save();
      firstNote.DeleteOne();
      List<Note> reducedNoteList = new List<Note> {secondNote};
      List<Note> deletedNoteList = Note.GetAll();
      Assert.Equal(reducedNoteList, deletedNoteList);
    }
    public void Dispose()
    {
      Note.DeleteAll();
      Tag.DeleteAll();
    }
  }
}
