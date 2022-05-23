namespace blogapi.Model
{
    public class subjectreq
    {
        public int sid { get; set; }
        public string subject_name { get; set; }
    }
    public class subjectresponse
    {
        public bool issuccess { get; set; }
        public string message { get; set; }
    }
}
