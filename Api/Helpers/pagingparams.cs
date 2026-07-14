using Humanizer;

public class PagingParams
{
    private const int MaxPagesize=50;
    public int pageNumber { get; set; }=1;
    private int _pagesize=10;
    public int PageSize
    {
        get=>_pagesize;
        set=> _pagesize=(value>MaxPagesize) ? MaxPagesize:value;
    }
    
}