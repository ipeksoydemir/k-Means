using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
namespace FinalProject_k_Means
{
    public class k_Means
    {
        public static List<string> Data = new List<string>(); //Dosyaya yazılacak Data
        static  bool LasIter=false; //son İterasyon mu

        public static List<string>  Calculate(List<Record> records, int k)
        {
            int i = 0;
            Console.WriteLine("İlk Merkezlerim:");
            List<Record> centers = new List<Record>();
            while (k > i)  // record içinden k tane random merkez belirlenir
            {
                Random random = new Random();
                int index = random.Next(0, records.Count);
                centers.Add(records[index]); 
                Console.WriteLine( 
                    records[index].GetSportsValue()
                    + "," + records[index].GetReligiousValue()
                    + "," + records[index].GetNatureValue()
                    + "," + records[index].GetTheatreValue()
                    + "," + records[index].GetShoppingValue()
                    + "," + records[index].GetPicnicValue()
                );
                i++;
            }
                
            List<Record> newCentroids = new List<Record>();
            List<List<Record>> clustered = new List<List<Record>>();
            int count = 0;
            clustered = Clustering(records, centers); // kümeleme yapılır
            newCentroids =CalculateNewCentersList(clustered); // yeni merkezler belirlenir
            while (!Equal(newCentroids, centers)) // yeni merkez ile eski merkez arası eşit sayılıncaya kadar dön
            {
                count++;
                centers.Clear(); //eski merkez sıfırlanır
                centers = newCentroids; // eski merkez artık yeni merkezdir
                clustered.Clear(); // eski kümelenmiş sıfırlanır
                clustered = Clustering(records, centers); // tekrar kümeleme yapılır
                newCentroids =CalculateNewCentersList(clustered); // yeni merkez hesaplanır
            }
            Console.WriteLine(count + " tane iterasyon yaptım ve sonuç: ");
                    
            WCSS_BCSS_DunnInex(clustered,records,centers);
            
            return Data;
        }
        
        static Record CenterOfRecordList(List<Record> centers)
        {
            float sports=0, religious=0, nature=0, theatre=0, shopping=0, picnic=0; 
            for (int i = 0; i <centers.Count ; i++)  // o kümenin elemanı kadar
            {
                sports += centers[i] .GetSportsValue();
                religious += centers[i].GetReligiousValue();
                nature += centers[i]. GetNatureValue();  
                theatre += centers[i] .GetTheatreValue();
                shopping += centers[i] .GetShoppingValue();
                picnic += centers[i] .GetPicnicValue();
            } 
            
            return new Record(sports/centers.Count ,religious/centers.Count ,
                nature / centers.Count , theatre / centers.Count , 
                shopping / centers.Count , picnic / centers.Count );
        }
        
        static void WCSS_BCSS_DunnInex(List<List<Record>> clustered,List<Record> records,List<Record> centers)
        {
            Console.WriteLine("\tKayıtlar\t   \t\tMin Uzaklık\tKümeNo\t Küme Merkezi");
            List<double> InterClusterDistance = new List<double>();
            List<double> IntraClusterDistance = new List<double>();
            LasIter = true;
            double WCSS = 0,BCSS=0;
            Clustering(records, centers);
            Record MeanOfCenters = CenterOfRecordList(centers);
            for (int i = 0; i < clustered.Count; i++)  // küme sayısı kadar döner
            {
                double dist = 0;
                Data.Add((i+1)+". Kume: "+clustered[i].Count.ToString());
                for (int j = 0; j < clustered[i].Count; j++) // o kümeye ait eleman syaısı kadar döner
                {
                    
                    dist += (SSE(clustered[i][j], centers[i]));
                    IntraClusterDistance.Add(dist);
                }
               
                WCSS += dist/clustered[i].Count;
                BCSS += (SSE(centers[i], MeanOfCenters)) ;
                InterClusterDistance.Add(SSE(centers[i], MeanOfCenters));
            }
           
            Console.WriteLine("WCSS:" + WCSS);
            Console.WriteLine("BCSS:" + BCSS);
           double DunnIndex = InterClusterDistance.Min() / IntraClusterDistance.Max();
           Console.WriteLine("DunnIndex:" +DunnIndex);
           Data.Add("WCSS:" + WCSS);
           Data.Add("BCSS:" + BCSS);
           Data.Add("DunnIndex:" +DunnIndex);
           
                               
        }

