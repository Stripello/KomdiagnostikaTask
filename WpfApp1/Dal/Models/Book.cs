using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace BookCathalog.Dal.Models
{
    public class Book
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public int Year { get; set; }
        public string Isbn { get; set; }
        public string Guid { get; set; }
        public string About { get; set; }
        public Image FrontPage { get; set; }

        //Must me set by buisness
        private const int maxYearOverCurrent = 2;
        public IList<TypeOfError> Validate()
        {
            var answer = new List<TypeOfError>();
            if (string.IsNullOrEmpty(Title))
            {
                answer.Add( TypeOfError.InvalidTitle);
            }
            if (string.IsNullOrEmpty(Author) || char.IsLower(Author[0]) || !Author.All(x => char.IsLetter(x)))
            {
                answer.Add(TypeOfError.InvalidAuthor);
            }
            if (Year < 0 || Year > DateTime.Now.Year + maxYearOverCurrent)
            {
                answer.Add(TypeOfError.InvalidYear);
            }
            if (Isbn.Length != 10)
            {
                answer.Add(TypeOfError.InvalidIsbn);
            }
            var isbnSum = 0;
            for (int i = 0; i < 9; i++)
            {
                //TODO: X case and 13 chars case
                if (!int.TryParse(Isbn[i].ToString(), out int tmp))
                {
                    answer.Add(TypeOfError.InvalidIsbn);
                }
                else
                {
                    isbnSum += (10 - i) * tmp;
                }
            }
            if (isbnSum % 11 != 0)
            {
                answer.Add(TypeOfError.InvalidIsbn);
            }
            if (!System.Guid.TryParse(Guid, out _))
            {
                answer.Add(TypeOfError.InvalidGuid);
            }

            return answer.Count > 0 ? answer : new List<TypeOfError> { TypeOfError.NoError };
        }
    }

}
