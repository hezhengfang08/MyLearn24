namespace MySelf.Zhihu.SharedModels
{
   public enum FeedType { Quesiton, Answer }
    public record FeedCreatedEvent
    {
        public FeedType FeedType { get; init; }

        public int FeedId { get; init; }

        public int UserId { get; init; }
    }
}
