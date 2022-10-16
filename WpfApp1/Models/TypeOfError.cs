namespace BookCathalog.Dal.Models
{
    public enum TypeOfError
    {
        NoError,
        InvalidTitle,
        InvalidAuthor,
        InvalidYear,
        InvalidIsbn,
        InvalidGuid,
        InvalidFrontPage,
        ImageError
    }
}