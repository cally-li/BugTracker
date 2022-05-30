namespace BugTracker.Models
{
    public class TicketHistoryItem
    {

        public int TicketHistoryItemId { get; set; }

        public string ChangedProperty { get; set; } 
        public string OldValue{ get; set; }
        public string NewValue{ get; set; }

        //date that ticket property changed / a ticket history item was created
        public DateTime DateChanged { get; set; } = DateTime.Now;
    }
}
