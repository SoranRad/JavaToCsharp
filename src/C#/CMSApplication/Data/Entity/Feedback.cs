namespace CMSApplication.Data.Entity
{
    public class Feedback : BaseEntity<long>
    {
        public string   FullName        { get; set; }
        public string   Email           { get; set; }
        public string   ContactNumber   { get; set; }
        public byte     Rate            { get; set; }
        public string   Commnets        { get; set; }
    }
}
