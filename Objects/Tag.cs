using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace PersonalManagement
{
  public class Tag
  {
    private int _id;
    private string _name;
    public Tag (string Name, int Id = 0)
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
    public override bool Equals (System.Object otherTag)
    {
      if (otherTag is Tag)
      {
        Tag newTag = (Tag) otherTag;
        bool idEquality = (this.GetId() == newTag.GetId());
        bool nameEquality = (this.GetName() == newTag.GetName());
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
      SqlCommand cmd = new SqlCommand ("INSERT INTO tags (name) OUTPUT INSERTED.id VALUES (@TagName);", conn);
      SqlParameter nameParameter = new SqlParameter();
      nameParameter.ParameterName = "@TagName";
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
    public static List<Tag> GetAll()
    {
      List<Tag> allTags = new List<Tag> {};
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlDataReader rdr;
      SqlCommand cmd = new SqlCommand ("SELECT * FROM tags;", conn);
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
    public static Tag Find (int queryId)
    {
      List<Tag> foundTags = new List<Tag> {};
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlDataReader rdr;
      SqlCommand cmd = new SqlCommand ("SELECT * FROM tags WHERE id = @QueryId;", conn);
      SqlParameter idParameter = new SqlParameter();
      idParameter.ParameterName = "@QueryId";
      idParameter.Value = queryId;
      cmd.Parameters.Add(idParameter);
      rdr = cmd.ExecuteReader();
      while (rdr.Read())
      {
        int tagId = rdr.GetInt32(0);
        string tagName = rdr.GetString(1);
        Tag foundTag = new Tag (tagName, tagId);
        foundTags.Add(foundTag);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return foundTags[0];
    }
    public void Update()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand ("UPDATE tags SET name = @NewTagName WHERE id = @TagId;", conn);
      SqlParameter newNameParameter = new SqlParameter();
      newNameParameter.ParameterName = "@NewTagName";
      newNameParameter.Value = this.GetName();
      SqlParameter idParameter = new SqlParameter();
      idParameter.ParameterName = "@TagId";
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
      SqlCommand cmd = new SqlCommand ("DELETE FROM tags WHERE id = @TagId;", conn);
      SqlParameter idParameter = new SqlParameter();
      idParameter.ParameterName = "@TagId";
      idParameter.Value = this.GetId();
      cmd.Parameters.Add(idParameter);
      cmd.ExecuteNonQuery();
    }
    public static void DeleteAll()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand ("DELETE FROM tags;", conn);
      cmd.ExecuteNonQuery();
    }
  }
}
