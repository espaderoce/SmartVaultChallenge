using Dapper;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using SmartVault.Library;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Xml.Serialization;

namespace SmartVault.DataGeneration
{
    partial class Program
    {
        static void Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json").Build();

            SQLiteConnection.CreateFile(configuration["DatabaseFileName"]);
            File.WriteAllText("TestDoc.txt", $"This is my test document{Environment.NewLine}This is my test document{Environment.NewLine}This is my test document{Environment.NewLine}This is my test document{Environment.NewLine}This is my test document{Environment.NewLine}This is my test document{Environment.NewLine}This is my test document{Environment.NewLine}This is my test document{Environment.NewLine}This is my test document{Environment.NewLine}This is my test document{Environment.NewLine}This is my test document{Environment.NewLine}This is my test document{Environment.NewLine}This is my test document{Environment.NewLine}This is my test document{Environment.NewLine}This is my test document{Environment.NewLine}This is my test document{Environment.NewLine}This is my test document{Environment.NewLine}This is my test document{Environment.NewLine}This is my test document{Environment.NewLine}This is my test document{Environment.NewLine}This is my test document{Environment.NewLine}This is my test document{Environment.NewLine}This is my test document{Environment.NewLine}This is my test document{Environment.NewLine}This is my test document{Environment.NewLine}This is my test document{Environment.NewLine}This is my test document{Environment.NewLine}This is my test document{Environment.NewLine}This is my test document{Environment.NewLine}This is my test document{Environment.NewLine}This is my test document{Environment.NewLine}This is my test document{Environment.NewLine}This is my test document{Environment.NewLine}This is my test document{Environment.NewLine}This is my test document{Environment.NewLine}This is my test document{Environment.NewLine}This is my test document{Environment.NewLine}This is my test document{Environment.NewLine}This is my test document{Environment.NewLine}This is my test document{Environment.NewLine}This is my test document{Environment.NewLine}This is my test document{Environment.NewLine}This is my test document{Environment.NewLine}This is my test document{Environment.NewLine}This is my test document{Environment.NewLine}This is my test document{Environment.NewLine}This is my test document{Environment.NewLine}This is my test document{Environment.NewLine}This is my test document{Environment.NewLine}This is my test document{Environment.NewLine}This is my test document{Environment.NewLine}This is my test document{Environment.NewLine}This is my test document{Environment.NewLine}This is my test document{Environment.NewLine}This is my test document{Environment.NewLine}This is my test document{Environment.NewLine}This is my test document{Environment.NewLine}This is my test document{Environment.NewLine}This is my test document{Environment.NewLine}This is my test document{Environment.NewLine}This is my test document{Environment.NewLine}This is my test document{Environment.NewLine}This is my test document{Environment.NewLine}This is my test document{Environment.NewLine}This is my test document{Environment.NewLine}This is my test document{Environment.NewLine}This is my test document{Environment.NewLine}This is my test document{Environment.NewLine}This is my test document{Environment.NewLine}This is my test document{Environment.NewLine}This is my test document{Environment.NewLine}This is my test document{Environment.NewLine}This is my test document{Environment.NewLine}This is my test document{Environment.NewLine}This is my test document{Environment.NewLine}This is my test document{Environment.NewLine}This is my test document{Environment.NewLine}This is my test document{Environment.NewLine}This is my test document{Environment.NewLine}This is my test document{Environment.NewLine}This is my test document{Environment.NewLine}This is my test document{Environment.NewLine}This is my test document{Environment.NewLine}This is my test document{Environment.NewLine}This is my test document{Environment.NewLine}This is my test document{Environment.NewLine}This is my test document{Environment.NewLine}This is my test document{Environment.NewLine}This is my test document{Environment.NewLine}This is my test document{Environment.NewLine}This is my test document{Environment.NewLine}This is my test document{Environment.NewLine}This is my test document{Environment.NewLine}This is my test document{Environment.NewLine}This is my test document{Environment.NewLine}This is my test document{Environment.NewLine}This is my test document{Environment.NewLine}This is my test document{Environment.NewLine}This is my test document{Environment.NewLine}This is my test document{Environment.NewLine}This is my test document{Environment.NewLine}");

