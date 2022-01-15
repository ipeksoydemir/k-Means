

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FinalProject_k_Means
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Record> records =
                OpenDataFile(
                    @"/Users/ipeksoydemir/RiderProjects/FinalProject_k-Means/FinalProject_k-Means/Final-data.txt");
            
                int k;
                try
                {
                    Console.WriteLine("k değeri belirleyiniz..");
                    k=Convert.ToInt32(Console.ReadLine());
                    KorkusuzcaPrintliyorum(k_Means.Calculate(records, k)); // k-means Algoritmasını çalıştırır ve 
                                                                                // sonuç.txt ye basar
                }
                catch (FormatException e)
                {
                    Console.WriteLine("Uygun değer girmediniz.. ");
                    throw;
                }
        }
        static List<Record> OpenDataFile(string path)
        {
            List<Record> records = new List<Record>();
            try
            {
                FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
                StreamReader sr = new StreamReader(fs);
                string s = "";
                while ((s = sr.ReadLine()) != null)
                {
                    string[] line = s.Split(',');
                    Record newRecord = new Record(line);
                    records.Add(newRecord);
                }

                sr.Close();
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine("Dosya bulunamadı..");
                throw;
            }

            return records;
        }
        static void KorkusuzcaPrintliyorum(List<string> Data)
        {
            string path =  @"/Users/ipeksoydemir/RiderProjects/FinalProject_k-Means/FinalProject_k-Means/Sonuc.txt";
           
            FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Write);
            using(StreamWriter writer = new StreamWriter(fileStream))
            {
                for (int i = 0; i < Data.Count; i++)
                {
                    writer.WriteLine(Data[i]);
                }
            }
            
        }
        
    }
}