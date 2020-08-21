using System;
using System.Collections.Generic;
using System.Text;

namespace JourneyDoc.Models.Dto
{
    /// <summary>
    /// Tags are much faster and simpler to handle separately from the Journey.
    /// Id is the Journeys ID.
    /// 
    /// </summary>
    public class TagListDto : ItemBase<string>
    {
        /// <summary>
        /// Serialization requires this
        /// </summary>
        public TagListDto()
        {
        }

        /// <summary>
        /// Convenience constructor to create from Journey.
        /// </summary>
        /// <param name="journey"></param>
        public TagListDto(Journey journey)
        {
            Id = journey.Id;
            TagList = journey.TagList;
            AccountId = journey.AccountId;
        }
        /// <summary>
        /// List of tags from Journey
        /// </summary>
        public IList<Tag> TagList { get; set; }

        /// <summary>
        /// Need this as well
        /// </summary>
        public string AccountId { get; set; }

    }
}
