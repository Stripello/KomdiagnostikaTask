using Prism.Mvvm;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Security.RightsManagement;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace BookCathalog.Dal.Models
{
    public class Book : BindableBase, IDataErrorInfo , IEqualityComparer
    {
        public int Id { get; set; }
        private string _title = string.Empty;
        public string Title
        {
            get => _title;
            set => SetProperty<string>(ref _title, value);
        }
        private string _author = string.Empty;
        public string Author
        {
            get => _author;
            set => SetProperty<string>(ref _author, value);
        }
        private int _year;
        public int Year
        {
            get => _year;
            set => SetProperty<int>(ref _year, value);
        }
        private string _isbn = string.Empty;
        public string Isbn
        {
            get => _isbn;
            set => SetProperty<string>(ref _isbn, value);
        }

        private string _guid = string.Empty;
        public string Guid
        {
            get => _guid;
            set => SetProperty<string>(ref _guid, value);
        }
        private string _about = string.Empty;
        public string About
        {
            get => _about;
            set => SetProperty<string>(ref _about, value);
        }
        private byte[] _frontPage;
        public byte[] FrontPage
        {
            get => _frontPage;
            set => SetProperty<byte[]>(ref _frontPage, value);
        }

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
                        if (string.IsNullOrEmpty(Isbn))
                        {
                            return "ISBN is empty.";
                        }
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

        public override bool Equals(object? obj)
        {
            if (obj == null || !(obj is Book))
            {
                return false;
            }
            var casted = (Book)obj;
            if (Title != casted.Title || Author != casted.Author || Year != casted.Year ||
                Isbn != casted.Isbn || Guid != casted.Guid || About != casted.About)
            {
                return false;
            }
            if (this.FrontPage.Length != casted.FrontPage.Length)
            {
                return false;
            }
            for (int i = 0; i < FrontPage.Length; i++)
            {
                if (FrontPage[i] != casted.FrontPage[i])
                {
                    return false;
                }
            }

            return true;
        }

        public new bool Equals(object? x, object? y)
        {
            if (x == null && y == null)
            {
                return true;
            }
            if ((x == null && y != null) || (x != null && y == null))
            {
                return false;
            }
            if (x is Book)
            {
                var casted = (Book)x;
                return casted.Equals(y);
            }
            else
            {
                return false;
            }

        }

        public int GetHashCode(object obj)
        {
            return GetHashCode(obj);
        }
        public Book CreateCopy()
        {
            var answer = new Book();
            answer.Title = _title;
            answer.Author = _author;
            answer.Year = _year;
            answer.Isbn = _isbn;
            answer.Guid = _guid;
            answer.About = _about;
            answer.FrontPage = (byte[])_frontPage.Clone();

            return answer;
        }
    }

}