        static bool Equal ( List<Record> newcenter,  List<Record> oldcenter)
        {
            bool canItNew = false;
            for (int i = 0; i < newcenter.Count; i++)
            {
                if (SSE(newcenter[i], oldcenter[i]) > 0.001f) // uzaklık 0.001den fazlaysa 
                {
                    canItNew = false;
                }
                else
                {
                    canItNew = true;
                }
            }
            return canItNew;
        }
        
        static double SSE(Record record, Record center)
        {
            double sum = 0;
            sum += Math.Pow(record.GetNatureValue() - center.GetNatureValue(), 2);
            sum += Math.Pow(record.GetSportsValue() - center.GetSportsValue(), 2);
            sum += Math.Pow(record.GetPicnicValue() - center.GetPicnicValue(), 2);
            sum += Math.Pow(record.GetReligiousValue() - center.GetReligiousValue(), 2);
            sum += Math.Pow(record.GetShoppingValue() - center.GetShoppingValue(), 2);
            sum += Math.Pow(record.GetTheatreValue() - center.GetTheatreValue(), 2);
            return sum;
        }
        
        static List<List<Record>> Clustering(List<Record> records,List<Record> centers)
        {
            
              List<List<Record>> clusteredDataSet = new List<List<Record>>();
              int j=0;
         
            for (int i = 0; i < records.Count; i++)
            {
              
                double minDistance = 20000; int clusterNo =0;      
                for ( j = 0; j < centers.Count; j++)
                {
                    double DistanceFromCenter = 0;
                    if (i == 0)
                    {
                        clusteredDataSet.Add( new List<Record>());
                    }
                    
                    DistanceFromCenter = Math.Sqrt(  SSE(records[i], centers[j]));
                 
                    if (DistanceFromCenter < minDistance)
                    {
                        minDistance = DistanceFromCenter;
                        clusterNo = j;
                    }                                           
                }
                clusteredDataSet[clusterNo].Add(records[i]); 
              
            
                if (LasIter) // Eğer son iterasyonsa
                {
                   
                  Console.Write(i + 1 + "\t" 
                                +records[i].GetSportsValue() 
                                +","+ records[i].GetReligiousValue() 
                                +","+ records[i].GetNatureValue() 
                                +","+ records[i].GetTheatreValue() 
                                +","+ records[i].GetShoppingValue() 
                                +","+ records[i].GetPicnicValue() 
                                + "\t=>  " + minDistance + " \t  "
                                +(clusterNo+1) + "\t ");
               
                  Console.Write( centers[clusterNo ].GetSportsValue()  
                                 +","+ centers[clusterNo ].GetReligiousValue() 
                                 +","+ centers[clusterNo ].GetNatureValue()
                                 +","+ centers[clusterNo ].GetTheatreValue()
                                 +","+ centers[clusterNo ].GetShoppingValue()
                                 +","+ centers[clusterNo ].GetPicnicValue() + "\n");
                  
                    string data = "Kayit" + (i + 1).ToString() + ":  \t"+records[i].GetSportsValue() 
                                  +","+ records[i].GetReligiousValue()
                                  +","+ records[i].GetNatureValue()
                                  +","+ records[i].GetTheatreValue()
                                  +","+ records[i].GetShoppingValue()
                                  +","+ records[i].GetPicnicValue()+
                                  " \t \t" + (clusterNo + 1) + ". Kume";
                    Data.Add(data);
                    
                }
            
            }
            return clusteredDataSet;
        }
        
        static List<Record> CalculateNewCentersList(List<List<Record>> clustered)
        {
            List<Record> newCenters = new List<Record>();
           
            for (int j = 0; j <clustered.Count ; j++) // k kadar döner
            {
                newCenters.Add(CenterOfRecordList(clustered[j]));
            }
            return newCenters;
        }
        
    }
}