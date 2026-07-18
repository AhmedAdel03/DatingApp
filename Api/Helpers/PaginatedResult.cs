
using Azure;
using Microsoft.EntityFrameworkCore;

namespace Api.Helpers
 {
public class PaginatedResult<T>
    {
        public PaginatedMetaData paginatedMetaData { get; set; }=default!;
        public List<T> Items { get; set; } =[];

        
    }
    public class PaginatedMetaData
    {
        public int CurrentPage { get; set; }    
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public int TotalCount { get; set; }
    };
    public  class paginationHelper
    {
public static async Task<PaginatedResult<T>>CreateAsync<T>(IQueryable<T> query,int pageNumber,int PageSize)
        {
            var count=await query.CountAsync();
            var Items=await query.Skip((pageNumber-1)*PageSize).Take(PageSize).ToListAsync();
            return new PaginatedResult<T>
            {
              paginatedMetaData=new PaginatedMetaData
              {
                  CurrentPage=pageNumber,
                  TotalPages=(int)Math.Ceiling(count /(double)PageSize),
                  PageSize=PageSize,
                  TotalCount=count
              }, 
              Items=Items
            };
            
        }
    };
    


 }