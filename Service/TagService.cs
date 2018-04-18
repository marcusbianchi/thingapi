using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using thingservice.Data;
using thingservice.Service.Interface;
using thingservice.Model;

namespace thingservice.Service
{
    public class TagService : ITagService
    {

        private IConfiguration _configuration;
        private readonly ApplicationDbContext _context;
        public TagService(IConfiguration configuration, ApplicationDbContext context)
        {
            _configuration = configuration;
            _context = context;
        }
        public async Task<(List<Tag>, int)> getTags(int startat, int quantity, TagFieldEnum fieldFilter, string fieldValue, TagFieldEnum orderField, OrderEnum order)
        {
            var queryTag = _context.Tags.Where(x => x.tagId > 0);
            queryTag = ApplyFilter(queryTag, fieldFilter, fieldValue);
            queryTag = ApplyOrder(queryTag, orderField, order);
            var tags = await queryTag
            .Skip(startat).Take(quantity).Select(item => new Tag()
            {
                tagId = item.tagId,
                tagDescription = item.tagDescription,
                tagName = item.tagName,
                physicalTag = item.physicalTag,
                tagGroup = item.tagGroup,
                tagType = item.tagType,
                thingGroupId = item.thingGroupId,
                thingGroup = new ThingGroup()
                {
                    thingGroupId = item.thingGroupId,
                    groupCode = item.thingGroup.groupCode,
                    groupDescription = item.thingGroup.groupDescription,
                    groupName = item.thingGroup.groupName,
                    thingsIds = item.thingGroup.thingsIds
                }
            }).ToListAsync();
            var queryTagCount = _context.Tags.Where(x => x.tagId > 0);
            queryTagCount = ApplyFilter(queryTagCount, fieldFilter, fieldValue);
            queryTagCount = ApplyOrder(queryTagCount, orderField, order);
            var totalCount = await queryTagCount.CountAsync();
            return (tags, totalCount);

        }

        public async Task<List<Tag>> getTagsPerType(TagTypeEnum type)
        {
            var tagsList = await _context.Tags.Where(x=> x.tagType == type).ToListAsync();

            return tagsList;
        }

        private IQueryable<Tag> ApplyFilter(IQueryable<Tag> queryTags,
       TagFieldEnum fieldFilter, string fieldValue)
        {
            switch (fieldFilter)
            {
                case TagFieldEnum.physicalTag:
                    queryTags = queryTags.Where(x => x.physicalTag.Contains(fieldValue));
                    break;
                case TagFieldEnum.tagDescription:
                    queryTags = queryTags.Where(x => x.tagDescription.Contains(fieldValue));
                    break;
                case TagFieldEnum.tagName:
                    queryTags = queryTags.Where(x => x.tagName.Contains(fieldValue));
                    break;
                case TagFieldEnum.tagType:
                    if(fieldValue.ToLower() == TagTypeEnum.Input.ToString().ToLower())
                    {
                        queryTags = queryTags.Where(x => x.tagType == TagTypeEnum.Input);
                    }
                    else if(fieldValue.ToLower() == TagTypeEnum.Output.ToString().ToLower())
                    {
                        queryTags = queryTags.Where(x => x.tagType == TagTypeEnum.Output);
                    }
                    break;
                default:
                    break;
            }
            return queryTags;
        }

        private IQueryable<Tag> ApplyOrder(IQueryable<Tag> queryTags,
        TagFieldEnum orderField, OrderEnum order)
        {
            switch (orderField)
            {
                case TagFieldEnum.physicalTag:
                    if (order == OrderEnum.Ascending)
                        queryTags = queryTags.OrderBy(x => x.physicalTag);
                    else
                        queryTags = queryTags.OrderByDescending(x => x.physicalTag);
                    break;
                case TagFieldEnum.tagDescription:
                    if (order == OrderEnum.Ascending)
                        queryTags = queryTags.OrderBy(x => x.tagDescription);
                    else
                        queryTags = queryTags.OrderByDescending(x => x.tagDescription);
                    break;
                case TagFieldEnum.tagName:
                    if (order == OrderEnum.Ascending)
                        queryTags = queryTags.OrderBy(x => x.tagName);
                    else
                        queryTags = queryTags.OrderByDescending(x => x.tagName);
                    break;
                default:
                    queryTags = queryTags.OrderBy(x => x.tagId);
                    break;
            }
            return queryTags;
        }

    }
}