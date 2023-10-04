using System.Net.Http.Headers;

namespace Book
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Price { get; set; }

        public override string ToString()
        {
            return Id + ", " + Title + ", " + Price;
        }
        
        public void ValidateTitle()
        {
            if (Title == null)
            {
                throw new ArgumentNullException("title is null");
            }                
            else if (Title.Length < 3) 
            {
                throw new ArgumentException("Title must be at least 3 charcthers");
            }
        }

        public void ValidatePrice()
        {
            if (Price < 0)
            {
                throw new ArgumentOutOfRangeException("Price must be positive");
            }
            else if (Price > 1200)
            {
                throw new ArgumentOutOfRangeException("Price must be less than 1201");
            }
        }

        public void Validate()
        {
            ValidateTitle();
            ValidatePrice();
        }
    }
}