namespace LibraryApi.Models;

public class Loan
{
    public int Id {get; set;}
    public int MemberId {get; set;}
    public int BookId {get; set;}
    public DateTime BorrowedAt {get; set;}
    public DateTime  DueAt {get; set;}
    public DateTime? ReturnedAt {get; set;}

}