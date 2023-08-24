namespace Online_Bookstore_API.Models.DTOs
{
    public class UserInfoDto
    {
        public String UserName { get; set; }

        [MaxLength(50)]
        public String FirstName { get; set; }
        [MaxLength(50)]
        public String LastName { get; set; }
        [MaxLength(500)]
        public String Address { get; set; }
        [MaxLength(250)]
        public String PhoneNumber { get; set; }
    }
}
