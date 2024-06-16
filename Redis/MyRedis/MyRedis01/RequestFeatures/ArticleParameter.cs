namespace MyRedis01.RequestFeatures
{
    public class ArticleParameter
    {
        private const int MaxPageSize = 20;
        public int PageNumber { get; set; } = 1;
        private int _pageSize = 4;
        public int PageSize
        {
            get { return _pageSize; }
            set
            {
                _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
            }
        }
    }
}
