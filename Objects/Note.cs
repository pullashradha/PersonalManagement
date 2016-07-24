using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace PersonalManagement
{
  public class Note
  {
    private int _id;
    private string _title;
    private string _content;
    public Note (string Title, string Content, int Id = 0)
    {
      _id = Id;
      _title = Title;
      _content = Content;
    }
    public int GetId()
    {
      return _id;
    }
    public string GetTitle()
    {
      return _title;
    }
    public void SetTitle (string newTitle)
    {
      _title = newTitle;
    }
    public string GetContent()
    {
      return _content;
    }
    public void SetContent (string newContent)
    {
      _content = newContent;
    }
    public override bool Equals (System.Object otherNote)
    {
      if (otherNote is Note)
      {
        Note newNote = (Note) otherNote;
        bool idEquality = (this.GetId() == newNote.GetId());
        bool titleEquality = (this.GetTitle() == newNote.GetTitle());
        bool contentEquality = (this.GetContent() == newNote.GetContent());
        return (idEquality && titleEquality && contentEquality);
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
      SqlCommand cmd = new SqlCommand ("INSERT INTO notes (title, content) OUTPUT INSERTED.id VALUES (@NoteTitle, @NoteContent);", conn);
      SqlParameter titleParameter = new SqlParameter();
      titleParameter.ParameterName = "@NoteTitle";
      titleParameter.Value = this.GetTitle();
      SqlParameter contentParameter = new SqlParameter();
      contentParameter.ParameterName = "@NoteContent";
      contentParameter.Value = this.GetContent();
      cmd.Parameters.Add(titleParameter);
      cmd.Parameters.Add(contentParameter);
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
    public static List<Note> GetAll()
    {
      List<Note> allNotes = new List<Note> {};
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlDataReader rdr;
      SqlCommand cmd = new SqlCommand ("SELECT * FROM notes;", conn);
      rdr = cmd.ExecuteReader();
      while (rdr.Read())
      {
        int noteId = rdr.GetInt32(0);
        string noteTitle = rdr.GetString(1);
        string noteContent = rdr.GetString(2);
        Note newNote = new Note (noteTitle, noteContent, noteId);
        allNotes.Add(newNote);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return allNotes;
    }
    public void AddTag (Tag newTag)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlDataReader rdr;
      SqlCommand cmd = new SqlCommand ("INSERT INTO notes_tags (note_id, tag_id) VALUES (@NoteId, @TagId);", conn);
      SqlParameter noteIdParameter = new SqlParameter();
      noteIdParameter.ParameterName = "@NoteId";
      noteIdParameter.Value = this.GetId();
      SqlParameter tagIdParameter = new SqlParameter();
      tagIdParameter.ParameterName = "@TagId";
      tagIdParameter.Value = newTag.GetId();
      cmd.Parameters.Add(noteIdParameter);
      cmd.Parameters.Add(tagIdParameter);
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
    public List<Tag> GetTags()
    {
      List<Tag> allTags = new List<Tag> {};
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlDataReader rdr;
      SqlCommand cmd = new SqlCommand ("SELECT tags.* FROM tags JOIN notes_tags ON (tags.id = notes_tags.tag_id) JOIN notes ON (notes.id = notes_tags.note_id) WHERE notes.id = @NoteId;", conn);
      SqlParameter idParameter = new SqlParameter();
      idParameter.ParameterName = "@NoteId";
      idParameter.Value = this.GetId();
      cmd.Parameters.Add(idParameter);
      rdr = cmd.ExecuteReader();
      while (rdr.Read())
      {
        int tagId = rdr.GetInt32(0);
        string tagName = rdr.GetString(1);
        Tag newTag = new Tag (tagName, tagId);
        allTags.Add(newTag);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return allTags;
    }
    public static Note Find (int queryId)
    {
      List<Note> foundNotes = new List<Note> {};
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlDataReader rdr;
      SqlCommand cmd = new SqlCommand ("SELECT * FROM notes WHERE id = @QueryId;", conn);
      SqlParameter idParameter = new SqlParameter();
      idParameter.ParameterName = "@QueryId";
      idParameter.Value = queryId;
      cmd.Parameters.Add(idParameter);
      rdr = cmd.ExecuteReader();
      while (rdr.Read())
      {
        int noteId = rdr.GetInt32(0);
        string noteTitle = rdr.GetString(1);
        string noteContent = rdr.GetString(2);
        Note foundNote = new Note (noteTitle, noteContent, noteId);
        foundNotes.Add(foundNote);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return foundNotes[0];
    }
    public void Update ()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand ("UPDATE notes SET title = @NewNoteTitle, content = @NewNoteContent WHERE id = @NoteId;", conn);
      SqlParameter newTitleParameter = new SqlParameter();
      newTitleParameter.ParameterName = "@NewNoteTitle";
      newTitleParameter.Value = this.GetTitle();
      SqlParameter newContentParameter = new SqlParameter();
      newContentParameter.ParameterName = "@NewNoteContent";
      newContentParameter.Value = this.GetContent();
      SqlParameter idParameter = new SqlParameter();
      idParameter.ParameterName = "@NoteId";
      idParameter.Value = this.GetId();
      cmd.Parameters.Add(newTitleParameter);
      cmd.Parameters.Add(newContentParameter);
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
      SqlCommand cmd = new SqlCommand ("DELETE FROM notes WHERE id = @NoteId;", conn);
      SqlParameter idParameter = new SqlParameter();
      idParameter.ParameterName = "@NoteId";
      idParameter.Value = this.GetId();
      cmd.Parameters.Add(idParameter);
      cmd.ExecuteNonQuery();
    }
    public static void DeleteAll()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand ("DELETE FROM notes;", conn);
      cmd.ExecuteNonQuery();
    }
  }
}
