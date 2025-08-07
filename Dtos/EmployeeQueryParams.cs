public class EmployeeQueryParams
{
    public string? Search { get; set; }
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 10;
     public string? Position { get; set; } 
}
