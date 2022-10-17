using System;
using System.ComponentModel;
using System.Windows.Controls;

namespace BookCathalog.Dal.Models
{
    public class Book : IDataErrorInfo
    {
        public string Title { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public int Year { get; set; }
        public string Isbn { get; set; } = string.Empty;
        public string Guid { get; set; } = string.Empty;
        public string About { get; set; } = string.Empty;
        public byte[] FrontPage { get; set; }
        //Buisness must set maximal year above current.
        private const int yearAboveCurrent = 3;
        public string Error { get => null; }

        //Using for validate data entered by user in AddBookDialog form.
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
                            if (!char.IsDigit(element) && !(element == 'X'))
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
                                    isbnSum += (10 - i) * 10;
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
                                    isbnSum += (i % 2 == 0 ? 1 : 3) * 10;
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
    }

}
