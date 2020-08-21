namespace JourneyDoc.Models.Interfaces
{
    /// <summary>
    /// Any object implementing this can be filtered by our generic SearchHandler.
    /// </summary>
    public interface IFilterableItem
    {
        /// <summary>
        /// Supporting filtering on data returned by this
        /// </summary>
        /// <returns>All text to filter on in lower case.</returns>
        string ValueToFilterOn();
    }
}