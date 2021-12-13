namespace Core.DTO.Paging
{
    public class BasePaging
    {
        public BasePaging()
        {
            PageId = 1;
        }

        public int PageId { get; set; }

        public int PageCount { get; set; }

        public int ActivePage { get; set; }

        public int StartPage { get; set; }

        public int EndPage { get; set; }

        public int TakeEntity { get; set; } = 10;

        public int SkipEntity { get; set; }
    }
}
