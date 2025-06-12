namespace PetInsurance.Shared.Response
{
    /// <summary>
    /// Represents the response containing a JWT token after a successful login.
    /// </summary>
    public class LoginTokenResponse
    {
        /// <summary>
        /// The JWT token issued to the authenticated user.
        /// </summary>
        public string Token { get; set; } = string.Empty;
    }
}