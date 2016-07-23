using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Xunit;

namespace PersonalManagement
{
  public class ContactTest : IDisposable
  {
    public ContactTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=personal_management_test;Integrated Security=SSPI;";
    }
    public void Dispose()
    {
      // Contact.DeleteAll();
    }
  }
}
