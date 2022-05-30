using System.ComponentModel.DataAnnotations.Schema;

namespace BugTracker.Models
{
    [Table("File Attachments")]
    public class FileAttachment
    {
        public int FileAttachmentId { get; set; }

        public string Name { get; set; }
        public int Size { get; set; }

        public string PublicId { get; set; }

        public int UploadedByUserId { get; set; }
        public User UploadedBy { get; set; }    

    }
}