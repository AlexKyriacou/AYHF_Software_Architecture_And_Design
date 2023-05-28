namespace AYHF_Software_Architecture_And_Design.Application.Dtos
{
    public class FeedbackDto
    {
        public int Id { get; set; }
        public UserDto Customer { get; set; }
        public string Message { get; set; }
        public DateTime FeedbackDate { get; set; }
    }
}
