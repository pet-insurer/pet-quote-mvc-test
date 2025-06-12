namespace PetInsurance.Shared.Models
{
    /// <summary>
    /// Represents user login credentials for authentication.  
    /// </summary>
    public class Credentials
    {
        /// <summary>
        /// The username or email used for authentication.  
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// The user's password for authentication.  
        /// </summary>
        public string Password { get; set; }
    }
}
