using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace PersonalManagement
{
  public class Account
  {
    private int _id;
    private string _firstName;
    private string _lastName;
    private string _email;
    private string _phoneNumber;
    private string _username;
    private string _password;
    public Account (string FirstName, string LastName, string Email, string PhoneNumber, string Username, string Password, int Id = 0)
    {
      _id = Id;
      _firstName = FirstName;
      _lastName = LastName;
      _email = Email;
      _phoneNumber = PhoneNumber;
      _username = Username;
      _password = Password;
    }
    public int GetId()
    {
      return _id;
    }
    public string GetFirstName()
    {
      return _firstName;
    }
    public void SetFirstName (string newFirstName)
    {
      _firstName = newFirstName;
    }
    public string GetLastName()
    {
      return _lastName;
    }
    public void SetLastName (string newLastName)
    {
      _lastName = newLastName;
    }
    public string GetEmail()
    {
      return _email;
    }
    public void SetEmail (string newEmail)
    {
      _email = newEmail;
    }
    public string GetPhoneNumber()
    {
      return _phoneNumber;
    }
    public void SetPhoneNumber (string newPhoneNumber)
    {
      _phoneNumber = newPhoneNumber;
    }
    public string GetUsername()
    {
      return _username;
    }
    public void SetUsername (string newUsername)
    {
      _username = newUsername;
    }
    public string GetPassword()
    {
      return _password;
    }
    public void SetPassword (string newPassword)
    {
      _password = newPassword;
    }
    public override bool Equals (System.Object otherAccount)
    {
      if (otherAccount is Account)
      {
        Account newAccount = (Account) otherAccount;
        bool idEquality = (this.GetId() == newAccount.GetId());
        bool firstNameEquality = (this.GetFirstName() == newAccount.GetFirstName());
        bool lastNameEquality = (this.GetLastName() == newAccount.GetLastName());
        bool emailEquality = (this.GetEmail() == newAccount.GetEmail());
        bool phoneNumberEquality = (this.GetPhoneNumber() == newAccount.GetPhoneNumber());
        bool usernameEquality = (this.GetUsername() == newAccount.GetUsername());
        bool passwordEquality = (this.GetPassword() == newAccount.GetPassword());
        return (idEquality && firstNameEquality && lastNameEquality && emailEquality && phoneNumberEquality && usernameEquality && passwordEquality);
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
      SqlCommand cmd = new SqlCommand ("INSERT INTO accounts (first_name, last_name, email, phone_number, username, password) OUTPUT INSERTED.id VALUES (@AccountFirstName, @AccountLastName, @AccountEmail, @AccountPhoneNumber, @AccountUsername, @AccountPassword);", conn);
      SqlParameter firstNameParameter = new SqlParameter();
      firstNameParameter.ParameterName = "@AccountFirstName";
      firstNameParameter.Value = this.GetFirstName();
      SqlParameter lastNameParameter = new SqlParameter();
      lastNameParameter.ParameterName = "@AccountLastName";
      lastNameParameter.Value = this.GetLastName();
      SqlParameter emailParameter = new SqlParameter();
      emailParameter.ParameterName = "@AccountEmail";
      emailParameter.Value = this.GetEmail();
      SqlParameter phoneNumberParameter = new SqlParameter();
      phoneNumberParameter.ParameterName = "@AccountPhoneNumber";
      phoneNumberParameter.Value = this.GetPhoneNumber();
      SqlParameter usernameParameter = new SqlParameter();
      usernameParameter.ParameterName = "@AccountUsername";
      usernameParameter.Value = this.GetUsername();
      SqlParameter passwordParameter = new SqlParameter();
      passwordParameter.ParameterName = "@AccountPassword";
      passwordParameter.Value = this.GetPassword();
      cmd.Parameters.Add(firstNameParameter);
      cmd.Parameters.Add(lastNameParameter);
      cmd.Parameters.Add(emailParameter);
      cmd.Parameters.Add(phoneNumberParameter);
      cmd.Parameters.Add(usernameParameter);
      cmd.Parameters.Add(passwordParameter);
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
    public static List<Account> GetAll()
    {
      List<Account> allAccounts = new List<Account> {};
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlDataReader rdr;
      SqlCommand cmd = new SqlCommand ("SELECT * FROM accounts;", conn);
      rdr = cmd.ExecuteReader();
      while (rdr.Read())
      {
        int accountId = rdr.GetInt32(0);
        string accountFirstName = rdr.GetString(1);
        string accountLastName = rdr.GetString(2);
        string accountEmail = rdr.GetString(3);
        string accountPhoneNumber = rdr.GetString(4);
        string accountUsername = rdr.GetString(5);
        string accountPassword = rdr.GetString(6);
        Account newAccount = new Account (accountFirstName, accountLastName, accountEmail, accountPhoneNumber, accountUsername, accountPassword, accountId);
        allAccounts.Add(newAccount);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return allAccounts;
    }
    public static Account Find (int queryId)
    {
      List<Account> foundAccounts = new List<Account> {};
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlDataReader rdr;
      SqlCommand cmd = new SqlCommand ("SELECT * FROM accounts WHERE id = @QueryId;", conn);
      SqlParameter idParameter = new SqlParameter();
      idParameter.ParameterName = "@QueryId";
      idParameter.Value = queryId;
      cmd.Parameters.Add(idParameter);
      rdr = cmd.ExecuteReader();
      while (rdr.Read())
      {
        int accountId = rdr.GetInt32(0);
        string accountFirstName = rdr.GetString(1);
        string accountLastName = rdr.GetString(2);
        string accountEmail = rdr.GetString(3);
        string accountPhoneNumber = rdr.GetString(4);
        string accountUsername = rdr.GetString(5);
        string accountPassword = rdr.GetString(6);
        Account foundAccount = new Account (accountFirstName, accountLastName, accountEmail, accountPhoneNumber, accountUsername, accountPassword, accountId);
        foundAccounts.Add(foundAccount);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return foundAccounts[0];
    }
    public void Update()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand ("UPDATE accounts SET first_name = @NewFirstName, last_name = @NewLastName, email = @NewEmail, phone_number = @NewPhoneNumber, username = @NewUsername, password = @NewPassword WHERE id = @AccountId;", conn);
      SqlParameter newFirstNameParameter = new SqlParameter();
      newFirstNameParameter.ParameterName = "@NewFirstName";
      newFirstNameParameter.Value = this.GetFirstName();
      SqlParameter newLastNameParameter = new SqlParameter();
      newLastNameParameter.ParameterName = "@NewLastName";
      newLastNameParameter.Value = this.GetLastName();
      SqlParameter newEmailParameter = new SqlParameter();
      newEmailParameter.ParameterName = "@NewEmail";
      newEmailParameter.Value = this.GetEmail();
      SqlParameter newPhoneNumberParameter = new SqlParameter();
      newPhoneNumberParameter.ParameterName = "@NewPhoneNumber";
      newPhoneNumberParameter.Value = this.GetPhoneNumber();
      SqlParameter newUsernameParameter = new SqlParameter();
      newUsernameParameter.ParameterName = "@NewUsername";
      newUsernameParameter.Value = this.GetUsername();
      SqlParameter newPasswordParameter = new SqlParameter();
      newPasswordParameter.ParameterName = "@NewPassword";
      newPasswordParameter.Value = this.GetPassword();
      SqlParameter idParameter = new SqlParameter();
      idParameter.ParameterName = "@AccountId";
      idParameter.Value = this.GetId();
      cmd.Parameters.Add(newFirstNameParameter);
      cmd.Parameters.Add(newLastNameParameter);
      cmd.Parameters.Add(newEmailParameter);
      cmd.Parameters.Add(newPhoneNumberParameter);
      cmd.Parameters.Add(newUsernameParameter);
      cmd.Parameters.Add(newPasswordParameter);
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
      SqlCommand cmd = new SqlCommand ("DELETE FROM accounts WHERE id = @AccountId;", conn);
      SqlParameter idParameter = new SqlParameter();
      idParameter.ParameterName = "@AccountId";
      idParameter.Value = this.GetId();
      cmd.Parameters.Add(idParameter);
      cmd.ExecuteNonQuery();
    }
    public static void DeleteAll()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand ("DELETE FROM accounts;", conn);
      cmd.ExecuteNonQuery();
    }
  }
}
