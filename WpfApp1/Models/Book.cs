using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace BookCathalog.Dal.Models
{
    public class Book : IDataErrorInfo
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public int Year { get; set; }
        public string Isbn { get; set; }
        public string Guid { get; set; }
        public string About { get; set; }
        public Image FrontPage { get; set; }
        //Buisness must set maximal year above current.
        private const int yearAboveCurrent = 3;
        public string Error { get => null; }


        public string this[string propertyName]
        {
            get
            {
                switch (propertyName)
                {
                    case nameof(Title):
                        if (string.IsNullOrEmpty(Title))
                        {
                            return "Empty field is not allowed.";
                        }
                        break;
                        case nameof(Author):
                        if (string.IsNullOrEmpty(Author))
                        {
                            return "Empty field is not allowed.";
                        }
                        break;
                    case nameof(Year):
                        if (Year < 0)
                        {
                            return "Year B.C. is not allowed.";
                        }
                        if (yearAboveCurrent + DateTime.Now.Year < Year)
                        {
                            return "Year is too big.";
                        }
                        break;
                    case nameof(Isbn):
                        var tmp = Isbn.ToUpper();
                        foreach (var element in tmp)
                        {
                            if (!char.IsDigit(element) && !(element=='X'))
                            {
                                return "ISBN contains forbidden symbols.";
                            }
                        }
                        var length = Isbn.Length;
                        if (length == 10)
                        {
                            var isbnSum = 0;
                            for (int i = 0; i < length; i++)
                            {
                                if (tmp[i] == 'X')
                                {
                                    isbnSum += (10-i) * 10;
                                }
                                else
                                {
                                    isbnSum += (10 - i) * int.Parse(tmp[i].ToString());
                                }
                            }
                            return isbnSum % 11 == 0 ? null : "Invalid 10-sign ISBN.";
                        }
                        else if (length == 13)
                        {
                            var isbnSum = 0;
                            for (int i = 0; i < length; i++)
                            {
                                if (tmp[i] == 'X')
                                {
                                    isbnSum += (i % 2 == 0 ? 1 :3) * 10;
                                }
                                else
                                {
                                    isbnSum += (i % 2 == 0 ? 1 : 3) * int.Parse(tmp[i].ToString());
                                }
                            }
                            return isbnSum % 10 == 0 ? null : "Invalid 13-sign ISBN.";
                        }
                        else
                        {
                            return "Invalid ISBN length.";
                        }
                        break;
                    case nameof(Guid):
                        if (!System.Guid.TryParse(Guid, out _))
                        {
                            return "Invalid GUID.";
                        }
                        break;
                }
                return null;
            }
        }

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
