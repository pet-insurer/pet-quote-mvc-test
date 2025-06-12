namespace PetInsurance.Shared.Models
{
    /// <summary>
    /// Represents an error view model to display error details in the UI.
    /// </summary>
    public class ErrorViewModel
    {
        /// <summary>
        /// Gets or sets the unique request ID associated with the error.
        /// </summary>
        public string? RequestId { get; set; }

        /// <summary>
        /// Indicates whether the request ID should be displayed.
        /// </summary>
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
