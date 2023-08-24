namespace Online_Bookstore_API.Models
{
    public class RegisterModel
    {
        [StringLength(50)]    
        public string FirstName { get; set; }
        [StringLength(50)] 
        public string LastName { get; set; }
        [StringLength(256)]
        public string UserName { get; set; } 
        [StringLength(256)]
        public string Address { get; set; }
        [StringLength(256)]
        public string Email { get; set; }
        [StringLength(256)]
        public string PhoneNumber { get; set; }
        [StringLength(256)]
        public string Password { get; set; }
    }
}
