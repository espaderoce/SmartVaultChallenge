using Dapper;
using Microsoft.Extensions.Configuration;
using SmartVault.Program.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Diagnostics;
using System.IO;

namespace SmartVault.Program
{
    partial class Program
    {
        static SQLiteConnection? connection;

        static void Main(string[] args)
        {
            SetConnection();
            //Print the Size of all the file.
            GetAllFileSizes();
            //Get and write every third file of one account.
            WriteEveryThirdFileToFile();
        }
        public static void SetConnection()
        {
            var configuration = new ConfigurationBuilder()
                                  .SetBasePath(Directory.GetCurrentDirectory())
                                  .AddJsonFile("appsettings.json").Build();


            connection = new SQLiteConnection(string.Format(configuration?["ConnectionStrings:DefaultConnection"] ?? "", configuration?["DatabaseFileName"]));
        }

        private static void GetAllFileSizes()
        {
            // TODO: Implement functionality

            connection.Open();
            double totalSize = 0;
            string query = "SELECT Id, FilePath FROM Document";
            IEnumerable<Document> documents = connection.Query<Document>(query);

            long fileSizeInBytes;
            foreach (var document in documents)
            {
                FileInfo fileInfo = new FileInfo(document.FilePath);
                if (fileInfo.Exists)
                {
                    fileSizeInBytes = fileInfo.Length;
                    totalSize += fileSizeInBytes / 1024.0;
                }
            }
            connection.Close();
            Console.WriteLine(totalSize + ": Kbs");
        }

        private static void WriteEveryThirdFileToFile()
        {
            // TODO: Implement functionality

            connection.Open();
            string destinationFilePath = $"{Directory.GetCurrentDirectory()}/newFile.txt";
            string query = "SELECT Account.Id,document.Id, Document.FilePath\r\nFROM Account INNER JOIN Document on Account.Id = Document.AccountId\r\nwhere (Document.Id % 3 = 0)\r\nGROUP BY Account.Id, Document.FilePath";
            IEnumerable<Document> documents = connection.Query<Document>(query);

            // Process the retrieved rows
            foreach (var document in documents)
            {
                string content = File.ReadAllText(document.FilePath);
                if (!File.Exists(destinationFilePath))
                {

                    File.WriteAllText(destinationFilePath, content);
                }
                else
                {
                    using (StreamWriter writer = File.AppendText(destinationFilePath))
                    {
                        writer.WriteLine(content);
                    }
                }

            }

            connection.Close();

        }
    }
}