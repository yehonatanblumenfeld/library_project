using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryModel
{
    [Serializable]
    public class Book : LibaryItem
    {    
        public string Discription {get;}
        public string WriterName  { get; set; }       
        public  Guid ISBN;
        public BookLength bookLength;   
        public Genre genre;
        public enum Genre
        {
            Romance = 1,
            Horror,
            Drama,
            Thriller,
            Fantasy,
            Magic,
            Action,
            Historical,
            Biography,
            Comics
        }
        public enum BookLength
        {
            Short = 0,
            Medium,
            Long 
        }
        public Book(string itemname , string discription , Genre genre , decimal price) : base(itemname ,new DateTimeOffset() , price)
        {           
            Discription = discription;            
            ISBN = Guid.NewGuid();            
            this.genre = genre;               
        }     
        public override string ToString()
        {
            return $"Book name: {ItemName}   Print date:{ReleaseYear.ToString("dd/MM/yyyy") }   Price:{Price}   Genre:{genre}\nCopy Number:   {CopyNumber} WriterName:   {WriterName} Length:   {bookLength} " +
                $"\nDiscription: {Discription} " +
                $"\nISBN:{ISBN}"; 
                
        }
    }
}
