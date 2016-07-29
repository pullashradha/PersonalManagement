using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Xunit;

namespace PersonalManagement
{
  public class AccountTest : IDisposable
  {
    public AccountTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=personal_management_test;Integrated Security=SSPI;";
    }
    [Fact]
    public void Test_Empty_DatabaseEmptyAtFirst()
    {
      int dBValues = Account.GetAll().Count;
      Assert.Equal(0, dBValues);
    }
    [Fact]
    public void Test_Equals_EntriesMatch()
    {
      Account firstAccount = new Account ("Rubab", "Shah", "shah@gmail.com", "503-555-5555", "rshah", "colors");
      Account secondAccount = new Account ("Rubab", "Shah", "shah@gmail.com", "503-555-5555", "rshah", "colors");
      Assert.Equal(firstAccount, secondAccount);
    }
    [Fact]
    public void Test_Save_SavesAccountToDatabase()
    {
      Account newAccount = new Account ("Rubab", "Shah", "shah@gmail.com", "503-555-5555", "rshah", "colors");
      newAccount.Save();
      List<Account> manualAccountList = new List<Account> {newAccount};
      List<Account> methodAccountList = Account.GetAll();
      Assert.Equal(manualAccountList, methodAccountList);
    }
    [Fact]
    public void Test_Find_ReturnsAccountById()
    {
      Account newAccount = new Account ("Rubab", "Shah", "shah@gmail.com", "503-555-5555", "rshah", "colors");
      newAccount.Save();
      Account foundAccount = Account.Find(newAccount.GetId());
      Assert.Equal(newAccount, foundAccount);
    }
    [Fact]
    public void Test_Update_UpdatesAccountEntry()
    {
      Account newAccount = new Account ("Rubab", "Shah", "shah@gmail.com", "503-555-5555", "rshah", "colors");
      newAccount.Save();
      newAccount.SetFirstName("Rubab ^^");
      newAccount.Update();
      Account foundAccount = Account.Find(newAccount.GetId());
      Account updatedAccount = new Account ("Rubab ^^", "Shah", "shah@gmail.com", "503-555-5555", "rshah", "colors");
      Assert.Equal(newAccount.GetFirstName(), foundAccount.GetFirstName());
      Assert.Equal(newAccount.GetFirstName(), updatedAccount.GetFirstName());
    }
    [Fact]
    public void Test_DeleteOne_DeletesOneAccount()
    {
      Account firstAccount = new Account ("Rubab", "Shah", "shah@gmail.com", "503-555-5555", "rshah", "colors");
      firstAccount.Save();
      Account secondAccount = new Account ("Maya", "Jones", "jones@gmail.com", "971-555-5555", "mjones", "animals");
      secondAccount.Save();
      firstAccount.DeleteOne();
      List<Account> reducedAccountList = new List<Account> {secondAccount};
      List<Account> deletedAccountList = Account.GetAll();
      Assert.Equal(reducedAccountList, deletedAccountList);
    }
    public void Dispose()
    {
      Account.DeleteAll();
    }
  }
}
