// See https://aka.ms/new-console-template for more information
using OfficeOpenXml;
using System.Net;

Console.WriteLine("Please select the data to download:");

Console.WriteLine("1: Covid Summary");
Console.WriteLine("2: UAE Covid Hisotry");


HttpClient client = new HttpClient();

client.GetAsync("").Wait();
client.Dispose();

