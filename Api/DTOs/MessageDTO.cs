namespace Api.DTOs
{
    public class MessageDTO
    {
         public string Id { get; set; }
        public required string Content { get; set; }
        public DateTime? DateRead { get; set; }
        public DateTime MessageSent { get; set; }
        public required string SenderId { get; set; }
        public required string SenderDisplayName { get; set; }
        public string? SenderImgUrl { get; set; }
        public required string RecipientId { get; set; }
        public required string RecipientDisplayName { get; set; }
        public string? RecipientImgUrl { get; set; }
    }
    
}