            using (var connection = new SQLiteConnection(string.Format(configuration?["ConnectionStrings:DefaultConnection"] ?? "", configuration?["DatabaseFileName"])))
            {
                connection.Open();
                using (SQLiteTransaction transaction = connection.BeginTransaction())
                {

                    using (SQLiteCommand command1 = connection.CreateCommand())
                    using (SQLiteCommand command2 = connection.CreateCommand())
                    using (SQLiteCommand command3 = connection.CreateCommand())
                    {
                        //command.Transaction = transaction;
                        var files = Directory.GetFiles(@"..\..\..\..\BusinessObjectSchema");
                        for (int i = 0; i <= 2; i++)
                        {
                            var serializer = new XmlSerializer(typeof(BusinessObject));
                            var businessObject = serializer.Deserialize(new StreamReader(files[i])) as BusinessObject;
                            connection.Execute(businessObject?.Script);

                        }
                        var documentNumber = 0;
                        //var randomDayIterator = (IEnumerator<DateTime>)(new List<DateTime>());
                        var documentPath = new FileInfo("TestDoc.txt").FullName;
                        var documentLength = File.ReadAllText(documentPath).Length;
                        //COMMAD SECTION
                        command1.CommandText = "INSERT INTO User (Id, FirstName, LastName, DateOfBirth, AccountId, Username, Password) VALUES(@param1,@param2,@param3,@param4,@param5,@param6,'e10adc3949ba59abbe56e057f20f883e')";
                        command1.Parameters.Add("@param1", DbType.Int32);
                        command1.Parameters.Add("@param2", DbType.String);
                        command1.Parameters.Add("@param3", DbType.String);
                        command1.Parameters.Add("@param4", DbType.String);
                        command1.Parameters.Add("@param5", DbType.Int32);
                        command1.Parameters.Add("@param6", DbType.String);
                        command1.Prepare();
                        //COMMAND 2
                        command2.CommandText = "INSERT INTO Account (Id, Name) VALUES(@param1,@param2)";
                        command2.Parameters.Add("@param1", DbType.Int32);
                        command2.Parameters.Add("@param2", DbType.String);
                        command2.Prepare();

                        //COMMAND 3
                        command3.CommandText = "INSERT INTO Document (Id, Name, FilePath, Length, AccountId) VALUES(@param1,@param2,@param3,@param4,@param5)";
                        command3.Parameters.Add("@param1", DbType.Int32);
                        command3.Parameters.Add("@param2", DbType.String);
                        command3.Parameters.Add("@param3", DbType.String);
                        command3.Parameters.Add("@param4", DbType.Int32);
                        command3.Parameters.Add("@param5", DbType.Int32);
                        command3.Prepare();
                        for (int i = 0; i < 100; i++)
                        {
                            var randomDayIterator = RandomDay().GetEnumerator();
                            randomDayIterator.MoveNext();

                            command1.Parameters["@param1"].Value = i;
                            command1.Parameters["@param2"].Value = $"FName{i}";
                            command1.Parameters["@param3"].Value = $"LName{i}";
                            command1.Parameters["@param4"].Value = randomDayIterator.Current.ToString("yyyy-MM-dd");
                            command1.Parameters["@param5"].Value = i;
                            command1.Parameters["@param6"].Value = $"UserName-{i}";

                            command2.Parameters["@param1"].Value = i;
                            command2.Parameters["@param2"].Value = $"Account{i}";

                            command1.ExecuteNonQuery();
                            command2.ExecuteNonQuery();


                            for (int d = 0; d < 10000; d++, documentNumber++)
                            {
                                command3.Parameters["@param1"].Value = documentNumber;
                                command3.Parameters["@param2"].Value = $"Document{i}-{d}.txt";
                                command3.Parameters["@param3"].Value = documentPath;
                                command3.Parameters["@param4"].Value = documentLength;
                                command3.Parameters["@param5"].Value = i;
                                command3.ExecuteNonQuery();
                            }
                        }
                       
                        
                    }
                    transaction.Commit();
                }


                var accountData = connection.Query("SELECT COUNT(*) FROM Account;");
                Console.WriteLine($"AccountCount: {JsonConvert.SerializeObject(accountData)}");
                var documentData = connection.Query("SELECT COUNT(*) FROM Document;");
                Console.WriteLine($"DocumentCount: {JsonConvert.SerializeObject(documentData)}");
                var userData = connection.Query("SELECT COUNT(*) FROM User;");
                Console.WriteLine($"UserCount: {JsonConvert.SerializeObject(userData)}");
                connection.Close();
            }
        }

        static IEnumerable<DateTime> RandomDay()
        {
            DateTime start = new DateTime(1985, 1, 1);
            Random gen = new Random();
            int range = (DateTime.Today - start).Days;
            while (true)
                yield return start.AddDays(gen.Next(range));
        }
    }
}