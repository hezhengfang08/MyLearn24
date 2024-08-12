using Zhaoxi.Forum.Domain.Category;
using Zhaoxi.Forum.Domain.Topic;

namespace Zhaoxi.Forum.Domain;

/// <summary>
/// 主题/帖子
/// </summary>
public class TopicEntity : BaseEntity
{
    public long UserId { get; set; }

    public string TopicName { get; set; }

    public string TopicContent { get; set; }

    public int Views { get; set; }

    public int FavouriteTimes { get; set; }

    public bool? IsLocked { get; set; } = false;

    public int? LastReciveUserId { get; set; }

    public DateTime? LastReciveTime { get; set; }

    public int Commits { get; set; }

    public DateTime UpdateDate { get; set; }

    public bool? Hoted { get; set; }

    public bool? Creamed { get; set; }

    public virtual CategoryEntity Category { get; set; }
    public virtual ICollection<PostsEntity> Postses { get; set; }
}