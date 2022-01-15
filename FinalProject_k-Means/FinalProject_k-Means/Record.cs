using System;
using System.Collections.Generic;
namespace FinalProject_k_Means
{
    public class Record
    {
        private float Sports, Religious, Nature, Theatre, Shopping, Picnic;

        public Record(string [] data)
        {
            try
            {
                this.Sports = Int32.Parse(data[0]);
                this.Religious = Int32.Parse(data[1]);
                this.Nature = Int32.Parse(data[2]);
                this.Theatre = Int32.Parse(data[3]);
                this.Shopping = Int32.Parse(data[4]);
                this.Picnic = Int32.Parse(data[5]);
            }
            catch (ArgumentNullException e)
            {
                Console.WriteLine(e);
                throw;
            }
           
        }
        public Record( float Sports,float Religious,float Nature,float Theatre,float Shopping,float Picnic)
        {
            try
            {
                this.Sports = Sports;
                this.Religious = Religious;
                this.Nature = Nature;
                this.Theatre = Theatre;
                this.Shopping = Shopping;
                this.Picnic = Picnic;
            }
            catch (ArgumentNullException e)
            {
                Console.WriteLine(e);
                throw;
            }
           
        }
        public float GetSportsValue()
        {
            return this.Sports;
        }
        public float GetReligiousValue()
        {
            return this.Religious;
        }
        public float GetNatureValue()
        {
            return this.Nature;
        }
        public float GetTheatreValue()
        {
            return this.Theatre;
        }
        public float GetShoppingValue()
        {
            return this.Shopping;
        }
        public float GetPicnicValue()
        {
            return this.Picnic;
        }
    }
}