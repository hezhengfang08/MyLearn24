using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using MySelf.Zero.Application.Contracts.Topic;
using MySelf.Zero.Application.Contracts.User;
using MySelf.Zero.Application.Validator;
using MySelf.Zero.Domain.Entities;
using MySelf.Zero.Domain.Repositories;
using MySelf.Zero.Domain.Shared;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace MySelf.Zero.Application
{
    public class TopicAppService : ApplicationService, ITopicAppService
    {
        private readonly ICategoryRepositroy categoryRepositroy;
        private readonly ITopicRepository topicRepository;
        private readonly IPostsRepository postsRepository;
        private readonly IUserRepository userRepository;
        private readonly IHttpContextAccessor httpContextAccessor;

        public TopicAppService(ICategoryRepositroy categoryRepositroy,
            ITopicRepository topicRepository,
            IPostsRepository postsRepository,
            IUserRepository userRepository,
            IHttpContextAccessor httpContextAccessor)
        {
            this.topicRepository = topicRepository;
            this.categoryRepositroy = categoryRepositroy;
            this.postsRepository = postsRepository;
            this.userRepository = userRepository;
            this.httpContextAccessor = httpContextAccessor;
        }

        public async Task<bool> AnyAsync()
        {
            return await topicRepository.GetCountAsync() > 0;
        }

        public async Task ImportAsync(IEnumerable<TopicImportDto> importDtos)
        {
            var categoryIds = importDtos.Select(d => d.CategoryId).ToArray();
            var categories = await categoryRepositroy.GetListOfIdArrayAsync(categoryIds);
            var topics = new List<TopicEntity>(categoryIds.Length);
            foreach (var dto in importDtos)
            {
                var topic = ObjectMapper.Map<TopicImportDto, TopicEntity>(dto);
                topic.Category = categories.Single(c => c.Id == dto.CategoryId);
                topics.Add(topic);
            }

            await topicRepository.InsertManyAsync(topics);
        }

        public async Task<ApiResponse<TopicDto>> GetTopicAsync(long id)
        {
            var response = new ApiResponse<TopicDto>();
            var topicEntity = await topicRepository.GetAsync(t => t.Id == id);
            if (topicEntity == null)
            {
                response.IsFailed("无效的参数");

                return response;
            }

            var topic = ObjectMapper.Map<TopicEntity, TopicDto>(topicEntity);
            response.IsSuccess(topic);

            return response;
        }

        public async Task<ApiResponse<PagedResultDto<TopicDto>>> GetTopicListAsync(long categoryId, int page = 1, int limit = 10)
        {
            var response = new ApiResponse<PagedResultDto<TopicDto>>();

            var result = await topicRepository.GetTopicByCategory(categoryId, page, limit);
            var topicList = ObjectMapper.Map<List<TopicEntity>, List<TopicDto>>(result.Item2);

            var userIds = result.Item2.Select(m => m.UserId).ToArray();
            var users = await userRepository.GetListOfIdArrayAsync(userIds);
            topicList.ForEach(topic =>
            {
                var user = users.FirstOrDefault(u => u.Id == topic.UserId);
                if (user != null)
                {
                    topic.User = ObjectMapper.Map<UserEntity, UserDto>(user);
                }
            });

            response.Result = new PagedResultDto<TopicDto>(result.Item1, topicList.AsReadOnly());


            return response;
        }

        public async Task<ApiResponse<PagedResultDto<PostsDto>>> GetPostsAsync(long topicId, int page = 1, int limit = 10)
        {
            var response = new ApiResponse<PagedResultDto<PostsDto>>();

            var result = await postsRepository.GetPostsByTopic(topicId, page, limit);
            var postsList = ObjectMapper.Map<List<PostsEntity>, List<PostsDto>>(result.Item2);

            var userIds = result.Item2.Select(m => m.UserId).ToArray();
            var users = await userRepository.GetListOfIdArrayAsync(userIds);
            postsList.ForEach(posts =>
            {
                var user = users.FirstOrDefault(u => u.Id == posts.UserId);
                if (user != null)
                {
                    posts.User = ObjectMapper.Map<UserEntity, UserDto>(user);
                }
            });

            response.Result = new PagedResultDto<PostsDto>(result.Item1, postsList.AsReadOnly());

            return response;
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Permission")]
        public async Task<ApiResponse> SendTopicAsync(SendTopicInput input)
        {
            var response = new ApiResponse();
            var httpContext = httpContextAccessor.HttpContext;
            var userId = CoreMvcAuthorizationUtil.GetUserId(httpContext);
            if (userId != null)
            {
                input.UserId = Convert.ToInt32(userId);
            }

            var isValid = ValidatorMapper.SendTopicInput.CustomValidate(input, out IEnumerable<string> errors);
            if (!isValid)
            {
                response.IsFailed(string.Join(';', errors));

                return response;
            }

            var topic = new TopicEntity
            {
                UserId = input.UserId,
                TopicName = input.TopicName,
                TopicContent = input.TopicContent,
                Category = await categoryRepositroy.GetAsync(input.CategoryId),
            };

            await topicRepository.InsertAsync(topic);

            return response;
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ApiResponse> ReplyPostsAsync(ReplyPostsInput input)
        {
            var response = new ApiResponse();
            var httpContext = httpContextAccessor.HttpContext;
            var userId = CoreMvcAuthorizationUtil.GetUserId(httpContext);
            if (userId != null)
            {
                input.UserId = Convert.ToInt32(userId);
            }

            var isValid = ValidatorMapper.ReplyPostsInput.CustomValidate(input, out IEnumerable<string> errors);
            if (!isValid)
            {
                response.IsFailed(string.Join(';', errors));

                return response;
            }


            var clientIp = CoreMvcClientIpUtil.GetClientIP(httpContext);
            var posts = new PostsEntity
            {
                UserId = input.UserId,
                PostContent = input.Content,
                IpAddress = clientIp,
                Topic = await topicRepository.GetAsync(input.TopicId)
            };
            //if (input.PostId.HasValue)
            //{
            //    posts.RecivedPostId = input.PostId.Value;
            //}

            await postsRepository.InsertAsync(posts);

            return response;
        }
    }
}
