namespace PhantomAbyssServer.Models.Responses
{
    public class GhostRun
    {
        public string DownloadUrl { get; set; }
        public string RunData { get; set; } = ""; // not used in our case
        public int Success { get; set; }
        public uint UserId { get; set; }
        public string UserName { get; set; }
    }
}