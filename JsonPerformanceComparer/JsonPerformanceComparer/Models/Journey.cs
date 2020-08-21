using JourneyDoc.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JourneyDoc.Models
{
    /// <summary>
    /// Data about a journey instance.
    /// </summary>
    public class Journey : ItemBase<string>, IEntityInTable<string>, IFilterableItem, IPresentableItem
    {
        /// <summary>
        /// Supply a method to convert a JourneyTemplateId to the corresponding JourneyTemplate
        /// </summary>
        public static Func<int?, Task<JourneyTemplate>> GetTemplateFromIdAsync;
        /// <summary>
        /// Supply a method to populate Owner from OwnerId
        /// </summary>
        public static Func<string, User> GetOwnerFromId;
        private JourneyTemplate _journeyTemplate;
        private string _presentationText;

        /// <summary>
        /// Name and description of this journey
        /// </summary>
        public GeneralDescription GeneralDescription { get; set; } = new GeneralDescription();

        /// <summary>
        /// The template it was based on
        /// </summary>
        public JourneyTemplate JourneyTemplate
        {
            get { return _journeyTemplate; }
            private set { SetProperty(ref _journeyTemplate, value); }
        }

        /// <summary>
        /// Id of journey template
        /// </summary>
        public int? JourneyTemplateId { get; set; }

        /// <summary>
        /// All the points of the journey
        /// </summary>
        public IList<JourneyPoint> Points { get; set; } = new List<JourneyPoint>();

        /// <summary>
        /// User responsible for the journey
        /// </summary>
        public User Owner { get; private set; }

        /// <summary>
        /// User id of user responsible for the journey
        /// </summary>
        public string OwnerId { get; set; }

        /// <summary>
        /// The observers of the journey. Not participating, but observing
        /// </summary>
        public IList<User> Observers { get; set; }

        /// <summary>
        /// The actual travelers
        /// </summary>
        public IList<User> Travelers { get; set; }

        /// <summary>
        /// The list of all tags that may be attached to the points of this journey
        /// </summary>
        public IList<Tag> TagList { get; set; } = new List<Tag>();

        /// <summary>
        /// True if a description or presentation text exist.
        /// </summary>
        public bool HasPresentationText
        {
            get
            {
                return !string.IsNullOrEmpty(GeneralDescription.Description) || !string.IsNullOrEmpty(PresentationText);
            }
        }

        /// <summary>
        /// Is location tracking enabled for this journey.
        /// Currently inherited from template, but this may change in the future.
        /// </summary>

        public bool IsLocationTrackingEnabled { get => _journeyTemplate?.IsLocationTrackingEnabled ?? false; }

        /// <summary>
        /// When did journey start?
        /// Returns oldest point (if any).
        /// </summary>
        public DateTimeOffset? Start
        {
            get
            {
                if (!Points.Any())
                    return null;
                return Points.Min(x => x.Start);
            }
        }

        /// <summary>
        /// What is the duration of the journey from start to end?
        /// </summary>
        public TimeSpan? Duration
        {
            get
            {
                if (!Start.HasValue)
                    return null;
                return Points.Max(x => x.Start).Value.Subtract(Start.Value);
            }
        }

        /// <summary>
        /// Account this journey belongs to. Could also have been found via Owner,
        /// but since that is only available on front-end and we need it on back-end,
        /// this is so much easier.
        /// </summary>
        public string AccountId { get; set; }

        /// <summary>
        /// Text used in presentation mode
        /// </summary>
        public string PresentationText
        {
            get { return _presentationText; }
            set { SetProperty(ref _presentationText, value); }
        }

        /// <summary>
        /// Path to image. 
        /// </summary>
        public string ImagePath
        {
            get
            {
                return Points
                    .OrderBy(x => x.Phase?.SortIndex)
                    .ThenBy(x => x.Start)
                    .FirstOrDefault(x => x.ImagePath != null)?.ImagePathLow;
            }
        }

        /// <summary>
        /// Path to sound. Currently not used.
        /// </summary>
        public string SoundPath { get; }

        /// <summary>
        /// Path to video. Currently not used.
        /// </summary>
        public string VideoPath { get; }

        /// <summary>
        /// Flag controling whether item is included in presentation or not.
        /// </summary>
        public bool IncludeInPresentation { get; set; }

        /// <summary>
        /// The group name in presentation mode.
        /// </summary>
        public string GroupName => "Journey";

        /// <summary>
        /// Add a new tag at position specified by its SortIndex and automatically allocating an Id
        /// </summary>
        /// <param name="tag"></param>
        /// <param name="sortIndex">Where should tag be added, -1 means at end.</param>
        public Tag AddTag(Tag tag, int sortIndex = -1)
        {
            tag.Id = TagList.Max(x => x.Id) + 1;

            if (sortIndex == -1)
                sortIndex = TagList.Count - 1;

            if (sortIndex == TagList.Count)
                TagList.Add(tag);
            else
            {
                // Make room by moving tags to the right one position
                foreach (Tag t in TagList.Where(x => x.SortIndex >= sortIndex))
                    t.SortIndex++;
                TagList.Insert(tag.SortIndex, tag);
            }
            return tag;
        }

        /// <summary>
        /// Build tag, either by getting it from existing list or adding/inserting a new tag to the list and returning that
        /// </summary>
        /// <param name="name">Name of new or existing tag</param>
        /// <param name="tagType">Type of tag</param>
        /// <param name="sortIndex">Sortindex, if 0 append at end.</param>
        /// <returns></returns>
        public Tag TagBuilder(string name, TagTypes tagType = TagTypes.Phase, int sortIndex = -1)
        {
            Tag rv = null;
            sortIndex = sortIndex != -1 ? sortIndex : TagList.Count() - 1;
            if (name != null)
                rv = TagList.FirstOrDefault(x => x.GeneralDescription.Name == name);
            if (rv == null)
            {
                rv = AddTag(
                    new Tag
                    {
                        GeneralDescription = new GeneralDescription { Name = name },
                        Type = tagType
                    },
                    sortIndex);
            }
            return rv;
        }

        /// <summary>
        /// We need to fix up tags and template information on load.
        /// </summary>
        /// <returns></returns>
        public override async Task PostClientSerializationAsync(object owner = null)
        {
            if (GetTemplateFromIdAsync != null)
                JourneyTemplate = await Journey.GetTemplateFromIdAsync(JourneyTemplateId);
            if (GetOwnerFromId != null)
                SetOwner(GetOwnerFromId(OwnerId));
            foreach (var item in Points)
            {
                await item.PostClientSerializationAsync(this);
            }
        }

        /// <summary>
        /// We need this to keep the JourneyTemplate property private so 
        /// it is not serialized into JSON.
        /// </summary>
        /// <param name="journeyTemplate"></param>
        public void SetJourneyTemplate(JourneyTemplate journeyTemplate)
        {
            JourneyTemplate = journeyTemplate;
        }

        /// <summary>
        /// Get list of journey points filtered and grouped by specified tagtype.
        /// </summary>
        /// <param name="tagTypesEnum">The tag type to filter on.</param>
        /// <returns></returns>
        public IList<GroupedPointItem> FilterAndGroupByTagType(TagTypes tagTypesEnum)
        {
            var rvList =
                GetTagsByType(tagTypesEnum)
                .GroupJoin(
                    Points.OrderBy(x => x.Tags.First().SortIndex)
                    .ThenByDescending(x => x.Start),
                tag => tag.Id,
                point => point.Tags.FirstOrDefault(x => x.Type == tagTypesEnum)?.Id,
                (x, y) => new GroupedPointItem { Tag = x, Points = y.ToList() });
            return rvList.ToList();
        }

        /// <summary>
        /// Get the list of presentable items, respecting the IncludeInPresentation flag and sorting
        /// by phase and then Start.
        /// </summary>
        /// <returns>The list</returns>
        public IList<IPresentableItem> GetPresentableItems()
        {
            var rv = new List<IPresentableItem>();
            if (IncludeInPresentation == true)
                rv.Add(this);

            var groupedByPhase = FilterAndGroupByTagType(TagTypes.Phase);
            foreach (var item in groupedByPhase)
            {
                if (item.Tag.IncludeInPresentation == true)
                    rv.Add(item.Tag);
                var relevantItems = item.Points
                    .Where(x => x.IncludeInPresentation == true)
                    .OrderBy(x => x.Start);
                foreach (var point in relevantItems)
                    rv.Add(point);
            }
            return rv;
        }

        /// <summary>
        /// Get distinct list of all tags with specified type.
        /// </summary>
        /// <param name="tagTypesEnum"></param>
        /// <returns>Sorted list of tags.</returns>
        public IEnumerable<Tag> GetTagsByType(TagTypes tagTypesEnum)
        {
            IEnumerable<Tag> rv = TagList
                .Where(t => t.Type == tagTypesEnum)
                .OrderBy(x => x.SortIndex);
            return rv;
        }

        /// <summary>
        /// Find a tag on its name.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public Tag FindTag(string name)
        {
            return TagList.FirstOrDefault(x => x.GeneralDescription.Name == name);
        }

        /// <summary>
        /// Supporting filtering on data returned by this
        /// </summary>
        /// <returns>All text to filter on in lower case.</returns>
        public string ValueToFilterOn()
        {
            string rv = GeneralDescription.Name + " " + GeneralDescription.Description + " ";
            rv += string.Join(" ", TagList.Select(y => y.GeneralDescription.Name));
            if (Points != null && Points.Any())
                rv += string.Join(" ", Points.Select(x => x.ValueToFilterOn()));
            return rv.ToLower();
        }

        /// <summary>
        /// To make Owner not serialize when being sent from backend, at least, it must be private. 
        /// So set via this.
        /// </summary>
        /// <param name="user"></param>
        public void SetOwner(User user)
        {
            Owner = user;
        }

        /// <summary>
        /// Convenient to show in default list representation
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return GeneralDescription.Name;
        }
    }

    /// <summary>
    /// Result when grouping points according to phase or other tag
    /// </summary>
    public class GroupedPointItem
    {
        /// <summary>
        /// The tag we grouped on
        /// </summary>
        public Tag Tag { get; set; }
        /// <summary>
        /// The list of points with that tag
        /// </summary>
        public IList<JourneyPoint> Points { get; set; }
    }
}


