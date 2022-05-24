namespace blogapi.Model
{
    public class blogreq
    {
        public int bid { get; set; }
        public int sid { get; set; }
        public string writer {get; set;}
        public string title{get;set;}
        public string synopsis {get;set;}
        public string description{get;set;}
        public string corporate_action{get;set;}
        public string languages{get;set;}
    }
    public class blogresponse
    {
        public bool issuccess { get; set; }
        public string message { get; set; }
    }
}
