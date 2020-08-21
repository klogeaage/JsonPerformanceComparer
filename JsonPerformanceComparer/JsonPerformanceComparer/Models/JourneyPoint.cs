using JourneyDoc.Common;
using JourneyDoc.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JourneyDoc.Models
{
    /// <summary>
    /// Represents a single point
    /// </summary>
    public class JourneyPoint : ItemBase<string>, IFilterableItem, IPresentableItem
    {
        private string _imagePath;
        private DateTimeOffset? _start;
        private LocationData _gpsLocation;
        private string _insights;
        private string _soundPath;
        private string _presentationText;
        private string _videoPath;
        private bool _includeInPresentation;

        /// <summary>
        /// General description of point
        /// </summary>
        public GeneralDescription GeneralDescription { get; set; } = new GeneralDescription();

        /// <summary>
        /// Path to image
        /// </summary>
        public string ImagePath
        {
            get { return _imagePath; }
            set
            {
                SetProperty(ref _imagePath, value);
                OnPropertyChanged("IsInCloud");
                OnPropertyChanged("ImagePathLow");
            }
        }

        /// <summary>
        /// Low resolution version of image.
        /// </summary>
        public string ImagePathLow
        {
            get { return _imagePath.IsRemote() ? _imagePath + ".low" : _imagePath; }
        }

        /// <summary>
        /// Id of image file
        /// </summary>
        public string ImageId => GetIdFromPath(ImagePath);

        /// <summary>
        /// Path to sound file
        /// </summary>
        public string SoundPath
        {
            get { return _soundPath; }
            set
            {
                SetProperty(ref _soundPath, value);
                OnPropertyChanged(nameof(IsInCloud));
            }
        }

        /// <summary>
        /// Id of sound file
        /// </summary>
        public string SoundId => GetIdFromPath(SoundPath);

        /// <summary>
        /// Path to Video
        /// </summary>
        public string VideoPath
        {
            get { return _videoPath; }
            set
            {
                SetProperty(ref _videoPath, value);
                OnPropertyChanged(nameof(IsInCloud));
            }
        }

        /// <summary>
        /// Id of video file
        /// </summary>
        public string VideoId => GetIdFromPath(VideoPath);

        /// <summary>
        /// Data about where a point was recorded. 
        /// If image is specified, data from this overrides the value given by device.
        /// </summary>
        public LocationData GpsLocation
        {
            get { return _gpsLocation; }
            set
            {
                SetProperty(ref _gpsLocation, value);
                OnPropertyChanged(nameof(HasGpsLocation));
                OnPropertyChanged(nameof(ShowLocation));
            }
        }

        /// <summary>
        /// Description of insights
        /// </summary>
        public string Insights
        {
            get { return _insights; }
            set { SetProperty(ref _insights, value); }
        }

        /// <summary>
        /// Text used for presentation
        /// </summary>
        public string PresentationText
        {
            get { return _presentationText; }
            set { SetProperty(ref _presentationText, value); }
        }

        /// <summary>
        /// Description of observations - is this still relevant?
        /// </summary>
        public string Observations { get; set; }

        /// <summary>
        /// Get the phase tag
        /// </summary>
        public Tag Phase
        {
            get
            {
                return Tags?.FirstOrDefault(x => x.Type == TagTypes.Phase);
            }
        }

        /// <summary>
        /// List of tags. Built from TagIds on demand.
        /// </summary>
        public IList<Tag> Tags { get; private set; } = new List<Tag>();

        /// <summary>
        /// List of tag ids, i.e. references to the owner journey TagList items.
        /// </summary>
        public IList<int> TagIds { get; set; } = new List<int>();

        /// <summary>
        /// When was the point started
        /// </summary>
        /// 
        public DateTimeOffset? Start
        {
            get { return _start; }
            set { SetProperty(ref _start, value); }
        }

        /// <summary>
        /// Which journey does this belong to?
        /// Only available during transmission to server
        /// </summary>
        public string JourneyId { get; set; }

        /// <summary>
        /// Which account?
        /// Only available during transmission to server.
        /// </summary>
        public string AccountId { get; set; }

        /// <summary>
        /// Flag controling whether item is included in presentation or not.
        /// </summary>
        public bool IncludeInPresentation { get => _includeInPresentation; set => SetProperty(ref _includeInPresentation, value); }

        /// <summary>
        /// Has all data been safely stored in cloud?
        /// </summary>
        public bool IsInCloud
        {
            get
            {
                bool rv = true;
                if (ImagePath != null)
                    rv = ImagePath.IsRemote();
                if (SoundPath != null)
                    rv = rv && SoundPath.IsRemote();
                if (VideoPath != null)
                    rv = rv && VideoPath.IsRemote();
                return rv;
            }
        }

        /// <summary>
        /// Should we show location info?
        /// </summary>
        public bool ShowLocation
        {
            get
            {
                return GpsLocation != null && TrackLocation;
            }
        }

        /// <summary>
        /// Is location tracking enabled?
        /// </summary>
        public bool TrackLocation { get; private set; }

        /// <summary>
        /// Do we have a GPS location that can be used for maps?
        /// </summary>
        public bool HasGpsLocation
        {
            get
            {
                return GpsLocation != null && GpsLocation.Latitude != 0;
            }
        }

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
        /// The group name in presentation mode.
        /// </summary>
        public string GroupName => Phase.GeneralDescription.Name;

        /// <summary>
        /// Add a tag
        /// </summary>
        /// <param name="tag"></param>
        public void AddTag(Tag tag)
        {
            TagIds.Add(tag.Id);
            Tags.Add(tag);
        }

        /// <summary>
        /// Remove a tag
        /// </summary>
        /// <param name="tag"></param>
        public void RemoveTag(Tag tag)
        {
            TagIds.Remove(tag.Id);
            Tags.Remove(tag);
        }

        /// <summary>
        /// Find a tag based on name
        /// </summary>
        /// <param name="name"></param>
        /// <returns>Tag or null if not found.</returns>
        public Tag FindTag(string name)
        {
            return Tags.FirstOrDefault(x => x.GeneralDescription.Name == name);
        }

        /// <summary>
        /// Build the list of tags from the ids and the owner's tag list.
        /// </summary>
        /// <param name="ownerTagList"></param>
        public void BuildTagsFromIds(IList<Tag> ownerTagList)
        {
            Tags.Clear();
            foreach (int tagId in TagIds)
            {
                Tags.Add(ownerTagList.First(x => x.Id == tagId));
            }
        }

        /// <summary>
        /// Supporting filtering on data returned by this
        /// </summary>
        /// <returns>All text to filter on in lower case.</returns>
        public string ValueToFilterOn()
        {
            string rv = GeneralDescription.Name + " " + GeneralDescription.Description + " ";
            if (Tags != null && Tags.Any())
                rv += string.Join(" ", Tags.Select(x => x.GeneralDescription.Name));
            rv += GpsLocation?.Address;
            return rv.ToLower();
        }

        /// <summary>
        /// Return the Id of a blob by its path.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public string GetIdFromPath(string path)
        {
            if (path.IsLocal())
                return null;
            int start = path.LastIndexOf('/') + 1;
            string rv = path.Substring(start, 36);
            return rv;
        }

        /// <summary>
        /// Fix up references to owner journey and account
        /// </summary>
        /// <param name="owner"></param>
        /// <returns></returns>
        public override Task PostClientSerializationAsync(object owner = null)
        {
            var journey = owner as Journey;
            AccountId = journey.AccountId;
            JourneyId = journey.Id;
            TrackLocation = journey.IsLocationTrackingEnabled;
            BuildTagsFromIds(journey.TagList);
            return Task.FromResult(0);
        }
    }
}